using System;
using Microsoft.Xna.Framework;
using MonoGameLibrary;

namespace ZeldaGame;

public class WallMasterEnemy : IEnemy
{
    private ISprite wallMasterSprite;
    private Vector2 position;
    private const int SideLength = 40; // how many steps Wall Master crawls before picking a new direction
    private int side = 0; // 0 = right, 1 = down, 2 = left, 3 = up
    private int stepsTaken = 0;
    private Random random = new Random();

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
        // Wall Master does not have a ranged attack, it grabs Link by crawling into him
    }

    public void LoadContent()
    {
        position = new Vector2(Core.Instance.Window.ClientBounds.Width, Core.Instance.Window.ClientBounds.Height) * 0.75f;
        wallMasterSprite = EnemySpriteFactory.Instance.CreateWallMasterSprite();
    }

    public void Update(GameTime gameTime)
    {
        switch (side)
        {
            case 0: MoveRight(); break;
            case 1: MoveDown(); break;
            case 2: MoveLeft(); break;
            case 3: MoveUp(); break;
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
