using Microsoft.Xna.Framework;
using MonoGameLibrary;
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

        // Clamp to prevent Link from being ejected off the screen or under the room
        int windowWidth = Core.Instance.Window.ClientBounds.Width;
        int windowHeight = Core.Instance.Window.ClientBounds.Height;
        float pWidth = player.BoundingBox.Width > 0 ? player.BoundingBox.Width : 64f;
        float pHeight = player.BoundingBox.Height > 0 ? player.BoundingBox.Height : 64f;

        correctedPosition.X = MathHelper.Clamp(correctedPosition.X, 0, windowWidth - pWidth);
        correctedPosition.Y = MathHelper.Clamp(correctedPosition.Y, 0, windowHeight - pHeight);

        player.Position = correctedPosition;
    }
}