using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeldaGame.GameScripts.Interfaces;

namespace ZeldaGame.GameScripts.Screens
{
    public class GameWinScreen : IScreen
    {
        private Game1 game;
        private SpriteFont font;

        public GameWinScreen(Game1 game)
        {
            this.game = game;
        }

        public void LoadContent(ContentManager content, GraphicsDevice device)
        {
            font = content.Load<SpriteFont>("AppleGaramond");
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            string message = "You win!";
            string instructions = "Press SPACE to return";

            float scale = 3.0f;

            Vector2 messageSize = font.MeasureString(message) * scale;
            Vector2 instructionsSize = font.MeasureString(instructions) * (scale / 2.0f);

            spriteBatch.DrawString(
                font,
                message,
                new Vector2((1024 - messageSize.X) / 2, 280),
                Color.Gold,
                0f,
                Vector2.Zero,
                scale,
                SpriteEffects.None,
                0f
            );

            spriteBatch.DrawString(
                font,
                instructions,
                new Vector2((1024 - instructionsSize.X) / 2, 360),
                Color.White,
                0f,
                Vector2.Zero,
                scale / 2.0f,
                SpriteEffects.None,
                0f
            );
        }
    }
}
