namespace ZeldaGame;

public class SwingSwordCommand : ICommand
{
    private readonly IPlayer player;

    public SwingSwordCommand(IPlayer player)
    {
        this.player = player;
    }

    public void Execute()
    {
        player.SwingSword();
    }
}
