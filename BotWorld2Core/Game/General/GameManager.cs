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
            _fabric = new BotFabric(_worldController, _gameCycleController);
            
            CreateBots();
            
        }

        private void CreateBots()
        {
            for (int i = 0; i < GameSettings.StartBotsCount; i++)
            {
                //find free cell
                int x = 0, y = 0;
                do
                {
                    x = Global.Random.Next(0, _worldController.Width);
                    y = Global.Random.Next(0, _worldController.Height);
                } while (_worldController.GetCell(new Vector2int(x, y)).CanStayHere);
                var bot = _fabric.CreateRandom(new Vector2int(x, y));
                _worldController.GetCell(bot.Position).PlaceBot(bot);
                _bots.Add(bot);
            }
        }
    }
}
