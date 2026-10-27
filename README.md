Overview
ZeldaGame is a top-down Legend of Zelda NES recreation built with Monogame and C#. 
This project features decoupled input routing via the Command pattern, custom entity state machines, texture and sprite factory pipelines, tile/block mapping, collision handlers, projectile physics, and an interactive main menu.
Features
-	A playable Link character; which has directional movement, sword swinging, item use, and collision with walls and items.
-	Multiple enemy types, such as Aquamentus, Gel, Goriya, Keese, Stalfos, and Wall Master: each enemy implements IEnemy, with sprite creation delegated to EnemySpriteFactory. All enemies have collision with the player and walls. They are loaded into their specific rooms for Sprint3.
-	Item system: The 15 item types, including rupees, hearts, keys, bombs, potions, triforce shards, etc, created with ItemSpriteFactory and are cycled through Cycler<IItem>.
-	Command pattern input: the keyboard and mouse input are separated from game logic through ICommand.
-	Sprite hierarchy: AnimatedSprite, NonAnimatedSprite, BlockSprite, and the player Sprite implement ISprite; the player sprite also implements IActionSprite.
-	Cycler logic: a Cycler<T> object navigates lists of game objects, like the blocks and sprites, and updates and draws the one selected at any given moment.
-	Main Menu: An interactive main menu to start and exit the game.
-	Collision: CollisionDetector is used to detect collision, with multiple different collision handlers used to handle each type of collision.
-	File loading: Rooms are loaded in from xml file, with specific layout, items, and enemies.
Architecture & Design
Factory Pattern
-	All input is routed through ICommand. Each concrete command (MoveLeftCommand, SwingSwordCommand, QuitGameCommand, etc.) encapsulates a single action, allowing keys to be rebound without touching game logic.
-	EnemySpriteFactory.Instance loads the Dungeon_Enemies and Boss_Enemies spritesheets, and uses Create[sprite name]Sprite() methods for each enemy.
-	Similarly, ItemSpriteFactory.Instance loads the zelda_items_weapons spritesheet, and uses Create[item name]Sprite() methods for each item.
-	Enemies and items use a private constructor and a static instance property.


State Pattern 
-	IState (Enter, Update, Exit) and IStateMachine (ChangeState, Update) define the contract. LinkStateMachine holds a reference to the IPlayer and starts in IdleLinkState. Game1 uses a GameStatus enum (MainMenu, Playing) to switch the update/draw loop.
Tilemapping & Tileset System
-	Tilemap.cs loads and parses room XML files via FromFile(), creating grid-aligned tile layers. It builds a parallel TileBlock[] blocks array for collision detection.
-	Passable vs Solid Logic (IsTileCollidable): Automatically classifies tile IDs. Floor tiles, corridors, and ladder rungs are marked non-collidable (false), while walls and barriers remain solid (true). Tileset (Tileset.cs): Slices source spritesheets into individual NonAnimatedSprite tiles based on columns, rows, and tile dimensions.
Collision Handlers
-	PlayerBlockCollisionHandler: Resolves overlaps between IPlayer and solid TileBlock objects by adjusting position based on collision side, with window boundary clamping. ItemBlockCollisionHandler: Repositions items out of solid tiles and reverses autonomous fairy movement directions (FairyItem) upon wall collisions.
-	PlayerItemCollisionHandler: Processes item collection effects (rupees, keys, hearts) and triggers special lift animations (PickUpItem) for items like the Boomerang, Bow, Heart Container, Triforce Shard, and Blue Candle. ProjectileBlockCollisionHandler: Intercepts projectile-block collisions and routes them to active projectiles.
-	Note: AI was used to help aid the development of the collision. Mainly, AI was used to fix issues that arose surrounding the logic of the collision.

Projectile Physics
-	Arrow.cs supports directional flight for both Link and Goriyas. Tracks wall collisions (hasHitWall) and screen boundaries. Bomb.cs implements a 2.0-second timer countdown (elapsedTime) before triggering detonation (hasExploded). Boomerang.cs features to and back throwing mechanics. Moves outward for a set number of steps (stepsBeforeComingBack), reverses velocity upon hitting walls or screen edges, and flags completion via HasReturned. WeaponType.cs defines the enumeration for Boomerang, Arrow, and Bomb.
Cycler
-	Cycler<T> (ICycler) wraps a List<T> and provides Next() and Previous() for navigation. It draws and updates the current selected item.


Sprite System
-	AnimatedSprite cycles through an array of Rectangle source rects on a timer. Draws at 3× scale. It is used by the enemies and items that need animation. NonAnimatedSprite draws a single Rectangle source rect at 3× scale. It is used for static objects. BlockSprite maps a frame index to a 16×16 tile on a 9-column sheet, draws at 80×80. It is used by Block. Sprite implements IActionSprite and manages the player sprite with SwingSword and UseItem actions. It is used by Player.
Controls
Input	Action
W, Up / A, Left / S, Down / D, Right	Move Link Up / Left / Down / Right
Z / N	Swing Sword
1 / 4	Use Boomerang 
2	Arrow
3	Bomb
Q	Quit Game
R	Reset
Left Click / Right Click	Cycle Room (Previous / Next)

Main Menu
Input	Action
Up / W 	Select previous item
Down / S 	Select next item
Enter / Space 	Execute selected item

Limitations
Block.Update accumulates elapsedTime without resetting automatically on draw calls. There are also few minor sprite glitches, including relating to the return animations with the boomerang. 


Assets
Asset	Path	Used By
Link spritesheet	spritesheets/Link	Player
Block spritesheet	spritesheets/dungeon-1-sheet	Tileset
Dungeon enemies	spritesheets/Dungeon_Enemies	EnemySpriteFactory
Boss enemies	spritesheets/Boss_Enemies	EnemySpriteFactory
Items & Weapons	spritesheets/zelda_items_weapons	ItemSpriteFactory
Title Font	MIDELTANK_Demo	Main Menu
Item Font	AppleGaramond	Main Menu
Menu Background	zelda_bg	Main Menu
Navigate Sound	Blip7	Main Menu
Menu Music	MainMenuMusic	Main Menu
Tilemap Layouts and Room Configuration	levels/*.xml	Gameplay Screen

.NET Code Analyzers
A few warnings appeared during development of Sprint3, mostly because of unused variables. These were either refactored or left in and warning was ignored.
