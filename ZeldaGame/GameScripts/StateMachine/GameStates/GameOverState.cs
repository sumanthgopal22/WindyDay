using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ZeldaGame.GameScripts.Interfaces;

namespace ZeldaGame.GameScripts.StateMachine.GameStates
{
    public class GameOverState : IState
    {
        private Game1 game;

        public GameOverState(Game1 game)
        {
            this.game = game;
        }

        public void Enter()
        {
            System.Diagnostics.Debug.WriteLine("Entering GameOverState.");
            game.CurrentScreen = game.GameOverScreen;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            // lowkey placeholder, maybe replace with a timer,
            // like after 3 seconds it exits
            if (keyboardState.IsKeyDown(Keys.Space)) {
                Exit();
            }   //idk if this works rn
        }

        public void Exit()
        {
            System.Diagnostics.Debug.WriteLine("Exiting GameOverState.");
            game.CurrentScreen = null;
        }
    }
}
