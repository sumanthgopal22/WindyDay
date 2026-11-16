using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ZeldaGame.GameScripts.Interfaces;
using ZeldaGame.GameScripts.Screens;

namespace ZeldaGame.GameScripts.StateMachine.GameStates
{
    public class ItemSelectState : IState
    {
        private Game1 game;
        private ItemSelectScreen itemSelectScreen;
        private KeyboardState previousKeyboardState;

        public ItemSelectState(Game1 game)
        {
            this.game = game;
            itemSelectScreen = (ItemSelectScreen)game.ItemSelectScreen;
        }

        public void Enter()
        {
            System.Diagnostics.Debug.WriteLine("Entering ItemSelectState.");
            game.CurrentScreen = itemSelectScreen;
            itemSelectScreen.Open();
            previousKeyboardState = Keyboard.GetState();
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();

            itemSelectScreen.Update(gameTime);

            if (itemSelectScreen.IsClosed)
            {
                game.ChangeState(new GameplayState(game));
                return;
            }

            // Start closing when pressing Enter, Tab, or I (ignored while still sliding)
            if (itemSelectScreen.IsOpen &&
                (IsKeyPressed(currentKeyboardState, Keys.Enter) ||
                 IsKeyPressed(currentKeyboardState, Keys.Tab) ||
                 IsKeyPressed(currentKeyboardState, Keys.I)))
            {
                itemSelectScreen.Close();
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
