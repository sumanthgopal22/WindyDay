namespace Sprint0Game;

public class MoveDownCommand : ICommand
{
    private IPlayer player;

    public MoveDownCommand(IPlayer player)
    {
        this.player = player;
    }

    public void Execute()
    {
        player.MoveDown();
    }
}