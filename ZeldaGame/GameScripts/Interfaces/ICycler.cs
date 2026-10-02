using Microsoft.Xna.Framework;

namespace ZeldaGame;

public interface ICycler
{
    void Next();

    void Previous();

    void Draw(GameTime gameTime);

    void Update(GameTime gameTime);
}