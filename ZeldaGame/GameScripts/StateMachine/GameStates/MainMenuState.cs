using Microsoft.Xna.Framework;
using ZeldaGame.GameScripts.Interfaces;

namespace ZeldaGame.GameScripts.StateMachine.GameStates
{
    public class MainMenuState : IState
    {
        private readonly GameStateMachine stateMachine;

        public MainMenuState(GameStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public void Enter()
        {
            System.Diagnostics.Debug.WriteLine("Entering MainMenuState.");
            stateMachine.Game.CurrentScreen = stateMachine.Game.MainMenuScreen;
        }

        public void Update(GameTime gameTime)
        {
            stateMachine.Game.CurrentScreen.Update(gameTime);
        }

        public void Exit()
        {
            System.Diagnostics.Debug.WriteLine("Exiting MainMenuState.");
            stateMachine.Game.CurrentScreen = null;
        }
    }
}
