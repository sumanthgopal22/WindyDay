using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeldaGame.GameScripts.Interfaces;

namespace ZeldaGame.GameScripts.StateMachine.GameStates
{
    public class PauseGameState : IState
    {
        private readonly Game1 game;
        private KeyboardState previousKeyboardState;

        public PauseGameState(Game1 game)
        {
            this.game = game;
        }

        public void Enter()
        {
            System.Diagnostics.Debug.WriteLine("Entering PauseGameState.");

            game.CurrentScreen = game.GameplayScreen;

            // So holding P doesn't immediately resume.
            previousKeyboardState = Keyboard.GetState();
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();

            bool pausePressed =
                currentKeyboardState.IsKeyDown(Keys.P) &&
                previousKeyboardState.IsKeyUp(Keys.P);

            if (pausePressed)
            {
                game.ChangeState(new GameplayState(game));
                return;
            }

            previousKeyboardState = currentKeyboardState;
        }

        public void Exit()
        {
            System.Diagnostics.Debug.WriteLine("Exiting PauseGameState.");
        }
    }
}
