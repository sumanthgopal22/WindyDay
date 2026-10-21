using Microsoft.Xna.Framework;
using MonoGameLibrary;
using ZeldaGame.GameScripts.Sprites;

namespace ZeldaGame;

//Goriya and Link both use this class
public class Arrow : IGameObject
{
    private ISprite arrowSprite;
    private Vector2 position;
    private Vector2 startPosition;
    private Vector2 velocity;
    public bool hasHitWall = false;

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

    public void Update(GameTime gameTime)
    {
        position += velocity;
        if (position.Y >= Core.Instance.Window.ClientBounds.Height || position.X >= Core.Instance.Window.ClientBounds.Width
         || position.X < 0 || position.Y < 0)
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
