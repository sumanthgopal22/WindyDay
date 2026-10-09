using Microsoft.Xna.Framework;
using ZeldaGame.GameScripts.Interfaces;

namespace ZeldaGame.GameScripts.StateMachine.LinkStates
{
    public class AttackingLinkState : IState
    {
        private IPlayer link;

        public AttackingLinkState(IPlayer link)
        {
            this.link = link;
        }

        public void Enter()
        {
            System.Diagnostics.Debug.WriteLine("Entering AttackingLinkState.");
            link.SwingSword();
        }

        public void Update(GameTime gameTime)
        {
            link.UpdateSprite(gameTime);

            if (!link.IsActionPlaying)
            {
                link.ChangeState(new IdleLinkState(link));
            }
        }

        public void Exit()
        {
            System.Diagnostics.Debug.WriteLine("Exiting AttackingLinkState.");
        }
    }
}
