using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using ZeldaGame.GameScripts.Interfaces;
using ZeldaGame.GameScripts.Sprites;

namespace ZeldaGame;

public class LinkSprite : IActionSprite
{
    private readonly Texture2D spriteTexture;
    private readonly Rectangle[][] walkingFrames;
    private readonly Rectangle[] itemUseFrames;
    private readonly Rectangle[] swordSwingFrames;
    private readonly Vector2[] swordSwingOffsets;
    private readonly Rectangle pickedUpItemFrame;
    private readonly Rectangle[] damagedLinkFrames;
    private readonly float spriteScale;
    private readonly double frameDuration;
    private readonly double actionDuration;
    private readonly double pickUpDuration;
    private double damageDuration;
    private double elapsedTime;
    private const double DamageFlashFrameDuration = 0.05;
    private double actionElapsedTime;
    private int currentFrame;
    private SpriteDirection currentDirection;
    private LinkSpriteAction? currentAction;

    public Vector2 Size { get;}
    public bool IsActionPlaying => currentAction.HasValue;

    public LinkSprite(
        Texture2D spriteTexture,
        Rectangle[][] walkingFrames,
        Rectangle[] itemUseFrames,
        Rectangle[] swordSwingFrames,
        Vector2[] swordSwingOffsets,
        Rectangle pickedUpItemFrame,
        Rectangle[] damagedLinkFrames,
        float spriteScale,
        double frameDuration,
        double actionDuration,
        double pickUpDuration,
        double damageDuration)
    {
        this.spriteTexture = spriteTexture;
        this.walkingFrames = walkingFrames;
        this.itemUseFrames = itemUseFrames;
        this.swordSwingFrames = swordSwingFrames;
        this.swordSwingOffsets = swordSwingOffsets;
        this.pickedUpItemFrame = pickedUpItemFrame;
        this.damagedLinkFrames = damagedLinkFrames;
        this.spriteScale = spriteScale;
        this.frameDuration = frameDuration;
        this.actionDuration = actionDuration;
        this.pickUpDuration = pickUpDuration;
        this.damageDuration = damageDuration;
        Size = new Vector2(
            walkingFrames[0][0].Width * spriteScale,
            walkingFrames[0][0].Height * spriteScale);
        elapsedTime = 0;
        currentFrame = 0;
    }

    public void SetDirection(SpriteDirection direction)
    {
        currentDirection = direction;
    }

    public void PlayAction(LinkSpriteAction action)
    {
        currentAction = action;
        actionElapsedTime = 0;
        elapsedTime = 0;
        currentFrame = 0;
    }

    public void Reset()
    {
        currentFrame = 0;
        elapsedTime = 0;
    }

    private double GetCurrentActionDuration()
    {
        if (currentAction == LinkSpriteAction.PickUpItem)
        {
            return pickUpDuration;
        }

        if (currentAction == LinkSpriteAction.Damaged)
        {
            return damageDuration;
        }

        return actionDuration;
    }

    public void Draw(GameTime gameTime, Vector2 position)
    {
        int directionIndex = GetDirectionIndex(currentDirection);
        Rectangle sourceRectangle = currentAction switch
        {
            LinkSpriteAction.SwingSword => swordSwingFrames[directionIndex],
            LinkSpriteAction.UseItem => itemUseFrames[directionIndex],
            LinkSpriteAction.PickUpItem => pickedUpItemFrame,
            LinkSpriteAction.Damaged => damagedLinkFrames[currentFrame],
            _ => walkingFrames[directionIndex][currentFrame]
        };

        Rectangle destinationRectangle = new Rectangle(
            (int)position.X,
            (int)position.Y,
            (int)(sourceRectangle.Width * spriteScale),
            (int)(sourceRectangle.Height * spriteScale));
        if (currentAction.HasValue && currentAction.Value == LinkSpriteAction.SwingSword)
        {
            Vector2 offset = swordSwingOffsets[directionIndex];
            destinationRectangle.Offset((int)offset.X, (int)offset.Y);
        }

        //Mirror link
        SpriteEffects flip = SpriteEffects.None;
        if (currentDirection == SpriteDirection.Left && currentAction != LinkSpriteAction.PickUpItem)
        {
            flip = SpriteEffects.FlipHorizontally;
        }

        Core.SpriteBatch.Draw(
            spriteTexture,
            destinationRectangle,
            sourceRectangle,
            Color.White,
            rotation: 0f,          
            origin: Vector2.Zero,  
            effects: flip,         
            layerDepth: 0f);       
    }

    private static int GetDirectionIndex(SpriteDirection direction)
    {
        return direction switch
        {
            SpriteDirection.Down => 0,
            SpriteDirection.Up => 1,
            SpriteDirection.Left => 2,
            SpriteDirection.Right => 3,
            _ => 0
        };
    }

    public void Update(GameTime gameTime)
    {
        if (currentAction.HasValue)
        {
            actionElapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

            if (currentAction == LinkSpriteAction.Damaged)
            {
                elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
                if (elapsedTime > DamageFlashFrameDuration)
                {
                    elapsedTime -= DamageFlashFrameDuration;
                    currentFrame = (currentFrame + 1) % damagedLinkFrames.Length;
                }
            }

            if (actionElapsedTime >= GetCurrentActionDuration())
            {
                currentAction = null;
                actionElapsedTime = 0;
                currentFrame = 0;
                elapsedTime = 0;
            }

            return;
        }

        elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

        if (elapsedTime > frameDuration)
        {
            elapsedTime -= frameDuration;
            currentFrame++;
        }

        if (currentFrame > 1)
            currentFrame = 0;
    }
}