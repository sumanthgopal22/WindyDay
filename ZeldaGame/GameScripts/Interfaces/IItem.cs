using Microsoft.Xna.Framework;

namespace ZeldaGame;

public interface IItem : IGameObject
{
    Vector2 Position { get; set; }
    Vector2 Size { get; }
}