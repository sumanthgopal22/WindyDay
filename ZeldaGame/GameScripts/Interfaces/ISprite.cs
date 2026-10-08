using Microsoft.Xna.Framework;
using ZeldaGame.GameScripts.Sprites;

namespace ZeldaGame;

public interface ISprite
{
    Vector2 Size { get; }

    void SetDirection(SpriteDirection direction);

    void Reset();

    void Draw(GameTime gameTime, Vector2 position);

    void Update(GameTime gameTime);
}