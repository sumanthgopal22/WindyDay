using Microsoft.Xna.Framework;

namespace ZeldaGame;

public enum CollisionSide { Top, Bottom, Left, Right, None }

public interface ICollidable
{
    Rectangle BoundingBox { get; }
}