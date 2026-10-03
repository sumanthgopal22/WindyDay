using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;

namespace ZeldaGame;

public class GoriyaSprite : ISprite
{
   private Texture2D spriteTexture;
    private double elapsedTime;
    private int currentFrame; 

    public Vector2 Size { get; }

    public GoriyaSprite(Texture2D spriteTexture)
    {
        this.spriteTexture = spriteTexture;
        elapsedTime = 0;
        currentFrame = 0;
    }

    public void Reset()
    {
        currentFrame = 0;
    }

    public void SetDirection(spriteDirection direction)
    {
        // GoriyaSprite does not have directional animations, so this method is not needed.
    }

    public void Draw(GameTime gameTime, Vector2 position)
    {
        Rectangle sourceRectangle;

        if (currentFrame == 0)
            sourceRectangle = new Rectangle(224, 11, 13, 16);
        else if (currentFrame == 1)
            sourceRectangle = new Rectangle(241, 11, 13, 16);
        else if (currentFrame == 2)
            sourceRectangle = new Rectangle(257, 11, 13, 16);
        else
            sourceRectangle = new Rectangle(275, 12, 14, 16);

        Core.SpriteBatch.Draw(spriteTexture, position, sourceRectangle, Color.White, 0f, Vector2.Zero, 2, SpriteEffects.None, 0f);
    }

    public void Update(GameTime gameTime)
    {
        double frameDuration = 0.1;
        elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

        if (elapsedTime > frameDuration)
        {
            elapsedTime -= frameDuration;
            currentFrame++;
        }

        if (currentFrame > 3)
            currentFrame = 0;
    }
}