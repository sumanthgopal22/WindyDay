using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input; 
using MonoGameLibrary;
using System;
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

        // Array holding all room file paths
        private string[] levelFiles;
        private int currentLevelIndex = 0;
        private MouseState previousMouseState;

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
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Tilemap.Draw(spriteBatch);
            game.Player.Draw(gameTime);
            game.EnemyCycler.Draw(gameTime);
            game.ItemCycler.Draw(gameTime);
            game.Block.Draw(gameTime);
        }

        public void LoadContent(ContentManager content, GraphicsDevice device)
        {
            Tilemap = Tilemap.FromFile(Core.Content, LevelFile);
            Tilemap.Scale = new Vector2(4.0f, 4.0f);

            Rectangle screenBounds = device.PresentationParameters.Bounds;

            RoomBounds = new Rectangle(
                (int)Tilemap.TileWidth,
                (int)Tilemap.TileHeight,
                screenBounds.Width - (int)Tilemap.TileWidth * 2,
                screenBounds.Height - (int)Tilemap.TileHeight * 2
            );

            int centerRow = Tilemap.Rows / 2;
            int centerColumn = Tilemap.Columns / 2;
        }

        public void Update(GameTime gameTime)
        {
            MouseState currentMouseState = Mouse.GetState();

            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                currentLevelIndex = (currentLevelIndex + 1) % levelFiles.Length;
                LevelFile = levelFiles[currentLevelIndex];

                Tilemap = Tilemap.FromFile(Core.Content, LevelFile);
                Tilemap.Scale = new Vector2(4.0f, 4.0f);
            }

            previousMouseState = currentMouseState;
        }
    }
}