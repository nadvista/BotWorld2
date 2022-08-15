using BotWorld2Core.Game.Bots;
using BotWorld2Core.Game.World;
using BotWorld2Core.Game.World.Schemes;

namespace BotWorld2Core.Game.General
{
    internal class GameManager
    {
        public event Action<WorldCell> OnCellUpdated;
        
        public int Step { get; private set; }
        public int BotsAlive => _bots.Count;

        private readonly WorldController _worldController;
        private readonly GameCycleController _gameCycleController;
        private readonly BotFabric _fabric;

        private List<BotModel> _bots = new List<BotModel>();
        private List<BotModel> _born = new List<BotModel>();
        private List<BotModel> _dead = new List<BotModel>();

        public GameManager()
        {
            _worldController = new WorldController(new IslandCreationScheme(), new Vector2int(GameSettings.WorldWidth, GameSettings.WorldHeight));
            _gameCycleController = new GameCycleController();
            _fabric = new BotFabric(_worldController, _gameCycleController, this);

            _worldController.CellUpdated += e => OnCellUpdated?.Invoke(e);

            CreateBots();
        }
        public void Update()
        {
            Step++;
            _gameCycleController.TryCallUpdate();
            _worldController.PlaceFood();

            foreach(var bot in _dead)
            {
                _bots.Remove(bot);
                bot.OnDead -= BotDead;
            }
            foreach(var bot in _born)
            {
                _bots.Add(bot);
                bot.OnDead += BotDead;
                _worldController.GetCell(bot.Position).PlaceBot(bot);
            }    

            _dead.Clear();
            _born.Clear();
        }
        public void AddBot(BotModel model)
        {
            _born.Add(model);
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
                } while (!_worldController.GetCell(new Vector2int(x, y)).CanStayHere);
                //creating bot
                var bot = _fabric.CreateRandom(new Vector2int(x, y));
                _worldController.GetCell(bot.Position).PlaceBot(bot);
                _bots.Add(bot);
                bot.OnDead += BotDead;
            }
        }
        public WorldCell GetCell(Vector2int cellPos) => _worldController.GetCell(cellPos);
        private void BotDead(BotModel model)
        {
            _worldController.GetCell(model.Position).RemoveBot();
            _dead.Add(model);
        }
    }
}
