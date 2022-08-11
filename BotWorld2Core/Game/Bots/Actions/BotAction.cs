using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotWorld2Core.Game.Bots.Actions
{
    internal abstract class BotAction : BotComponent
    {
        public abstract void Execute();
    }
}
