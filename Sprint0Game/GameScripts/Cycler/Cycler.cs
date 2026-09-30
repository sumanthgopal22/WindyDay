using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Sprint0Game;

public class Cycler
{
    private int currentIndex = 0;
    private List<IEnemy> enemiesList;    
    public Cycler()
    {
        enemiesList = [];
    }
    public void Next()
    {
        currentIndex = (currentIndex + 1) % enemiesList.Count;

    }
    public void Previous()
    {
        currentIndex = (currentIndex - 1 + enemiesList.Count) % enemiesList.Count;
    }

    public void Add(IEnemy enemy)
    {
        enemiesList.Add(enemy);
    }

    public void Draw(GameTime gameTime)
    {
       enemiesList[currentIndex].Draw(gameTime);
    }

    public void Update(GameTime gameTime)
    {
        enemiesList[currentIndex].Update(gameTime);

    }

}