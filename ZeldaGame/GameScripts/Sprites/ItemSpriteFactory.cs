using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;

namespace ZeldaGame;

public class ItemSpriteFactory
{
    private Texture2D items;

    public static ItemSpriteFactory Instance { get; } = new ItemSpriteFactory();

    private ItemSpriteFactory() { }

    public void LoadTextures()
    {
        // Load item spritesheet
        items = Core.Content.Load<Texture2D>("spritesheets/zelda_items_weapons");
    }

    public ISprite CreateFairySprite()
    {
        // Extract fairy frames from spritesheet and set a frame duration
        Rectangle[] fairySourceRectangles = [new Rectangle(40, 0, 8, 16), new Rectangle(48, 0, 8, 16)];
        double fairyFrameDuration = 0.1;

        // Return the animated fairy sprite
        return new AnimatedSprite(items, fairySourceRectangles, fairyFrameDuration);
    }
}