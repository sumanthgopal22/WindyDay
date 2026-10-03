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

    public ISprite CreateWoodenBoomerangSprite()
    {
        // Extract the wooden boomerang frame from the spritesheet
        Rectangle woodenBoomerangSourceRectangle = new Rectangle(129, 0, 5, 12);

        // Return the wooden boomerang sprite
        return new NonAnimatedSprite(items, woodenBoomerangSourceRectangle);
    }

    public ISprite CreateBombSprite()
    {
        // Extract the bomb frame from the spritesheet
        Rectangle bombSourceRectangle = new Rectangle(136, 0, 8, 16);

        // Return the bomb sprite
        return new NonAnimatedSprite(items, bombSourceRectangle);
    }

    public ISprite CreateCompassSprite()
    {
        // Extract the compass frame from the spritesheet
        Rectangle compassSourceRectangle = new Rectangle(258, 0, 16, 16);

        // Return the compass sprite
        return new NonAnimatedSprite(items, compassSourceRectangle);
    }

    public ISprite CreateBowSprite()
    {
        // Extract the bow frame from the spritesheet
        Rectangle bowSourceRectangle = new Rectangle(144, 0, 8, 16);

        // Return the bow sprite
        return new NonAnimatedSprite(items, bowSourceRectangle);
    }

    public ISprite CreateWoodenArrowSprite()
    {
        // Extract the wooden arrow frame from the spritesheet
        Rectangle woodenArrowSourceRectangle = new Rectangle(154, 0, 5, 16);

        // Return the wooden arrow sprite
        return new NonAnimatedSprite(items, woodenArrowSourceRectangle);
    }

    public ISprite CreateBlueCandleSprite()
    {
        // Extract the blue candle frame from the spritesheet
        Rectangle blueCandleSourceRectangle = new Rectangle(160, 16, 8, 16);

        // Return the blue candle sprite
        return new NonAnimatedSprite(items, blueCandleSourceRectangle);
    }

    public ISprite CreateBluePotionSprite()
    {
        // Extract the blue potion frame from the spritesheet
        Rectangle blueCandleSourceRectangle = new Rectangle(80, 16, 8, 16);

        // Return the blue potion sprite
        return new NonAnimatedSprite(items, blueCandleSourceRectangle);
    }

    public ISprite CreateNormalKeySprite()
    {
        // Extract the normal key frame from the spritesheet
        Rectangle normalKeySourceRectangle = new Rectangle(240, 0, 8, 16);

        // Return the normal key sprite
        return new NonAnimatedSprite(items, normalKeySourceRectangle);
    }

    public ISprite CreateMapSprite()
    {
        // Extract the normal key frame from the spritesheet
        Rectangle mapSourceRectangle = new Rectangle(184, 0, 8, 16);

        // Return the normal key sprite
        return new NonAnimatedSprite(items, mapSourceRectangle);
    }
}