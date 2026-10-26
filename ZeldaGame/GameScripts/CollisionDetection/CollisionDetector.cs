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
            side = first.BoundingBox.Center.X < second.BoundingBox.Center.X 
                ? CollisionSide.Right 
                : CollisionSide.Left;
        }
        else
        {
            side = first.BoundingBox.Center.Y < second.BoundingBox.Center.Y 
                ? CollisionSide.Bottom 
                : CollisionSide.Top;
        }

        return true;
    }
}