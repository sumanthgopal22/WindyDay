using ZeldaGame.GameScripts.Tilemapping;

namespace ZeldaGame;

public static class PlayerEnemyCollisionHandler
{
    public static void Handle(IPlayer player, IEnemy enemy, CollisionSide side)
    {
        player.OnCollideWithEnemy(enemy, side);
    }
}