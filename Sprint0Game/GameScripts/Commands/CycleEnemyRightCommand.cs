namespace Sprint0Game;

public class CycleEnemyRightCommand : ICommand
{
    private CycleEnemy cycler;
    public CycleEnemyRightCommand(CycleEnemy cycler)
    {
        this.cycler = cycler;
    }

    public void Execute()
    {
        cycler.Next();
    }
}