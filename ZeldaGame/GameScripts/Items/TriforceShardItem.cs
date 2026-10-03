using Microsoft.Xna.Framework;

namespace ZeldaGame;

public class TriforceShardItem : IItem
{
    private readonly ISprite sprite;
    public Vector2 Position { get; set; }
    public Vector2 Size => sprite.Size;

    public TriforceShardItem(ISprite sprite, Vector2 initialPosition)
    {
        this.sprite = sprite;
        Position = initialPosition;
    }

    public void Update(GameTime gameTime)
    {
        // Autonomous item movement
        // Position += new Vector2(0.5f, -0.5f);

        // Update item animation
        sprite.Update(gameTime);
    }

    public void Draw(GameTime gameTime)
    {
        // Draw item
        sprite.Draw(gameTime, Position);
    }
}