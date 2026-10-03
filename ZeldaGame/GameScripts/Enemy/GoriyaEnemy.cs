using Microsoft.Xna.Framework;
using MonoGameLibrary;

namespace ZeldaGame;

public class GoriyaEnemy : IEnemy
{
    private ISprite goriyaSprite;
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
        goriyaSprite = EnemySpriteFactory.Instance.CreateGoriyaSprite();
    }

    public void Draw(GameTime gameTime)
    {
        goriyaSprite.Draw(gameTime, position);
    }

    public void Update(GameTime gameTime)
    {
        goriyaSprite.Update(gameTime);
    }
}