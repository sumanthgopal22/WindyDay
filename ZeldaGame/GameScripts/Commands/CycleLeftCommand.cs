namespace ZeldaGame;

public class CycleLeftCommand : ICommand
{
    private Cycler cycler;
    public CycleLeftCommand(Cycler cycler)
    {
        this.cycler = cycler;
    }

    public void Execute()
    {
        cycler.Previous();
    }
}