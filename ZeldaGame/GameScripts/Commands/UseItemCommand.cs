namespace ZeldaGame;

public class UseItemCommand : ICommand
{
    private readonly IPlayer player;
    private readonly WeaponType weaponType;

    public UseItemCommand(IPlayer player, WeaponType weaponType)
    {
        this.player = player;
        this.weaponType = weaponType;
    }

    public void Execute()
    {
        player.UseItem(weaponType);
    }
}
