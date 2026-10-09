using Microsoft.Xna.Framework;
using MonoGameLibrary;
using ZeldaGame.GameScripts.StateMachine.GameStates;

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
        game.ChangeState(new MainMenuState(game));
    }
}