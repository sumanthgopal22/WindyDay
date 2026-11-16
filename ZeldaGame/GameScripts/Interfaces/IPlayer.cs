using System;
using Microsoft.Xna.Framework;
using ZeldaGame.GameScripts.Interfaces;

namespace ZeldaGame;

public interface IPlayer : ICollidable
{
    event Action<IProjectile> ProjectileSpawned;

    bool IsActionPlaying { get; }
    bool IsIdle { get; set; }
    bool IsWalking { get; set; }
    bool IsUsingItem { get; set; }
    bool IsSwingingSword { get; set; }
    bool IsDamaged { get; set; }
    bool IsDead { get; set; }
    bool IsDeathAnimationComplete { get; }
    int Health { get; set; }
    int MaxHealth { get; }

    new Rectangle BoundingBox { get; }
    Vector2 Position { get; set; }
    
    void CancelMovement();
    void MoveRight();
    void MoveLeft();
    void MoveUp();
    void MoveDown();
    void UseItem(ProjectileType projectileType);
    void PickUpItem(IItem item);
    void DropHeldItem();

    void SwingSword();
    void UpdateSprite(GameTime gameTime);
    void ResetSprite();
    void LoadContent();
    void Draw(GameTime gameTime);
    void Update(GameTime gameTime);
    void ChangeState(IState state);
    void OnCollideWithEnemy(ICollidable target, CollisionSide side);
    void Reset();
}