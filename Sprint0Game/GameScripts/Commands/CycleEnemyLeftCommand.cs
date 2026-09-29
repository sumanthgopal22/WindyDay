namespace Sprint0Game;

public class CycleEnemyLeftCommand : ICommand
{
    private CycleEnemy cycler;
    public CycleEnemyLeftCommand(CycleEnemy cycler)
    {
        this.cycler = cycler;
    }

    public void Execute()
    {
        cycler.Previous();
    }
}