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

    public ISprite CreateGoriyaSprite()
    {
        // Extract goriya frames from spritesheet and set a frame duration
        Rectangle[] goriyaSourceRectangles = [new Rectangle(224, 11, 13, 16), new Rectangle(241, 11, 13, 16), new Rectangle(257, 11, 13, 16), new Rectangle(275, 12, 14, 16)];
        double goriyaFrameDuration = 0.1;

        // Return the animated goriya sprite
        return new AnimatedSprite(dungeonEnemies, goriyaSourceRectangles, goriyaFrameDuration);
    }

    public ISprite CreateWallMasterSprite()
    {
        // Extract wall master frames from spritesheet and set a frame duration
        Rectangle[] wallMasterSourceRectangles = [new Rectangle(393, 11, 16, 16), new Rectangle(410, 12, 14, 15)];
        double wallMasterFrameDuration = 0.1;

        // Return the animated wall master sprite
        return new AnimatedSprite(dungeonEnemies, wallMasterSourceRectangles, wallMasterFrameDuration);
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
}
