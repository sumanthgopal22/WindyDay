using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ZeldaGame;

public interface IBlock
{
    void LoadContent();

    void Draw(GameTime gameTime);

    void Update(GameTime gameTime);

}