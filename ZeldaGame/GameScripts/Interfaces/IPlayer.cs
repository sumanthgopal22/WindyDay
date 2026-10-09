using Microsoft.Xna.Framework;
using ZeldaGame.GameScripts.Interfaces;

namespace ZeldaGame;

public interface IPlayer
{
    bool IsActionPlaying { get; }
    bool IsIdle { get; set; }
    bool IsWalking { get; set; }
    void MoveRight();

    void MoveLeft();

    void MoveUp();

    void MoveDown();

    void UseItem();

    void SwingSword();

    void Teleport(Vector2 targetPosition);

    void UpdateSprite(GameTime gameTime);
    void ResetSprite();

    void LoadContent();

    void Draw(GameTime gameTime);

    void Update(GameTime gameTime);
    void ChangeState(IState state);
}