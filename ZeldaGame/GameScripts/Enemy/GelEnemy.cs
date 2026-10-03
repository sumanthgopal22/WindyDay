using Microsoft.Xna.Framework;
using MonoGameLibrary;

namespace ZeldaGame;

public class GelEnemy : IEnemy
{
    private ISprite gelSprite;
    private Vector2 position, direction = Vector2.Zero;

    public void MoveRight()
    {
        direction.X += 1;
    }

    public void MoveLeft()
    {
        direction.X -= 1;
    }

    public void MoveUp()
    {
        direction.Y += 1;
    }

    public void MoveDown()
    {
        direction.Y -= 1;
    }
    
    public void LoadContent()
    {
        position = new Vector2(Core.Instance.Window.ClientBounds.Width, Core.Instance.Window.ClientBounds.Height) * 0.75f;
        gelSprite = EnemySpriteFactory.Instance.CreateGelSprite();
    }

    public void Update(GameTime gameTime)
    {
        gelSprite.Update(gameTime);
    }

    public void Draw(GameTime gameTime)
    {
        gelSprite.Draw(gameTime, position);
    }
    
}