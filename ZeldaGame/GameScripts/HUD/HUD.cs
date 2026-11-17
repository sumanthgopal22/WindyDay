using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input; 
using MonoGameLibrary;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ZeldaGame.GameScripts.Interfaces;
using ZeldaGame.GameScripts.Tilemapping;

namespace ZeldaGame
{
    public class HUD
    {
        private readonly HUDSprites HUDsprt;

        public HUD()
        {
            HUDsprt = new HUDSprites();
        }
        public void LoadContent(GraphicsDevice graphicsDevice)
        {
            HUDsprt.LoadContent(graphicsDevice);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            HUDsprt.Draw(spriteBatch);
        }
    }
}