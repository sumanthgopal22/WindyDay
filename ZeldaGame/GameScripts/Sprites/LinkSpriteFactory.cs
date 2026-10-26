using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using ZeldaGame.GameScripts.Interfaces;

namespace ZeldaGame;

public class LinkSpriteFactory
{
    private Texture2D link;
    private const float SpriteScale = 4f;
    private const double FrameDuration = 0.2;
    private const double ActionDuration = 0.25;

    public static LinkSpriteFactory Instance { get; } = new LinkSpriteFactory();

    private LinkSpriteFactory() { }

    public void LoadTextures()
    {
        link = Core.Content.Load<Texture2D>("spritesheets/Link");
    }

    public IActionSprite CreateLinkSprite()
    {
        Rectangle[][] walkingFrames =
        [
            [new Rectangle(1, 11, 16, 16), new Rectangle(18, 11, 16, 16)],
            [new Rectangle(69, 11, 16, 16), new Rectangle(86, 11, 16, 16)],
            [new Rectangle(35, 11, 16, 16), new Rectangle(52, 11, 16, 16)],
            [new Rectangle(35, 11, 16, 16), new Rectangle(52, 11, 16, 16)]
        ];

        Rectangle[] itemUseFrames =
        [
            new Rectangle(107, 11, 16, 16),
            new Rectangle(141, 11, 16, 16),
            new Rectangle(124, 11, 16, 16),
            new Rectangle(124, 11, 16, 16)
        ];

        Rectangle[] swordSwingFrames =
        [
            new Rectangle(18, 47, 16, 27),
            new Rectangle(18, 97, 16, 28),
            new Rectangle(18, 77, 27, 16),
            new Rectangle(18, 77, 27, 16)
        ];

        Vector2[] swordSwingOffsets =
        [
            new Vector2(0, 0),
            new Vector2(0, -(28 - 16) * SpriteScale),
            new Vector2(-(27 - 16) * SpriteScale, 0),
            new Vector2(0, 0)
        ];

        return new LinkSprite(
            link,
            walkingFrames,
            itemUseFrames,
            swordSwingFrames,
            swordSwingOffsets,
            SpriteScale,
            FrameDuration,
            ActionDuration);
    }
}
