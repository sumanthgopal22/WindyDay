using Microsoft.Xna.Framework;
using ZeldaGame.GameScripts.Interfaces;

namespace ZeldaGame.GameScripts.StateMachine.LinkStates
{
    public class UsingItemLinkState : IState
    {
        private readonly LinkStateMachine stateMachine;

        public UsingItemLinkState(LinkStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public void Enter()
        {
            stateMachine.Link.UseItem();
        }

        public void Update(GameTime gameTime)
        {
            stateMachine.Link.UpdateSprite(gameTime);

            if (!stateMachine.Link.IsActionPlaying)
            {
                stateMachine.ChangeState(new IdleLinkState(stateMachine));
            }
        }

        public void Exit()
        {

        }
    }
}
