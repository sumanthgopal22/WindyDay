using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeldaGame.GameScripts.StateMachine.GameStates;

namespace ZeldaGame.GameScripts.Commands
{
    public class StartGameCommand : ICommand
    {
        private readonly Game1 _game;

        public StartGameCommand(Game1 game)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
        }

        public void Execute() => _game.GameStateMachine.ChangeState(new GameplayState(_game.GameStateMachine));
    }
}
