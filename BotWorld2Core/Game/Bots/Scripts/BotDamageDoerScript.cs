using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotWorld2Core.Game.Bots.Components;

namespace BotWorld2Core.Game.Bots.Scripts
{
    public class BotDamageDoerScript : BotScript
    {
        private const float DAMAGE_PER_STEP = 1;
        private BotStatsController _stats;

        public override void ModelCreated()
        {
            _stats = _self.GetComponent<BotStatsController>();
        }
        public override void Update()
        {
            _stats.TakeHit(DAMAGE_PER_STEP);
        }
    }
}
