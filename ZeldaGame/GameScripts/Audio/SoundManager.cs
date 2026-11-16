using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace ZeldaGame;

public class SoundManager
{
    private SoundEffect bombBlow;
    private SoundEffect getHeart;
    private SoundEffect getItem;
    private SoundEffect fanfare;
    private SoundEffect boomerangThrow;
    private SoundEffect hurt;
    private SoundEffect shieldDeflect;

    //Singleton
    public static SoundManager Instance { get; } = new SoundManager();

    private SoundManager() { }

    public void LoadContent(ContentManager content)
    {
        bombBlow = content.Load<SoundEffect>("sounds/The Legend of Zelda Cartoon Sound Effects Bomb Apearance and Selfdrestruction");
        getHeart = content.Load<SoundEffect>("sounds/The Legend of Zelda Cartoon Sound Effects Health Heart");
        getItem = content.Load<SoundEffect>("sounds/The Legend of Zelda Cartoon Sound Effects Item Obtained");
        fanfare = content.Load<SoundEffect>("sounds/The Legend of Zelda Cartoon Sound Effects Item Received");
        boomerangThrow = content.Load<SoundEffect>("sounds/The Legend of Zelda Cartoon Sound Effects Magical Boomerang Thrown");
        hurt = content.Load<SoundEffect>("sounds/The Legend of Zelda Cartoon Sound Effects Player Hurt");
        shieldDeflect = content.Load<SoundEffect>("sounds/The Legend of Zelda Cartoon Sound Effects Shield Deflecting");
    }

    public void Play(Sfx sfx)
    {
        switch (sfx)
        {
            case Sfx.BombBlow:
                bombBlow.Play();
                break;

            case Sfx.GetHeart:
                getHeart.Play();
                break;

            case Sfx.GetItem:
                getItem.Play();
                break;

            case Sfx.Fanfare:
                fanfare.Play();
                break;

            case Sfx.BoomerangThrow:
                boomerangThrow.Play();
                break;

            case Sfx.Hurt:
                hurt.Play();
                break;

            case Sfx.ShieldDeflect:
                shieldDeflect.Play();
                break;

            default:
                break;
        }
    }
}
