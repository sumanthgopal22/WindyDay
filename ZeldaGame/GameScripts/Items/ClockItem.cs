using Microsoft.Xna.Framework;

namespace ZeldaGame;

public class ClockItem : IItem
{
    private readonly ISprite sprite;
    public Vector2 Position { get; set; }
    public Vector2 Size => sprite.Size;

    public ClockItem(ISprite sprite, Vector2 initialPosition)
    {
        this.sprite = sprite;
        Position = initialPosition;
    }

    public void Update(GameTime gameTime)
    {
    }

    public void Draw(GameTime gameTime)
    {
        // Draw item
        sprite.Draw(gameTime, Position);
    }
}