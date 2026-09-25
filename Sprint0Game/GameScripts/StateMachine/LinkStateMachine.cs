using Sprint0Game.GameScripts.Interfaces;
using Sprint0Game.GameScripts.StateMachine.LinkStates;

namespace Sprint0Game.GameScripts.StateMachine
{
    public class LinkStateMachine
    {
        private ILinkState state;

        public LinkStateMachine(IPlayer link)
        {
            state = new IdleLinkState(link);
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
