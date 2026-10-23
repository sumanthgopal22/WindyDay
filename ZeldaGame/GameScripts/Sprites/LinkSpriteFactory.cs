using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using ZeldaGame.GameScripts.Interfaces;

namespace ZeldaGame;

public class LinkSpriteFactory
{
    private Texture2D link;
    private const float SpriteScale = 2f;
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
            [new Rectangle(0, 0, 29, 32), new Rectangle(0, 58, 29, 32)],
            [new Rectangle(116, 0, 29, 32), new Rectangle(116, 58, 29, 32)],
            [new Rectangle(58, 0, 29, 32), new Rectangle(58, 58, 29, 32)],
            [new Rectangle(174, 58, 29, 32), new Rectangle(174, 0, 29, 32)]
        ];

        Rectangle[] itemUseFrames =
        [
            new Rectangle(0, 116, 31, 30),
            new Rectangle(116, 116, 31, 31),
            new Rectangle(58, 116, 29, 30),
            new Rectangle(173, 116, 30, 30)
        ];

        Rectangle[] swordSwingFrames =
        [
            new Rectangle(0, 160, 29, 58),
            new Rectangle(114, 160, 29, 58),
            new Rectangle(36, 174, 64, 32),
            new Rectangle(152, 174, 64, 32)
        ];

        Vector2[] swordSwingOffsets =
        [
            new Vector2(0, -2),
            new Vector2(0, -50),
            new Vector2(-62, 0),
            new Vector2(-16, 0)
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
