using Microsoft.Xna.Framework;

namespace Sprint0Game.GameScripts.Interfaces
{
    public interface IStateMachine
    {
        void ChangeState(IState newState);
        void Update(GameTime gameTime);
    }
}
