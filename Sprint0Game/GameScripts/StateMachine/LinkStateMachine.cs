using Sprint0Game.GameScripts.Interfaces;
using Sprint0Game.GameScripts.StateMachine.LinkStates;

namespace Sprint0Game.GameScripts.StateMachine
{
    /* Allows a single point of control for the Game class.
     The StateMachine is used by States and certain Commands
    to determine how to change the Game. */
    public class LinkStateMachine
    {
        private ILinkState state;

        /* This Link player goes through the StateMachine,
         is used by the States within the machine, and used
        by the Commands the States call. Those commands may
        affect data within the player, such as health. */
        public IPlayer Link { get; set; }

        public LinkStateMachine(IPlayer link)
        {
            Link = link;
            state = new IdleLinkState(this);
        }

        public void UpdateState(ILinkState state)
        {
            this.state = state;
        }

        public void TakeDamage()
        {
            state.TakeDamage();
        }

        public void Move()
        {
            state.Move();
        }

        public void Attack()
        {
            state.Attack();
        }

        public void UseItem()
        {
            state.UseItem();
        }

        public void Interact()
        {
            state.Interact();
        }

        public void Heal()
        {
            state.Heal();
        }
    }
}
