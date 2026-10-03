using Microsoft.Xna.Framework;

namespace ZeldaGame;

public interface IGameObject
{
    void Draw(GameTime gameTime);

    void Update(GameTime gameTime);
}