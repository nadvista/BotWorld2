using BotWorld2Core.Game.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotWorld2Core.Game.Scripts
{
    public abstract class Script : Updatable
    {
        public Script() : base() { }
        public Script(GameCycleController controller) : base(controller)
        { }

        public abstract void Reset();
    }
}
