namespace Sprint0Game;

public class TeleportCommand : ICommand
{
    private IPlayer player;
    private MouseController mouseController;

    public TeleportCommand(IPlayer player, MouseController mouseController)
    {
        this.player = player;
        this.mouseController = mouseController;
    }

    public void Execute()
    {
        player.Teleport(mouseController.TargetPosition);
    }
}