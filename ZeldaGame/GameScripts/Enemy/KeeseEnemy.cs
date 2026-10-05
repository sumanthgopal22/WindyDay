using System;
using Microsoft.Xna.Framework;
using MonoGameLibrary;

namespace ZeldaGame;

public class KeeseEnemy : IEnemy
{
    private ISprite keeseSprite;
    private Vector2 position;
    private const int SideLength = 40; // how many steps Keese flies before picking a new direction
    private int side = 0; // 0 = right, 1 = down, 2 = left, 3 = up, 4 = up-right, 5 = down-right, 6 = down-left, 7 = up-left
    private int stepsTaken = 0;
    private Random random = new Random();

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

    public void LoadContent()
    {
        position = new Vector2(Core.Instance.Window.ClientBounds.Width, Core.Instance.Window.ClientBounds.Height) * 0.75f;
        keeseSprite = EnemySpriteFactory.Instance.CreateKeeseSprite();
    }

    public void Update(GameTime gameTime)
    {
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

        // Like the original game, Keese flutters around by picking a random direction every so often
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
