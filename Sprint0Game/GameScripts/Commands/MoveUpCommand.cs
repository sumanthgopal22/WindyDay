namespace Sprint0Game;

public class MoveUpCommand : ICommand
{
    private IPlayer player;

    public MoveUpCommand(IPlayer player)
    {
        this.player = player;
    }

    public void Execute()
    {
        player.MoveUp();
    }
}