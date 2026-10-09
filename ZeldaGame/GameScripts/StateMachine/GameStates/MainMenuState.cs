using Microsoft.Xna.Framework;
using ZeldaGame.GameScripts.Interfaces;

namespace ZeldaGame.GameScripts.StateMachine.GameStates
{
    public class MainMenuState : IState
    {
        private Game1 game;

        public MainMenuState(Game1 game)
        {
            this.game = game;
        }

        public void Enter()
        {
            System.Diagnostics.Debug.WriteLine("Entering MainMenuState.");
            game.CurrentScreen = game.MainMenuScreen;
        }

        public void Update(GameTime gameTime)
        {
            game.CurrentScreen.Update(gameTime);
        }

        public void Exit()
        {
            System.Diagnostics.Debug.WriteLine("Exiting MainMenuState.");
            game.CurrentScreen = null;
        }
    }
}
