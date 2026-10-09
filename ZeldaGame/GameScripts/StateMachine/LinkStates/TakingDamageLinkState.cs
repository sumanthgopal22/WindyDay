using Microsoft.Xna.Framework;

namespace ZeldaGame.GameScripts.StateMachine.LinkStates
{
    public class TakingDamageLinkState
    {
        private IPlayer link;

        public TakingDamageLinkState(IPlayer link)
        {
            this.link = link;
        }

        public void Enter()
        {
            System.Diagnostics.Debug.WriteLine("Entering TakingDamageLinkState.");
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
            System.Diagnostics.Debug.WriteLine("Exiting TakingDamageLinkState.");
        }
    }
}
