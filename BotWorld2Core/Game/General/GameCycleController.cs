using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotWorld2Core.Game.General
{
    internal class GameCycleController
    {
        public static event Action OnUpdate;

        public void CallUpdate()
        {
            OnUpdate?.Invoke();
        }
    }
}
