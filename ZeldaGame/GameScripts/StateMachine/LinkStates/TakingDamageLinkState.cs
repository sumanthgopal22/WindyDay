using Microsoft.Xna.Framework;

namespace ZeldaGame.GameScripts.StateMachine.LinkStates
{
    public class TakingDamageLinkState
    {
        private readonly LinkStateMachine stateMachine;

        public TakingDamageLinkState(LinkStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public void Enter()
        {
            //TODO: Something like Link.TakeDamage();
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
