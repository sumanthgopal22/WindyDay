using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ZeldaGame.GameScripts.Interfaces;

namespace ZeldaGame.GameScripts.StateMachine.GameStates
{
    public class ItemSelectState : IState
    {
        private Game1 game;
        private KeyboardState previousKeyboardState;

        public ItemSelectState(Game1 game)
        {
            this.game = game;
        }

        public void Enter()
        {
            System.Diagnostics.Debug.WriteLine("Entering ItemSelectState.");
            game.CurrentScreen = game.ItemSelectScreen;
            previousKeyboardState = Keyboard.GetState();
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();

            game.CurrentScreen.Update(gameTime);

            // Return to gameplay when pressing Enter, Tab, or I
            if (IsKeyPressed(currentKeyboardState, Keys.Enter) ||
                IsKeyPressed(currentKeyboardState, Keys.Tab) ||
                IsKeyPressed(currentKeyboardState, Keys.I))
            {
                game.ChangeState(new GameplayState(game));
            }

            previousKeyboardState = currentKeyboardState;
        }

        public void Exit()
        {
            System.Diagnostics.Debug.WriteLine("Exiting ItemSelectState.");
            game.CurrentScreen = null;
        }

        private bool IsKeyPressed(KeyboardState current, Keys key)
        {
            return current.IsKeyDown(key) && previousKeyboardState.IsKeyUp(key);
        }
    }
}