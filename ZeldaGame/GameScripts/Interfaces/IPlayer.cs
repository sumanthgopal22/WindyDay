using Microsoft.Xna.Framework;

namespace ZeldaGame;

public interface IPlayer
{
    void MoveRight();

    void MoveLeft();

    void MoveUp();

    void MoveDown();

    void UseItem();

    void SwingSword();

    void Teleport(Vector2 targetPosition);

    void LoadContent();

    void Draw(GameTime gameTime);

    void Update(GameTime gameTime);
}