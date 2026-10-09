using Microsoft.Xna.Framework;
using System.Numerics;
using System.Threading;
using ZeldaGame.GameScripts.Interfaces;
using ZeldaGame.GameScripts.Screens;

namespace ZeldaGame.GameScripts.StateMachine.GameStates
{
    public class GameplayState : IState
    {
        private readonly GameStateMachine stateMachine;

        public GameplayState(GameStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public void Enter()
        {
            System.Diagnostics.Debug.WriteLine("Entering GameplayState.");
            stateMachine.Game.Screen = new GameplayScreen(stateMachine.Game);
        }

        public void Update(GameTime gameTime)
        {
            // Update controllers and player when playing 
            foreach (IController controller in stateMachine.Game.ControllerList)
            {
                controller.Update(gameTime);
            }

            stateMachine.Game.Player.Update(gameTime);
            stateMachine.Game.EnemyCycler.Update(gameTime);
            stateMachine.Game.ItemCycler.Update(gameTime);
            stateMachine.Game.Block.Update(gameTime);
            stateMachine.Game.Screen.Update(gameTime);
        }

        public void Exit()
        {
            System.Diagnostics.Debug.WriteLine("Exiting GameplayState.");
            stateMachine.Game.Screen = null;
        }
    }
}
