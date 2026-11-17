using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;

namespace ZeldaGame;

public class HUDSprites
{
    private Texture2D HUDelements;
    private Texture2D background;
    private const int scale = 4;
    private const int HUDHeight = 256;

    public void LoadContent(GraphicsDevice graphicsDevice)
    {
        HUDelements = Core.Content.Load<Texture2D>("spritesheets/HUDandPause");
        background = new Texture2D(graphicsDevice, 1, 1);
        background.SetData(new[] {Color.Black});
    }

    private void DrawAt(SpriteBatch spriteBatch, Rectangle source, int destinationX, int destinationY)
    {
        Rectangle destination = new Rectangle(destinationX*scale, destinationY*scale, source.Width*scale, source.Height*scale);
        spriteBatch.Draw(HUDelements,destination,source,Color.White);
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(background, new Rectangle(0,0, Game1.GameWidth, 256), Color.Black);
        DrawAt(spriteBatch, new Rectangle(258, 11, 256, 56), 0, 0);
        spriteBatch.Draw(background, new Rectangle(16*scale, 8*scale, 64*scale, 40*scale), Color.Black);
        spriteBatch.Draw(background, new Rectangle(176*scale, 32*scale, 64*scale, 16*scale), Color.Black);
        DrawRupeeCount(spriteBatch);
        DrawKeyCount(spriteBatch);
        DrawBombCount(spriteBatch);
        DrawItemB(spriteBatch);
        DrawItemA(spriteBatch);
        DrawHearts(spriteBatch);
        DrawMap(spriteBatch);
    }
    private void DrawFont(SpriteBatch spriteBatch, int destinationX, int destinationY, int choice)
    {
        switch (choice)
        {
            case 0: 
                DrawAt(spriteBatch, new Rectangle(528, 117, 8, 8), destinationX, destinationY);
                break;
            case 1: 
                DrawAt(spriteBatch, new Rectangle(537, 117, 8, 8), destinationX, destinationY);
                break;
            case 2: 
                DrawAt(spriteBatch, new Rectangle(546, 117, 8, 8), destinationX, destinationY);
                break;
            case 3: 
                DrawAt(spriteBatch, new Rectangle(555, 117, 8, 8), destinationX, destinationY);
                break;
            case 4: 
                DrawAt(spriteBatch, new Rectangle(564, 117, 8, 8), destinationX, destinationY);
                break;
            case 5: 
                DrawAt(spriteBatch, new Rectangle(573, 117, 8, 8), destinationX, destinationY);
                break;
            case 6: 
                DrawAt(spriteBatch, new Rectangle(582, 117, 8, 8), destinationX, destinationY);
                break;
            case 7: 
                DrawAt(spriteBatch, new Rectangle(591, 117, 8, 8), destinationX, destinationY);
                break;
            case 8: 
                DrawAt(spriteBatch, new Rectangle(600, 117, 8, 8), destinationX, destinationY);
                break;
            case 9: 
                DrawAt(spriteBatch, new Rectangle(609, 117, 8, 8), destinationX, destinationY);
                break;
            //A
            case 10: 
                DrawAt(spriteBatch, new Rectangle(618, 117, 8, 8), destinationX, destinationY);
                break;
            //Empty Heart
            case 11: 
                DrawAt(spriteBatch, new Rectangle(627, 117, 8, 8), destinationX, destinationY);
                break;
            //Half Heart
            case 12: 
                DrawAt(spriteBatch, new Rectangle(636, 117, 8, 8), destinationX, destinationY);
                break;
            //Full Heart
            case 13: 
                DrawAt(spriteBatch, new Rectangle(645, 117, 8, 8), destinationX, destinationY);
                break;
            //X
            case 14: 
                DrawAt(spriteBatch, new Rectangle(519, 117, 8, 8), destinationX, destinationY);
                break;
        }
    }
    private void DrawItem(SpriteBatch spriteBatch, int destinationX, int destinationY, int choice)
    {
        switch (choice)
        {
            //Sword
            case 0: 
                DrawAt(spriteBatch, new Rectangle(555, 137, 8, 16), destinationX, destinationY);
                break;
            //Boomerang
            case 1: 
                DrawAt(spriteBatch, new Rectangle(584, 137, 8, 16), destinationX, destinationY);
                break;
            //Bomb
            case 2: 
                DrawAt(spriteBatch, new Rectangle(604, 137, 8, 16), destinationX, destinationY);
                break;
        }
    }
    private void DrawRupeeCount(SpriteBatch spriteBatch)
    {
        DrawFont(spriteBatch, 96, 16, 14);
        DrawFont(spriteBatch, 104, 16, 0);
        DrawFont(spriteBatch, 112, 16, 0);
    }
    private void DrawKeyCount(SpriteBatch spriteBatch)
    {
        DrawFont(spriteBatch, 96, 32, 14);
        DrawFont(spriteBatch, 104, 32, 0);
        DrawFont(spriteBatch, 112, 32, 0);
    }
    private void DrawBombCount(SpriteBatch spriteBatch)
    {
        DrawFont(spriteBatch, 96, 40, 14);
        DrawFont(spriteBatch, 104, 40, 0);
        DrawFont(spriteBatch, 112, 40, 0);
    }
    private void DrawItemB(SpriteBatch spriteBatch)
    {
        DrawItem(spriteBatch, 128,24,1);
    }
    private void DrawItemA(SpriteBatch spriteBatch)
    {
        DrawItem(spriteBatch, 152,24,0);
    }
    private void DrawHearts(SpriteBatch spriteBatch)
    {
        DrawFont(spriteBatch, 176,40,13);
        DrawFont(spriteBatch, 184,40,13);
        DrawFont(spriteBatch, 192,40,13);
    }
    private void DrawMap(SpriteBatch spriteBatch)
    {
        
        DrawAt(spriteBatch, new Rectangle(681, 108, 8, 8), 48, 16);
        DrawAt(spriteBatch, new Rectangle(681, 108, 8, 8), 48, 24);
        DrawAt(spriteBatch, new Rectangle(681, 108, 8, 8), 48, 32);
        DrawAt(spriteBatch, new Rectangle(681, 108, 8, 8), 56, 24);
        DrawAt(spriteBatch, new Rectangle(681, 108, 8, 8), 40, 24);
        DrawAt(spriteBatch, new Rectangle(681, 108, 8, 8), 64, 20);
        DrawAt(spriteBatch, new Rectangle(663, 108, 8, 8), 72, 20);
        DrawAt(spriteBatch, new Rectangle(663, 108, 8, 8), 40, 16);
        DrawAt(spriteBatch, new Rectangle(672, 108, 8, 8), 40, 32);
        DrawAt(spriteBatch, new Rectangle(672, 108, 8, 8), 56, 32);
        DrawAt(spriteBatch, new Rectangle(663, 108, 8, 8), 32, 24);

    }
}