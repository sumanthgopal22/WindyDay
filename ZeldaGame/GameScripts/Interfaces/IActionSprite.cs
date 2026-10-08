using ZeldaGame.GameScripts.Sprites;

namespace ZeldaGame.GameScripts.Interfaces
{
    public interface IActionSprite : ISprite
    {
        void PlayAction(LinkSpriteAction action);

        bool IsActionPlaying { get; }
    }
}
