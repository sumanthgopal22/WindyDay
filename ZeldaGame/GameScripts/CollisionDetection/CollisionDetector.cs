using Microsoft.Xna.Framework;

namespace ZeldaGame;

public static class CollisionDetector
{
    public static bool CheckCollision(ICollidable first, ICollidable second, out CollisionSide side, out Rectangle overlap)
    {
        overlap = Rectangle.Intersect(first.BoundingBox, second.BoundingBox);
        if (overlap.IsEmpty)
        {
            side = CollisionSide.None;
            return false;
        }

        // Horizontal contact takes precedence when overlap width is narrower than overlap height
        if (overlap.Width < overlap.Height)
        {
            if (first.BoundingBox.Center.X < second.BoundingBox.Center.X)
            {
                side = CollisionSide.Right;
            }
            else
            {
                side = CollisionSide.Left;
            }
            
        }
        else
        {
            if (first.BoundingBox.Center.Y < second.BoundingBox.Center.Y)
            {
                side = CollisionSide.Bottom;
            }
            else
            {
                side = CollisionSide.Top;
            }
        }

        return true;
    }
}