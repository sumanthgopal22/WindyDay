using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using ZeldaGame.GameScripts.Interfaces;

namespace ZeldaGame.GameScripts.Screens
{
    public class GameplayScreen : IScreen
    {
        Game1 game;

        public GameplayScreen(Game1 game)
        {
            this.game = game;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            game.Player.Draw(gameTime);
            game.EnemyCycler.Draw(gameTime);
            game.ItemCycler.Draw(gameTime);
            game.Block.Draw(gameTime);
        }

        public void LoadContent(ContentManager content, GraphicsDevice device)
        {
            
        }

        public void Update(GameTime gameTime)
        {
            
        }
    }
}
