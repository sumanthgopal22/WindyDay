using ZeldaGame.GameScripts.Tilemapping;

namespace ZeldaGame;

public static class EnemyProjectileCollisionHandler
{
    public static void Handle(IProjectile projectile, IEnemy enemy, CollisionSide side)
    {
        projectile.OnCollideWithEnemy(enemy, side);
        enemy.OnCollideWithObject(projectile, side);
    }
}