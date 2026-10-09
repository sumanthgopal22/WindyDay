using Microsoft.Xna.Framework;
using ZeldaGame.GameScripts.Interfaces;

namespace ZeldaGame.GameScripts.StateMachine.LinkStates
{
    public class IdleLinkState : IState
    {
        private IPlayer link;

        public IdleLinkState(IPlayer link)
        {
            this.link = link;
        }

        public void Enter()
        {
            System.Diagnostics.Debug.WriteLine("Entering IdleLinkState.");
            link.IsIdle = true;
            link.ResetSprite();
        }

        public void Update(GameTime gameTime)
        {
            // Link is idle, do nothing
        }

        public void Exit()
        {
            System.Diagnostics.Debug.WriteLine("Exiting IdleLinkState.");
            link.IsIdle = false;
        }
    }
}
