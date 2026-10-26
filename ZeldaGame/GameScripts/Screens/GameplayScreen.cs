using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input; 
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

        private Game1 game;

        public Tilemap Tilemap { get; set; }
        public Rectangle RoomBounds { get; set; }
        public string LevelFile { get; set; }

        // Array holding all room file paths
        private string[] levelFiles;
        private int currentLevelIndex = 0;
        private MouseState previousMouseState;
        private readonly Dictionary<string, RoomEntities> entitiesByRoom = new();
        private readonly Random random = new();

        public GameplayScreen(Game1 game)
        {
            this.game = game;

            levelFiles = new string[]
            {
                "levels/d01r03.xml", "levels/d01r04.xml", "levels/d01r05.xml",
                "levels/d01r06.xml", "levels/d01r07.xml", "levels/d01r08.xml",
                "levels/d01r09.xml", "levels/d01r11.xml", "levels/d01r12.xml",
                "levels/d01r13.xml", "levels/d01r14.xml", "levels/d01r15.xml",
                "levels/d01r16.xml", "levels/d01r17.xml", "levels/d01r18.xml",
                "levels/d01r19.xml", "levels/d01r20.xml"
            };

            // Set default starting level to the first item in our array
            currentLevelIndex = 0;
            LevelFile = levelFiles[currentLevelIndex];

            // Listen for spawned projectiles, route them to active room
            game.Player.ProjectileSpawned += projectile =>
            {
                if (entitiesByRoom.TryGetValue(LevelFile, out RoomEntities entities))
                {
                    entities.Projectiles.Add(projectile);
                }
            };
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Tilemap.Draw(spriteBatch);

            RoomEntities entities = entitiesByRoom[LevelFile];

            foreach (IProjectile projectile in entities.Projectiles)
            {
                projectile.Draw(gameTime);
            }

            foreach (IEnemy enemy in entities.Enemies)
            {
                enemy.Draw(gameTime);
            }

            foreach (IItem item in entities.Items)
            {
                item.Draw(gameTime);
            }

            game.Player.Draw(gameTime);
        }

        public void LoadContent(ContentManager content, GraphicsDevice device)
        {
            LoadRoom(LevelFile, device);
        }

        private void LoadRoom(string levelFile, GraphicsDevice device)
        {
            LevelFile = levelFile;
            Tilemap = Tilemap.FromFile(Core.Content, LevelFile, new Vector2(4.0f, 4.0f));

            Rectangle screenBounds = device.PresentationParameters.Bounds;

            RoomBounds = new Rectangle(
                (int)Tilemap.TileWidth,
                (int)Tilemap.TileHeight,
                screenBounds.Width - (int)Tilemap.TileWidth * 2,
                screenBounds.Height - (int)Tilemap.TileHeight * 2
            );

            if (entitiesByRoom.Count == 0)
            {
                CreateRoomEntities();
            }
        }

        private void CreateRoomEntities()
        {
            var rooms = new RoomEntities[levelFiles.Length];
            for (int i = 0; i < rooms.Length; i++)
            {
                rooms[i] = new RoomEntities();
            }

            var enemyTypes = new List<Func<Vector2, IEnemy>>
            {
                position => new AquamentusEnemy(position),
                position => new GelEnemy(position),
                position => new GoriyaEnemy(position),
                position => new KeeseEnemy(position),
                position => new StalfosEnemy(position),
                position => new WallMasterEnemy(position)
            };

            var itemTypes = new List<Func<Vector2, IItem>>
            {
                position => new BlueCandleItem(ItemSpriteFactory.Instance.CreateBlueCandleSprite(), position),
                position => new BluePotionItem(ItemSpriteFactory.Instance.CreateBluePotionSprite(), position),
                position => new BombItem(ItemSpriteFactory.Instance.CreateBombSprite(), position),
                position => new BowItem(ItemSpriteFactory.Instance.CreateBowSprite(), position),
                position => new ClockItem(ItemSpriteFactory.Instance.CreateClockSprite(), position),
                position => new CompassItem(ItemSpriteFactory.Instance.CreateCompassSprite(), position),
                position => new FairyItem(ItemSpriteFactory.Instance.CreateFairySprite(), position),
                position => new HeartContainerItem(ItemSpriteFactory.Instance.CreateHeartContainerSprite(), position),
                position => new HeartItem(ItemSpriteFactory.Instance.CreateHeartSprite(), position),
                position => new MapItem(ItemSpriteFactory.Instance.CreateMapSprite(), position),
                position => new NormalKeyItem(ItemSpriteFactory.Instance.CreateNormalKeySprite(), position),
                position => new RupeeItem(ItemSpriteFactory.Instance.CreateRupeeSprite(), position),
                position => new TriforceShardItem(ItemSpriteFactory.Instance.CreateTriforceShardSprite(), position),
                position => new WoodenArrowItem(ItemSpriteFactory.Instance.CreateWoodenArrowSprite(), position),
                position => new WoodenBoomerangItem(ItemSpriteFactory.Instance.CreateWoodenBoomerangSprite(), position)
            };

            foreach (Func<Vector2, IEnemy> createEnemy in Shuffle(enemyTypes))
            {
                AddEnemy(rooms[random.Next(rooms.Length)], createEnemy);
            }

            foreach (Func<Vector2, IItem> createItem in Shuffle(itemTypes))
            {
                AddItem(rooms[random.Next(rooms.Length)], createItem);
            }

            foreach (RoomEntities entities in rooms)
            {
                if (entities.Enemies.Count == 0 || random.Next(3) == 0)
                {
                    AddEnemy(entities, enemyTypes[random.Next(enemyTypes.Count)]);
                }

                if (entities.Items.Count == 0 || random.Next(3) == 0)
                {
                    AddItem(entities, itemTypes[random.Next(itemTypes.Count)]);
                }
            }

            for (int i = 0; i < rooms.Length; i++)
            {
                entitiesByRoom.Add(levelFiles[i], rooms[i]);
            }
        }

        private void AddEnemy(RoomEntities entities, Func<Vector2, IEnemy> createEnemy)
        {
            IEnemy enemy = createEnemy(GetSpawnPosition(entities));
            enemy.LoadContent();

            // Route projectiles created by this enemy to the room's projectile list
            enemy.ProjectileSpawned += projectile =>
            {
                entities.Projectiles.Add(projectile);
            };

            entities.Enemies.Add(enemy);
        }

        private void AddItem(RoomEntities entities, Func<Vector2, IItem> createItem)
        {
            entities.Items.Add(createItem(GetSpawnPosition(entities)));
        }

        private Vector2 GetSpawnPosition(RoomEntities entities)
        {
            var roomCenter = new Vector2(
                (RoomBounds.Left + RoomBounds.Right) / 2f,
                (RoomBounds.Top + RoomBounds.Bottom) / 2f
            );
            int horizontalRange = RoomBounds.Width / 4;
            int verticalRange = RoomBounds.Height / 4;
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

                if (Tilemap != null && Tilemap.Blocks != null)
                {
                    foreach (TileBlock block in Tilemap.Blocks)
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
            // Enemies vs. Blocks
            // Projectiles vs. Enemies
        }

        public void Update(GameTime gameTime)
        {
            RoomEntities entities = entitiesByRoom[LevelFile];

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

            // Prune collected items
            entities.Items.RemoveAll(item => !item.IsActive);

            MouseState currentMouseState = Mouse.GetState();

            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                currentLevelIndex = (currentLevelIndex + 1) % levelFiles.Length;
                LoadRoom(levelFiles[currentLevelIndex], Core.Graphics.GraphicsDevice);
            }

            previousMouseState = currentMouseState;
        }
    }
}