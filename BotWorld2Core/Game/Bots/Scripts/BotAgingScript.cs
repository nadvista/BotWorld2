using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotWorld2Core.Game.Bots.Components;

namespace BotWorld2Core.Game.Bots.Scripts
{
    public class BotAgingScript : BotScript
    {
        private const int AGE_PER_STEP = 1;
        private BotStatistics _statistics;

        public override void ModelCreated()
        {
            _statistics = _self.GetComponent<BotStatistics>();
        }
        public override void Update()
        {
            _statistics.AddAge(AGE_PER_STEP);
        }
    }
}
