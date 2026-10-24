using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using ZeldaGame.GameScripts.Sprites;

namespace ZeldaGame;

public class NonAnimatedSprite : ISprite
{
    public Texture2D SpriteTexture { get; set; }
    public Rectangle SourceRectangle { get; set; }

    private float rotation = 0f;
    public int Width => SourceRectangle.Width;
    public int Height => SourceRectangle.Height;

    public Vector2 Size { get; }

    public NonAnimatedSprite(Texture2D spriteTexture, Rectangle sourceRectangle)
    {
        SpriteTexture = spriteTexture;
        SourceRectangle = sourceRectangle;
        Size = new Vector2(sourceRectangle.Width * 4, sourceRectangle.Height * 4);
    }

    public NonAnimatedSprite(Texture2D spriteTexture, int x, int y, int width, int height)
    {
        SpriteTexture = spriteTexture;
        SourceRectangle = new Rectangle(x, y, width, height);
        Size = new Vector2(width * 4, height * 4);
    }

    public void SetDirection(SpriteDirection direction)
    {
        switch (direction)
        {
            case SpriteDirection.Right:
                rotation = MathHelper.PiOver2;
                break;
            case SpriteDirection.Down:
                rotation = MathHelper.Pi;
                break;
            case SpriteDirection.Left:
                rotation = -MathHelper.PiOver2;
                break;
            default:
                rotation = 0f;
                break;
        }
    }

    public void Reset()
    {
    }

    public void Draw(GameTime gameTime, Vector2 position)
    {
        // Rotate around the middle of the sprite instead of its top-left corner
        Vector2 origin = new Vector2(SourceRectangle.Width / 2f, SourceRectangle.Height / 2f);
        Rectangle destinationRectangle = new Rectangle((int)(position.X + Size.X / 2),
        (int)(position.Y + Size.Y / 2),
        SourceRectangle.Width * 4,
        SourceRectangle.Height * 4);

        Core.SpriteBatch.Draw(SpriteTexture, destinationRectangle, SourceRectangle, 
        Color.White, rotation, origin, SpriteEffects.None, 0f);
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color, Vector2 scale)
    {
        Rectangle destinationRectangle = new Rectangle(
            (int)position.X,
            (int)position.Y,
            (int)(SourceRectangle.Width * scale.X),
            (int)(SourceRectangle.Height * scale.Y)
        );

        spriteBatch.Draw(
            SpriteTexture,
            destinationRectangle,
            SourceRectangle,
            color
        );
    }

    public void Update(GameTime gameTime)
    {
    }
}