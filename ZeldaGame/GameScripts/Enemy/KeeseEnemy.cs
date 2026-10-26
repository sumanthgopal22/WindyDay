using System;
using Microsoft.Xna.Framework;
using ZeldaGame.GameScripts.Tilemapping;

namespace ZeldaGame;

public class KeeseEnemy : IEnemy
{
    private ISprite keeseSprite;
    private Vector2 position;
    private Vector2 previousPosition;
    private const int SideLength = 40; // how many steps Keese flies before picking a new direction
    private int side = 0; // 0 = right, 1 = down, 2 = left, 3 = up, 4 = up-right, 5 = down-right, 6 = down-left, 7 = up-left
    private int stepsTaken = 0;
    public event Action<IProjectile> ProjectileSpawned;
    private Random random = new Random();
    public Rectangle BoundingBox => new Rectangle((int)position.X, (int)position.Y, (int)keeseSprite.Size.X, (int)keeseSprite.Size.Y);


    public KeeseEnemy(Vector2 startPosition)
    {
        position = startPosition;
    }

    public void MoveRight()
    {
        position.X += 1;
    }

    public void MoveLeft()
    {
        position.X -= 1;
    }

    public void MoveUp()
    {
        position.Y -= 1;
    }

    public void MoveDown()
    {
        position.Y += 1;
    }

    public void Attack()
    {
        // Keese does not have a ranged attack, it only hurts Link by flying into him
    }

    public void OnCollideWithObject(ICollidable target, CollisionSide side)
    {
        if (target is TileBlock tile && tile.IsCollidable)
        {
            position = previousPosition;

            this.side = random.Next(8);
            stepsTaken = 0;
        }
    }

    public void LoadContent()
    {
        keeseSprite = EnemySpriteFactory.Instance.CreateKeeseSprite();
    }

    public void Update(GameTime gameTime)
    {

        previousPosition = position;

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
            case 4: 
                MoveUp(); 
                MoveRight(); 
                break;
            case 5: 
                MoveDown(); 
                MoveRight(); 
                break;
            case 6: 
                MoveDown(); 
                MoveLeft(); 
                break;
            case 7: 
                MoveUp(); 
                MoveLeft(); 
                break;
        }

        stepsTaken++;
        if (stepsTaken >= SideLength)
        {
            stepsTaken = 0;
            side = random.Next(8);
        }

        keeseSprite.Update(gameTime);
    }

    public void Draw(GameTime gameTime)
    {
        keeseSprite.Draw(gameTime, position);
    }
}
