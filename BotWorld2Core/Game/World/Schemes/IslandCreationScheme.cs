using BotWorld2Core.Game.General;

namespace BotWorld2Core.Game.World.Schemes
{
    internal class IslandCreationScheme : IWorldCreationScheme
    {
        private const float radiusMultiplyer = .05f;

        public WorldCell GetCell(int x, int y)
        {
            var offsetX = x - GameSettings.WorldWidth / 2;
            var offsetY = y - GameSettings.WorldHeight / 2;

            var radius = Math.Sqrt(offsetX * offsetX + offsetY * offsetY) * radiusMultiplyer;
            var sunLevel = 1 / radius;

            var isWall = false;// x <= 0 || x >= GameSettings.WorldWidth - 1 || y <= 0 || y >= GameSettings.WorldHeight - 1;
            return new WorldCell(isWall, false, (float)sunLevel, x, y);
        }
    }
}
