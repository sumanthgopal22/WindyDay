using Microsoft.Xna.Framework;
using MonoGameLibrary;

namespace ZeldaGame;

public class Fireball : IGameObject
{
    private ISprite fireballSprite;
    private Vector2 position;
    private Vector2 velocity; // how many pixels the fireball moves each frame
    public double framesAlive;

    public Fireball(Vector2 startPosition, Vector2 velocity)
    {
        position = startPosition;
        this.velocity = velocity;
        fireballSprite = EnemySpriteFactory.Instance.CreateFireballSprite();
    }

    public double elapsedFramesFireball()
    {
        return framesAlive;
    }

    public void Update(GameTime gameTime)
    {
        position += velocity;
        fireballSprite.Update(gameTime);
        framesAlive++;
    }

    public void Draw(GameTime gameTime)
    {
        fireballSprite.Draw(gameTime, position);
    }
}
