using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using ZeldaGame.GameScripts.Sprites;

namespace ZeldaGame;

public class NonAnimatedSprite : ISprite
{
    public Texture2D SpriteTexture { get; set; }
    public Rectangle SourceRectangle { get; set; }

    public int Width => SourceRectangle.Width;
    public int Height => SourceRectangle.Height;

    public Vector2 Size { get; }

    public NonAnimatedSprite(Texture2D spriteTexture, Rectangle sourceRectangle)
    {
        this.SpriteTexture = spriteTexture;
        this.SourceRectangle = sourceRectangle;
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
        Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, SourceRectangle.Width * 3, SourceRectangle.Height * 3);

        Core.SpriteBatch.Draw(SpriteTexture, destinationRectangle, SourceRectangle, Color.White);
    }

    public void Update(GameTime gameTime)
    {
    }
}