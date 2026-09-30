using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;

namespace ZeldaGame;

public class Block : IBlock
{
    private ISprite block;
    private Texture2D spriteTexture;
    private Vector2 position;


    public void LoadContent()
    {
        position = new Vector2(Core.Instance.Window.ClientBounds.Width, Core.Instance.Window.ClientBounds.Height) * 0.25f;
        spriteTexture = Core.Content.Load<Texture2D>("spritesheets/BlocksSheet");
        block = new BlockSprite(spriteTexture);
    }

    public void Draw(GameTime gameTime)
    {
        block.Draw(gameTime, position);
    }

    public void Update(GameTime gameTime)
    {
        
    }
}