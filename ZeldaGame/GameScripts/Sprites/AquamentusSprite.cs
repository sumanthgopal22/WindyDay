using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;

namespace ZeldaGame;

public class AquamentusSprite : ISprite
{
   private Texture2D spriteTexture;
    private double elapsedTime;
    private int currentFrame; 

    public Vector2 Size { get; }

    public AquamentusSprite(Texture2D spriteTexture)
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
        // AquamentusSprite does not have directional animations, so this method is not needed.
    }

    public void Draw(GameTime gameTime, Vector2 position)
    {
        Rectangle sourceRectangle;

        if (currentFrame == 0)
            sourceRectangle = new Rectangle(1, 11, 24, 32);
        else if (currentFrame == 1)
            sourceRectangle = new Rectangle(26, 11, 24, 32);
        else if (currentFrame == 2)
            sourceRectangle = new Rectangle(51, 11, 24, 32);
        else
            sourceRectangle = new Rectangle(76, 11, 24, 32);

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

        if (currentFrame > 1)
            currentFrame = 0;
    }
}