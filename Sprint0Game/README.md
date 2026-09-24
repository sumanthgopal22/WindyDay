# Details
- This simple game allows you to move a small Sonic sprite around a 1280 x 720 window 
using WASD on the keyboard and left-click on the mouse
    - W: move up
    - A: move left
    - S: move down
    - D: move right
    - Left-Click: teleport to wherever the cursor is located on the window

- Sprint0Game references a MonoGameLibrary with a Core.cs script, which I set up following
the tutorial on MonoGame

- Game1.cs and the other game scripts are in the GameScripts folder within Sprint0Game,
organized into different folders:
    - Commands contains all the input command classes
    - Controllers contains the keyboard controller and mouse controller classes
    - Interfaces contains ICommand, IController, IPlayer, and ISprite
    - Players contains the Player class
    - Sprites contains the Sprite class

# AI Usage
- I used Gemini to help me with the organization and structuring of my code, as well as
to help me fix logic and errors and give me specific C# and MonoGame syntax when needed.

# Sources for other materials
- MonoGame tutorials: https://docs.monogame.net/articles/tutorials/building_2d_games/index.html
- Sonic spritesheet: https://www.spriters-resource.com/custom_edited/sonicthehedgehogcustoms/asset/599204/