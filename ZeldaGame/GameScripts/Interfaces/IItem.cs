using Microsoft.Xna.Framework;

namespace ZeldaGame;

public interface IItem
{
    Vector2 Position { get; set; }
    Vector2 Size { get; }

    void Draw(GameTime gameTime);

    void Update(GameTime gameTime);
}