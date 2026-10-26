using ZeldaGame.GameScripts.Tilemapping;

namespace ZeldaGame;

public static class EnemyBlockTileCollisionHandler
{
    public static void Handle(IEnemy enemy, TileBlock block, CollisionSide side)
    {
        if (!block.IsCollidable) return;

        enemy.OnCollideWithObject(block, side);
    }
}