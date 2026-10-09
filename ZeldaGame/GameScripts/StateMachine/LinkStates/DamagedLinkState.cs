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
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Exit()
        {
            System.Diagnostics.Debug.WriteLine("Exiting DamagedLinkState.");
        }
    }
}
