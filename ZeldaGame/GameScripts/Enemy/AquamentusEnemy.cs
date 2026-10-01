using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;

namespace ZeldaGame;

public class AquamentusEnemy : IEnemy
{
    private ISprite aquamentusSprite;
    private Texture2D spriteTexture;
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
        spriteTexture = Core.Content.Load<Texture2D>("spritesheets/Boss_Enemies");
        aquamentusSprite = new AquamentusSprite(spriteTexture);
    }

    public void Draw(GameTime gameTime)
    {
        aquamentusSprite.Draw(gameTime, position);
    }

    public void Update(GameTime gameTime)
    {
        aquamentusSprite.Update(gameTime);
    }
}