using BotWorld2Core.Game.General;
using BotWorld2Core.Game.World;

namespace BotWorld2.BotWorld2Core.Game
{
    internal class BotWorldApp
    {
        protected readonly IWorldController _world;
        protected readonly GameManager _game;
        protected readonly GameCycleController _cycleController;
    }
}
