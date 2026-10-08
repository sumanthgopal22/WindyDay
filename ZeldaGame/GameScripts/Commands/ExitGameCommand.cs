using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
