using Microsoft.Xna.Framework;
using ZeldaGame.GameScripts.Interfaces;

namespace ZeldaGame.GameScripts.StateMachine.LinkStates
{
    public class IdleLinkState : IState
    {
        private readonly LinkStateMachine stateMachine;

        public IdleLinkState(LinkStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public void Enter()
        {
            // Start idle animation
        }

        public void Update(GameTime gameTime)
        {
            // Update idle animation
        }

        public void Exit()
        {
            // Clean up idle state
        }
    }
}
