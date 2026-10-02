namespace ZeldaGame;

public class CycleLeftCommand : ICommand
{
    private ICycler cycler;
    public CycleLeftCommand(ICycler cycler)
    {
        this.cycler = cycler;
    }

    public void Execute()
    {
        cycler.Previous();
    }
}