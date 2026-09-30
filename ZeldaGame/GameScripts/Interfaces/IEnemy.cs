using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ZeldaGame;

public interface IEnemy
{

    void MoveRight();

    void MoveLeft();

    void MoveUp();

    void MoveDown();

    void LoadContent();

    void Draw(GameTime gameTime);

    void Update(GameTime gameTime);

}