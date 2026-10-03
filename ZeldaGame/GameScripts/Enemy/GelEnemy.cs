using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;

namespace ZeldaGame;

public class GelEnemy : IEnemy
{
    private ISprite gelSprite;
    private Texture2D spriteTexture;
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
        spriteTexture = Core.Content.Load<Texture2D>("spritesheets/Dungeon_Enemies");
        gelSprite = new GelSprite(spriteTexture);
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