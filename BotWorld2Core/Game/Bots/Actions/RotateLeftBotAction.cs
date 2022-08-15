using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotWorld2Core.Game.Bots.Actions
{
    internal class RotateLeftBotAction : BotAction
    {
        public override void Execute()
        {
            _self.Forward.RotateLeft();
        }
    }
}
