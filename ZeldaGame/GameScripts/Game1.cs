using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;

namespace ZeldaGame;

public enum GameStatus
{
    MainMenu,
    Playing
}

public class Game1 : Core
{
    private List<IController> controllerList;
    private IPlayer player;
    private IEnemy gelEnemy;
    private IEnemy keeseEnemy;
    private Cycler enemyCycler;
    private MainMenuScreen menu;
    private Block block;

    // Current state of the game (set as main menu by default)
    public GameStatus CurrentState { get; private set; } = GameStatus.MainMenu;

    public Game1() : base("Sprint 0 Game", 1280, 720, false)
    {
    }

    public void StartGame()
    {
        CurrentState = GameStatus.Playing;
    }

    protected override void Initialize()
    {
        menu = new MainMenuScreen();

        // Set up the main menu
        menu.AddItem("Start Game", new StartGameCommand(this));
        menu.AddItem("Exit", new ExitGameCommand(this));

        player = new Player();
        gelEnemy = new GelEnemy();
        keeseEnemy = new KeeseEnemy();
        block = new Block();

        // Adding to enemyCycler to cycle through enemies being displayed for Sprint2
        enemyCycler = new Cycler();
        enemyCycler.Add(gelEnemy);
        enemyCycler.Add(keeseEnemy);
        

        //Keyboard controls for Link
        KeyboardController keyboardController = new KeyboardController();
        keyboardController.RegisterCommand(Keys.Q, new QuitGameCommand());
        keyboardController.RegisterCommand(Keys.D, new MoveRightCommand(player));
        keyboardController.RegisterCommand(Keys.A, new MoveLeftCommand(player));
        keyboardController.RegisterCommand(Keys.W, new MoveUpCommand(player));
        keyboardController.RegisterCommand(Keys.S, new MoveDownCommand(player));
        
        //Mouse controls for Link
        MouseController mouseController = new MouseController();
        mouseController.RegisterCommand(new TeleportCommand(player, mouseController));

        //For cycling through enemies
        keyboardController.RegisterCommand(Keys.P, new CycleRightCommand(enemyCycler));
        keyboardController.RegisterCommand(Keys.O, new CycleLeftCommand(enemyCycler));

        keyboardController.RegisterCommand(Keys.T, new PreviousBlockCommand(block));
        keyboardController.RegisterCommand(Keys.Y, new NextBlockCommand(block));

        controllerList = [keyboardController, mouseController];

        base.Initialize();
    }

    protected override void LoadContent()
    {
        menu.LoadContent(Content, GraphicsDevice);
        player.LoadContent();
        gelEnemy.LoadContent();
        keeseEnemy.LoadContent();
        block.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        if (CurrentState == GameStatus.MainMenu)
        {
            // Update the main menu when main menu
            menu.Update(gameTime);
        }
        else if (CurrentState == GameStatus.Playing)
        {
            // Update controllers and player when playing 
            foreach (IController controller in controllerList)
            {
                controller.Update(gameTime);
            }

            player.Update(gameTime);
            enemyCycler.Update(gameTime);
            block.Update(gameTime);
            
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Color(147, 187, 236));

        SpriteBatch.Begin();

        if (CurrentState == GameStatus.MainMenu)
        {
            // Draw menu when the game still hasen't started
            menu.Draw(SpriteBatch);
        }
        else if (CurrentState == GameStatus.Playing)
        {
            // Draw player and enemies when the game starts
            player.Draw(gameTime);
            enemyCycler.Draw(gameTime);
            block.Draw(gameTime);
        }

        SpriteBatch.End();

        base.Draw(gameTime);
    }
}