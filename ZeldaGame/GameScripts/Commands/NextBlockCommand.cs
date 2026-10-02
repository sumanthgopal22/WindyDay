namespace ZeldaGame;

public class NextBlockCommand : ICommand
{
    private IBlock block;

    public NextBlockCommand(IBlock block)
    {
        this.block = block;
    }

    public void Execute()
    {
        block.NextBlock();
    }
}