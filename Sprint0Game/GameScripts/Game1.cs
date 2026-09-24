using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;

namespace Sprint0Game;

public class Game1 : Core
{
    private List <IController> controllerList;
    private IPlayer player;

    public Game1() : base("Sprint 0 Game", 1280, 720, false)
    {
        
    }

    protected override void Initialize()
    {
        player = new Player();
        2player = new Player();

        KeyboardController keyboardController = new KeyboardController();
        keyboardController.RegisterCommand(Keys.Escape, new QuitGameCommand());
        keyboardController.RegisterCommand(Keys.D, new MoveRightCommand(player));
        keyboardController.RegisterCommand(Keys.A, new MoveLeftCommand(player));
        keyboardController.RegisterCommand(Keys.W, new MoveUpCommand(player));
        keyboardController.RegisterCommand(Keys.S, new MoveDownCommand(player));

        MouseController mouseController = new MouseController();
        mouseController.RegisterCommand(new TeleportCommand(player, mouseController));

        controllerList = [keyboardController, mouseController];

        base.Initialize();
    }

    protected override void LoadContent()
    {
        player.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        foreach (IController controller in controllerList)
        {
            controller.Update(gameTime);
        }

        player.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Color(147,187,236));

        SpriteBatch.Begin();

        player.Draw(gameTime);

        SpriteBatch.End();

        base.Draw(gameTime);
    }
}
