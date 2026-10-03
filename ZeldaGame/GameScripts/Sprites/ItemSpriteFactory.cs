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

    /* Animated Items */

    public ISprite CreateFairySprite()
    {
        // Extract fairy frames from spritesheet and set a frame duration
        Rectangle[] fairySourceRectangles = [new Rectangle(40, 0, 8, 16), new Rectangle(48, 0, 8, 16)];
        double fairyFrameDuration = 0.1;

        // Return the animated fairy sprite
        return new AnimatedSprite(items, fairySourceRectangles, fairyFrameDuration);
    }

    public ISprite CreateHeartSprite()
    {
        // Extract heart frames from spritesheet and set a frame duration
        Rectangle[] heartSourceRectangles = [new Rectangle(0, 0, 8, 8), new Rectangle(0, 8, 8, 8)];
        double heartFrameDuration = 0.2;

        // Return the animated heart sprite
        return new AnimatedSprite(items, heartSourceRectangles, heartFrameDuration);
    }

    public ISprite CreateRupeeSprite()
    {
        // Extract rupee frames from spritesheet and set a frame duration
        Rectangle[] rupeeSourceRectangles = [new Rectangle(72, 0, 8, 16), new Rectangle(72, 16, 8, 16)];
        double rupeeFrameDuration = 0.2;

        // Return the animated rupee sprite
        return new AnimatedSprite(items, rupeeSourceRectangles, rupeeFrameDuration);
    }

    public ISprite CreateTriforceShardSprite()
    {
        // Extract rupee frames from spritesheet and set a frame duration
        Rectangle[] triforceShardSourceRectangles = [new Rectangle(275, 3, 10, 10), new Rectangle(275, 19, 10, 10)];
        double triforceShardFrameDuration = 0.2;

        // Return the animated rupee sprite
        return new AnimatedSprite(items, triforceShardSourceRectangles, triforceShardFrameDuration);
    }

    /* Non-Animated Items */
    public ISprite CreateHeartContainerSprite()
    {
        // Extract the heart container frame from the spritesheet
        Rectangle heartContainerSourceRectangle = new Rectangle(25, 1, 13, 13);

        // Return the heart container sprite
        return new NonAnimatedSprite(items, heartContainerSourceRectangle);
    }

    public ISprite CreateClockSprite()
    {
        // Extract the clock frame from the spritesheet
        Rectangle clockSourceRectangle = new Rectangle(58, 0, 11, 16);

        // Return the clock sprite
        return new NonAnimatedSprite(items, clockSourceRectangle);
    }
}