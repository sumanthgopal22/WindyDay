using Microsoft.Xna.Framework;
using ZeldaGame.GameScripts.Interfaces;

namespace ZeldaGame.GameScripts.StateMachine.LinkStates
{
    public class WalkingLinkState : IState
    {
        private readonly LinkStateMachine stateMachine;

        public WalkingLinkState(LinkStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public void Enter()
        {
        }

        public void Update(GameTime gameTime)
        {
            stateMachine.Link.UpdateSprite(gameTime);
        }

        public void Exit()
        {
        }
    }
}
