using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ZeldaGame;

public interface IEnemy : IGameObject
{
    event System.Action<IProjectile> ProjectileSpawned;

    void MoveRight();
    void MoveLeft();
    void MoveUp();
    void MoveDown();
    void Attack();
    void LoadContent();
}