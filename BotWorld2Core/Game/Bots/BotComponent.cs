using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotWorld2Core.Game.Bots
{
    internal class BotComponent
    {
        protected BotModel _self { get; private set; }
        public void SetBot(BotModel self)
        {
            _self = self;
        }
    }
}
