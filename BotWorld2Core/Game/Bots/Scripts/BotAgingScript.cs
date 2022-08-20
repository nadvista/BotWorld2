using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotWorld2Core.Game.Bots.Scripts
{
    public class BotAgingScript : BotScript
    {
        public override void Update()
        {
            _self.Age++;
        }
    }
}
