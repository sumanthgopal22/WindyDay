using Microsoft.Xna.Framework;
using MonoGameLibrary;

namespace ZeldaGame;

public class KeeseEnemy : IEnemy
{
    private ISprite gelSprite;
    private Vector2 position;
    public void MoveRight()
    {
        
    }

    public void MoveLeft()
    {
        
    }

    public void MoveUp()
    {
        
    }

    public void MoveDown()
    {
        
    }

    public void LoadContent()
    {
        position = new Vector2(Core.Instance.Window.ClientBounds.Width, Core.Instance.Window.ClientBounds.Height) * 0.75f;
        gelSprite = EnemySpriteFactory.Instance.CreateKeeseSprite();
    }

    public void Draw(GameTime gameTime)
    {
        gelSprite.Draw(gameTime, position);
    }

    public void Update(GameTime gameTime)
    {
        gelSprite.Update(gameTime);
    }
}