using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;

namespace ZeldaGame;

public class Sprite : ISprite
{

    private Texture2D spriteTexture;
    private double elapsedTime;
    private int currentFrame;
    private spriteDirection currentDirection;

    public Vector2 Size { get; }

    public Sprite(Texture2D spriteTexture)
    {
        this.spriteTexture = spriteTexture;
        elapsedTime = 0;
        currentFrame = 0;
    }

    public void SetDirection(spriteDirection direction)
    {
        currentDirection = direction;
    }

    public void Reset()
    {
        currentFrame = 0;
        elapsedTime = 0;
    }

    public void Draw(GameTime gameTime, Vector2 position)
    {
        Rectangle sourceRectangle;

        //downward-facing animation
        switch(currentDirection)
        {
            case spriteDirection.Down:
                if (currentFrame == 0)
                    sourceRectangle = new Rectangle(0, 0, 29, 32);
                else
                    sourceRectangle = new Rectangle(0, 58, 29, 32);
                break;
            case spriteDirection.Up:
                if (currentFrame == 0)
                    sourceRectangle = new Rectangle(116, 0, 29, 32);
                else
                    sourceRectangle = new Rectangle(116, 58, 29, 32);
                break;
            case spriteDirection.Left:
                if (currentFrame == 0)
                    sourceRectangle = new Rectangle(58, 0, 29, 32);
                else
                    sourceRectangle = new Rectangle(58, 58, 29, 32);
                break;
            case spriteDirection.Right:
                if (currentFrame == 0)
                    sourceRectangle = new Rectangle(174, 58, 29, 32);
                else
                    sourceRectangle = new Rectangle(174, 0, 29, 32);
                break;
            default:
                sourceRectangle = new Rectangle(0, 0, 29, 32);
                break;
        }

        //insert new source rectangles for all facing directions
        
         Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, 76,84);

        Core.SpriteBatch.Draw(spriteTexture, destinationRectangle, sourceRectangle, Color.White);
    }

    public void Update(GameTime gameTime)
    {
        double frameDuration = 0.2;
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