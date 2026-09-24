using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Sprint0Game;

public class KeyboardController : IController
{
    private Dictionary<Keys, ICommand> controllerMappings;

    public KeyboardController()
    {
        controllerMappings = new Dictionary<Keys, ICommand>();    
    }

    public void RegisterCommand(Keys key, ICommand command)
    {
        controllerMappings[key] = command;
    }
    
    public void Update(GameTime gameTime)
    {
        Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();

        foreach (Keys key in pressedKeys)
        {
            if (controllerMappings.TryGetValue(key, out ICommand command))
                command.Execute();
        }
    }
}