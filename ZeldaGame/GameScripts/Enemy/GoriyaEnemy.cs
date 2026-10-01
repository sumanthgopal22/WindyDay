using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;

namespace ZeldaGame;

public class GoriyaEnemy : IEnemy
{
    private ISprite goriyaSprite;
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
        spriteTexture = Core.Content.Load<Texture2D>("spritesheets/Dungeon_Enemies");
        goriyaSprite = new GoriyaSprite(spriteTexture);
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