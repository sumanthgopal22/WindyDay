using MonoGameLibrary;

namespace Sprint0Game;

public class QuitGameCommand : ICommand
{
    public void Execute()
    {
        Core.Instance.Exit();
    }
}