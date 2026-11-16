using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ZeldaGame.GameScripts.Interfaces;

namespace ZeldaGame.GameScripts.Screens
{
    public class ItemSelectScreen : IScreen
    {
        private readonly Game1 game;
        private Texture2D _hudSpriteSheet;
        private Texture2D _pixel;

        private const float Scale = 4f;

        private readonly Rectangle _topInventoryTemplateSrc = new Rectangle(1, 11, 256, 88);
        private readonly Rectangle _bottomDungeonTemplateSrc = new Rectangle(258, 112, 256, 88);

        private readonly Rectangle _equippedSlotMask = new Rectangle(68, 24, 8, 16);
        private readonly Rectangle _topCyanGuidesMask = new Rectangle(124, 10, 96, 12);
        private readonly Rectangle _inventoryGridMask = new Rectangle(126, 24, 92, 32);
        private readonly Rectangle _mapIconMask = new Rectangle(44, 112, 8, 16);
        private readonly Rectangle _compassIconMask = new Rectangle(40, 152, 16, 16);
        private readonly Rectangle _dungeonMapGridMask = new Rectangle(128, 96, 64, 64);

        private readonly Rectangle _boomerangSrc = new Rectangle(584, 137, 8, 16);
        private readonly Rectangle _bombSrc = new Rectangle(604, 137, 8, 16);
        private readonly Rectangle _mapItemSrc = new Rectangle(601, 156, 8, 16);
        private readonly Rectangle _selectionCursorSrc = new Rectangle(519, 137, 16, 16);

        private readonly Vector2 _equippedItemPos = new Vector2(68, 24);
        private readonly Vector2 _slot0ItemPos = new Vector2(128, 24);
        private readonly Vector2 _slot1ItemPos = new Vector2(152, 24);
        private readonly Vector2 _cursorPos = new Vector2(124, 24);
        private readonly Vector2 _mapItemPos = new Vector2(44, 112);

        public ItemSelectScreen(Game1 game)
        {
            this.game = game;
        }

        public void LoadContent(ContentManager content, GraphicsDevice device)
        {
            _hudSpriteSheet = content.Load<Texture2D>("HUDandPause");

            // 1x1 white texture used for drawing solid background masks and dungeon rooms
            _pixel = new Texture2D(device, 1, 1);
            _pixel.SetData(new[] { Color.White });
        }

        public void Update(GameTime gameTime)
        {
            // Inventory cursor movement and item selection logic will go here later
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // Draw the black backdrop across the full 1024x704 virtual screen
            DrawRect(spriteBatch, new Rectangle(0, 0, 256, 176), Color.Black);

            // Draw Top Half (INVENTORY, B-Button Box, Item Grid) & Bottom Half (MAP, COMPASS, Parchment)
            DrawSprite(spriteBatch, _topInventoryTemplateSrc, Vector2.Zero);
            DrawSprite(spriteBatch, _bottomDungeonTemplateSrc, new Vector2(0, 88));

            // Mask out the bright template placeholder rectangles with pure black
            DrawRect(spriteBatch, _equippedSlotMask, Color.Black);
            DrawRect(spriteBatch, _topCyanGuidesMask, Color.Black);
            DrawRect(spriteBatch, _inventoryGridMask, Color.Black);
            DrawRect(spriteBatch, _mapIconMask, Color.Black);
            DrawRect(spriteBatch, _compassIconMask, Color.Black);

            // Mask out the blue/yellow checkerboard on the parchment with the exact parchment orange
            Color parchmentOrange = new Color(252, 152, 56);
            DrawRect(spriteBatch, _dungeonMapGridMask, parchmentOrange);

            // Draw the placeholder items & selection cursor
            DrawSprite(spriteBatch, _boomerangSrc, _equippedItemPos); // Equipped in B slot
            DrawSprite(spriteBatch, _boomerangSrc, _slot0ItemPos);    // Inventory Slot 1
            DrawSprite(spriteBatch, _bombSrc, _slot1ItemPos);         // Inventory Slot 2
            DrawSprite(spriteBatch, _selectionCursorSrc, _cursorPos); // Red corner brackets
            DrawSprite(spriteBatch, _mapItemSrc, _mapItemPos);        // Map scroll under MAP

            // Draw the dungeon room layout on the parchment
            DrawDungeonMinimap(spriteBatch);
        }

        private void DrawDungeonMinimap(SpriteBatch spriteBatch)
        {
            Point[] rooms = new[]
            {
                new Point(144, 112),
                new Point(144, 120),
                new Point(128, 128), new Point(136, 128), new Point(144, 128),
                new Point(152, 128), new Point(160, 128), new Point(160, 120),
                new Point(136, 136), new Point(144, 136),
                new Point(144, 144),
                new Point(136, 152), new Point(144, 152), new Point(152, 152)
            };

            foreach (Point room in rooms)
            {
                // Draw 7x7 room square with 1px spacing for hallways/connections
                DrawRect(spriteBatch, new Rectangle(room.X, room.Y, 7, 7), Color.Black);
            }

            // Connect horizontal/vertical hallways between rooms
            DrawRect(spriteBatch, new Rectangle(135, 131, 26, 2), Color.Black); // Main horizontal corridor
            DrawRect(spriteBatch, new Rectangle(147, 119, 2, 34), Color.Black); // Main vertical spine
            DrawRect(spriteBatch, new Rectangle(163, 127, 2, 2), Color.Black);  // Right branch connector
            DrawRect(spriteBatch, new Rectangle(143, 155, 10, 2), Color.Black); // Bottom entrance connectors

            // Green Triforce/Goal indicator in the top room
            DrawRect(spriteBatch, new Rectangle(146, 114, 3, 3), new Color(0, 168, 0));
        }

        private void DrawSprite(SpriteBatch spriteBatch, Rectangle sourceRect, Vector2 nativePosition)
        {
            spriteBatch.Draw(
                _hudSpriteSheet,
                nativePosition * Scale,
                sourceRect,
                Color.White,
                0f,
                Vector2.Zero,
                Scale,
                SpriteEffects.None,
                0f
            );
        }

        private void DrawRect(SpriteBatch spriteBatch, Rectangle nativeRect, Color color)
        {
            Rectangle scaledRect = new Rectangle(
                (int)(nativeRect.X * Scale),
                (int)(nativeRect.Y * Scale),
                (int)(nativeRect.Width * Scale),
                (int)(nativeRect.Height * Scale)
            );
            spriteBatch.Draw(_pixel, scaledRect, color);
        }
    }
}