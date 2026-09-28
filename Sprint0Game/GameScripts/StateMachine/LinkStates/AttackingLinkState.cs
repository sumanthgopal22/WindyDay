using Microsoft.Xna.Framework;
using Sprint0Game.GameScripts.Interfaces;

namespace Sprint0Game.GameScripts.StateMachine.LinkStates
{
    public class AttackingLinkState : IState
    {
        private readonly LinkStateMachine stateMachine;

        public AttackingLinkState(LinkStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public void Enter()
        {
            // Start attacking animation
        }

        public void Update(GameTime gameTime)
        {
            // Update attacking animation
        }

        public void Exit()
        {
            // Clean up attacking state
        }
    }
}
