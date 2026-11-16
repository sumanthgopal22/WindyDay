using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ZeldaGame.GameScripts.Interfaces;

namespace ZeldaGame.GameScripts.StateMachine.GameStates
{
    public class GameplayState : IState
    {
        private Game1 game;
        private KeyboardState previousKeyboardState;

        public GameplayState(Game1 game)
        {
            this.game = game;
        }

        public void Enter()
        {
            System.Diagnostics.Debug.WriteLine("Entering GameplayState.");
            game.CurrentScreen = game.GameplayScreen;
            previousKeyboardState = Keyboard.GetState();
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();

            // Open Item Selection screen on single key press
            if ((currentKeyboardState.IsKeyDown(Keys.Enter) && previousKeyboardState.IsKeyUp(Keys.Enter)) ||
                (currentKeyboardState.IsKeyDown(Keys.Tab) && previousKeyboardState.IsKeyUp(Keys.Tab)) ||
                (currentKeyboardState.IsKeyDown(Keys.I) && previousKeyboardState.IsKeyUp(Keys.I)))
            {
                game.ChangeState(new ItemSelectState(game));
                return;
            }

            // Update controllers and player when playing 
            foreach (IController controller in game.ControllerList)
            {
                controller.Update(gameTime);
            }

            game.CurrentScreen.Update(gameTime);
            previousKeyboardState = currentKeyboardState;
        }

        public void Exit()
        {
            System.Diagnostics.Debug.WriteLine("Exiting GameplayState.");
            game.CurrentScreen = null;
        }
    }
}
