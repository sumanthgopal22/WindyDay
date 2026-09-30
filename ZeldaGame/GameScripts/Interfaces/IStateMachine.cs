using Microsoft.Xna.Framework;

namespace ZeldaGame.GameScripts.Interfaces
{
    public interface IStateMachine
    {
        void ChangeState(IState newState);
        void Update(GameTime gameTime);
    }
}
