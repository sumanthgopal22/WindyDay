using Microsoft.Xna.Framework;

namespace Sprint0Game;

public interface IPlayer
{
    void MoveRight();

    void MoveLeft();

    void MoveUp();

    void MoveDown();

    void Teleport(Vector2 targetPosition);

    void LoadContent();

    void Draw(GameTime gameTime);

    void Update(GameTime gameTime);
}