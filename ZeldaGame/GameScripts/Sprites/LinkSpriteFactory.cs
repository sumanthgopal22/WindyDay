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
    private const double PickUpDuration = 1.0;
    private const double DamageDuration = 1.0;
    
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

        Rectangle[][] damagedLinkFrames =
        [
            [
                new Rectangle(1, 311, 16, 16), new Rectangle(18, 311, 16, 16), new Rectangle(35, 311, 16, 16), new Rectangle(52, 311, 16, 16),
                new Rectangle(1, 328, 16, 16), new Rectangle(18, 328, 16, 16), new Rectangle(35, 328, 16, 16), new Rectangle(52, 328, 16, 16)
            ],
            [
                new Rectangle(1, 345, 16, 16), new Rectangle(18, 345, 16, 16), new Rectangle(35, 345, 16, 16), new Rectangle(52, 345, 16, 16),
                new Rectangle(1, 362, 16, 16), new Rectangle(18, 362, 16, 16), new Rectangle(35, 362, 16, 16), new Rectangle(52, 362, 16, 16)
            ],
            [
                new Rectangle(1, 379, 16, 16), new Rectangle(18, 379, 16, 16), new Rectangle(35, 379, 16, 16), new Rectangle(52, 379, 16, 16),
                new Rectangle(1, 396, 16, 16), new Rectangle(18, 396, 16, 16), new Rectangle(35, 396, 16, 16), new Rectangle(52, 396, 16, 16)
            ],
            [
                new Rectangle(1, 379, 16, 16), new Rectangle(18, 379, 16, 16), new Rectangle(35, 379, 16, 16), new Rectangle(52, 379, 16, 16),
                new Rectangle(1, 396, 16, 16), new Rectangle(18, 396, 16, 16), new Rectangle(35, 396, 16, 16), new Rectangle(52, 396, 16, 16)
            ]
        ];

        //Stand alone, for when he picks things up
        Rectangle pickUpItemFrame = new Rectangle(230, 11, 16, 16);

        return new LinkSprite(
            link,
            walkingFrames,
            itemUseFrames,
            swordSwingFrames,
            swordSwingOffsets,
            pickUpItemFrame,
            damagedLinkFrames,
            SpriteScale,
            FrameDuration,
            ActionDuration,
            PickUpDuration,
            DamageDuration);
    }
}
