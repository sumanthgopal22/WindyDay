using Microsoft.Xna.Framework;

namespace ZeldaGame;

//Goriya and Link both use this class
public class Bomb : IProjectile
{
    private ISprite bombSprite;
    private ISprite explosionEffect;
    private Vector2 position;
    private const double elaspedBombTime = 2.0;
    private const double explosionTime = 0.3;
    private double elapsedTime = 0.0;
    public bool hasExploded;
    private bool isFinished;

    public bool isHostile => false;
    public bool IsActive => !isFinished;
    public Rectangle BoundingBox => new Rectangle((int)position.X, (int)position.Y, (int)bombSprite.Size.X, (int)bombSprite.Size.Y);

    public Bomb(Vector2 position)
    {
        bombSprite = ItemSpriteFactory.Instance.CreateBombSprite();
        explosionEffect = ItemSpriteFactory.Instance.CreateBombExplosionEffectSprite();
        this.position = position - bombSprite.Size / 2;
        hasExploded = false;
        isFinished = false;
    }

    public void OnCollideWithObject(ICollidable target, CollisionSide side)
    {
        
    }


    public void Update(GameTime gameTime)
    {
        elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
        if (elapsedTime >= elaspedBombTime && !hasExploded)
        {
            hasExploded = true;

            Vector2 bombCenter = position + bombSprite.Size /2;
            position = bombCenter - explosionEffect.Size / 2;
        }

        if (hasExploded == true)
        {
            explosionEffect.Update(gameTime);

            if (elapsedTime >= elaspedBombTime + explosionTime)
            {
                isFinished = true;
            }
        }
        else
        {
            bombSprite.Update(gameTime);

        }

    }

    public void Draw(GameTime gameTime)
    {
        if (hasExploded == true)
        {
            explosionEffect.Draw(gameTime, position);
        }
        else
        {
            bombSprite.Draw(gameTime, position);

        }
    }


}