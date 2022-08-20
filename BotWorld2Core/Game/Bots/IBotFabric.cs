using BotWorld2Core.Game.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotWorld2Core.Game.Bots
{
    public interface IBotFabric
    {
        public event Action<BotModel> BotCreated;
        public BotModel CreateRandom(Vector2int position, int Step = 0);
        public bool CreateChild(BotModel parent, out BotModel child, int Step = 0);
    }
}
