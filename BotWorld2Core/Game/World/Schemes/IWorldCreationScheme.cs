using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotWorld2Core.Game.World.Schemes
{
    internal interface IWorldCreationScheme
    {
        public WorldCell GetCell(int x, int y);
    }
}
