namespace ZeldaGame;

public class CycleRightCommand : ICommand
{
    private ICycler cycler;
    public CycleRightCommand(ICycler cycler)
    {
        this.cycler = cycler;
    }

    public void Execute()
    {
        cycler.Next();
    }
}