using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeldaGame.GameScripts.Interfaces;

namespace ZeldaGame.GameScripts.Screens
{
    public class GameOverScreen : IScreen
    {
        private Game1 game;
        private Texture2D background;

        public GameOverScreen(Game1 game)
        {
            this.game = game;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (background != null)
            {
                spriteBatch.Draw(
                    background,
                    new Rectangle(0, 0, background.Width, background.Height),
                    Color.White
                );
            }
        }

        public void LoadContent(ContentManager content, GraphicsDevice device)
        {
            background = content.Load<Texture2D>("TLoZ_Game_Over.png");
        }

        public void Update(GameTime gameTime)
        {

        }
    }
}
