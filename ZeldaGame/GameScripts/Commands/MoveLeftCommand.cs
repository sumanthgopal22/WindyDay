namespace ZeldaGame;

public class MoveLeftCommand : ICommand
{
    private IPlayer player;
    private IEnemy enemy;

    public MoveLeftCommand(IPlayer player)
    {
        this.player = player;
    }

    public MoveLeftCommand(IEnemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        player.MoveLeft();
    }

    public void ExecuteEnemy()
    {
        enemy.MoveLeft();
    }

}