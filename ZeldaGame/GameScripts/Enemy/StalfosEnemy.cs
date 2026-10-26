using System;
using Microsoft.Xna.Framework;
using ZeldaGame.GameScripts.Tilemapping;

namespace ZeldaGame;

public class StalfosEnemy : IEnemy
{
    private ISprite stalfosSprite;
    private Vector2 position;
    private Vector2 previousPosition;
    private const int StepsBeforeTurning = 60; // how many steps (pixels) Stalfos walks before picking a new direction
    private int side = 0; // 0 = right, 1 = down, 2 = left, 3 = up
    private int stepsTaken = 0;
    private Random random = new Random();
    public event Action<IProjectile> ProjectileSpawned;
    public Rectangle BoundingBox => new Rectangle((int)position.X, (int)position.Y, (int)stalfosSprite.Size.X, (int)stalfosSprite.Size.Y);


    public StalfosEnemy(Vector2 startPosition)
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
        // Stalfos does not have a ranged attack, it only hurts Link by walking into him
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
        stalfosSprite = EnemySpriteFactory.Instance.CreateStalfosSprite();
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

        // Like the original game, Stalfos wanders by picking a random direction every so often
        stepsTaken++;
        if (stepsTaken >= StepsBeforeTurning)
        {
            stepsTaken = 0;
            side = random.Next(4);
        }

        stalfosSprite.Update(gameTime);
    }

    public void Draw(GameTime gameTime)
    {
        stalfosSprite.Draw(gameTime, position);
    }
}
