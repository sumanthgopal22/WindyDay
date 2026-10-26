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