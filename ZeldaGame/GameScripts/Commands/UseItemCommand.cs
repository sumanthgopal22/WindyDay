namespace ZeldaGame;

public class UseItemCommand : ICommand
{
    private readonly IPlayer player;
    private readonly ProjectileType projectileType;

    public UseItemCommand(IPlayer player, ProjectileType projectileType)
    {
        this.player = player;
        this.projectileType = projectileType;
    }

    public void Execute()
    {
        player.UseItem(projectileType);
    }
}
