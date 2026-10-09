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
    public bool IsIdle {  get; set; }
    public bool IsWalking { get; set; }

    public void MoveRight()
    {
        if (!IsWalking)
        {
            ChangeState(new WalkingLinkState(this));
        }

        movement = new Vector2(5f, 0f);
        sprite.SetDirection(SpriteDirection.Right);
    }

    public void MoveLeft()
    {
        if (!IsWalking)
        {
            ChangeState(new WalkingLinkState(this));
        }

        movement = new Vector2(-5f, 0f);
        sprite.SetDirection(SpriteDirection.Left);
    }

    public void MoveUp()
    {
        if (!IsWalking)
        {
            ChangeState(new WalkingLinkState(this));
        }

        movement = new Vector2(0f, -5f);
        sprite.SetDirection(SpriteDirection.Up);
    }

    public void MoveDown()
    {
        if (!IsWalking)
        {
            ChangeState(new WalkingLinkState(this));
        }

        movement = new Vector2(0f, 5f);
        sprite.SetDirection(SpriteDirection.Down);
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
        IsIdle = true;
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
        if (movement == Vector2.Zero && !(stateMachine.CurrentState is IdleLinkState))
        {
            ChangeState(new IdleLinkState(this));
        }

        movement = Vector2.Zero;
    }

    public void Draw(GameTime gameTime)
    {
        sprite.Draw(gameTime, position);
    }

    public void ChangeState(IState state)
    {
        stateMachine.ChangeState(state);
    }
}