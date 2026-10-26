namespace ZeldaGame;

public class UseItem3Command : ICommand
{
    private readonly IPlayer player;

    public UseItem3Command(IPlayer player)
    {
        this.player = player;
    }

    public void Execute()
    {
        player.UseItem3();
    }
}