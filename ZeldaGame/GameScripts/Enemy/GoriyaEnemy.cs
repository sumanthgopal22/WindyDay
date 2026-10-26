using Microsoft.Xna.Framework;
using ZeldaGame.GameScripts.Sprites;
using System;

namespace ZeldaGame;

public class GoriyaEnemy : IEnemy
{
    private ISprite goriyaSprite;
    private ISprite goriyaUpSprite;
    private ISprite goriyaDownSprite;
    private ISprite goriyaLeftSprite;
    private ISprite goriyaRightSprite;
    private Vector2 position;
    private Random random = new Random();
    private const int SideLength = 80; // pixels per side of the square basically the max amount of steps sprite can take before changing directions
    private int side = 0; // 0 = right, 1 = down, 2 = left, 3 = up
    private int stepsTaken = 0;
    private Boomerang activeBoomerang = null; // null when Goriya is not throwing
    private const double SecondsBetweenAttacks = 3.0;
    private double secondsSinceLastAttack = 0;

    public event System.Action<IProjectile> ProjectileSpawned;
    public Rectangle BoundingBox => new Rectangle((int)position.X, (int)position.Y, (int)goriyaSprite.Size.X, (int)goriyaSprite.Size.Y);


    public GoriyaEnemy(Vector2 startPosition)
    {
        position = startPosition;
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
            case 0: 
                velocity = new Vector2(3, 0); 
                break;  // right
            case 1: 
                velocity = new Vector2(0, 3); 
                break;  // down
            case 2: 
                velocity = new Vector2(-3, 0); 
                break; // left
            default: 
                velocity = new Vector2(0, -3); 
                break; // up
        }

        // Start the boomerang roughly in the middle of Goriya
        Vector2 throwPosition = new Vector2(position.X + 8, position.Y + 12);

        // Mark as hostile so collision handlers target Link instead of enemies
        activeBoomerang = new Boomerang(throwPosition, velocity, isHostile: true);
        ProjectileSpawned?.Invoke(activeBoomerang);
    }

    public void OnCollideWithObject(ICollidable target, CollisionSide side)
    {
        
    }
    
    public void LoadContent()
    {
        goriyaUpSprite = EnemySpriteFactory.Instance.CreateGoriyaSprite(SpriteDirection.Up);
        goriyaDownSprite = EnemySpriteFactory.Instance.CreateGoriyaSprite(SpriteDirection.Down);
        goriyaLeftSprite = EnemySpriteFactory.Instance.CreateGoriyaSprite(SpriteDirection.Left);
        goriyaRightSprite = EnemySpriteFactory.Instance.CreateGoriyaSprite(SpriteDirection.Right);
        goriyaSprite = goriyaRightSprite;
    }

    public void Update(GameTime gameTime)
    {
        // While the boomerang is flying, Goriya stands still and waits for it to come back
        if (activeBoomerang != null)
        {
            activeBoomerang.Update(gameTime);

            if (activeBoomerang.HasReturned || !activeBoomerang.IsActive)
            {
                activeBoomerang = null;
            }
            else
            {
                goriyaSprite.Update(gameTime);
                return;
            }
        }

        switch (side)
        {
            case 0: 
                MoveRight(); 
                break;
            case 1: 
                MoveDown(); 
                break;
            case 2: 
                MoveLeft(); 
                break;
            case 3: 
                MoveUp(); 
                break;
        }

        stepsTaken++;
        if (stepsTaken >= SideLength)
        {
            stepsTaken = 0;
            side = random.Next(4);
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

        // if (activeBoomerang != null)
        // {
        //     activeBoomerang.Draw(gameTime);
        // }
    }
}