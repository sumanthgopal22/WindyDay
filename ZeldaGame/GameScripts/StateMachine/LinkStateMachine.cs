using Microsoft.Xna.Framework;
using ZeldaGame.GameScripts.Interfaces;
using ZeldaGame.GameScripts.StateMachine.LinkStates;
using System;

namespace ZeldaGame.GameScripts.StateMachine
{
    public class LinkStateMachine : IStateMachine
    {
        public IState CurrentState { get; private set; }

        public LinkStateMachine(IPlayer link)
        {
            CurrentState = new IdleLinkState(link);
            CurrentState.Enter();
        }

        public void ChangeState(IState newState)
        {
            // null check
            ArgumentNullException.ThrowIfNull(newState);

            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }

        public void Update(GameTime gameTime)
        {
            CurrentState.Update(gameTime);
        }
    }
}
