using Microsoft.Xna.Framework;
using Sprint0Game.GameScripts.Interfaces;

namespace Sprint0Game.GameScripts.StateMachine.LinkStates
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
