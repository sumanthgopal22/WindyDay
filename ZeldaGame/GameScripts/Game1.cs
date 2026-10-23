using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using ZeldaGame.GameScripts.Interfaces;
using ZeldaGame.GameScripts.Screens;
using ZeldaGame.GameScripts.StateMachine;

namespace ZeldaGame;

public class Game1 : Core
{
    // Controllers
    public List<IController> ControllerList { get; private set; }

    // Player
    public IPlayer Player {  get; private set; }

    // Screens
    public IScreen CurrentScreen { get; set; }
    public IScreen MainMenuScreen { get; private set; }
    public IScreen GameplayScreen { get; private set; }

    // State machine
    public GameStateMachine GameStateMachine { get; set; }

    // ===================================================================================

    public Game1() : base("Sprint 0 Game", 1024, 704, false)
    {
    }

    protected override void Initialize()
    {
        // Initialize screens
        MainMenuScreen = new MainMenuScreen(this);
        GameplayScreen = new GameplayScreen(this);
        CurrentScreen = MainMenuScreen;

        // Initialize state machine
        GameStateMachine = new GameStateMachine(this);

        // Initialize player //
        Player = new Player();
        
        //Keyboard controls for Link
        KeyboardController keyboardController = new KeyboardController();
        keyboardController.RegisterHeldCommand(Keys.D, new MoveRightCommand(Player));
        keyboardController.RegisterHeldCommand(Keys.A, new MoveLeftCommand(Player));
        keyboardController.RegisterHeldCommand(Keys.W, new MoveUpCommand(Player));
        keyboardController.RegisterHeldCommand(Keys.S, new MoveDownCommand(Player));
        keyboardController.RegisterPressCommand(Keys.Z, new SwingSwordCommand(Player));
        keyboardController.RegisterPressCommand(Keys.N, new SwingSwordCommand(Player));
        keyboardController.RegisterPressCommand(Keys.D1, new UseItemCommand(Player));
        keyboardController.RegisterPressCommand(Keys.D2, new UseItemCommand(Player));
        keyboardController.RegisterPressCommand(Keys.D3, new UseItemCommand(Player));
        keyboardController.RegisterPressCommand(Keys.D4, new UseItemCommand(Player));

        // Arrow key controls
        keyboardController.RegisterHeldCommand(Keys.Up, new MoveUpCommand(Player));
        keyboardController.RegisterHeldCommand(Keys.Down, new MoveDownCommand(Player));
        keyboardController.RegisterHeldCommand(Keys.Right, new MoveRightCommand(Player));
        keyboardController.RegisterHeldCommand(Keys.Left, new MoveLeftCommand(Player));

        // For reseting game state and quitting the game
        keyboardController.RegisterPressCommand(Keys.R, new ResetGameCommand(this));
        keyboardController.RegisterPressCommand(Keys.Q, new QuitGameCommand());

        ControllerList = [keyboardController];

        base.Initialize();
    }

    protected override void LoadContent()
    {
        // Load sprite factories before the gameplay screen creates room entities.
        EnemySpriteFactory.Instance.LoadTextures();
        ItemSpriteFactory.Instance.LoadTextures();
        LinkSpriteFactory.Instance.LoadTextures();

        // Screens
        MainMenuScreen.LoadContent(Content, GraphicsDevice);
        GameplayScreen.LoadContent(Content, GraphicsDevice);

        // Player
        Player.LoadContent();

    }

    protected override void Update(GameTime gameTime)
    {
        GameStateMachine.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Color(147, 187, 236));
        SpriteBatch.Begin();

        CurrentScreen.Draw(SpriteBatch, gameTime);

        SpriteBatch.End();
        base.Draw(gameTime);
    }

    public void ChangeState(IState state)
    {
        GameStateMachine.ChangeState(state);
    }
}
