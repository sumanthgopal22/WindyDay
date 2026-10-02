using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;

namespace ZeldaGame;

public class AnimatedSprite : ISprite
{
    private Texture2D spriteTexture;
    private Rectangle[] sourceRectangles;
    private double frameDuration;
    private double elapsedTime;
    private int currentFrame; 

    public Vector2 Size { get; }

    public AnimatedSprite(Texture2D spriteTexture, Rectangle[] sourceRectangles, double frameDuration)
    {
        this.spriteTexture = spriteTexture;
        this.sourceRectangles = sourceRectangles;
        this.frameDuration = frameDuration;

        elapsedTime = 0;
        currentFrame = 0;

        Size = new Vector2(sourceRectangles[0].Width, sourceRectangles[0].Height);
    }

    public void SetDirection(spriteDirection direction)
    {
    }

    public void Reset()
    {
        currentFrame = 0;
    }

    public void Draw(GameTime gameTime, Vector2 position)
    {
        Rectangle sourceRectangle = sourceRectangles[currentFrame];

        Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, sourceRectangles[0].Width * 3, sourceRectangles[0].Height * 3);

        Core.SpriteBatch.Draw(spriteTexture, destinationRectangle, sourceRectangle, Color.White);
    }

    public void Update(GameTime gameTime)
    {
        elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

        if (elapsedTime > frameDuration)
        {
            elapsedTime -= frameDuration;
            currentFrame = (currentFrame + 1) % sourceRectangles.Length;
        }
    }
}