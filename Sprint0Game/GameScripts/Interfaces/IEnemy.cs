using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Sprint0Game;

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