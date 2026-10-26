using Microsoft.Xna.Framework;
using MonoGameLibrary;

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

        // Bounce within game window bounds
        int minX = 64, maxX = Core.Instance.Window.ClientBounds.Width - 64 - (int)Size.X;
        int minY = 64, maxY = Core.Instance.Window.ClientBounds.Height - 64 - (int)Size.Y;

        if (Position.X <= minX || Position.X >= maxX)
        {
            velocity.X = -velocity.X;
            Position = new Vector2(MathHelper.Clamp(Position.X, minX, maxX), Position.Y);
        }
        if (Position.Y <= minY || Position.Y >= maxY)
        {
            velocity.Y = -velocity.Y;
            Position = new Vector2(Position.X, MathHelper.Clamp(Position.Y, minY, maxY));
        }

        // Update item animation
        sprite.Update(gameTime);
    }

    public void Draw(GameTime gameTime)
    {
        // Draw item
        sprite.Draw(gameTime, Position);
    }
}