using MonoGameLibrary;

namespace ZeldaGame;

public class QuitGameCommand : ICommand
{
    public void Execute()
    {
        Core.Instance.Exit();
    }
}