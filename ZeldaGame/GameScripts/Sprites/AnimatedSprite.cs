using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;

namespace ZeldaGame;

public class AnimatedSprite : ISprite
{
    private Texture2D spriteTexture;
    private Rectangle[] sourceRectangles;
    private double frameDuration;
    private double elapsedTime;
    private int currentFrame; 
    private SpriteEffects[] frameEffects;

    public Vector2 Size { get; private set; }
    
    // Every frame uses the same effect (for example, all frames flipped or none flipped)
    public AnimatedSprite(Texture2D spriteTexture, Rectangle[] sourceRectangles, double frameDuration, SpriteEffects effects)
    {
        SpriteEffects[] sameEffectForEveryFrame = new SpriteEffects[sourceRectangles.Length];
        for (int i = 0; i < sameEffectForEveryFrame.Length; i++)
        {
            sameEffectForEveryFrame[i] = effects;
        }

        Initialize(spriteTexture, sourceRectangles, frameDuration, sameEffectForEveryFrame);
    }

    // No effect on any frame
    public AnimatedSprite(Texture2D spriteTexture, Rectangle[] sourceRectangles, double frameDuration)
    {
        SpriteEffects[] noEffectOnAnyFrame = new SpriteEffects[sourceRectangles.Length];

        Initialize(spriteTexture, sourceRectangles, frameDuration, noEffectOnAnyFrame);
    }

    // Allows each frame to have its own effect, alternates between normal and flipped frames
    public AnimatedSprite(Texture2D spriteTexture, Rectangle[] sourceRectangles, double frameDuration, SpriteEffects[] frameEffects)
    {
        Initialize(spriteTexture, sourceRectangles, frameDuration, frameEffects);
    }

    private void Initialize(Texture2D spriteTexture, Rectangle[] sourceRectangles, double frameDuration, SpriteEffects[] frameEffects)
    {
        this.spriteTexture = spriteTexture;
        this.sourceRectangles = sourceRectangles;
        this.frameDuration = frameDuration;
        this.frameEffects = frameEffects;

        elapsedTime = 0;
        currentFrame = 0;

        Size = new Vector2(sourceRectangles[0].Width, sourceRectangles[0].Height);
    }

    public void SetDirection(spriteDirection direction)
    {
    }

    public void Reset()
    {
        currentFrame = 0;
    }

    public void Draw(GameTime gameTime, Vector2 position)
    {
        Rectangle sourceRectangle = sourceRectangles[currentFrame];

        Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, sourceRectangles[0].Width * 3, sourceRectangles[0].Height * 3);

        Core.SpriteBatch.Draw(spriteTexture, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, frameEffects[currentFrame], 0f);
    }

    public void Update(GameTime gameTime)
    {
        elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

        if (elapsedTime > frameDuration)
        {
            elapsedTime -= frameDuration;
            currentFrame = (currentFrame + 1) % sourceRectangles.Length;
        }
    }
}