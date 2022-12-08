using BotWorld2Core.Game.General;
using BotWorld2Core.Game.World;

namespace StandartAssembly.World.Schemes
{
    public class RandomCreationScheme : IWorldCreationScheme
    {
        public WorldCell GetCell(int x, int y)
        {
            var offsetX = x - GameSettings.WorldWidth / 2;
            var offsetY = y - GameSettings.WorldHeight / 2;

            var sunLevel = Global.Random.NextDouble() * Global.Random.Next(2);

            var isWall = false;//= x <= 0 || x >= GameSettings.WorldWidth - 1 || y <= 0 || y >= GameSettings.WorldHeight - 1;
            return new WorldCell(isWall, false, (float)sunLevel, x, y);
        }
    }
}
