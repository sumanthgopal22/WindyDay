using System;
using Microsoft.Xna.Framework;
using MonoGameLibrary;
using ZeldaGame.GameScripts.Interfaces;
using ZeldaGame.GameScripts.Sprites;
using ZeldaGame.GameScripts.StateMachine;
using ZeldaGame.GameScripts.StateMachine.LinkStates;

namespace ZeldaGame;

public class Player : IPlayer
{
    public event Action<IProjectile> ProjectileSpawned;

    private IActionSprite sprite;
    private Vector2 position, movement;
    private LinkStateMachine stateMachine;

    public bool IsActionPlaying => sprite.IsActionPlaying;
    public bool IsIdle { get; set; }
    public bool IsWalking { get; set; }
    public bool IsUsingItem { get; set; }
    public bool IsSwingingSword { get; set; }

    public Rectangle BoundingBox => new Rectangle((int)position.X, (int)position.Y, (int)sprite.Size.X, (int)sprite.Size.Y);

    private SpriteDirection finalDirection;
    private Boomerang activeBoomerang;
    private Arrow activeArrow;
    private Bomb bomb;

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
        if (IsUsingItem || IsSwingingSword || (activeBoomerang != null && activeBoomerang.IsActive) || (activeArrow != null && activeArrow.IsActive))
        {
            return;
        }

        BoomerangThrow();

        ChangeState(new UsingItemLinkState(this));
        sprite.PlayAction(LinkSpriteAction.UseItem);
    }

    public void UseItem2()
    {
        if (IsUsingItem || IsSwingingSword || (activeBoomerang != null && activeBoomerang.IsActive) || (activeArrow != null && activeArrow.IsActive))
        {
            return;
        }

        FireArrow();

        ChangeState(new UsingItemLinkState(this));
        sprite.PlayAction(LinkSpriteAction.UseItem);
    }

    public void UseItem3()
    {
        if (IsUsingItem || IsSwingingSword || (activeBoomerang != null && activeBoomerang.IsActive) || (activeArrow != null && activeArrow.IsActive))
        {
            return;
        }

        PlaceBomb();

        ChangeState(new UsingItemLinkState(this));
        sprite.PlayAction(LinkSpriteAction.UseItem);
    }

    // Matches the original Zelda
    private const float BoomerangSpeed = 4 * 4;
    private const int BoomerangStepsOut = 128 / 4;

    public void BoomerangThrow()
    {
        Vector2 velocity = finalDirection switch
        {
            SpriteDirection.Right => new Vector2(BoomerangSpeed, 0),
            SpriteDirection.Down  => new Vector2(0, BoomerangSpeed),
            SpriteDirection.Left  => new Vector2(-BoomerangSpeed, 0),
            _                     => new Vector2(0, -BoomerangSpeed)
        };

        Vector2 throwPosition = new Vector2(position.X + 13, position.Y + 16);
        activeBoomerang = new Boomerang(throwPosition, velocity, BoomerangStepsOut, isHostile: false);
        ProjectileSpawned?.Invoke(activeBoomerang);
    }

    private const float ArrowSpeed = 4 * 4;

    public void FireArrow()
    {
        Vector2 velocity = finalDirection switch
        {
            SpriteDirection.Right => new Vector2(ArrowSpeed, 0),
            SpriteDirection.Down  => new Vector2(0, ArrowSpeed),
            SpriteDirection.Left  => new Vector2(-ArrowSpeed, 0),
            _                     => new Vector2(0, -ArrowSpeed)
        };

        Vector2 linkCenter = position + sprite.Size / 2;
        activeArrow = new Arrow(linkCenter, velocity);
        ProjectileSpawned?.Invoke(activeArrow);
    }

    public void PlaceBomb()
    {
        Vector2 linkCenter = position + sprite.Size / 2;
        bomb = new Bomb(linkCenter);

        // If Bomb implements IProjectile, route it into the room's collision pipeline
        if (bomb is IProjectile projectile)
        {
            ProjectileSpawned?.Invoke(projectile);
        }
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

        Vector2 nextPosition = position + movement;
        
        int windowWidth = Core.Instance.Window.ClientBounds.Width;
        int windowHeight = Core.Instance.Window.ClientBounds.Height;

        nextPosition.X = MathHelper.Clamp(nextPosition.X, 0, windowWidth - sprite.Size.X);
        nextPosition.Y = MathHelper.Clamp(nextPosition.Y, 0, windowHeight - sprite.Size.Y);

        position = nextPosition;

        if (movement == Vector2.Zero && IsWalking)
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