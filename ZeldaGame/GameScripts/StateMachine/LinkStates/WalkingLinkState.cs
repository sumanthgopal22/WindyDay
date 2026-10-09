using Microsoft.Xna.Framework;
using ZeldaGame.GameScripts.Interfaces;

namespace ZeldaGame.GameScripts.StateMachine.LinkStates
{
    public class WalkingLinkState : IState
    {
        private IPlayer link;

        public WalkingLinkState(IPlayer link)
        {
            this.link = link;
        }

        public void Enter()
        {
            System.Diagnostics.Debug.WriteLine("Entering WalkingLinkState.");
            link.IsWalking = true;
        }

        public void Update(GameTime gameTime)
        {
            link.UpdateSprite(gameTime);
        }

        public void Exit()
        {
            System.Diagnostics.Debug.WriteLine("Exiting WalkingLinkState.");
            link.IsWalking = false;
        }
    }
}
