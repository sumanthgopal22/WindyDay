using Microsoft.Xna.Framework;
using ZeldaGame.GameScripts.Interfaces;

namespace ZeldaGame;

public interface IPlayer : ICollidable
{
    bool IsActionPlaying { get; }
    bool IsIdle { get; set; }
    bool IsWalking { get; set; }
    bool IsUsingItem { get; set; }
    bool IsSwingingSword { get; set; }
    event System.Action<IProjectile> ProjectileSpawned;

    void MoveRight();

    void MoveLeft();

    void MoveUp();

    void MoveDown();

    void UseItem();

    void UseItem2();

    void SwingSword();

    void Teleport(Vector2 targetPosition);

    void UpdateSprite(GameTime gameTime);
    void ResetSprite();

    void LoadContent();

    void Draw(GameTime gameTime);

    void Update(GameTime gameTime);

    void ChangeState(IState state);
}