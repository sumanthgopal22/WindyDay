using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using ZeldaGame.GameScripts.Interfaces;
using ZeldaGame.GameScripts.Sprites;
using ZeldaGame.GameScripts.StateMachine;
using ZeldaGame.GameScripts.StateMachine.LinkStates;

namespace ZeldaGame;

public class Player : IPlayer
{
    private IActionSprite sprite;
    private Texture2D spriteTexture;
    private Vector2 position, movement;
    private LinkStateMachine stateMachine;

    public bool IsActionPlaying => sprite.IsActionPlaying;

    public void MoveRight()
    {
        movement = new Vector2(5f, 0f);
        sprite.SetDirection(SpriteDirection.Right);

        if (!stateMachine.IsWalking)
        {
            stateMachine.ChangeState(new WalkingLinkState(stateMachine));
        }
    }

    public void MoveLeft()
    {
        movement = new Vector2(-5f, 0f);
        sprite.SetDirection(SpriteDirection.Left);

        if (!stateMachine.IsWalking)
        {
            stateMachine.ChangeState(new WalkingLinkState(stateMachine));
        }
    }

    public void MoveUp()
    {
        movement = new Vector2(0f, -5f);
        sprite.SetDirection(SpriteDirection.Up);

        if (!stateMachine.IsWalking)
        {
            stateMachine.ChangeState(new WalkingLinkState(stateMachine));
        }
    }

    public void MoveDown()
    {
        movement = new Vector2(0f, 5f);
        sprite.SetDirection(SpriteDirection.Down);

        if (!stateMachine.IsWalking)
        {
            stateMachine.ChangeState(new WalkingLinkState(stateMachine));
        }
    }

    public void UseItem()
    {
        sprite.PlayAction(LinkSpriteAction.UseItem);
    }

    public void SwingSword()
    {
        sprite.PlayAction(LinkSpriteAction.SwingSword);
    }

    public void Teleport(Vector2 targetPosition)
    {
        int windowWidth = Core.Instance.Window.ClientBounds.Width;
        int windowHeight = Core.Instance.Window.ClientBounds.Height;

        targetPosition.X = MathHelper.Clamp(targetPosition.X, 0, windowWidth - sprite.Size.X);
        targetPosition.Y = MathHelper.Clamp(targetPosition.Y, 0, windowHeight - sprite.Size.Y);

        position = targetPosition;
    }

    public void UpdateSprite(GameTime gameTime)
    {
        sprite.Update(gameTime);
    }

    public void ResetSprite()
    {
        sprite.Reset();
    }

    public void LoadContent()
    {
        position = new Vector2(Core.Instance.Window.ClientBounds.Width, Core.Instance.Window.ClientBounds.Height) * 0.5f;
        spriteTexture = Core.Content.Load<Texture2D>("spritesheets/Link");
        sprite = new LinkSprite(spriteTexture);
        stateMachine = new LinkStateMachine(this);
    }

    public void Update(GameTime gameTime)
    {
        stateMachine.Update(gameTime);

        Vector2 nextPosition = position + movement;
        
        int windowWidth = Core.Instance.Window.ClientBounds.Width;
        int windowHeight = Core.Instance.Window.ClientBounds.Height;

        nextPosition.X = MathHelper.Clamp(nextPosition.X, 0, windowWidth - sprite.Size.X);
        nextPosition.Y = MathHelper.Clamp(nextPosition.Y, 0, windowHeight - sprite.Size.Y);

        position = nextPosition;

        // If idle, change to idle state
        if (movement == Vector2.Zero && !stateMachine.IsIdle)
        {
            stateMachine.ChangeState(new IdleLinkState(stateMachine));
        }

        movement = Vector2.Zero;
    }

    public void Draw(GameTime gameTime)
    {
        sprite.Draw(gameTime, position);
    }
}