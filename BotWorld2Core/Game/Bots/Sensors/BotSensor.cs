using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotWorld2Core.Game.Bots.Sensors
{
    internal abstract class BotSensor : BotComponent
    {
        public abstract double[] GetData();
    }
}
