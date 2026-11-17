using Microsoft.Xna.Framework;
using System;
using ZeldaGame.GameScripts.Interfaces;
using ZeldaGame.GameScripts.StateMachine.GameStates;

namespace ZeldaGame.GameScripts.StateMachine
{
    public class GameStateMachine : IStateMachine
    {
        public IState CurrentState { get; private set; }

        public GameStateMachine(Game1 game)
        {
            CurrentState = new MainMenuState(game);
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
