using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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

        public GameplayScreen(Game1 game)
        {
            this.game = game;

            // Default "test" level file
            LevelFile = "levels/d01r01.xml";
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
            
        }
    }
}
