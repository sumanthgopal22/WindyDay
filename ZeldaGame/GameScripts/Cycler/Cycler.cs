using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ZeldaGame;

public class Cycler<T> : ICycler where T : IGameObject
{
    private int currentIndex = 0;
    private List<T> list;

    public Cycler()
    {
        list = [];
    }

    public void Next()
    {
        if (list.Count == 0)
            return;
        
        currentIndex = (currentIndex + 1) % list.Count;
    }

    public void Previous()
    {
        if (list.Count == 0)
            return;

        currentIndex = (currentIndex - 1 + list.Count) % list.Count;
    }

    public void Add(T entity)
    {
        list.Add(entity);
    }

    public void Draw(GameTime gameTime)
    {
        if (list.Count == 0)
            return;
        
        list[currentIndex].Draw(gameTime);
    }

    public void Update(GameTime gameTime)
    {
        if (list.Count == 0)
            return;

        list[currentIndex].Update(gameTime);
    }
}