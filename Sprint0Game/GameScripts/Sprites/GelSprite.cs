using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;

namespace Sprint0Game;

public class GelSprite : ISprite
{
   private Texture2D spriteTexture;
    private double elapsedTime;
    private int currentFrame; 

    public Vector2 Size { get; }

    public GelSprite(Texture2D spriteTexture)
    {
        this.spriteTexture = spriteTexture;
        elapsedTime = 0;
        currentFrame = 0;
        Size = new Vector2(8f, 16f);
    }

    public void Reset()
    {
        currentFrame = 0;
    }

    public void Draw(GameTime gameTime, Vector2 position)
    {
        Rectangle sourceRectangle;

        if (currentFrame == 0)
            sourceRectangle = new Rectangle(1, 11, 8, 16);

        else
            sourceRectangle = new Rectangle(10, 11, 8, 16);

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