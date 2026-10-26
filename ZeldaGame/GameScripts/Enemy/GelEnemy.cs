using System;
using Microsoft.Xna.Framework;
using ZeldaGame.GameScripts.Tilemapping;

namespace ZeldaGame;

public class GelEnemy : IEnemy
{
    private ISprite gelSprite;
    private Vector2 position;
    private Vector2 previousPosition;
    private const int SideLength = 80; // pixels per side of the square basically the max amount of steps sprite can take before changing directions
    private int side = 0; // 0 = right, 1 = down, 2 = left, 3 = up
    private int stepsTaken = 0;
    private Random random = new Random();
    public event System.Action<IProjectile> ProjectileSpawned;
    public Rectangle BoundingBox => new Rectangle((int)position.X, (int)position.Y, (int)gelSprite.Size.X, (int)gelSprite.Size.Y);

    public GelEnemy(Vector2 startPostion)
    {
        position = startPostion;

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
        gelSprite = EnemySpriteFactory.Instance.CreateGelSprite();
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

        stepsTaken++;
        if (stepsTaken >= SideLength)
        {
            stepsTaken = 0;
            side = random.Next(4);
        }
        gelSprite.Update(gameTime);

    }

    public void Draw(GameTime gameTime)
    {
        gelSprite.Draw(gameTime, position);
    }

}