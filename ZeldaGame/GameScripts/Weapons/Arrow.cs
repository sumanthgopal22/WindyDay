using Microsoft.Xna.Framework;
using MonoGameLibrary;
using ZeldaGame.GameScripts.Sprites;
using ZeldaGame.GameScripts.Tilemapping;

namespace ZeldaGame;

//Goriya and Link both use this class
public class Arrow : IProjectile
{
    private ISprite arrowSprite;
    private Vector2 position;
    private Vector2 startPosition;
    private Vector2 velocity;
    public bool hasHitWall = false;

    public bool isHostile { get; }

    public bool IsActive => !hasHitWall;

    public Rectangle BoundingBox => new Rectangle((int)position.X, (int)position.Y, (int)arrowSprite.Size.X, (int)arrowSprite.Size.Y);

    public Arrow(Vector2 startPosition, Vector2 velocity)
    {
        this.startPosition = startPosition;
        this.velocity = velocity;
        arrowSprite = ItemSpriteFactory.Instance.CreateWoodenArrowSprite();
        // startPosition is where the middle of the arrow should be, so shift back by half its size
        position = startPosition - arrowSprite.Size / 2;

        if (velocity.X > 0)
        {
            arrowSprite.SetDirection(SpriteDirection.Right);
        }
            
        else if (velocity.X < 0)
        {
            arrowSprite.SetDirection(SpriteDirection.Left);
        }
            
        else if (velocity.Y > 0)
        {
            arrowSprite.SetDirection(SpriteDirection.Down);
        }
            
        else
        {
            arrowSprite.SetDirection(SpriteDirection.Up);

        }

    }

    public void OnCollideWithObject(ICollidable target, CollisionSide side)
    {
        if (target is TileBlock tile && tile.IsCollidable)
        {
            hasHitWall = true;
        }
    }

    public void Update(GameTime gameTime)
    {
        position += velocity;
        if (position.Y >= Core.Instance.Window.ClientBounds.Height || position.X >= Core.Instance.Window.ClientBounds.Width || position.X < 0 || position.Y < 0)
        {
            hasHitWall = true;
        }

        arrowSprite.Update(gameTime);

    }

    public void Draw(GameTime gameTime)
    {
        arrowSprite.Draw(gameTime, position);
    }
}
