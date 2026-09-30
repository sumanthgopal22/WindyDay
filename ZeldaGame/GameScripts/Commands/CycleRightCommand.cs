namespace ZeldaGame;

public class CycleRightCommand : ICommand
{
    private Cycler cycler;
    public CycleRightCommand(Cycler cycler)
    {
        this.cycler = cycler;
    }

    public void Execute()
    {
        cycler.Next();
    }
}