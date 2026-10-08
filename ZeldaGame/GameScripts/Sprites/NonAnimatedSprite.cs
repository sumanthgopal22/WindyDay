using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using ZeldaGame.GameScripts.Sprites;

namespace ZeldaGame;

public class NonAnimatedSprite : ISprite
{
    private Texture2D spriteTexture;
    private Rectangle sourceRectangle;

    public Vector2 Size { get; }

    public NonAnimatedSprite(Texture2D spriteTexture, Rectangle sourceRectangle)
    {
        this.spriteTexture = spriteTexture;
        this.sourceRectangle = sourceRectangle;
        Size = new Vector2(sourceRectangle.Width, sourceRectangle.Height);
    }

    public void SetDirection(SpriteDirection direction)
    {
    }

    public void Reset()
    {
    }

    public void Draw(GameTime gameTime, Vector2 position)
    {
        Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, sourceRectangle.Width * 3, sourceRectangle.Height * 3);

        Core.SpriteBatch.Draw(spriteTexture, destinationRectangle, sourceRectangle, Color.White);
    }

    public void Update(GameTime gameTime)
    {
    }
}