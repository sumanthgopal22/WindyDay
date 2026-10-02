using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaGame;

public class FairyItem : IItem
{
    private readonly ISprite sprite;
    public Vector2 Position { get; set; }
    public Vector2 Size => sprite.Size;

    public FairyItem(ISprite sprite, Vector2 initialPosition)
    {
        this.sprite = sprite;
        Position = initialPosition;
    }

    public void Update(GameTime gameTime)
    {
        // Autonomous item movement
        Position += new Vector2(0.5f, -0.5f);

        // Update item animation
        sprite.Update(gameTime);
    }

    public void Draw(GameTime gameTime)
    {
        // Draw item
        sprite.Draw(gameTime, Position);
    }
}