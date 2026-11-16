using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ZeldaGame.GameScripts.Interfaces;

namespace ZeldaGame.GameScripts.Screens
{
    public class ItemSelectScreen : IScreen
    {
        private readonly Game1 game;
        private Texture2D hudSpriteSheet;
        private Texture2D pixel;

        private const float Scale = 4f;

        // Background Template Source Rectangles
        private readonly Rectangle topInventoryTemplate = new Rectangle(1, 11, 256, 88);
        private readonly Rectangle bottomDungeonTemplate = new Rectangle(258, 112, 256, 88);

        // Placeholder Guide Masks
        private readonly Rectangle equippedSlotMask = new Rectangle(64, 48, 16, 16);
        private readonly Rectangle topCyanGuidesMask = new Rectangle(124, 24, 96, 20);
        private readonly Rectangle inventoryGridMask = new Rectangle(128, 48, 92, 32);
        private readonly Rectangle mapIconMask = new Rectangle(40, 112, 16, 16);
        private readonly Rectangle compassIconMask = new Rectangle(40, 152, 16, 16);
        private readonly Rectangle dungeonMapGridMask = new Rectangle(128, 96, 64, 64);

        // Item & UI Sprite Source Rectangles
        private readonly Rectangle boomerang = new Rectangle(584, 137, 8, 16);
        private readonly Rectangle bomb = new Rectangle(604, 137, 8, 16);
        private readonly Rectangle mapItem = new Rectangle(601, 156, 8, 16);
        private readonly Rectangle selectionCursor = new Rectangle(519, 137, 16, 16);

        // Destination Positions
        private readonly Vector2 equippedItemPos = new Vector2(68, 48);
        private readonly Vector2 slot0ItemPos = new Vector2(132, 48);
        private readonly Vector2 slot1ItemPos = new Vector2(156, 48);
        private readonly Vector2 cursorPos = new Vector2(128, 48);
        private readonly Vector2 mapItemPos = new Vector2(44, 112);

        public ItemSelectScreen(Game1 game)
        {
            this.game = game;
        }

        public void LoadContent(ContentManager content, GraphicsDevice device)
        {
            Texture2D rawSheet = content.Load<Texture2D>("spritesheets/HUDandPause");

            // Strip the gray icon background from the sprite sheet
            // so items and the selection cursor have transparent backgrounds
            Color[] pixels = new Color[rawSheet.Width * rawSheet.Height];
            rawSheet.GetData(pixels);
            Color iconSheetGray = new Color(116, 116, 116);

            for (int i = 0; i < pixels.Length; i++)
            {
                if (pixels[i] == iconSheetGray)
                {
                    pixels[i] = Color.Transparent;
                }
            }

            hudSpriteSheet = new Texture2D(device, rawSheet.Width, rawSheet.Height);
            hudSpriteSheet.SetData(pixels);

            pixel = new Texture2D(device, 1, 1);
            pixel.SetData(new[] { Color.White });
        }

        public void Update(GameTime gameTime)
        {
            // Inventory cursor movement and item selection logic will go here later
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // Draw the black backdrop across the screen
            DrawRect(spriteBatch, new Rectangle(0, 0, 256, 176), Color.Black);

            // Draw top half (inventory, B-Button box, item grid) and bottom half (map, compass, parchment)
            DrawSprite(spriteBatch, topInventoryTemplate, Vector2.Zero);
            DrawSprite(spriteBatch, bottomDungeonTemplate, new Vector2(0, 88));

            // Mask out the bright template placeholder rectangles with pure black
            DrawRect(spriteBatch, equippedSlotMask, Color.Black);
            DrawRect(spriteBatch, topCyanGuidesMask, Color.Black);
            DrawRect(spriteBatch, inventoryGridMask, Color.Black);
            DrawRect(spriteBatch, mapIconMask, Color.Black);
            DrawRect(spriteBatch, compassIconMask, Color.Black);

            // Mask out the checkerboard on the map with the actual map color
            Color parchmentOrange = new Color(252, 152, 56);
            DrawRect(spriteBatch, dungeonMapGridMask, parchmentOrange);

            // Draw the placeholder items and selection cursor
            DrawSprite(spriteBatch, boomerang, equippedItemPos); // Equipped in B slot
            DrawSprite(spriteBatch, boomerang, slot0ItemPos);    // Inventory slot 1
            DrawSprite(spriteBatch, bomb, slot1ItemPos);         // Inventory slot 2
            DrawSprite(spriteBatch, selectionCursor, cursorPos); // Red corner brackets
            DrawSprite(spriteBatch, mapItem, mapItemPos);        // Map item

            // Draw the dungeon room layout centered on the parchment
            DrawDungeonMinimap(spriteBatch);
        }

        private void DrawDungeonMinimap(SpriteBatch spriteBatch)
        {
            const int offsetX = 6;
            const int offsetY = 3;

            Point[] rooms = new[]
            {
                new Point(144 + offsetX, 112 + offsetY), // Top room (green marker)
                new Point(144 + offsetX, 120 + offsetY),
                new Point(128 + offsetX, 128 + offsetY), new Point(136 + offsetX, 128 + offsetY), new Point(144 + offsetX, 128 + offsetY),
                new Point(152 + offsetX, 128 + offsetY), new Point(160 + offsetX, 128 + offsetY), new Point(160 + offsetX, 120 + offsetY),
                new Point(136 + offsetX, 136 + offsetY), new Point(144 + offsetX, 136 + offsetY),
                new Point(144 + offsetX, 144 + offsetY),
                new Point(136 + offsetX, 152 + offsetY), new Point(144 + offsetX, 152 + offsetY), new Point(152 + offsetX, 152 + offsetY)
            };

            foreach (Point room in rooms)
            {
                DrawRect(spriteBatch, new Rectangle(room.X, room.Y, 7, 7), Color.Black);
            }

            // Connect horizontal/vertical hallways between rooms
            DrawRect(spriteBatch, new Rectangle(135 + offsetX, 131 + offsetY, 26, 2), Color.Black); // Main horizontal corridor
            DrawRect(spriteBatch, new Rectangle(147 + offsetX, 119 + offsetY, 2, 34), Color.Black); // Main vertical spine
            DrawRect(spriteBatch, new Rectangle(163 + offsetX, 127 + offsetY, 2, 2), Color.Black);  // Right branch connector
            DrawRect(spriteBatch, new Rectangle(143 + offsetX, 155 + offsetY, 10, 2), Color.Black); // Bottom entrance connectors

            // Green marker in the top room
            DrawRect(spriteBatch, new Rectangle(146 + offsetX, 114 + offsetY, 3, 3), new Color(0, 168, 0));
        }

        private void DrawSprite(SpriteBatch spriteBatch, Rectangle sourceRect, Vector2 nativePosition)
        {
            spriteBatch.Draw(
                hudSpriteSheet,
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
            spriteBatch.Draw(pixel, scaledRect, color);
        }
    }
}