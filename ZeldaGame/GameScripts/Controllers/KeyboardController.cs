using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ZeldaGame;

public class KeyboardController : IController
{
    private Dictionary<Keys, ICommand> heldMappings = new();
    private Dictionary<Keys, ICommand> pressedMappings = new();
    private KeyboardState previousKeyboardState;

    // for when using movement, held keys
    public void RegisterCommand(Keys key, ICommand command)      
    {
        heldMappings[key] = command;
    }

    // for when single press commands
    public void RegisterPressCommand(Keys key, ICommand command)
    {
        pressedMappings[key] = command;
    }

    public void Update(GameTime gameTime)
    {
        KeyboardState keyboardState = Keyboard.GetState();

        foreach (Keys key in keyboardState.GetPressedKeys())
        {
            if (heldMappings.TryGetValue(key, out ICommand held))
            held.Execute();

            if (previousKeyboardState.IsKeyUp(key) && pressedMappings.TryGetValue(key, out ICommand pressed))
            pressed.Execute();
        }

        previousKeyboardState = keyboardState;
    }

}