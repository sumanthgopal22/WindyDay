using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using ZeldaGame.GameScripts.Commands;
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

    // Enemies
    private IEnemy gelEnemy;
    private IEnemy keeseEnemy;
    private IEnemy goriyaEnemy;
    private IEnemy wallMasterEnemy;
    private IEnemy stalfosEnemy;
    private IEnemy aquamentusEnemy;
    public Cycler<IEnemy> EnemyCycler { get; private set; }

    // Screens
    public IScreen CurrentScreen { get; set; }
    public IScreen MainMenuScreen { get; private set; }
    public IScreen GameplayScreen { get; private set; }

    // Blocks
    public Block Block { get; private set; }

    // Items
    private Vector2 itemSpawn;
    public Cycler<IItem> ItemCycler { get; private set; }
    private IItem fairyItem;
    private IItem heartItem;
    private IItem rupeeItem;
    private IItem triforceShardItem;
    private IItem heartContainerItem;
    private IItem clockItem;
    private IItem woodenBoomerangItem;
    private IItem bombItem;
    private IItem compassItem;
    private IItem bowItem;
    private IItem woodenArrowItem;
    private IItem blueCandleItem;
    private IItem bluePotionItem;
    private IItem normalKeyItem;
    private IItem mapItem;

    // State machine
    public GameStateMachine GameStateMachine { get; set; }

    // ===================================================================================

    public Game1() : base("Sprint 0 Game", 1280, 720, false)
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

        // Initialize enemies // 
        gelEnemy = new GelEnemy(Vector2.Zero);
        keeseEnemy = new KeeseEnemy(Vector2.Zero);
        goriyaEnemy = new GoriyaEnemy(Vector2.Zero);
        wallMasterEnemy = new WallMasterEnemy(Vector2.Zero);
        stalfosEnemy = new StalfosEnemy(Vector2.Zero);
        aquamentusEnemy = new AquamentusEnemy(Vector2.Zero);

        // Intialize blocks//
        Block = new Block();

        // Adding to enemyCycler to cycle through enemies being displayed for Sprint2
        EnemyCycler = new Cycler<IEnemy>();

        // Initialize itemCycler to cycle through items
        ItemCycler = new Cycler<IItem>();
        
        //Keyboard controls for Link
        KeyboardController keyboardController = new KeyboardController();
        keyboardController.RegisterHeldCommand(Keys.D, new MoveRightCommand(Player));
        keyboardController.RegisterHeldCommand(Keys.A, new MoveLeftCommand(Player));
        keyboardController.RegisterHeldCommand(Keys.W, new MoveUpCommand(Player));
        keyboardController.RegisterHeldCommand(Keys.S, new MoveDownCommand(Player));
        keyboardController.RegisterPressCommand(Keys.Z, new SwingSwordCommand(Player));
        keyboardController.RegisterPressCommand(Keys.N, new SwingSwordCommand(Player));
        keyboardController.RegisterPressCommand(Keys.NumPad1, new UseItemCommand(Player));
        keyboardController.RegisterPressCommand(Keys.NumPad2, new UseItemCommand(Player));
        keyboardController.RegisterPressCommand(Keys.NumPad3, new UseItemCommand(Player));
        keyboardController.RegisterPressCommand(Keys.NumPad4, new UseItemCommand(Player));

        // Arrow key controls
        keyboardController.RegisterHeldCommand(Keys.Up, new MoveUpCommand(Player));
        keyboardController.RegisterHeldCommand(Keys.Down, new MoveDownCommand(Player));
        keyboardController.RegisterHeldCommand(Keys.Right, new MoveRightCommand(Player));
        keyboardController.RegisterHeldCommand(Keys.Left, new MoveLeftCommand(Player));

        // For reseting game state and quitting the game
        keyboardController.RegisterPressCommand(Keys.R, new ResetGameCommand(this));
        keyboardController.RegisterPressCommand(Keys.Q, new QuitGameCommand());

        //For cycling through enemies
        keyboardController.RegisterPressCommand(Keys.P, new CycleRightCommand(EnemyCycler));
        keyboardController.RegisterPressCommand(Keys.O, new CycleLeftCommand(EnemyCycler));

        // Cycling through blocks
        keyboardController.RegisterPressCommand(Keys.T, new PreviousBlockCommand(Block));
        keyboardController.RegisterPressCommand(Keys.Y, new NextBlockCommand(Block));

        // Cycling through items
        keyboardController.RegisterPressCommand(Keys.I, new CycleRightCommand(ItemCycler));
        keyboardController.RegisterPressCommand(Keys.U, new CycleLeftCommand(ItemCycler));

        ControllerList = [keyboardController];

        base.Initialize();
    }

    protected override void LoadContent()
    {
        // Screens
        MainMenuScreen.LoadContent(Content, GraphicsDevice);
        GameplayScreen.LoadContent(Content, GraphicsDevice);

        // Player
        Player.LoadContent();

        // Load Enemies //
        EnemySpriteFactory.Instance.LoadTextures();
        gelEnemy.LoadContent();
        keeseEnemy.LoadContent();
        goriyaEnemy.LoadContent();
        wallMasterEnemy.LoadContent();
        stalfosEnemy.LoadContent();
        aquamentusEnemy.LoadContent();

        // Load blocks //
        Block.LoadContent();

        // Add enemies to enemyCycler //
        EnemyCycler.Add(gelEnemy);
        EnemyCycler.Add(keeseEnemy);
        EnemyCycler.Add(goriyaEnemy);
        EnemyCycler.Add(wallMasterEnemy);
        EnemyCycler.Add(stalfosEnemy);
        EnemyCycler.Add(aquamentusEnemy);
        
        /* Load Items */
        ItemSpriteFactory.Instance.LoadTextures();
        itemSpawn = new Vector2(Instance.Window.ClientBounds.Width * 0.5f, Instance.Window.ClientBounds.Height * 0.25f);

        fairyItem = new FairyItem(ItemSpriteFactory.Instance.CreateFairySprite(), itemSpawn);
        ItemCycler.Add(fairyItem);
        heartItem = new HeartItem(ItemSpriteFactory.Instance.CreateHeartSprite(), itemSpawn);
        ItemCycler.Add(heartItem);
        rupeeItem = new RupeeItem(ItemSpriteFactory.Instance.CreateRupeeSprite(), itemSpawn);
        ItemCycler.Add(rupeeItem);
        triforceShardItem = new TriforceShardItem(ItemSpriteFactory.Instance.CreateTriforceShardSprite(), itemSpawn);
        ItemCycler.Add(triforceShardItem);
        heartContainerItem = new HeartContainerItem(ItemSpriteFactory.Instance.CreateHeartContainerSprite(), itemSpawn);
        ItemCycler.Add(heartContainerItem);
        clockItem = new ClockItem(ItemSpriteFactory.Instance.CreateClockSprite(), itemSpawn);
        ItemCycler.Add(clockItem);
        woodenBoomerangItem = new WoodenBoomerangItem(ItemSpriteFactory.Instance.CreateWoodenBoomerangSprite(), itemSpawn);
        ItemCycler.Add(woodenBoomerangItem);
        bombItem = new BombItem(ItemSpriteFactory.Instance.CreateBombSprite(), itemSpawn);
        ItemCycler.Add(bombItem);
        compassItem = new CompassItem(ItemSpriteFactory.Instance.CreateCompassSprite(), itemSpawn);
        ItemCycler.Add(compassItem);
        bowItem = new BowItem(ItemSpriteFactory.Instance.CreateBowSprite(), itemSpawn);
        ItemCycler.Add(bowItem);
        woodenArrowItem = new WoodenArrowItem(ItemSpriteFactory.Instance.CreateWoodenArrowSprite(), itemSpawn);
        ItemCycler.Add(woodenArrowItem);
        blueCandleItem = new BlueCandleItem(ItemSpriteFactory.Instance.CreateBlueCandleSprite(), itemSpawn);
        ItemCycler.Add(blueCandleItem);
        bluePotionItem = new BluePotionItem(ItemSpriteFactory.Instance.CreateBluePotionSprite(), itemSpawn);
        ItemCycler.Add(bluePotionItem);
        normalKeyItem = new NormalKeyItem(ItemSpriteFactory.Instance.CreateNormalKeySprite(), itemSpawn);
        ItemCycler.Add(normalKeyItem);
        mapItem = new MapItem(ItemSpriteFactory.Instance.CreateMapSprite(), itemSpawn);
        ItemCycler.Add(mapItem);
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
}
