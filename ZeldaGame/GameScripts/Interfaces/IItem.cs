using Microsoft.Xna.Framework;

namespace ZeldaGame;

public interface IItem : IGameObject, ICollidable
{
    Vector2 Position { get; set; }
    Vector2 Size { get; }
    bool IsActive { get; set; }
}