using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media; 
using MonoGameLibrary;
using System;
using System.Collections.Generic;
using ZeldaGame.GameScripts.Interfaces;
using ZeldaGame.GameScripts.Tilemapping;

namespace ZeldaGame.GameScripts.Screens
{
    public class GameplayScreen : IScreen
    {
        private sealed class RoomEntities
        {
            public List<IEnemy> Enemies { get; } = new();
            public List<IItem> Items { get; } = new();
            public List<IProjectile> Projectiles { get; } = new();
            public List<Vector2> SpawnPositions { get; } = new();
        }

        private sealed class RoomState
        {
            public string LevelFile { get; init; }
            public Tilemap Tilemap { get; init; }
            public Rectangle Bounds { get; init; }
            public RoomEntities Entities { get; init; }
        }

        private sealed class RoomTransition
        {
            public RoomState Source { get; init; }
            public RoomState Destination { get; init; }
            public RoomDoor Door { get; init; }
            public float ElapsedSeconds { get; set; }
        }

        private const float RoomTransitionDuration = 0.35f;
        private const float DoorReturnCooldownDuration = 0.75f;
        private readonly string[] roomFiles =
        {
            "levels/d01r03.xml", "levels/d01r04.xml", "levels/d01r05.xml",
            "levels/d01r06.xml", "levels/d01r07.xml", "levels/d01r08.xml",
            "levels/d01r09.xml", "levels/d01r11.xml", "levels/d01r12.xml",
            "levels/d01r13.xml", "levels/d01r14.xml", "levels/d01r15.xml",
            "levels/d01r17.xml", "levels/d01r18.xml", "levels/d01r19.xml",
            "levels/d01r20.xml"
        };
        private readonly Game1 game;
        private readonly Dictionary<string, RoomState> roomsByFile = new();
        private readonly Random random = new();
        private RoomState currentRoom;
        private RoomTransition transition;
        private string blockedDoorDestination;
        private DoorDirection? blockedDoorDirection;
        private float doorReturnCooldownRemaining;

        public Tilemap Tilemap { get; private set; }
        public Rectangle RoomBounds { get; private set; }
        public string LevelFile { get; private set; }

        private KeyboardState previousKeyboardState;
        private HUD hud;
        private const int HUDHeight = 256;

        public GameplayScreen(Game1 game)
        {
            this.game = game;
            LevelFile = roomFiles[0];

            // Listen for spawned projectiles, route them to active room
            game.Player.ProjectileSpawned += projectile =>
            {
                if (currentRoom != null)
                {
                    currentRoom.Entities.Projectiles.Add(projectile);
                }
            };
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Draw(spriteBatch, gameTime, Vector2.Zero);
            Matrix scale = Matrix.CreateScale(Game1.WindowScale);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: scale);
            hud.Draw(spriteBatch);
            spriteBatch.End();
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Vector2 screenOffset)
        {
            if (transition == null)
            {
                DrawRoomWithCamera(spriteBatch, currentRoom, gameTime, screenOffset, drawPlayer: true);
                return;
            }

            float progress = MathHelper.Clamp(
                transition.ElapsedSeconds / RoomTransitionDuration,
                0f,
                1f
            );
            Vector2 sourceOffset = GetTransitionMovement(transition.Door.Direction, progress) + screenOffset;
            Vector2 destinationOffset = GetTransitionStartOffset(transition.Door.Direction) + sourceOffset;

            DrawRoomWithCamera(spriteBatch, currentRoom, gameTime, sourceOffset, drawPlayer: true);
            DrawRoomWithCamera(spriteBatch, transition.Destination, gameTime, destinationOffset, drawPlayer: false);
        }

        private void DrawRoomWithCamera(
            SpriteBatch spriteBatch,
            RoomState room,
            GameTime gameTime,
            Vector2 offset,
            bool drawPlayer
        )
        {
            Matrix scale = Matrix.CreateScale(Game1.WindowScale, Game1.WindowScale, 1f);
            Matrix camera = Matrix.CreateTranslation(offset.X, offset.Y + HUDHeight, 0f) * scale;
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: camera);
            DrawRoom(spriteBatch, room, gameTime, drawPlayer);
            spriteBatch.End();
        }

        private void DrawRoom(SpriteBatch spriteBatch, RoomState room, GameTime gameTime, bool drawPlayer)
        {
            room.Tilemap.Draw(spriteBatch);

            foreach (IProjectile projectile in room.Entities.Projectiles)
            {
                projectile.Draw(gameTime);
            }

            foreach (IEnemy enemy in room.Entities.Enemies)
            {
                enemy.Draw(gameTime);
            }

            foreach (IItem item in room.Entities.Items)
            {
                item.Draw(gameTime);
            }

            if (drawPlayer)
            {
                game.Player.Draw(gameTime);
            }
        }

        private static Vector2 GetTransitionMovement(DoorDirection direction, float progress)
        {
            return direction switch
            {
                DoorDirection.Left => new Vector2(progress * Game1.GameWidth, 0f),
                DoorDirection.Right => new Vector2(-progress * Game1.GameWidth, 0f),
                DoorDirection.Up => new Vector2(0f, progress * Game1.GameHeight),
                DoorDirection.Down => new Vector2(0f, -progress * Game1.GameHeight),
                _ => Vector2.Zero
            };
        }

        private static Vector2 GetTransitionStartOffset(DoorDirection direction)
        {
            return direction switch
            {
                DoorDirection.Left => new Vector2(-Game1.GameWidth, 0f),
                DoorDirection.Right => new Vector2(Game1.GameWidth, 0f),
                DoorDirection.Up => new Vector2(0f, -Game1.GameHeight),
                DoorDirection.Down => new Vector2(0f, Game1.GameHeight),
                _ => Vector2.Zero
            };
        }

        public void LoadContent(ContentManager content, GraphicsDevice device)
        {
            LoadRoom(LevelFile);
            previousKeyboardState = Keyboard.GetState();
            hud = new HUD();
            hud.LoadContent(device);
        }

        private void LoadRoom(string levelFile)
        {
            System.Diagnostics.Debug.WriteLine($"Active Room: {levelFile}");

            ClearDoorReturnLock();
            currentRoom = GetOrCreateRoom(levelFile);
            ActivateRoom(currentRoom);
            game.Player.Position = GetSafePlayerSpawnPosition(currentRoom);
        }

        private RoomState GetOrCreateRoom(string levelFile)
        {
            if (roomsByFile.TryGetValue(levelFile, out RoomState room))
            {
                return room;
            }

            Tilemap tilemap = Tilemap.FromFile(Core.Content, levelFile, new Vector2(4.0f, 4.0f));

            Rectangle screenBounds = new Rectangle(0, 0, Game1.GameWidth, Game1.GameHeight);
            Rectangle bounds = new Rectangle(
                (int)tilemap.TileWidth,
                (int)tilemap.TileHeight,
                screenBounds.Width - (int)tilemap.TileWidth * 2,
                screenBounds.Height - (int)tilemap.TileHeight * 2
            );

            room = new RoomState
            {
                LevelFile = levelFile,
                Tilemap = tilemap,
                Bounds = bounds,
                Entities = CreateEntitiesForRoom(levelFile, tilemap, bounds)
            };
            roomsByFile.Add(levelFile, room);

            return room;
        }

        private void ActivateRoom(RoomState room)
        {
            currentRoom = room;
            LevelFile = room.LevelFile;
            Tilemap = room.Tilemap;
            RoomBounds = room.Bounds;
        }

        private RoomEntities CreateEntitiesForRoom(string levelFile, Tilemap tilemap, Rectangle bounds)
        {
            RoomEntities entities = new RoomEntities();
            if (tilemap.HasItemConfiguration)
            {
                foreach (ItemSpawn spawn in tilemap.ItemSpawns)
                {
                    AddConfiguredItem(entities, spawn, levelFile);
                }
            }
            else
            {
                AddRandomItem(entities, tilemap, bounds);
            }

            if (tilemap.HasEnemyConfiguration)
            {
                foreach (EnemySpawn spawn in tilemap.EnemySpawns)
                {
                    AddConfiguredEnemy(entities, spawn, tilemap, levelFile);
                }
            }
            else
            {
                AddRandomEnemy(entities, tilemap, bounds);
            }

            return entities;
        }

        private void AddRandomItem(RoomEntities entities, Tilemap tilemap, Rectangle bounds)
        {
            Vector2 position = GetSpawnPosition(entities, tilemap, bounds);
            var itemTypes = new List<Func<Vector2, IItem>>
            {
                pos => new BlueCandleItem(ItemSpriteFactory.Instance.CreateBlueCandleSprite(), pos),
                pos => new BluePotionItem(ItemSpriteFactory.Instance.CreateBluePotionSprite(), pos),
                pos => new BombItem(ItemSpriteFactory.Instance.CreateBombSprite(), pos),
                pos => new BowItem(ItemSpriteFactory.Instance.CreateBowSprite(), pos),
                pos => new ClockItem(ItemSpriteFactory.Instance.CreateClockSprite(), pos),
                pos => new CompassItem(ItemSpriteFactory.Instance.CreateCompassSprite(), pos),
                pos => new FairyItem(ItemSpriteFactory.Instance.CreateFairySprite(), pos),
                pos => new HeartContainerItem(ItemSpriteFactory.Instance.CreateHeartContainerSprite(), pos),
                pos => new HeartItem(ItemSpriteFactory.Instance.CreateHeartSprite(), pos),
                pos => new MapItem(ItemSpriteFactory.Instance.CreateMapSprite(), pos),
                pos => new NormalKeyItem(ItemSpriteFactory.Instance.CreateNormalKeySprite(), pos),
                pos => new RupeeItem(ItemSpriteFactory.Instance.CreateRupeeSprite(), pos),
                pos => new TriforceShardItem(ItemSpriteFactory.Instance.CreateTriforceShardSprite(), pos),
                pos => new WoodenArrowItem(ItemSpriteFactory.Instance.CreateWoodenArrowSprite(), pos),
                pos => new WoodenBoomerangItem(ItemSpriteFactory.Instance.CreateWoodenBoomerangSprite(), pos)
            };
            entities.Items.Add(itemTypes[random.Next(itemTypes.Count)](position));
        }

        private void AddRandomEnemy(RoomEntities entities, Tilemap tilemap, Rectangle bounds)
        {
            Vector2 position = GetSpawnPosition(entities, tilemap, bounds);
            IEnemy enemy = random.Next(6) switch
            {
                0 => new AquamentusEnemy(position),
                1 => new GelEnemy(position),
                2 => new GoriyaEnemy(position),
                3 => new KeeseEnemy(position),
                4 => new StalfosEnemy(position),
                _ => new WallMasterEnemy(position)
            };
            LoadEnemy(entities, enemy);
        }

        private void AddConfiguredItem(RoomEntities entities, ItemSpawn spawn, string levelFile)
        {
            IItem item = spawn.Type.ToLowerInvariant() switch
            {
                "key" => new NormalKeyItem(ItemSpriteFactory.Instance.CreateNormalKeySprite(), spawn.Position),
                "compass" => new CompassItem(ItemSpriteFactory.Instance.CreateCompassSprite(), spawn.Position),
                "boomerang" => new WoodenBoomerangItem(ItemSpriteFactory.Instance.CreateWoodenBoomerangSprite(), spawn.Position),
                _ => throw new InvalidOperationException($"Unknown item type '{spawn.Type}' in room '{levelFile}'.")
            };
            entities.Items.Add(item);
        }

        private Vector2 GetSafePlayerSpawnPosition(RoomState room)
        {
            if (room.Tilemap == null || room.Tilemap.Blocks == null)
            {
                return new Vector2(Game1.GameWidth / 2f - 32, Game1.GameHeight / 2f - 32);
            }

            float pWidth = game.Player.BoundingBox.Width > 0 ? game.Player.BoundingBox.Width : 64f;
            float pHeight = game.Player.BoundingBox.Height > 0 ? game.Player.BoundingBox.Height : 64f;

            // Scan candidate floor tiles starting from the lower half of the room upwards
            for (int row = room.Tilemap.Rows - 5; row >= 1; row--)
            {
                for (int col = 1; col < room.Tilemap.Columns - 1; col++)
                {
                    TileBlock candidate = room.Tilemap.GetBlock(col, row);
                    if (candidate == null || candidate.IsCollidable) continue;

                    // Verify a full player bounding box at this spot does not intersect ANY solid tile
                    Rectangle proposedBox = new Rectangle((int)candidate.Position.X, (int)candidate.Position.Y, (int)pWidth, (int)pHeight);
                    bool overlapsSolid = false;

                    foreach (TileBlock block in room.Tilemap.Blocks)
                    {
                        if (block.IsCollidable && block.BoundingBox.Intersects(proposedBox))
                        {
                            overlapsSolid = true;
                            break;
                        }
                    }

                    if (!overlapsSolid)
                    {
                        return candidate.Position;
                    }
                }
            }

            // Fallback: room center
            return new Vector2(room.Bounds.Center.X - pWidth / 2f, room.Bounds.Center.Y - pHeight / 2f);
        }

        private void AddEnemy(
            RoomEntities entities,
            Func<Vector2, IEnemy> createEnemy,
            Tilemap tilemap,
            Rectangle bounds
        )
        {
            IEnemy enemy = createEnemy(GetSpawnPosition(entities, tilemap, bounds));
            LoadEnemy(entities, enemy);
        }

        private void AddConfiguredEnemy(RoomEntities entities, EnemySpawn spawn, Tilemap tilemap, string levelFile)
        {
            Func<Vector2, IEnemy> createEnemy = spawn.Type.ToLowerInvariant() switch
            {
                "aquamentus" => position => new AquamentusEnemy(position),
                "gel" => position => new GelEnemy(position),
                "goriya" => position => new GoriyaEnemy(position),
                "keese" => position => new KeeseEnemy(position),
                "stalfos" => position => new StalfosEnemy(position),
                "wallmaster" => position => new WallMasterEnemy(position),
                _ => throw new InvalidOperationException($"Unknown enemy type '{spawn.Type}' in room '{levelFile}'.")
            };

            Vector2 position = spawn.Type.Equals("Aquamentus", StringComparison.OrdinalIgnoreCase)
                ? spawn.Position
                : GetRandomizedEnemyPosition(entities, tilemap, spawn);
            LoadEnemy(entities, createEnemy(position));
        }

        private Vector2 GetRandomizedEnemyPosition(RoomEntities entities, Tilemap tilemap, EnemySpawn spawn)
        {
            (int width, int height) = spawn.Type.ToLowerInvariant() switch
            {
                "gel" => (32, 64),
                "goriya" => (52, 64),
                "keese" => (64, 64),
                "stalfos" => (60, 64),
                "wallmaster" => (64, 64),
                _ => throw new InvalidOperationException($"Cannot randomize spawn for enemy type '{spawn.Type}'.")
            };

            List<Vector2> candidates = new();
            for (int offsetY = -64; offsetY <= 64; offsetY += 32)
            {
                for (int offsetX = -64; offsetX <= 64; offsetX += 32)
                {
                    candidates.Add(spawn.Position + new Vector2(offsetX, offsetY));
                }
            }

            foreach (Vector2 candidate in Shuffle(candidates))
            {
                Rectangle proposedBounds = new Rectangle(
                    (int)candidate.X,
                    (int)candidate.Y,
                    width,
                    height
                );
                if (proposedBounds.Left < 0
                    || proposedBounds.Top < 0
                    || proposedBounds.Right > tilemap.Columns * tilemap.TileWidth
                    || proposedBounds.Bottom > tilemap.Rows * tilemap.TileHeight)
                {
                    continue;
                }

                bool overlapsSolidTile = false;
                foreach (TileBlock block in tilemap.Blocks)
                {
                    if (block.IsCollidable && block.BoundingBox.Intersects(proposedBounds))
                    {
                        overlapsSolidTile = true;
                        break;
                    }
                }

                if (overlapsSolidTile)
                {
                    continue;
                }

                bool overlapsEntity = entities.Enemies.Exists(enemy => enemy.BoundingBox.Intersects(proposedBounds))
                    || entities.Items.Exists(item => item.BoundingBox.Intersects(proposedBounds));
                if (!overlapsEntity)
                {
                    return candidate;
                }
            }

            throw new InvalidOperationException(
                $"No clear randomized spawn position found near ({spawn.Position.X}, {spawn.Position.Y}) for {spawn.Type}."
            );
        }

        private static void LoadEnemy(RoomEntities entities, IEnemy enemy)
        {
            enemy.LoadContent();

            // Route projectiles created by this enemy to the room's projectile list
            enemy.ProjectileSpawned += projectile =>
            {
                entities.Projectiles.Add(projectile);
            };

            entities.Enemies.Add(enemy);
        }

        private Vector2 GetSpawnPosition(RoomEntities entities, Tilemap tilemap, Rectangle bounds)
        {
            var roomCenter = new Vector2(
                (bounds.Left + bounds.Right) / 2f,
                (bounds.Top + bounds.Bottom) / 2f
            );
            int horizontalRange = bounds.Width / 4;
            int verticalRange = bounds.Height / 4;
            int minX = (int)roomCenter.X - horizontalRange;
            int maxX = (int)roomCenter.X + horizontalRange;
            int minY = (int)roomCenter.Y - verticalRange;
            int maxY = (int)roomCenter.Y + verticalRange;

            for (int attempt = 0; attempt < 50; attempt++)
            {
                var position = new Vector2(random.Next(minX, maxX), random.Next(minY, maxY));
                if (Vector2.Distance(position, roomCenter) < 96)
                {
                    continue;
                }

                // Check against solid room blocks
                Rectangle proposedBounds = new Rectangle((int)position.X, (int)position.Y, 32, 32);
                bool insideSolidBlock = false;

                if (tilemap != null && tilemap.Blocks != null)
                {
                    foreach (TileBlock block in tilemap.Blocks)
                    {
                        if (block.IsCollidable && block.BoundingBox.Intersects(proposedBounds))
                        {
                            insideSolidBlock = true;
                            break;
                        }
                    }
                }

                if (insideSolidBlock)
                {
                    continue;
                }

                // Check against already spawned entity positions
                bool overlaps = false;
                foreach (Vector2 existingPosition in entities.SpawnPositions)
                {
                    if (Vector2.Distance(position, existingPosition) < 48)
                    {
                        overlaps = true;
                        break;
                    }
                }

                if (!overlaps)
                {
                    entities.SpawnPositions.Add(position);
                    return position;
                }
            }

            var fallbackPosition = new Vector2(random.Next(minX, maxX), random.Next(minY, maxY));
            entities.SpawnPositions.Add(fallbackPosition);
            return fallbackPosition;
        }

        private List<T> Shuffle<T>(List<T> source)
        {
            var shuffled = new List<T>(source);
            for (int i = shuffled.Count - 1; i > 0; i--)
            {
                int swapIndex = random.Next(i + 1);
                (shuffled[i], shuffled[swapIndex]) = (shuffled[swapIndex], shuffled[i]);
            }

            return shuffled;
        }

        private void DetectCollisions(RoomEntities entities)
        {
            // If Link is dead, entities will not collide with him
            if (game.Player.IsDead)
            {
                return;
            }

            // Link vs. Items
            foreach (IItem item in entities.Items)
            {
                if (CollisionDetector.CheckCollision(game.Player, item, out CollisionSide side, out _))
                {
                    PlayerItemCollisionHandler.Handle(game.Player, item);
                }
            }

            // Projectiles vs. Solid Tiles
            foreach (IProjectile projectile in entities.Projectiles)
            {
                foreach (TileBlock block in Tilemap.Blocks)
                {
                    if (CollisionDetector.CheckCollision(projectile, block, out CollisionSide side, out _))
                    {
                        ProjectileBlockCollisionHandler.Handle(projectile, block, side);
                    }
                }
            }

            // Link vs. Blocks
            foreach (TileBlock block in Tilemap.Blocks)
            {
                if (CollisionDetector.CheckCollision(game.Player, block, out CollisionSide side, out Rectangle overlap))
                {
                    PlayerBlockCollisionHandler.Handle(game.Player, block, side, overlap);
                }
            }

            // Items vs. Blocks (for autonomous items like the Fairy)
            foreach (IItem item in entities.Items)
            {
                foreach (TileBlock block in Tilemap.Blocks)
                {
                    if (CollisionDetector.CheckCollision(item, block, out CollisionSide side, out Rectangle overlap))
                    {
                        ItemBlockCollisionHandler.Handle(item, block, side, overlap);
                    }
                }
            }

            // Link vs. Enemies
            foreach (IEnemy enemy in entities.Enemies)
            {
                if (CollisionDetector.CheckCollision(game.Player, enemy, out CollisionSide side, out _))
                {
                    PlayerEnemyCollisionHandler.Handle(game.Player, enemy, side);
                }
            }

            // Enemies vs. Blocks
            foreach (IEnemy enemy in entities.Enemies)
            {
                foreach (TileBlock block in Tilemap.Blocks)
                {
                    if (CollisionDetector.CheckCollision(enemy, block, out CollisionSide side, out _))
                    {
                        EnemyBlockTileCollisionHandler.Handle(enemy, block, side);
                    }
                }
            }

            // Projectiles vs. Enemies
            foreach (IEnemy enemy in entities.Enemies)
            {
                foreach (IProjectile projectile in entities.Projectiles)
                {
                    if (projectile.isHostile)
                    {
                        continue;
                    }

                    if (CollisionDetector.CheckCollision(projectile, enemy, out CollisionSide side, out _))
                    {
                        EnemyProjectileCollisionHandler.Handle(projectile, enemy, side);
                    }
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();

            // Mute with 'E'
            if (currentKeyboardState.IsKeyDown(Keys.E) && previousKeyboardState.IsKeyUp(Keys.E))
            {
                MediaPlayer.IsMuted = !MediaPlayer.IsMuted;
            }
            previousKeyboardState = currentKeyboardState;

            if (transition != null)
            {
                game.Player.CancelMovement();
                transition.ElapsedSeconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (transition.ElapsedSeconds >= RoomTransitionDuration)
                {
                    blockedDoorDestination = transition.Source.LevelFile;
                    blockedDoorDirection = GetOppositeDirection(transition.Door.Direction);
                    doorReturnCooldownRemaining = DoorReturnCooldownDuration;
                    ActivateRoom(transition.Destination);
                    game.Player.Position = transition.Door.ArrivalPosition;
                    transition = null;
                }

                return;
            }

            doorReturnCooldownRemaining = Math.Max(
                0f,
                doorReturnCooldownRemaining - (float)gameTime.ElapsedGameTime.TotalSeconds
            );

            // Update player
            game.Player.Update(gameTime);
            if (TryStartRoomTransition())
            {
                return;
            }

            RoomEntities entities = currentRoom.Entities;

            foreach (IEnemy enemy in entities.Enemies)
            {
                enemy.Update(gameTime);
            }

            foreach (IItem item in entities.Items)
            {
                item.Update(gameTime);
            }

            // Update projectiles and remove spent ones
            for (int i = entities.Projectiles.Count - 1; i >= 0; i--)
            {
                IProjectile projectile = entities.Projectiles[i];
                projectile.Update(gameTime);
                if (!projectile.IsActive)
                {
                    entities.Projectiles.RemoveAt(i);
                }
            }

            // Run collision detection and resolution sweeps
            DetectCollisions(entities);
            
            entities.Enemies.RemoveAll(enemy => !enemy.IsAlive);

            // Prune collected items
            entities.Items.RemoveAll(item => !item.IsActive);

        }

        private bool TryStartRoomTransition()
        {
            foreach (RoomDoor door in currentRoom.Tilemap.Doors)
            {
                bool playerAtDoor = IsPlayerCenteredInDoorway(door);
                bool isBlockedReturnDoor = door.Destination == blockedDoorDestination
                    && door.Direction == blockedDoorDirection;

                if (isBlockedReturnDoor)
                {
                    if (doorReturnCooldownRemaining > 0f || playerAtDoor)
                    {
                        continue;
                    }

                    ClearDoorReturnLock();
                }

                if (!playerAtDoor)
                {
                    continue;
                }

                transition = new RoomTransition
                {
                    Source = currentRoom,
                    Destination = GetOrCreateRoom(door.Destination),
                    Door = door
                };
                return true;
            }

            return false;
        }

        private bool IsPlayerCenteredInDoorway(RoomDoor door)
        {
            Rectangle activationBounds = door.TriggerBounds;
            activationBounds.Inflate(-16, -16);
            return activationBounds.Contains(game.Player.BoundingBox.Center);
        }

        private static DoorDirection GetOppositeDirection(DoorDirection direction)
        {
            return direction switch
            {
                DoorDirection.Left => DoorDirection.Right,
                DoorDirection.Right => DoorDirection.Left,
                DoorDirection.Up => DoorDirection.Down,
                DoorDirection.Down => DoorDirection.Up,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }

        private void ClearDoorReturnLock()
        {
            blockedDoorDestination = null;
            blockedDoorDirection = null;
            doorReturnCooldownRemaining = 0f;
        }

        public void Reset()
        {
            transition = null;
            ClearDoorReturnLock();
            roomsByFile.Clear();

            game.Player.Reset();

            LevelFile = roomFiles[0];
            LoadRoom(LevelFile);
        }
    }
}