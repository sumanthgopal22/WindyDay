using System.Collections.Generic;
using Microsoft.Xna.Framework;

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
    public event System.Action<IProjectile> ProjectileSpawned;
    public Rectangle BoundingBox => new Rectangle((int)position.X, (int)position.Y, (int)aquamentusSprite.Size.X, (int)aquamentusSprite.Size.Y);
    public bool IsCollidable { get; set; }
    public bool IsAlive { get; set; } = true;


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
        Vector2 mouthPosition = new Vector2(position.X, position.Y + 20);

        // Adding 3 fireballs like the game
        Vector2 upAndLeft = new Vector2(-2, -1);
        Vector2 straightLeft = new Vector2(-2, 0);
        Vector2 downAndLeft = new Vector2(-2, 1);

        fireballs.Add(new Fireball(mouthPosition, upAndLeft));
        fireballs.Add(new Fireball(mouthPosition, straightLeft));
        fireballs.Add(new Fireball(mouthPosition, downAndLeft));
    }

    public void OnCollideWithObject(ICollidable target, CollisionSide side)
    {
        if (target is IProjectile weapon)
        {
            IsAlive = false;
            IsCollidable = false;
        }
    }

    public void LoadContent()
    {
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
