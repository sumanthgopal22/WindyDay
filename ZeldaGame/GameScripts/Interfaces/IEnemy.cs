namespace ZeldaGame;

public interface IEnemy : IGameObject, ICollidable
{
    event System.Action<IProjectile> ProjectileSpawned;
    bool IsCollidable { get; set; }
    bool IsAlive { get; set; }

    void MoveRight();
    void MoveLeft();
    void MoveUp();
    void MoveDown();
    void Attack();
    void OnCollideWithObject(ICollidable target, CollisionSide side);
    void LoadContent();
}