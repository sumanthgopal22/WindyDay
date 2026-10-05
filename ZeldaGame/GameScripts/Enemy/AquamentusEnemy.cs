using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGameLibrary;

namespace ZeldaGame;

public class AquamentusEnemy : IEnemy
{
    private ISprite aquamentusSprite;
    private Vector2 position;
    private const int SideLength = 100; // how many steps (pixels) Aquamentus moves before going opposite direction
    private int side = 0; // 0 = right and 1 = left
    private int stepsTaken = 0;
    private List<Fireball> fireballs = new List<Fireball>();
    private const double SecondsBetweenAttacks = 2.0;
    private double secondsSinceLastAttack = 0;
    public AquamentusEnemy(Vector2 startPosition)
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
    }

    public void MoveDown()
    {
    }

    // Shoots three fireballs to the left: one angled up, one straight, and one angled down
    public void Attack()
    {
        // Aquamentus faces left, so the fireballs start near its mouth on the left side of the sprite
        Vector2 mouthPosition = new Vector2(position.X, position.Y + 20);

        // Adding 3 fireballs like the game
        Vector2 upAndLeft = new Vector2(-2, -1);
        Vector2 straightLeft = new Vector2(-2, 0);
        Vector2 downAndLeft = new Vector2(-2, 1);

        // Adding to the list
        fireballs.Add(new Fireball(mouthPosition, upAndLeft));
        fireballs.Add(new Fireball(mouthPosition, straightLeft));
        fireballs.Add(new Fireball(mouthPosition, downAndLeft));
    }

    public void LoadContent()
    {
        position = new Vector2(Core.Instance.Window.ClientBounds.Width, Core.Instance.Window.ClientBounds.Height) * 0.75f;
        aquamentusSprite = EnemySpriteFactory.Instance.CreateAquamentusSprite();
    }

    public void Update(GameTime gameTime)
    {
        switch (side)
        {
            case 0:
                MoveRight();
                break;
            case 1:
                MoveLeft();
                break;
        }

        stepsTaken++;
        if (stepsTaken >= SideLength)
        {
            stepsTaken = 0;
            side = (side + 1) % 2;
        }

        // Attack every few seconds
        secondsSinceLastAttack += gameTime.ElapsedGameTime.TotalSeconds;
        if (secondsSinceLastAttack >= SecondsBetweenAttacks)
        {
            secondsSinceLastAttack = 0;
            Attack();
        }

        UpdateFireballs(gameTime);

        aquamentusSprite.Update(gameTime);
    }

    private void UpdateFireballs(GameTime gameTime)
    {
        // Go through the list backwards so removing a fireball does not skip the next one
        for (int i = fireballs.Count - 1; i >= 0; i--)
        {
            Fireball fireball = fireballs[i];
            fireball.Update(gameTime);

            if (fireball.elapsedFramesFireball() > 500)
            {
                fireballs.RemoveAt(i);
            }
        }
    }

    public void Draw(GameTime gameTime)
    {
        aquamentusSprite.Draw(gameTime, position);

        foreach (Fireball fireball in fireballs)
        {
            fireball.Draw(gameTime);
        }
    }
}
