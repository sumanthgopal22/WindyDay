using Microsoft.Xna.Framework;
using ZeldaGame.GameScripts.Interfaces;

namespace ZeldaGame.GameScripts.StateMachine.LinkStates
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
