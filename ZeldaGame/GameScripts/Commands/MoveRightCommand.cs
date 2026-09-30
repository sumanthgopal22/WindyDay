namespace ZeldaGame;

public class MoveRightCommand : ICommand
{
    private IPlayer player;

    public MoveRightCommand(IPlayer player)
    {
        this.player = player;
    }

    public void Execute()
    {
        player.MoveRight();
    }
}