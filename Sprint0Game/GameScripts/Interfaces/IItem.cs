using Microsoft.Xna.Framework;

namespace Sprint0Game;

public interface IItem
{
    Vector2 Position { get; set; }
    Vector2 Size { get; }

    void Draw(GameTime gameTime);

    void Update(GameTime gameTime);
}