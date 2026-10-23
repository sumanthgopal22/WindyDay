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
        private Game1 game;

        public Tilemap Tilemap { get; set; }
        public Rectangle RoomBounds { get; set; }
        public string LevelFile { get; set; }

        private string[] levelFiles;
        private int currentLevelIndex = 0;
        private MouseState previousMouseState;
        private KeyboardState previousKeyboardState;

        // Map popup state variables
        private bool isMapOpen = false;
        private Texture2D pixelTexture;
        private SpriteFont mapFont;

        private Dictionary<string, Rectangle> roomClickZones;
        private Dictionary<string, Point> levelToGridMap;
        private Dictionary<Point, string> gridToLevelMap;
        private string hoveredMapLevelFile;

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

            currentLevelIndex = 0;
            LevelFile = levelFiles[currentLevelIndex];
        }

        public void LoadContent(ContentManager content, GraphicsDevice device)
        {
            // Create a 1x1 white pixel texture for UI shapes/backgrounds
            pixelTexture = new Texture2D(device, 1, 1);
            pixelTexture.SetData(new Color[] { Color.White });

            // Load font asset for the header text (asset name configured via MGCB)[cite: 2]
            mapFont = content.Load<SpriteFont>("AppleGaramond");

            // Load initial level & bounds properly
            LoadLevel(LevelFile, device);

            Rectangle screenBounds = device.PresentationParameters.Bounds;
            SetupMapClickZones(screenBounds);
        }

        private void LoadLevel(string file, GraphicsDevice device)
        {
            LevelFile = file;
            Tilemap = Tilemap.FromFile(Core.Content, LevelFile);
            Tilemap.Scale = new Vector2(4.0f, 4.0f);

            Rectangle screenBounds = device.PresentationParameters.Bounds;

            RoomBounds = new Rectangle(
                (int)Tilemap.TileWidth,
                (int)Tilemap.TileHeight,
                screenBounds.Width - (int)Tilemap.TileWidth * 2,
                screenBounds.Height - (int)Tilemap.TileHeight * 2
            );

            if (game != null && game.Player != null)
            {
                // game.Player.Position = new Vector2(RoomBounds.Center.X, RoomBounds.Center.Y);
            }
        }

        private void SetupMapClickZones(Rectangle screenBounds)
        {
            roomClickZones = new Dictionary<string, Rectangle>();
            levelToGridMap = new Dictionary<string, Point>();
            gridToLevelMap = new Dictionary<Point, string>();

            // Center the map popup layout on the screen
            int startX = screenBounds.Width / 2 - 120;
            int startY = screenBounds.Height / 2 - 100;
            int w = 40;
            int h = 24;
            int gap = 6;

            Dictionary<string, Point> gridPositions = new Dictionary<string, Point>()
            {
                { "levels/d01r15.xml", new Point(0, 0) },
                { "levels/d01r14.xml", new Point(1, 0) },
                { "levels/d01r13.xml", new Point(1, 1) },
                { "levels/d01r11.xml", new Point(0, 2) },
                { "levels/d01r12.xml", new Point(1, 2) },
                { "levels/d01r17.xml", new Point(2, 2) },
                { "levels/d01r19.xml", new Point(3, 2) },
                { "levels/d01r20.xml", new Point(4, 2) },
                { "levels/d01r09.xml", new Point(0, 3) },
                { "levels/d01r07.xml", new Point(1, 3) },
                { "levels/d01r08.xml", new Point(2, 3) },
                { "levels/d01r18.xml", new Point(3, 3) },
                { "levels/d01r06.xml", new Point(1, 4) },
                { "levels/d01r04.xml", new Point(0, 5) },
                { "levels/d01r03.xml", new Point(1, 5) },
                { "levels/d01r05.xml", new Point(2, 5) },
                { "levels/d01r16.xml", new Point(4, 0) }
            };

            foreach (var kvp in gridPositions)
            {
                if (Array.Exists(levelFiles, file => file == kvp.Key))
                {
                    int x = startX + kvp.Value.X * (w + gap);
                    int y = startY + kvp.Value.Y * (h + gap);
                    roomClickZones[kvp.Key] = new Rectangle(x, y, w, h);
                    levelToGridMap[kvp.Key] = kvp.Value;
                    gridToLevelMap[kvp.Value] = kvp.Key;
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();
            MouseState currentMouseState = Mouse.GetState();

            // Toggle map view when 'Z' is pressed
            if (currentKeyboardState.IsKeyDown(Keys.Z) && previousKeyboardState.IsKeyUp(Keys.Z))
            {
                isMapOpen = !isMapOpen;
                if (isMapOpen)
                {
                    // Default keyboard selection to the current active room when opening the map
                    hoveredMapLevelFile = LevelFile;
                }
            }

            if (isMapOpen)
            {
                // --- Keyboard Navigation (Arrow keys + WASD) ---
                Point moveDir = Point.Zero;

                if ((currentKeyboardState.IsKeyDown(Keys.Up) && previousKeyboardState.IsKeyUp(Keys.Up)) ||
                    (currentKeyboardState.IsKeyDown(Keys.W) && previousKeyboardState.IsKeyUp(Keys.W)))
                {
                    moveDir = new Point(0, -1);
                }
                else if ((currentKeyboardState.IsKeyDown(Keys.Down) && previousKeyboardState.IsKeyUp(Keys.Down)) ||
                         (currentKeyboardState.IsKeyDown(Keys.S) && previousKeyboardState.IsKeyUp(Keys.S)))
                {
                    moveDir = new Point(0, 1);
                }
                else if ((currentKeyboardState.IsKeyDown(Keys.Left) && previousKeyboardState.IsKeyUp(Keys.Left)) ||
                         (currentKeyboardState.IsKeyDown(Keys.A) && previousKeyboardState.IsKeyUp(Keys.A)))
                {
                    moveDir = new Point(-1, 0);
                }
                else if ((currentKeyboardState.IsKeyDown(Keys.Right) && previousKeyboardState.IsKeyUp(Keys.Right)) ||
                         (currentKeyboardState.IsKeyDown(Keys.D) && previousKeyboardState.IsKeyUp(Keys.D)))
                {
                    moveDir = new Point(1, 0);
                }

                if (moveDir != Point.Zero && levelToGridMap.ContainsKey(hoveredMapLevelFile))
                {
                    Point currentGridPos = levelToGridMap[hoveredMapLevelFile];
                    Point targetGridPos = new Point(currentGridPos.X + moveDir.X, currentGridPos.Y + moveDir.Y);

                    // If a valid room exists at the target grid position, move selection there
                    if (gridToLevelMap.ContainsKey(targetGridPos))
                    {
                        hoveredMapLevelFile = gridToLevelMap[targetGridPos];
                    }
                }

                // Select current hovered room with Enter key
                if (currentKeyboardState.IsKeyDown(Keys.Enter) && previousKeyboardState.IsKeyUp(Keys.Enter))
                {
                    currentLevelIndex = Array.IndexOf(levelFiles, hoveredMapLevelFile);
                    LoadLevel(hoveredMapLevelFile, game.GraphicsDevice);
                    isMapOpen = false;
                }

                // --- Mouse Click Navigation ---
                if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
                {
                    Point mousePos = new Point(currentMouseState.X, currentMouseState.Y);

                    foreach (var zone in roomClickZones)
                    {
                        if (zone.Value.Contains(mousePos))
                        {
                            hoveredMapLevelFile = zone.Key;
                            currentLevelIndex = Array.IndexOf(levelFiles, zone.Key);
                            LoadLevel(zone.Key, game.GraphicsDevice);
                            isMapOpen = false;
                            break;
                        }
                    }
                }
            }

            previousMouseState = currentMouseState;
            previousKeyboardState = currentKeyboardState;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // Draw gameplay elements normally
            Tilemap.Draw(spriteBatch);
            game.Player.Draw(gameTime);

            // If map is open, draw the retro layout overlay
            if (isMapOpen)
            {
                Rectangle screenBounds = game.GraphicsDevice.PresentationParameters.Bounds;

                // Pure black background filling the screen like the reference image
                spriteBatch.Draw(pixelTexture, screenBounds, Color.Black);

                // Draw each room box
                foreach (var zone in roomClickZones)
                {
                    bool isCurrentRoom = (zone.Key == LevelFile);
                    bool isHoveredRoom = (zone.Key == hoveredMapLevelFile);

                    Color roomColor;
                    if (isCurrentRoom)
                    {
                        roomColor = new Color(110, 230, 0); // Bright Green for active room
                    }
                    else if (isHoveredRoom)
                    {
                        roomColor = Color.Gold;             // Gold/Yellow for keyboard-hovered room
                    }
                    else
                    {
                        roomColor = new Color(85, 65, 230); // Retro Blue for standard rooms
                    }

                    spriteBatch.Draw(pixelTexture, zone.Value, roomColor);
                }

                // Draw "Level-#" text above the menu (subtracting 2 from the file number string)
                if (mapFont != null)
                {
                    string roomNumStr = LevelFile.Substring(LevelFile.Length - 6, 2);
                    if (int.TryParse(roomNumStr, out int roomNumber))
                    {
                        int levelNum = roomNumber - 2; // e.g., 03 becomes 1
                        string headerText = $"Level-{levelNum}";

                        Vector2 textSize = mapFont.MeasureString(headerText);
                        Vector2 textPos = new Vector2(
                            (screenBounds.Width - textSize.X) / 2,
                            (screenBounds.Height / 2 - 100) - textSize.Y - 15
                        );

                        spriteBatch.DrawString(mapFont, headerText, textPos, Color.White);
                    }
                }
            }
        }
    }
}