using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotWorld2Core.Game.General
{
    internal abstract class Updatable
    {
        public Updatable()
        {
            GameCycleController.OnUpdate += Update;
        }
        protected abstract void Update();
    }
}
