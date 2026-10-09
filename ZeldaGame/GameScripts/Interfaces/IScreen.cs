using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaGame.GameScripts.Interfaces
{
    public interface IScreen
    {
        void LoadContent(ContentManager content, GraphicsDevice device);
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}
