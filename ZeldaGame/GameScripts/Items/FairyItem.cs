using Microsoft.Xna.Framework;

namespace ZeldaGame;

public class FairyItem : IItem
{
    private readonly ISprite sprite;
    private Vector2 velocity = new Vector2(1.5f, -1.5f); // Diagonal flutter
    public Vector2 Position { get; set; }
    public Vector2 Size => sprite.Size;
    public bool IsActive { get; set; } = true;
    public Rectangle BoundingBox => new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);

    public FairyItem(ISprite sprite, Vector2 initialPosition)
    {
        this.sprite = sprite;
        Position = initialPosition;
    }

    public void ReverseDirection(CollisionSide side)
    {
        switch (side)
        {
            case CollisionSide.Left:
            case CollisionSide.Right:
                velocity.X = -velocity.X;
                break;

            case CollisionSide.Top:
            case CollisionSide.Bottom:
                velocity.Y = -velocity.Y;
                break;
        }
    }

    public void Update(GameTime gameTime)
    {
        // Autonomous item movement
        Position += velocity;

        // Update item animation
        sprite.Update(gameTime);
    }

    public void Draw(GameTime gameTime)
    {
        // Draw item
        sprite.Draw(gameTime, Position);
    }
}