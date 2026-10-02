using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ZeldaGame;

public interface IEnemy : IGameObject
{
    void MoveRight();

    void MoveLeft();

    void MoveUp();

    void MoveDown();

    void LoadContent();
}