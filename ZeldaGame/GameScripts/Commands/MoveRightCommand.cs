namespace ZeldaGame;

public class MoveRightCommand : ICommand
{
    private IPlayer player;
    private IEnemy enemy;

    public MoveRightCommand(IPlayer player)
    {
        this.player = player;
    }

    public MoveRightCommand(IEnemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        player.MoveRight();
    }

    public void ExecuteEnemy()
    {
        enemy.MoveRight();
    }
}