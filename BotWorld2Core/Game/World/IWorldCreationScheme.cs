namespace BotWorld2Core.Game.World
{
    public interface IWorldCreationScheme
    {
        public WorldCell GetCell(int x, int y);
    }
}
