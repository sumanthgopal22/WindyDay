using Microsoft.Xna.Framework;
using ZeldaGame.GameScripts.Interfaces;

namespace ZeldaGame.GameScripts.StateMachine.LinkStates
{
    public class DeadLinkState : IState
    {
        private readonly IPlayer link;
        private float deathTimer = 1.5f; // Fallback/delay before Game Over screen

        public DeadLinkState(IPlayer link)
        {
            this.link = link;
        }

        public void Enter()
        {
            System.Diagnostics.Debug.WriteLine("Entering DeadLinkState.");
            link.IsDead = true;
            link.CancelMovement();
        }

        public void Update(GameTime gameTime)
        {
            link.UpdateSprite(gameTime);
            deathTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public bool IsDeathSequenceComplete => !link.IsActionPlaying;

        public void Exit()
        {
            System.Diagnostics.Debug.WriteLine("Exiting DeadLinkState.");
        }
    }
}