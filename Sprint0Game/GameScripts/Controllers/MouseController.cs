using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Sprint0Game;

public class MouseController : IController
{
    private ICommand leftClickCommand;
    private MouseState previousMouseState;

    public Vector2 TargetPosition { get; private set; }

    public MouseController()
    {
        TargetPosition = new Vector2(0f, 0f);
    }

    public void RegisterCommand(ICommand command)
    {
        leftClickCommand = command;
    }

    public void Update(GameTime gameTime)
    {
        MouseState currentMouseState = Mouse.GetState();

        if ((currentMouseState.LeftButton == ButtonState.Pressed) && (previousMouseState.LeftButton == ButtonState.Released))
        {
            TargetPosition = new Vector2(currentMouseState.X, currentMouseState.Y);
            leftClickCommand.Execute();
        }

        previousMouseState = currentMouseState;
    }
}