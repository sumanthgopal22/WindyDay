namespace ZeldaGame;

public class UseItem2Command : ICommand
{
    private readonly IPlayer player;

    public UseItem2Command(IPlayer player)
    {
        this.player = player;
    }

    public void Execute()
    {
        player.UseItem2();
    }
}
