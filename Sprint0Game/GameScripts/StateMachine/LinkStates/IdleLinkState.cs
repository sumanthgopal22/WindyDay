using Sprint0Game.GameScripts.Interfaces;

namespace Sprint0Game.GameScripts.StateMachine.LinkStates
{
    public class IdleLinkState : ILinkState
    {
        private IPlayer link;
        private ICommand command;

        public IdleLinkState(IPlayer link)
        {
            this.link = link;
        }

        public void TakeDamage()
        {
            // command = new LinkTakeDamageCommand();
            // command.Execute();
        }

        public void Move()
        {
            // command = new LinkMoveCommand();
            // command.Execute();
        }

        public void Attack()
        {
            // command = new LinkAttackCommand();
            // command.Execute();
        }

        public void UseItem()
        {
            // command = new LinkUseItemCommand();
            // command.Execute();
        }

        public void Interact()
        {
            // command = new LinkInteractCommand();
            // command.Execute();
        }

        public void Heal()
        {
            // command = new LinkHealCommand();
            // command.Execute();
        }
    }
}
