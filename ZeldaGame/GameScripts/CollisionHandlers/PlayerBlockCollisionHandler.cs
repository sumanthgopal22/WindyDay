using Microsoft.Xna.Framework;
using ZeldaGame.GameScripts.Tilemapping;

namespace ZeldaGame;

public static class PlayerBlockCollisionHandler
{
    public static void Handle(IPlayer player, TileBlock block, CollisionSide side, Rectangle overlap)
    {
        // Ignore walkable tiles such as open floors
        if (!block.IsCollidable)
        {
            return;
        }

        Vector2 correctedPosition = player.Position;

        switch (side)
        {
            case CollisionSide.Right:
                correctedPosition.X -= overlap.Width;
                break;

            case CollisionSide.Left:
                correctedPosition.X += overlap.Width;
                break;

            case CollisionSide.Bottom:
                correctedPosition.Y -= overlap.Height;
                break;

            case CollisionSide.Top:
                correctedPosition.Y += overlap.Height;
                break;
        }

        player.Position = correctedPosition;
    }
}