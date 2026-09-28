using Microsoft.Xna.Framework;
using Sprint0Game.GameScripts.Interfaces;
using Sprint0Game.GameScripts.StateMachine.LinkStates;
using System;

namespace Sprint0Game.GameScripts.StateMachine
{
    public class LinkStateMachine : IStateMachine
    {
        private IState state;

        public IPlayer Link { get; }
        public IState CurrentState => state;

        public LinkStateMachine(IPlayer link)
        {
            Link = link;

            state = new IdleLinkState(this);
            state.Enter();
        }

        public void ChangeState(IState newState)
        {
            // null check
            ArgumentNullException.ThrowIfNull(newState);

            state.Exit();
            state = newState;
            state.Enter();
        }

        public void Update(GameTime gameTime)
        {
            state.Update(gameTime);
        }
    }
}
