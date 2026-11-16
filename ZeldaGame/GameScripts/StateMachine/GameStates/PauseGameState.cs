using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame.GameScripts.StateMachine.GameStates
{
    public class PauseGameState
    {
        private Game1 game;

        public PauseGameState(Game1 game)
        {
            this.game = game;
        }

        public void Enter()
        {
            System.Diagnostics.Debug.WriteLine("Entering PauseGameState.");
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Exit()
        {
            System.Diagnostics.Debug.WriteLine("Exiting PauseGameState.");
        }
    }
}
