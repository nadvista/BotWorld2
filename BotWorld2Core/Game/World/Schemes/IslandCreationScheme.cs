using BotWorld2Core.Game.General;

namespace BotWorld2Core.Game.World.Schemes
{
    internal class IslandCreationScheme : IWorldCreationScheme
    {
        private const float radiusMultiplyer = .1f;

        public WorldCell GetCell(int x, int y)
        {
            Vector2int offset1 = new Vector2int(x ,y) - new Vector2int(GameSettings.WorldWidth / 4, GameSettings.WorldHeight / 2);
            Vector2int offset2 = new Vector2int(x, y) - new Vector2int(GameSettings.WorldWidth * 3 / 4, GameSettings.WorldHeight / 2);

            

            var radius = Math.Sqrt(offset1.X * offset1.X + offset1.Y * offset1.Y) * radiusMultiplyer;
            var radius2 = Math.Sqrt(offset2.X * offset2.X + offset2.Y * offset2.Y) * radiusMultiplyer;

            var sunLevel = 1 / (Math.Min(radius, radius2)+0.00000000001);

            var isWall = false;// x <= 0 || x >= GameSettings.WorldWidth - 1 || y <= 0 || y >= GameSettings.WorldHeight - 1;
            return new WorldCell(isWall, false, (float)sunLevel, x, y);
        }
    }
}
