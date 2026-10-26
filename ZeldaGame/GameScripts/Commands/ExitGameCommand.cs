using Microsoft.Xna.Framework;
using System;

namespace ZeldaGame.GameScripts.Commands
{
    public class ExitGameCommand : ICommand
    {
        private readonly Game game;

        public ExitGameCommand(Game game)
        {
            this.game = game ?? throw new ArgumentNullException(nameof(game));
        }

        public void Execute() => game.Exit();
    }
}
