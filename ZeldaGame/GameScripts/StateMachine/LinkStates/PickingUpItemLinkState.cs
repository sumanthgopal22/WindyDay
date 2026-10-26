using Microsoft.Xna.Framework;
using ZeldaGame.GameScripts.Interfaces;

namespace ZeldaGame.GameScripts.StateMachine.LinkStates
{
    // Link holds an item over his head and can't move until the pose finishes
    public class PickingUpItemLinkState : IState
    {
        private IPlayer link;

        public PickingUpItemLinkState(IPlayer link)
        {
            this.link = link;
        }

        public void Enter()
        {
            System.Diagnostics.Debug.WriteLine("Entering PickingUpItemLinkState.");
            link.IsUsingItem = true;
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
            System.Diagnostics.Debug.WriteLine("Exiting PickingUpItemLinkState.");
            link.IsUsingItem = false;
            link.DropHeldItem();
        }
    }
}
