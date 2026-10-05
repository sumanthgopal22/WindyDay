using Microsoft.Xna.Framework;

namespace ZeldaGame;

public class Boomerang : IGameObject
{
    private ISprite boomerangSprite;
    private Vector2 position;
    private Vector2 startPosition;
    private Vector2 velocity; // how many pixels the boomerang moves each frame while flying out
    private const int StepsBeforeComingBack = 60; // how far the boomerang flies out before turning around
    private int stepsTaken = 0;
    private bool isComingBack = false;
    // True once the boomerang has made it back to where it was thrown from
    public bool HasReturned { get; private set; } = false;

    public Boomerang(Vector2 startPosition, Vector2 velocity)
    {
        this.startPosition = startPosition;
        this.velocity = velocity;
        position = startPosition;
        boomerangSprite = EnemySpriteFactory.Instance.CreateBoomerangSprite();
    }

    public void Update(GameTime gameTime)
    {
        if (isComingBack)
        {
            // Fly back the same way it came out
            position -= velocity;
        }
        else
        {
            position += velocity;
        }

        stepsTaken++;
        if (stepsTaken >= StepsBeforeComingBack)
        {
            stepsTaken = 0;

            if (isComingBack)
            {
                // It has flown back the full distance, so it is back where it started
                position = startPosition;
                HasReturned = true;
            }
            else
            {
                isComingBack = true;
            }
        }

        boomerangSprite.Update(gameTime);
    }

    public void Draw(GameTime gameTime)
    {
        boomerangSprite.Draw(gameTime, position);
    }
}
