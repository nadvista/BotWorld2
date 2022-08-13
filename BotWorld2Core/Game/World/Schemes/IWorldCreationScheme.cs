namespace BotWorld2Core.Game.World.Schemes
{
    internal interface IWorldCreationScheme
    {
        public WorldCell GetCell(int x, int y);
    }
}
