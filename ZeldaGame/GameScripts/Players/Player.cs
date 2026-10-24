using Microsoft.Xna.Framework;
using MonoGameLibrary;
using ZeldaGame.GameScripts.Interfaces;
using ZeldaGame.GameScripts.Sprites;
using ZeldaGame.GameScripts.StateMachine;
using ZeldaGame.GameScripts.StateMachine.LinkStates;

namespace ZeldaGame;

public class Player : IPlayer
{
    private IActionSprite sprite;
    private Vector2 position, movement;
    private LinkStateMachine stateMachine;

    public bool IsActionPlaying => sprite.IsActionPlaying;
    public bool IsIdle {  get; set; }
    public bool IsWalking { get; set; }
    public bool IsUsingItem { get; set; }
    public bool IsSwingingSword { get; set; }

    SpriteDirection finalDirection;
    Boomerang boomerang;
    Arrow arrow;

    public void MoveRight()
    {
        if (IsUsingItem || IsSwingingSword)
        {
            return;
        }
        if (!IsWalking)
        {
            ChangeState(new WalkingLinkState(this));
        }

        finalDirection = SpriteDirection.Right;
        movement = new Vector2(4f, 0f);
        sprite.SetDirection(finalDirection);
    }

    public void MoveLeft()
    {
        if (IsUsingItem || IsSwingingSword)
        {
            return;
        }
        if (!IsWalking)
        {
            ChangeState(new WalkingLinkState(this));
        }

        finalDirection = SpriteDirection.Left;
        movement = new Vector2(-4f, 0f);
        sprite.SetDirection(finalDirection);
    }

    public void MoveUp()
    {
        if (IsUsingItem || IsSwingingSword)
        {
            return;
        }
        if (!IsWalking)
        {
            ChangeState(new WalkingLinkState(this));
        }

        finalDirection = SpriteDirection.Up;
        movement = new Vector2(0f, -4f);
        sprite.SetDirection(finalDirection);
    }

    public void MoveDown()
    {
        if (IsUsingItem || IsSwingingSword)
        {
            return;
        }
        if (!IsWalking)
        {
            ChangeState(new WalkingLinkState(this));
        }

        finalDirection = SpriteDirection.Down;
        movement = new Vector2(0f, 4f);
        sprite.SetDirection(finalDirection);
    }

    public void UseItem()
    {
        if (IsUsingItem || IsSwingingSword || boomerang != null || arrow != null)
        {
            return;
        }

        BoomerangThrow();

        ChangeState(new UsingItemLinkState(this));
        sprite.PlayAction(LinkSpriteAction.UseItem);
    }

    public void UseItem2()
    {
        if (IsUsingItem || IsSwingingSword || boomerang != null || arrow != null)
        {
            return;
        }

        FireArrow();

        ChangeState(new UsingItemLinkState(this));
        sprite.PlayAction(LinkSpriteAction.UseItem);
    }

    // Matches the original Zelda
    private const float BoomerangSpeed = 4 * 4;
    private const int BoomerangStepsOut = 128 / 4;

    public void BoomerangThrow()
    {
        Vector2 velocity;
        switch (finalDirection)
        {
            case SpriteDirection.Right:
                velocity = new Vector2(BoomerangSpeed, 0);
                break;  // right
            case SpriteDirection.Down:
                velocity = new Vector2(0, BoomerangSpeed);
                break;  // down
            case SpriteDirection.Left:
                velocity = new Vector2(-BoomerangSpeed, 0);
                break; // left
            default:
                velocity = new Vector2(0, -BoomerangSpeed);
                break; // up
        }

        Vector2 throwPosition = new Vector2(position.X + 13, position.Y + 16);

        boomerang = new Boomerang(throwPosition, velocity, BoomerangStepsOut);

    }

    private const float ArrowSpeed = 4 * 4;
    public void FireArrow()
    {
        Vector2 velocity;
        switch (finalDirection)
        {
            case SpriteDirection.Right:
                velocity = new Vector2(ArrowSpeed, 0);
                break;  // right
            case SpriteDirection.Down:
                velocity = new Vector2(0, ArrowSpeed);
                break;  // down
            case SpriteDirection.Left:
                velocity = new Vector2(-ArrowSpeed, 0);
                break; // left
            default:
                velocity = new Vector2(0, -ArrowSpeed);
                break; // up
        }

        // Fire the arrow from the middle of Link
        Vector2 linkCenter = position + sprite.Size / 2;

        arrow = new Arrow(linkCenter, velocity);
    }

    public void SwingSword()
    {
        if (IsUsingItem || IsSwingingSword)
        {
            return;
        }
        
        ChangeState(new AttackingLinkState(this));
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
        sprite = LinkSpriteFactory.Instance.CreateLinkSprite();
        stateMachine = new LinkStateMachine(this);
        IsIdle = true;
    }

    public void Update(GameTime gameTime)
    {
        stateMachine.Update(gameTime);

         if (boomerang != null)
        {
            boomerang.Update(gameTime);

            if (boomerang.HasReturned)
            {
                boomerang = null;
            }
        }

        if (arrow != null)
        {
            arrow.Update(gameTime);

            if (arrow.hasHitWall == true)
            {
                arrow = null;
            }
        }

        Vector2 nextPosition = position + movement;
        
        int windowWidth = Core.Instance.Window.ClientBounds.Width;
        int windowHeight = Core.Instance.Window.ClientBounds.Height;

        nextPosition.X = MathHelper.Clamp(nextPosition.X, 0, windowWidth - sprite.Size.X);
        nextPosition.Y = MathHelper.Clamp(nextPosition.Y, 0, windowHeight - sprite.Size.Y);

        position = nextPosition;

        // If idle, change to idle state
        if (movement == Vector2.Zero && IsWalking)
        {
            ChangeState(new IdleLinkState(this));
        }

        movement = Vector2.Zero;
    }

    public void Draw(GameTime gameTime)
    {
        sprite.Draw(gameTime, position);

        if (boomerang != null)
        {
            boomerang.Draw(gameTime);
        }

        if (arrow != null)
        {
            arrow.Draw(gameTime);
        }
    }

    public void ChangeState(IState state)
    {
        stateMachine.ChangeState(state);
    }
}