using BotWorld2Core.Game.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotWorld2Core.Game.World.Schemes
{
    internal class IslandCreationScheme : IWorldCreationScheme
    {
        private const int radiusMultiplyer = 10;
        public WorldCell GetCell(int x, int y)
        {
            var radius = Math.Sqrt(x * x + y * y);
            var isWall = x <= 0 || x >= GameSettings.WorldWidth - 1 || y <= 0 || y >= GameSettings.WorldHeight - 1;
            return new WorldCell(isWall, false, (float)(1 / radius * radiusMultiplyer), x, y);
        }
    }
}
