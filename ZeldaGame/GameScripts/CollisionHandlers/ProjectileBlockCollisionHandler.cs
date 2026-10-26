using ZeldaGame.GameScripts.Tilemapping;

namespace ZeldaGame;

public static class ProjectileBlockCollisionHandler
{
    public static void Handle(IProjectile projectile, TileBlock block, CollisionSide side)
    {
        if (!block.IsCollidable) return;

        projectile.OnCollideWithObject(block, side);
    }
}