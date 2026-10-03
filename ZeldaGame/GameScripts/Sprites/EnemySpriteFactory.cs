using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;

namespace ZeldaGame;

public class EnemySpriteFactory
{
    private Texture2D dungeonEnemies;
    private Texture2D bossEnemies;
    public static EnemySpriteFactory Instance { get; } = new EnemySpriteFactory();

    private EnemySpriteFactory() { }

    public void LoadTextures()
    {
        // Load enemy spritesheets
        dungeonEnemies = Core.Content.Load<Texture2D>("spritesheets/Dungeon_Enemies");
        bossEnemies = Core.Content.Load<Texture2D>("spritesheets/Boss_Enemies");
    }

    // Dungeon Enemies //
    
    public ISprite CreateGelSprite()
    {
        // Extract gel frames from spritesheet and set a frame duration
        Rectangle[] gelSourceRectangles = [new Rectangle(1, 11, 8, 16), new Rectangle(10, 11, 8, 16)];
        double gelFrameDuration = 0.1;

        // Return the animated gel sprite
        return new AnimatedSprite(dungeonEnemies, gelSourceRectangles, gelFrameDuration);
    }

    public ISprite CreateKeeseSprite()
    {
        // Extract keese frames from spritesheet and set a frame duration
        Rectangle[] keeseSourceRectangles = [new Rectangle(183, 11, 16, 16), new Rectangle(200, 11, 16, 16)];
        double keeseFrameDuration = 0.1;

        // Return the animated keese sprite
        return new AnimatedSprite(dungeonEnemies, keeseSourceRectangles, keeseFrameDuration);
    }

    public ISprite CreateGoriyaSprite(spriteDirection direction)
    {
        // Extract goriya frames from spritesheet and set a frame duration
        // Up and down only have one frame each, so animate by alternating between the original and a flipped copy
        Rectangle[] goriyaDownSourceRectangles = [new Rectangle(224, 11, 13, 16), new Rectangle(224, 11, 13, 16)];
        Rectangle[] goriyaUpSourceRectangles = [new Rectangle(241, 11, 13, 16), new Rectangle(241, 11, 13, 16)];
        SpriteEffects[] goriyaFlipEffects = [SpriteEffects.None, SpriteEffects.FlipHorizontally];
        Rectangle[] goriyaSideSourceRectangles = [new Rectangle(257, 11, 13, 16), new Rectangle(275, 12, 14, 16)];
        double goriyaFrameDuration = 0.1;

        // Return the animated goriya sprite for the direction it is facing
        if (direction == spriteDirection.Down)
        {
            return new AnimatedSprite(dungeonEnemies, goriyaDownSourceRectangles, goriyaFrameDuration, goriyaFlipEffects);
        }
        else if (direction == spriteDirection.Up)
        {
            return new AnimatedSprite(dungeonEnemies, goriyaUpSourceRectangles, goriyaFrameDuration, goriyaFlipEffects);
        }
        else if (direction == spriteDirection.Left)
        {
            // The spritesheet only has right-facing frames, so flip them to face left
            return new AnimatedSprite(dungeonEnemies, goriyaSideSourceRectangles, goriyaFrameDuration, SpriteEffects.FlipHorizontally);
        }
        else
        {
            return new AnimatedSprite(dungeonEnemies, goriyaSideSourceRectangles, goriyaFrameDuration);
        }
    }

    public ISprite CreateBoomerangSprite()
    {
        // Extract boomerang frames from spritesheet and set a frame duration
        // The spritesheet has upright, diagonal, and flat boomerangs. Flipping the diagonal one makes the opposite diagonal,
        // so these four frames together make the boomerang look like it is spinning
        Rectangle uprightBoomerang = new Rectangle(291, 15, 8, 8);
        Rectangle diagonalBoomerang = new Rectangle(299, 15, 8, 8);
        Rectangle flatBoomerang = new Rectangle(308, 15, 8, 8);

        Rectangle[] boomerangSourceRectangles = [uprightBoomerang, diagonalBoomerang, flatBoomerang, diagonalBoomerang];
        SpriteEffects[] boomerangEffects = [SpriteEffects.None, SpriteEffects.None, SpriteEffects.None, SpriteEffects.FlipVertically];
        double boomerangFrameDuration = 0.05;

        // Return the animated boomerang sprite
        return new AnimatedSprite(dungeonEnemies, boomerangSourceRectangles, boomerangFrameDuration, boomerangEffects);
    }

    public ISprite CreateWallMasterSprite()
    {
        // Extract wall master frames from spritesheet and set a frame duration
        Rectangle[] wallMasterSourceRectangles = [new Rectangle(393, 11, 16, 16), new Rectangle(410, 12, 14, 15)];
        double wallMasterFrameDuration = 0.1;

        // Return the animated wall master sprite
        return new AnimatedSprite(dungeonEnemies, wallMasterSourceRectangles, wallMasterFrameDuration);
    }

    public ISprite CreateStalfosSprite()
    {
        // Extract stalfos frames from spritesheet and set a frame duration
        // Stalfos only has one frame, so animate by alternating between the original and a flipped copy
        Rectangle[] stalfosSourceRectangles = [new Rectangle(2, 59, 15, 16), new Rectangle(2, 59, 15, 16)];
        SpriteEffects[] stalfosFlipEffects = [SpriteEffects.None, SpriteEffects.FlipHorizontally];
        double stalfosFrameDuration = 0.2;

        // Return the animated stalfos sprite
        return new AnimatedSprite(dungeonEnemies, stalfosSourceRectangles, stalfosFrameDuration, stalfosFlipEffects);
    }

    // Boss Enemies //

    public ISprite CreateAquamentusSprite()
    {
        // Extract aquamentus frames from spritesheet and set a frame duration
        Rectangle[] aquamentusSourceRectangles = [new Rectangle(1, 11, 24, 32), new Rectangle(26, 11, 24, 32)];
        double aquamentusFrameDuration = 0.1;

        // Return the animated aquamentus sprite
        return new AnimatedSprite(bossEnemies, aquamentusSourceRectangles, aquamentusFrameDuration);
    }

    public ISprite CreateFireballSprite()
    {
        // Extract fireball frames from spritesheet and set a frame duration
        Rectangle[] fireballSourceRectangles = [new Rectangle(101, 14, 8, 10), new Rectangle(110, 14, 8, 10), new Rectangle(119, 14, 8, 10), new Rectangle(128, 14, 8, 10)];
        double fireballFrameDuration = 0.05;

        // Return the animated fireball sprite
        return new AnimatedSprite(bossEnemies, fireballSourceRectangles, fireballFrameDuration);
    }
}
