using Microsoft.Xna.Framework;
using ZeldaGame.GameScripts.Interfaces;

namespace ZeldaGame.GameScripts.StateMachine.LinkStates
{
    public class DamagedLinkState : IState
    {
        private readonly LinkStateMachine stateMachine;

        public DamagedLinkState(LinkStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public void Enter()
        {
            
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Exit()
        {
        }
    }
}
