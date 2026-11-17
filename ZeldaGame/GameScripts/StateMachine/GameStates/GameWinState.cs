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
    public class GameWinState : IState
    {
        private readonly Game1 game;
        private KeyboardState previousKeyboardState;

        public GameWinState(Game1 game)
        {
            this.game = game;
        }

        public void Enter()
        {
            System.Diagnostics.Debug.WriteLine("Entering GameWinState.");

            game.CurrentScreen = game.GameWinScreen;
            previousKeyboardState = Keyboard.GetState();
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();

            bool spacePressed =
                currentKeyboardState.IsKeyDown(Keys.Space) &&
                previousKeyboardState.IsKeyUp(Keys.Space);

            if (spacePressed)
            {
                game.ChangeState(new MainMenuState(game));
                return;
            }

            previousKeyboardState = currentKeyboardState;
        }

        public void Exit()
        {
            System.Diagnostics.Debug.WriteLine("Exiting GameWinState.");
            game.CurrentScreen = null;
        }
    }
}
