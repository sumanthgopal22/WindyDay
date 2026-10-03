using Microsoft.Xna.Framework;

namespace ZeldaGame;

public enum spriteDirection
{
    Down,
    Up,
    Left,
    Right
}

public interface ISprite
{
    Vector2 Size { get; }

    void SetDirection(spriteDirection direction);

    void Reset();

    void Draw(GameTime gameTime, Vector2 position);

    void Update(GameTime gameTime);
}

public interface IActionSprite : ISprite
{
    void PlayAction(SpriteAction action);

    bool IsActionPlaying { get; }
}