using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ZeldaGame.GameScripts.Interfaces;

namespace ZeldaGame.GameScripts.StateMachine.GameStates
{
    public class GameOverState : IState
    {
        private readonly Game1 game;
        private KeyboardState previousKeyboardState;

        public GameOverState(Game1 game)
        {
            this.game = game;
        }

        public void Enter()
        {
            System.Diagnostics.Debug.WriteLine("Entering GameOverState.");

            game.CurrentScreen = game.GameOverScreen;
            previousKeyboardState = Keyboard.GetState();
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();

            bool spacePressed =
                currentKeyboardState.IsKeyDown(Keys.Space) &&
                previousKeyboardState.IsKeyUp(Keys.Space);

            bool rPressed =
                currentKeyboardState.IsKeyDown(Keys.R) &&
                previousKeyboardState.IsKeyUp(Keys.R);

            if (spacePressed)
            {
                game.ResetLevel();
                game.ChangeState(new GameplayState(game));
                return;
            }

            if (rPressed)
            {
                game.ChangeState(new MainMenuState(game));
                return;
            }

            previousKeyboardState = currentKeyboardState;
        }

        public void Exit()
        {
            System.Diagnostics.Debug.WriteLine("Exiting GameOverState.");
            game.CurrentScreen = null;
        }
    }
}
