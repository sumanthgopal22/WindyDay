namespace ZeldaGame;

public interface IProjectile : ICollidable, IGameObject
{
    bool IsActive { get; }
    bool isHostile { get; }

    void OnCollideWithObject(ICollidable target, CollisionSide side);
    void OnCollideWithEnemy(ICollidable target, CollisionSide side);

}