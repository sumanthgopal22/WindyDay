namespace ZeldaGame;

public interface IEnemy : IGameObject
{
    void MoveRight();
    void MoveLeft();
    void MoveUp();
    void MoveDown();
    void Attack();
    void LoadContent();

}