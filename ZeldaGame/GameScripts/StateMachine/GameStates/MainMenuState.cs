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

        }

        public void Update(GameTime gameTime)
        {
            stateMachine.Game.MainMenu.Update(gameTime);
        }

        public void Exit()
        {
            
        }
    }
}
