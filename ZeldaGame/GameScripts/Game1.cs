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

    // The game is laid out at the NES screen size times 4 (256x176 * 4).
    // Use GameWidth/GameHeight for anything that measures the screen, not the window size,
    // because the window can be smaller and Draw scales the picture down to fit it.
    public const int GameWidth = 1024;
    public const int GameHeight = 704;
    private const int WindowWidth = 768;
    private const int WindowHeight = 528;

    public Game1() : base("Sprint 0 Game", WindowWidth, WindowHeight, false)
    {
    }

    protected override void Initialize()
    {
        // Initialize player //
        Player = new Player();

        // Initialize screens
        MainMenuScreen = new MainMenuScreen(this);
        GameplayScreen = new GameplayScreen(this);
        CurrentScreen = MainMenuScreen;

        // Initialize state machine
        GameStateMachine = new GameStateMachine(this);
        
        //Keyboard controls for Link
        KeyboardController keyboardController = new KeyboardController();
        keyboardController.RegisterHeldCommand(Keys.D, new MoveRightCommand(Player));
        keyboardController.RegisterHeldCommand(Keys.A, new MoveLeftCommand(Player));
        keyboardController.RegisterHeldCommand(Keys.W, new MoveUpCommand(Player));
        keyboardController.RegisterHeldCommand(Keys.S, new MoveDownCommand(Player));
        keyboardController.RegisterPressCommand(Keys.Z, new SwingSwordCommand(Player));
        keyboardController.RegisterPressCommand(Keys.N, new SwingSwordCommand(Player));
        keyboardController.RegisterPressCommand(Keys.D1, new UseItemCommand(Player, ProjectileType.Boomerang));
        keyboardController.RegisterPressCommand(Keys.D2, new UseItemCommand(Player, ProjectileType.Arrow));
        keyboardController.RegisterPressCommand(Keys.D3, new UseItemCommand(Player, ProjectileType.Bomb));
        keyboardController.RegisterPressCommand(Keys.D4, new UseItemCommand(Player, ProjectileType.Boomerang));

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

        // Player
        Player.LoadContent();

        // Screens
        MainMenuScreen.LoadContent(Content, GraphicsDevice);
        GameplayScreen.LoadContent(Content, GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
        GameStateMachine.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Color(147, 187, 236));
        
        // Shrink the 1024x704 game down to the window size
        Matrix scale = Matrix.CreateScale((float)WindowWidth / GameWidth);
        SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: scale);

        CurrentScreen.Draw(SpriteBatch, gameTime);

        SpriteBatch.End();
        base.Draw(gameTime);
    }

    public void ChangeState(IState state)
    {
        GameStateMachine.ChangeState(state);
    }
}
