using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;

namespace ZeldaGame;

public class WallMasterSprite : ISprite
{
   private Texture2D spriteTexture;
    private double elapsedTime;
    private int currentFrame; 

    public Vector2 Size { get; }

    public WallMasterSprite(Texture2D spriteTexture)
    {
        this.spriteTexture = spriteTexture;
        elapsedTime = 0;
        currentFrame = 0;
    }

    public void Reset()
    {
        currentFrame = 0;
    }

    public void Draw(GameTime gameTime, Vector2 position)
    {
        Rectangle sourceRectangle;

        if (currentFrame == 0)
            sourceRectangle = new Rectangle(393, 11, 16, 16);
        else
            sourceRectangle = new Rectangle(410, 12, 14, 15);

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