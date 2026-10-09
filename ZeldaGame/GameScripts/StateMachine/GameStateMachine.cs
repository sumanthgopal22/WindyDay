using Microsoft.Xna.Framework;
using System;
using ZeldaGame.GameScripts.Interfaces;
using ZeldaGame.GameScripts.StateMachine.GameStates;

namespace ZeldaGame.GameScripts.StateMachine
{
    public class GameStateMachine : IStateMachine
    {
        private IState currentState;

        public GameStateMachine(Game1 game)
        {
            currentState = new MainMenuState(game);
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
