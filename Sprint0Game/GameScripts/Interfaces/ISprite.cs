using Microsoft.Xna.Framework;

namespace Sprint0Game;

public interface ISprite
{
    Vector2 Size { get; }

    void Reset();

    void Draw(GameTime gameTime, Vector2 position);

    void Update(GameTime gameTime);
}