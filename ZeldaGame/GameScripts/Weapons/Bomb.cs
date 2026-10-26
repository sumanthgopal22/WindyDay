using Microsoft.Xna.Framework;

namespace ZeldaGame;

//Goriya and Link both use this class
public class Bomb : IProjectile
{
    private ISprite bombSprite;
    private Vector2 position;
    private const double elaspedBombTime = 2.0;
    private double elapsedTime = 0.0;
    public bool hasExploded;

    public bool isHostile => false;
    public bool IsActive => !hasExploded;
    public Rectangle BoundingBox => new Rectangle((int)position.X, (int)position.Y, (int)bombSprite.Size.X, (int)bombSprite.Size.Y);

    public Bomb(Vector2 position)
    {
        bombSprite = ItemSpriteFactory.Instance.CreateBombSprite();
        this.position = position - bombSprite.Size / 2;
        hasExploded = false;
    }

    public void OnCollideWithObject(ICollidable target, CollisionSide side)
    {
        
    }


    public void Update(GameTime gameTime)
    {
        elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
        if (elapsedTime >= elaspedBombTime)
        {
            hasExploded = true;
        }

        bombSprite.Update(gameTime);
    }

    public void Draw(GameTime gameTime)
    {
        bombSprite.Draw(gameTime, position);
    }


}