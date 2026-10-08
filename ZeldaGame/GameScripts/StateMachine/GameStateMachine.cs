using Microsoft.Xna.Framework;
using System;
using ZeldaGame.GameScripts.Interfaces;
using ZeldaGame.GameScripts.StateMachine.GameStates;

namespace ZeldaGame.GameScripts.StateMachine
{
    public class GameStateMachine : IStateMachine
    {
        private IState currentState;

        public Game1 Game { get; }

        public bool IsInMainMenu => currentState is MainMenuState;
        public bool IsInGameplay => currentState is GameplayState;

        public GameStateMachine(Game1 game)
        {
            Game = game;
            currentState = new MainMenuState(this);
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
