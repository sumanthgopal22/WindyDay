using Microsoft.Xna.Framework;
using MonoGameLibrary;

namespace ZeldaGame;

public class Fireball : IGameObject
{
    private ISprite fireballSprite;
    private Vector2 position;
    private Vector2 velocity; // how many pixels the fireball moves each frame

    public Fireball(Vector2 startPosition, Vector2 velocity)
    {
        position = startPosition;
        this.velocity = velocity;
        fireballSprite = EnemySpriteFactory.Instance.CreateFireballSprite();
    }

    // True once the fireball has completely left the window
    public bool IsOffScreen()
    {
        Rectangle window = Core.Instance.Window.ClientBounds;

        bool pastLeftEdge = position.X < -fireballSprite.Size.X * 3;
        bool pastRightEdge = position.X > window.Width;
        bool pastTopEdge = position.Y < -fireballSprite.Size.Y * 3;
        bool pastBottomEdge = position.Y > window.Height;

        return pastLeftEdge || pastRightEdge || pastTopEdge || pastBottomEdge;
    }

    public void Update(GameTime gameTime)
    {
        position += velocity;
        fireballSprite.Update(gameTime);
    }

    public void Draw(GameTime gameTime)
    {
        fireballSprite.Draw(gameTime, position);
    }
}
