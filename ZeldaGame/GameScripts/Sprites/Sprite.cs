using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;

namespace ZeldaGame;

public enum SpriteAction
{
    UseItem,
    SwingSword
}

public class Sprite : IActionSprite
{
    private Texture2D spriteTexture;
    private double elapsedTime;
    private double actionElapsedTime;
    private int currentFrame;
    private spriteDirection currentDirection;
    private SpriteAction? currentAction;

    private const double ActionDuration = 0.4;

    public Vector2 Size { get; }
    public bool IsActionPlaying => currentAction.HasValue;

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

    public void PlayAction(SpriteAction action)
    {
        currentAction = action;
        actionElapsedTime = 0;
        elapsedTime = 0;
        currentFrame = 0;
    }

    public void Reset()
    {
        currentFrame = 0;
        elapsedTime = 0;
    }

    public void Draw(GameTime gameTime, Vector2 position)
    {
        Rectangle sourceRectangle;

        int x = currentDirection switch
        {
            spriteDirection.Down => 0,
            spriteDirection.Left => 58,
            spriteDirection.Up => 114,
            spriteDirection.Right => 174,
            _ => 0
        };

        if (currentAction.HasValue)
        {
            if (currentAction.Value == SpriteAction.SwingSword)
            {
                switch (currentDirection)
                {
                    case spriteDirection.Left:
                        sourceRectangle = new Rectangle(x-22, 174, 64, 32);
                        break;
                    case spriteDirection.Right:
                        sourceRectangle = new Rectangle(x-22, 174, 64, 32);
                        break;
                    default:
                        sourceRectangle = new Rectangle(x, 160, 29, 58);
                        break;
                }
            }
            else
            {
                sourceRectangle = new Rectangle(x, 116, 29, 32);
            }
        }
        else
        {
            switch (currentDirection)
            {
                case spriteDirection.Down:
                    sourceRectangle = new Rectangle(0, currentFrame == 0 ? 0 : 58, 29, 32);
                    break;
                case spriteDirection.Up:
                    sourceRectangle = new Rectangle(116, currentFrame == 0 ? 0 : 58, 29, 32);
                    break;
                case spriteDirection.Left:
                    sourceRectangle = new Rectangle(58, currentFrame == 0 ? 0 : 58, 29, 32);
                    break;
                case spriteDirection.Right:
                    sourceRectangle = new Rectangle(174, currentFrame == 0 ? 58 : 0, 29, 32);
                    break;
                default:
                    sourceRectangle = new Rectangle(0, 0, 29, 32);
                    break;
            }
        }

        Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, 76, 84);
        if (currentAction.HasValue && currentAction.Value == SpriteAction.SwingSword)
        {
            if (currentDirection == spriteDirection.Left || currentDirection == spriteDirection.Right)
            {
                destinationRectangle.Width = 158;
                destinationRectangle.X += currentDirection == spriteDirection.Left ? -76 : -20;
            }
            else if (currentDirection == spriteDirection.Up || currentDirection == spriteDirection.Down)
            {
                destinationRectangle.Height = 152;
                destinationRectangle.Y += currentDirection == spriteDirection.Up ? -66 : -2;
            }
        }

        Core.SpriteBatch.Draw(spriteTexture, destinationRectangle, sourceRectangle, Color.White);
    }

    public void Update(GameTime gameTime)
    {
        if (currentAction.HasValue)
        {
            actionElapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
            if (actionElapsedTime >= ActionDuration)
            {
                currentAction = null;
                actionElapsedTime = 0;
                currentFrame = 0;
                elapsedTime = 0;
            }

            return;
        }

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