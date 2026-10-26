using Microsoft.Xna.Framework;
using ZeldaGame.GameScripts.Tilemapping;

namespace ZeldaGame;

//Goriya and Link both use this class
public class Boomerang : IGameObject, ICollidable, IProjectile
{
    private ISprite boomerangSprite;
    private Vector2 position;
    private Vector2 startPosition;
    private Vector2 velocity; // how many pixels the boomerang moves each frame while flying out
    private int stepsBeforeComingBack; // how far the boomerang flies out before turning around
    private int stepsTaken = 0;
    private bool isComingBack = false;
    // True once the boomerang has made it back to where it was thrown from
    public bool HasReturned { get; private set; } = false;
    public bool isHostile { get; }

    public bool IsActive => !HasReturned;

    public Rectangle BoundingBox => new Rectangle((int)position.X, (int)position.Y, (int)boomerangSprite.Size.X, (int)boomerangSprite.Size.Y);

    public Boomerang(Vector2 startPosition, Vector2 velocity, int stepsBeforeComingBack = 30, bool isHostile = false)
    {
        this.startPosition = startPosition;
        this.velocity = velocity;
        this.stepsBeforeComingBack = stepsBeforeComingBack;
        this.isHostile = isHostile;
        position = startPosition;
        boomerangSprite = EnemySpriteFactory.Instance.CreateBoomerangSprite();
    }

    public void TriggerReturn()
    {
        isComingBack = true;
        stepsTaken = 0;
    }

    public void OnCollideWithObject(ICollidable target, CollisionSide side)
    {
        if (target is TileBlock tile && tile.IsCollidable)
        {
            TriggerReturn();
        }
    }

    public void Update(GameTime gameTime)
    {
        if (isComingBack == true)
        {
            // Fly back the same way it came out
            position -= velocity;
        }
        else
        {
            position += velocity;
        }

        stepsTaken++;
        if (stepsTaken >= stepsBeforeComingBack)
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
