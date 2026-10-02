using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;

namespace ZeldaGame;

public class Block : IBlock
{
    private BlockSprite block;
    private Texture2D spriteTexture;
    private Vector2 position;
    private int currentFrame;
    private double elapsedTime;


    public void LoadContent()
    {
        position = new Vector2(Core.Instance.Window.ClientBounds.Width, Core.Instance.Window.ClientBounds.Height) * 0.25f;
        spriteTexture = Core.Content.Load<Texture2D>("spritesheets/BlocksSheet");
        block = new BlockSprite(spriteTexture);
        currentFrame = 0;
        elapsedTime = 0;
    }

    public void Draw(GameTime gameTime)
    {
        block.SetFrame(currentFrame);
        block.Draw(gameTime, position);
    }

    public void Update(GameTime gameTime)
    {
        elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
        block.Update(gameTime);
    }

    public void NextBlock()
    {
        if (elapsedTime >= 0.1)
        {
            elapsedTime = 0;
            if (currentFrame < 32)
            {
                currentFrame++;
            
             } else
             {
                currentFrame = 0;
             }
        }
    }

    public void PreviousBlock()
    {
       if (elapsedTime >= 0.1)
        {
            elapsedTime = 0;
            if (currentFrame > 0)
            {
                currentFrame--;
            
             } else
             {
                currentFrame = 32;
             }
        }
    }
}