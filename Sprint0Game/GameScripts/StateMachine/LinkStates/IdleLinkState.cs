using Sprint0Game.GameScripts.Interfaces;
using Sprint0Game.GameScripts.Commands;

namespace Sprint0Game.GameScripts.StateMachine.LinkStates
{
    public class IdleLinkState : ILinkState
    {
        private LinkStateMachine stateMachine;
        private IPlayer link;
        private ICommand command;

        public IdleLinkState(LinkStateMachine sm)
        {
            stateMachine = sm;
            link = stateMachine.Link;
        }

        public void TakeDamage()
        {
            command = new LinkTakeDamageCommand(stateMachine);
            command.Execute();
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
