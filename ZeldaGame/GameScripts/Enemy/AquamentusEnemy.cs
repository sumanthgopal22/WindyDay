using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;

namespace ZeldaGame;

public class AquamentusEnemy : IEnemy
{
    private ISprite aquamentusSprite;
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
        movement = new Vector2(0f, 5f);

    }

    public void MoveDown()
    {
        movement = new Vector2(0f, -5f);

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