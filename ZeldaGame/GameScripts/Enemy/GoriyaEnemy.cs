using Microsoft.Xna.Framework;
using MonoGameLibrary;

namespace ZeldaGame;

public class GoriyaEnemy : IEnemy
{
    private ISprite goriyaSprite;
    private ISprite goriyaUpSprite;
    private ISprite goriyaDownSprite;
    private ISprite goriyaLeftSprite;
    private ISprite goriyaRightSprite;
    private Vector2 position;
    private const int SideLength = 80; // pixels per side of the square basically the max amount of steps sprite can take before changing directions
    private int side = 0; // 0 = right, 1 = down, 2 = left, 3 = up
    private int stepsTaken = 0;

    private Boomerang boomerang = null; // null when Goriya is not throwing
    private const double SecondsBetweenAttacks = 3.0;
    private double secondsSinceLastAttack = 0;

    public GoriyaEnemy(Vector2 startPostion)
    {
        position = startPostion;
    }
    public void MoveRight()
    {
        position.X += 1;
        goriyaSprite = goriyaRightSprite;
    }

    public void MoveLeft()
    {
        position.X -= 1;
        goriyaSprite = goriyaLeftSprite;
    }

    public void MoveUp()
    {
        position.Y -= 1;
        goriyaSprite = goriyaUpSprite;
    }

    public void MoveDown()
    {
        position.Y += 1;
        goriyaSprite = goriyaDownSprite;
    }

    // Throws a boomerang in the direction Goriya is facing
    public void Attack()
    {
        Vector2 velocity;
        switch (side)
        {
            case 0: velocity = new Vector2(3, 0); break;  // right
            case 1: velocity = new Vector2(0, 3); break;  // down
            case 2: velocity = new Vector2(-3, 0); break; // left
            default: velocity = new Vector2(0, -3); break; // up
        }

        // Start the boomerang roughly in the middle of Goriya
        Vector2 throwPosition = new Vector2(position.X + 8, position.Y + 12);

        boomerang = new Boomerang(throwPosition, velocity);
    }
    
    public void LoadContent()
    {
        position = new Vector2(Core.Instance.Window.ClientBounds.Width, Core.Instance.Window.ClientBounds.Height) * 0.75f;
        goriyaUpSprite = EnemySpriteFactory.Instance.CreateGoriyaSprite(spriteDirection.Up);
        goriyaDownSprite = EnemySpriteFactory.Instance.CreateGoriyaSprite(spriteDirection.Down);
        goriyaLeftSprite = EnemySpriteFactory.Instance.CreateGoriyaSprite(spriteDirection.Left);
        goriyaRightSprite = EnemySpriteFactory.Instance.CreateGoriyaSprite(spriteDirection.Right);
        goriyaSprite = goriyaRightSprite;
    }

    public void Update(GameTime gameTime)
    {
        // While the boomerang is flying, Goriya stands still and waits for it to come back
        if (boomerang != null)
        {
            boomerang.Update(gameTime);

            if (boomerang.HasReturned)
            {
                boomerang = null;
            }

            goriyaSprite.Update(gameTime);
            return;
        }

        switch (side)
        {
            case 0: MoveRight(); break;
            case 1: MoveDown(); break;
            case 2: MoveLeft(); break;
            case 3: MoveUp(); break;
        }

        stepsTaken++;
        if (stepsTaken >= SideLength)
        {
            stepsTaken = 0;
            side = (side + 1) % 4;
        }

        // Attack every few seconds
        secondsSinceLastAttack += gameTime.ElapsedGameTime.TotalSeconds;
        if (secondsSinceLastAttack >= SecondsBetweenAttacks)
        {
            secondsSinceLastAttack = 0;
            Attack();
        }

        goriyaSprite.Update(gameTime);
    }

    public void Draw(GameTime gameTime)
    {
        goriyaSprite.Draw(gameTime, position);

        if (boomerang != null)
        {
            boomerang.Draw(gameTime);
        }
    }
}