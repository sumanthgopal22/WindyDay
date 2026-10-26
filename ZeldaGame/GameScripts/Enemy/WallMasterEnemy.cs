using System;
using Microsoft.Xna.Framework;
using ZeldaGame.GameScripts.Tilemapping;

namespace ZeldaGame;

public class WallMasterEnemy : IEnemy
{
    private ISprite wallMasterSprite;
    private Vector2 position;
    private Vector2 previousPosition;
    private const int SideLength = 40; // how many steps Wall Master crawls before picking a new direction
    private int side = 0; // 0 = right, 1 = down, 2 = left, 3 = up
    private int stepsTaken = 0;
    private Random random = new Random();
    public event Action<IProjectile> ProjectileSpawned;
    public Rectangle BoundingBox => new Rectangle((int)position.X, (int)position.Y, (int)wallMasterSprite.Size.X, (int)wallMasterSprite.Size.Y);

    public WallMasterEnemy(Vector2 startPosition)
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
        
    }

    public void OnCollideWithObject(ICollidable target, CollisionSide side)
    {
        if (target is TileBlock tile && tile.IsCollidable)
        {
            position = previousPosition;

            this.side = random.Next(4);
            stepsTaken = 0;
        }
    }

    public void LoadContent()
    {
        wallMasterSprite = EnemySpriteFactory.Instance.CreateWallMasterSprite();
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
        }

        // Wall Master crawls around by picking a random direction every so often
        stepsTaken++;
        if (stepsTaken >= SideLength)
        {
            stepsTaken = 0;
            side = random.Next(4);
        }

        wallMasterSprite.Update(gameTime);
    }

    public void Draw(GameTime gameTime)
    {
        wallMasterSprite.Draw(gameTime, position);
    }
}
