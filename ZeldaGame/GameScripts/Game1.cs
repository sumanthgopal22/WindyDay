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
    private IEnemy goriyaEnemy;
    private IEnemy wallMasterEnemy;
    private IEnemy aquamentusEnemy;
    private Cycler<IEnemy> enemyCycler;
    private MainMenuScreen menu;
    private Block block;
    private Vector2 itemSpawn;
    private Cycler<IItem> itemCycler;
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

        // Initialize player //
        player = new Player();

        // Initialize enemies // 
        gelEnemy = new GelEnemy();
        keeseEnemy = new KeeseEnemy();
        goriyaEnemy = new GoriyaEnemy();
        wallMasterEnemy = new WallMasterEnemy();
        aquamentusEnemy = new AquamentusEnemy();
        block = new Block();

        // Adding to enemyCycler to cycle through enemies being displayed for Sprint2
        enemyCycler = new Cycler<IEnemy>();

        // Initialize itemCycler to cycle through items
        itemCycler = new Cycler<IItem>();
        
        //Keyboard controls for Link
        KeyboardController keyboardController = new KeyboardController();
        keyboardController.RegisterPressCommand(Keys.Q, new QuitGameCommand());
        keyboardController.RegisterCommand(Keys.D, new MoveRightCommand(player));
        keyboardController.RegisterCommand(Keys.A, new MoveLeftCommand(player));
        keyboardController.RegisterCommand(Keys.W, new MoveUpCommand(player));
        keyboardController.RegisterCommand(Keys.S, new MoveDownCommand(player));
        keyboardController.RegisterCommandOnKeyDown(Keys.K, new UseItemCommand(player));
        keyboardController.RegisterCommandOnKeyDown(Keys.L, new SwingSwordCommand(player));
        
        //Mouse controls for Link
        MouseController mouseController = new MouseController();
        mouseController.RegisterCommand(new TeleportCommand(player, mouseController));

        //For cycling through enemies
        keyboardController.RegisterPressCommand(Keys.P, new CycleRightCommand(enemyCycler));
        keyboardController.RegisterPressCommand(Keys.O, new CycleLeftCommand(enemyCycler));

        // Cycling through blocks
        keyboardController.RegisterPressCommand(Keys.T, new PreviousBlockCommand(block));
        keyboardController.RegisterPressCommand(Keys.Y, new NextBlockCommand(block));

        // Cycling through items
        keyboardController.RegisterPressCommand(Keys.I, new CycleRightCommand(itemCycler));
        keyboardController.RegisterPressCommand(Keys.U, new CycleLeftCommand(itemCycler));

        controllerList = [keyboardController, mouseController];

        base.Initialize();
    }

    protected override void LoadContent()
    {
        menu.LoadContent(Content, GraphicsDevice);
        player.LoadContent();

        // Load Enemies //
        EnemySpriteFactory.Instance.LoadTextures();
        gelEnemy.LoadContent();
        keeseEnemy.LoadContent();
        goriyaEnemy.LoadContent();
        wallMasterEnemy.LoadContent();
        aquamentusEnemy.LoadContent();
        block.LoadContent();

        // Add enemies to enemyCycler //
        enemyCycler.Add(gelEnemy);
        enemyCycler.Add(keeseEnemy);
        enemyCycler.Add(goriyaEnemy);
        enemyCycler.Add(wallMasterEnemy);
        enemyCycler.Add(aquamentusEnemy);
        
        /* Load Items */
        ItemSpriteFactory.Instance.LoadTextures();
        itemSpawn = new Vector2(Instance.Window.ClientBounds.Width * 0.5f, Instance.Window.ClientBounds.Height * 0.25f);

        fairyItem = new FairyItem(ItemSpriteFactory.Instance.CreateFairySprite(), itemSpawn);
        itemCycler.Add(fairyItem);
        heartItem = new HeartItem(ItemSpriteFactory.Instance.CreateHeartSprite(), itemSpawn);
        itemCycler.Add(heartItem);
        rupeeItem = new RupeeItem(ItemSpriteFactory.Instance.CreateRupeeSprite(), itemSpawn);
        itemCycler.Add(rupeeItem);
        triforceShardItem = new TriforceShardItem(ItemSpriteFactory.Instance.CreateTriforceShardSprite(), itemSpawn);
        itemCycler.Add(triforceShardItem);
        heartContainerItem = new HeartContainerItem(ItemSpriteFactory.Instance.CreateHeartContainerSprite(), itemSpawn);
        itemCycler.Add(heartContainerItem);
        clockItem = new ClockItem(ItemSpriteFactory.Instance.CreateClockSprite(), itemSpawn);
        itemCycler.Add(clockItem);
        woodenBoomerangItem = new WoodenBoomerangItem(ItemSpriteFactory.Instance.CreateWoodenBoomerangSprite(), itemSpawn);
        itemCycler.Add(woodenBoomerangItem);
        bombItem = new BombItem(ItemSpriteFactory.Instance.CreateBombSprite(), itemSpawn);
        itemCycler.Add(bombItem);
        compassItem = new CompassItem(ItemSpriteFactory.Instance.CreateCompassSprite(), itemSpawn);
        itemCycler.Add(compassItem);
        bowItem = new BowItem(ItemSpriteFactory.Instance.CreateBowSprite(), itemSpawn);
        itemCycler.Add(bowItem);
        woodenArrowItem = new WoodenArrowItem(ItemSpriteFactory.Instance.CreateWoodenArrowSprite(), itemSpawn);
        itemCycler.Add(woodenArrowItem);
        blueCandleItem = new BlueCandleItem(ItemSpriteFactory.Instance.CreateBlueCandleSprite(), itemSpawn);
        itemCycler.Add(blueCandleItem);
        bluePotionItem = new BluePotionItem(ItemSpriteFactory.Instance.CreateBluePotionSprite(), itemSpawn);
        itemCycler.Add(bluePotionItem);
        normalKeyItem = new NormalKeyItem(ItemSpriteFactory.Instance.CreateNormalKeySprite(), itemSpawn);
        itemCycler.Add(normalKeyItem);
        mapItem = new MapItem(ItemSpriteFactory.Instance.CreateMapSprite(), itemSpawn);
        itemCycler.Add(mapItem);
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
            itemCycler.Update(gameTime);
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
            itemCycler.Draw(gameTime);
            block.Draw(gameTime);
        }

        SpriteBatch.End();

        base.Draw(gameTime);
    }
}