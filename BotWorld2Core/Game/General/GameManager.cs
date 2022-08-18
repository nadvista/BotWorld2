using BotWorld2Core.Game.Bots;
using BotWorld2Core.Game.Scripts;
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
        private List<Script> _scripts = new List<Script>();

        public GameManager(WorldController world)
        {
            _worldController = world;
            _worldController.CellUpdated += e => OnCellUpdated?.Invoke(e);
            _gameCycleController = new GameCycleController();
            _fabric = new BotFabric(_worldController, _gameCycleController, this);
            CreateBots();
        }

        public void Update()
        {
            _gameCycleController.Update();

            Step++;
            _worldController.PlaceFood();

            foreach (var bot in _dead)
            {
                _bots.Remove(bot);
            }
            foreach (var bot in _born)
            {
                _bots.Add(bot);
            }

            _dead.Clear();
            _born.Clear();
        }
        public void AddScript(Script script)
        {
            script.SetCycleController(_gameCycleController);
            _scripts.Add(script);
        }
        public void AddBot(BotModel model)
        {
            _born.Add(model);
            model.OnDead += BotDead;
        }
        public WorldCell GetCell(Vector2int cellPos) => _worldController.GetCell(cellPos);
        public void Reset()
        {
            _bots.Clear();
            _born.Clear();
            _dead.Clear();
            _worldController.Reset();
            Step = 0;
            CreateBots();
            _scripts.ForEach(e => e.Reset());
        }

        private void BotDead(BotModel model)
        {
            _dead.Add(model);
            model.OnDead -= BotDead;
            _worldController.GetCell(model.Position).RemoveBot();
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
                AddBot(bot);
            }
        }
    }
}
