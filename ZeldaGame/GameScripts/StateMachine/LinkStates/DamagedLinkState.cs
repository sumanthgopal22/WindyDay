using Microsoft.Xna.Framework;
using ZeldaGame.GameScripts.Interfaces;

namespace ZeldaGame.GameScripts.StateMachine.LinkStates
{
    public class DamagedLinkState : IState
    {
        private IPlayer link;

        public DamagedLinkState(IPlayer link)
        {
            this.link = link;
        }

        public void Enter()
        {
            System.Diagnostics.Debug.WriteLine("Entering DamagedLinkState.");
            link.IsDamaged = true;
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
            System.Diagnostics.Debug.WriteLine("Exiting DamagedLinkState.");
            link.IsDamaged = false;
        }
    }
}
