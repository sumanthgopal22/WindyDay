using Microsoft.Xna.Framework;
using ZeldaGame.GameScripts.Interfaces;
using ZeldaGame.GameScripts.StateMachine.LinkStates;
using System;

namespace ZeldaGame.GameScripts.StateMachine
{
    public class LinkStateMachine : IStateMachine
    {
        private IState currentState;
        public IPlayer Link { get; }    // Property for simplicity: Player talks to StateMachine, not State, but State needs to know Player

        public bool IsIdle => currentState is IdleLinkState;
        public bool IsWalking => currentState is WalkingLinkState;

        public LinkStateMachine(IPlayer link)
        {
            Link = link;

            currentState = new IdleLinkState(this);
            currentState.Enter();
        }

        public void ChangeState(IState newState)
        {
            // null check
            ArgumentNullException.ThrowIfNull(newState);

            currentState.Exit();
            currentState = newState;
            currentState.Enter();
        }

        public void Update(GameTime gameTime)
        {
            currentState.Update(gameTime);
        }
    }
}
