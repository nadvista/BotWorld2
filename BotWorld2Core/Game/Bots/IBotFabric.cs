using BotWorld2Core.Game.General;
namespace BotWorld2Core.Game.Bots
{
    public interface IBotFabric
    {
        public event Action<BotModel> BotCreated;
        public event Action<BotModel> BotRemoved;
        public BotModel CreateRandom(Vector2int position, int Step = 0);
        public bool CreateChild(BotModel parent, out BotModel child, int Step = 0);
    }
}
