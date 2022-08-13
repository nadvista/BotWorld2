using BotWorld2Core.Game.Bots;
using BotWorld2Core.Game.World;
using BotWorld2Core.Game.World.Schemes;

namespace BotWorld2Core.Game.General
{
    internal class GameManager
    {
        private readonly WorldController _worldController;
        private readonly GameCycleController _gameCycleController;
        private readonly BotFabric _fabric;
        private List<BotModel> _bots = new List<BotModel>();
        public GameManager()
        {
            _worldController = new WorldController(new IslandCreationScheme(), new Vector2int(GameSettings.WorldWidth, GameSettings.WorldHeight));
            _gameCycleController = new GameCycleController();
            _fabric = new BotFabric(_worldController);

        }

    }
}
