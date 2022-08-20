namespace BotWorld2Core.Game.World.Schemes
{
    public interface IWorldCreationScheme
    {
        public WorldCell GetCell(int x, int y);
    }
}
