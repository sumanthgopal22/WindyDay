using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;

namespace Sprint0Game;

public class Player : IPlayer
{
    private ISprite sprite;
    private Texture2D spriteTexture;
    private Vector2 position, movement;

    public void MoveRight()
    {
        movement = new Vector2(5f, 0f);
    }

    public void MoveLeft()
    {
        movement = new Vector2(-5f, 0f);
    }

    public void MoveUp()
    {
        movement = new Vector2(0f, -5f);
    }

    public void MoveDown()
    {
        movement = new Vector2(0f, 5f);
    }

    public void Teleport(Vector2 targetPosition)
    {
        int windowWidth = Core.Instance.Window.ClientBounds.Width;
        int windowHeight = Core.Instance.Window.ClientBounds.Height;

        targetPosition.X = MathHelper.Clamp(targetPosition.X, 0, windowWidth - sprite.Size.X);
        targetPosition.Y = MathHelper.Clamp(targetPosition.Y, 0, windowHeight - sprite.Size.Y);

        position = targetPosition;
    }

    public void LoadContent()
    {
        position = new Vector2(Core.Instance.Window.ClientBounds.Width, Core.Instance.Window.ClientBounds.Height) * 0.5f;
        spriteTexture = Core.Content.Load<Texture2D>("spritesheets/sonic");
        sprite = new Sprite(spriteTexture);
    }

    public void Draw(GameTime gameTime)
    {
        sprite.Draw(gameTime, position);
    }

    public void Update(GameTime gameTime)
    {
        Vector2 nextPosition = position + movement;
        
        int windowWidth = Core.Instance.Window.ClientBounds.Width;
        int windowHeight = Core.Instance.Window.ClientBounds.Height;

        nextPosition.X = MathHelper.Clamp(nextPosition.X, 0, windowWidth - sprite.Size.X);
        nextPosition.Y = MathHelper.Clamp(nextPosition.Y, 0, windowHeight - sprite.Size.Y);

        position = nextPosition;

        if (movement != Vector2.Zero)
            sprite.Update(gameTime);
        else
            sprite.Reset();
        
        movement = new Vector2(0f, 0f);
    }
}