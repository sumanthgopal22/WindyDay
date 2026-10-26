using Microsoft.Xna.Framework;
using ZeldaGame.GameScripts.Interfaces;

namespace ZeldaGame.GameScripts.StateMachine.GameStates
{
    public class GameplayState : IState
    {
        private Game1 game;

        public GameplayState(Game1 game)
        {
            this.game = game;
        }

        public void Enter()
        {
            System.Diagnostics.Debug.WriteLine("Entering GameplayState.");
            game.CurrentScreen = game.GameplayScreen;
        }

        public void Update(GameTime gameTime)
        {
            // Update controllers and player when playing 
            foreach (IController controller in game.ControllerList)
            {
                controller.Update(gameTime);
            }

            game.Player.Update(gameTime);
            game.CurrentScreen.Update(gameTime);
        }

        public void Exit()
        {
            System.Diagnostics.Debug.WriteLine("Exiting GameplayState.");
            game.CurrentScreen = null;
        }
    }
}
