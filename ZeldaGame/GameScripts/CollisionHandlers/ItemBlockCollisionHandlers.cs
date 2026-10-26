using Microsoft.Xna.Framework;
using ZeldaGame.GameScripts.Tilemapping;

namespace ZeldaGame;

public static class ItemBlockCollisionHandler
{
    public static void Handle(IItem item, TileBlock block, CollisionSide side, Rectangle overlap)
    {
        if (!block.IsCollidable)
        {
            return;
        }

        // Shift item out of solid tile
        Vector2 correctedPosition = item.Position;

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

        item.Position = correctedPosition;

        // If the item has autonomous movement (e.g., FairyItem), reverse its velocity
        if (item is FairyItem fairy)
        {
            fairy.ReverseDirection(side);
        }
    }
}