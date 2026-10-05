using System;
using Microsoft.Xna.Framework;
using MonoGameLibrary;

namespace ZeldaGame;

public class StalfosEnemy : IEnemy
{
    private ISprite stalfosSprite;
    private Vector2 position;
    private const int StepsBeforeTurning = 60; // how many steps (pixels) Stalfos walks before picking a new direction
    private int direction = 0; // 0 = right, 1 = down, 2 = left, 3 = up
    private int stepsTaken = 0;
    private Random random = new Random();

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

    public void LoadContent()
    {
        position = new Vector2(Core.Instance.Window.ClientBounds.Width, Core.Instance.Window.ClientBounds.Height) * 0.75f;
        stalfosSprite = EnemySpriteFactory.Instance.CreateStalfosSprite();
    }

    public void Update(GameTime gameTime)
    {
        switch (direction)
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
            direction = random.Next(4);
        }

        stalfosSprite.Update(gameTime);
    }

    public void Draw(GameTime gameTime)
    {
        stalfosSprite.Draw(gameTime, position);
    }
}
