using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ZeldaGame;

public class KeyboardController : IController
{
    private Dictionary<Keys, ICommand> controllerMappings;
    private HashSet<Keys> keyDownOnlyCommands;
    private KeyboardState previousKeyboardState;

    public KeyboardController()
    {
        controllerMappings = new Dictionary<Keys, ICommand>();
        keyDownOnlyCommands = new HashSet<Keys>();
    }

    // for when using movement, held keys
    public void RegisterCommand(Keys key, ICommand command)
    {
        controllerMappings[key] = command;
        keyDownOnlyCommands.Remove(key);
    }

    // for when single press commands
    public void RegisterCommandOnKeyDown(Keys key, ICommand command)
    {
        controllerMappings[key] = command;
        keyDownOnlyCommands.Add(key);
    }

    public void RegisterPressCommand(Keys key, ICommand command)
    {
        RegisterCommandOnKeyDown(key, command);
    }

    public void Update(GameTime gameTime)
    {
        KeyboardState keyboardState = Keyboard.GetState();
        Keys[] pressedKeys = keyboardState.GetPressedKeys();

        foreach (Keys key in pressedKeys)
        {
            bool isNewlyPressed = !previousKeyboardState.IsKeyDown(key);
            if (controllerMappings.TryGetValue(key, out ICommand command)
                && (!keyDownOnlyCommands.Contains(key) || isNewlyPressed))
            {
                command.Execute();
            }
        }

        previousKeyboardState = keyboardState;
    }
}
