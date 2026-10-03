using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;

namespace ZeldaGame;

public class BlockSprite : ISprite
{
    private Texture2D spriteTexture;
    private int currentFrame; 

    public Vector2 Size { get; }

    public BlockSprite(Texture2D spriteTexture)
    {
        this.spriteTexture = spriteTexture;
        currentFrame = 0;
        Size = new Vector2(80f, 80f);
    }

    public void SetDirection(spriteDirection direction)
    {
        // BlockSprite does not have directional animations, so this method is not needed.
    }

    public void Reset()
    {
        currentFrame = 0;
    }

    public void SetFrame(int frame)
    {
        currentFrame = frame;
    }

    public void Draw(GameTime gameTime, Vector2 position)
    {
        int x = (currentFrame % 9) * 16 - 1;
        int y = (currentFrame / 9) * 16;
        Rectangle sourceRectangle = new Rectangle(x,y,16,16);

        Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, 80,80);

        Core.SpriteBatch.Draw(spriteTexture, destinationRectangle, sourceRectangle, Color.White);
    }

    public void Update(GameTime gameTime)
    {
    }
}