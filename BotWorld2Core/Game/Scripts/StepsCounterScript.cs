using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotWorld2Core.Game.Scripts
{
    internal class StepsCounterScript : Script
    {
        public int Step { get; private set; } = 0;

        public override void Reset()
        {
            Step = 0;
        }

        public override void Update()
        {
            Step++;
        }
    }
}
