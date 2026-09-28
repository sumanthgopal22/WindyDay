using Microsoft.Xna.Framework;

namespace Sprint0Game.GameScripts.Interfaces
{
    public interface IState
    {
        void Enter();
        void Update(GameTime gameTime);
        void Exit();
    }
}
