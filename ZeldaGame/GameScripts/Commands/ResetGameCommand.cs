using Microsoft.Xna.Framework;
using MonoGameLibrary;

namespace ZeldaGame;

public class ResetGameCommand : ICommand
{
    Game1 game;
    public ResetGameCommand(Game1 game)
    {
        this.game = game;
    }
    public void Execute()
    {
        game.returnToMain();
    }
}