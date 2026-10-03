using Microsoft.Xna.Framework;
using MonoGameLibrary;

namespace ZeldaGame;

public class WallMasterEnemy : IEnemy
{
    private ISprite wallMasterSprite;
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
        wallMasterSprite = EnemySpriteFactory.Instance.CreateWallMasterSprite();
    }

    public void Draw(GameTime gameTime)
    {
        wallMasterSprite.Draw(gameTime, position);
    }

    public void Update(GameTime gameTime)
    {
        wallMasterSprite.Update(gameTime);
    }
}