using Sprint0Game.GameScripts.StateMachine;

namespace Sprint0Game.GameScripts.Commands
{
    public class LinkTakeDamageCommand : ICommand
    {
        private LinkStateMachine stateMachine;
        private IPlayer link;

        public int Amount { get; set; }

        public LinkTakeDamageCommand(LinkStateMachine sm)
        {
            stateMachine = sm;
            link = stateMachine.Link;
            Amount = 0;
        }

        public void Execute()
        {
            // stateMachine.UpdateState(new TakingDamageLinkState(link));
            // link.Health -= Amount;
        }
    }
}
