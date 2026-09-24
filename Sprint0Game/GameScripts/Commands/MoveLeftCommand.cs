namespace Sprint0Game;

public class MoveLeftCommand : ICommand
{
    private IPlayer player;

    public MoveLeftCommand(IPlayer player)
    {
        this.player = player;
    }

    public void Execute()
    {
        player.MoveLeft();
    }
}