using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;

namespace Sprint0Game;

public class Sprite : ISprite
{
    private Texture2D spriteTexture;
    private double elapsedTime;
    private int currentFrame; 

    public Vector2 Size { get; }

    public Sprite(Texture2D spriteTexture)
    {
        this.spriteTexture = spriteTexture;
        elapsedTime = 0;
        currentFrame = 0;
        Size = new Vector2(24f, 32f);
    }

    public void Reset()
    {
        currentFrame = 0;
    }

    public void Draw(GameTime gameTime, Vector2 position)
    {
        Rectangle sourceRectangle;

        if (currentFrame == 0)
            sourceRectangle = new Rectangle(1, 1, 24, 32);

        else if (currentFrame == 1)
            sourceRectangle = new Rectangle(26, 1, 24, 32);

        else if (currentFrame == 2)
            sourceRectangle = new Rectangle(51, 1, 24, 32);

        else
            sourceRectangle = new Rectangle(1, 34, 24, 32);

        Core.SpriteBatch.Draw(spriteTexture, position, sourceRectangle, Color.White);
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

        if (currentFrame > 2)
            currentFrame = 0;
    }
}