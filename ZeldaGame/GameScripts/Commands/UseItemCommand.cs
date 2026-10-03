namespace ZeldaGame;

public class UseItemCommand : ICommand
{
    private readonly IPlayer player;

    public UseItemCommand(IPlayer player)
    {
        this.player = player;
    }

    public void Execute()
    {
        player.UseItem();
    }
}
