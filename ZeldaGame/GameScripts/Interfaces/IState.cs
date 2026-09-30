using Microsoft.Xna.Framework;

namespace ZeldaGame.GameScripts.Interfaces
{
    public interface IState
    {
        void Enter();
        void Update(GameTime gameTime);
        void Exit();
    }
}
