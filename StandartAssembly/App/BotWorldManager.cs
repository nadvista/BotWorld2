using BotWorld2.StandartAssembly;
using BotWorld2Core.Game.Bots;
using BotWorld2Core.Game.General;
using BotWorld2Core.Game.World;

namespace BotWorld2.StandartAssembly.App
{
    public class BotWorldManager : GameManager
    {
        private readonly List<BotModel> _born = new List<BotModel>();
        private readonly List<BotModel> _dead = new List<BotModel>();

        public BotWorldManager(IWorldController world, GameCycleController cycleController, IBotFabric fabric) : base(world, cycleController, fabric)
        {
            _fabric.BotCreated += AddBot;
            _fabric.BotRemoved += RemoveBot;
            CreateBots();
        }

        public void Update()
        {
            _gameCycleController.Update();

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
        public void AddBot(BotModel model)
        {
            _born.Add(model);
        }
        public void Reset()
        {
            base.Reset();

            _born.Clear();
            _dead.Clear();
            CreateBots();
        }
        public void RemoveBot(BotModel model)
        {
            _dead.Add(model);
        }
        private void CreateBots()
        {
            for (int i = 0; i < GameSettings.StartBotsCount; i++)
            {
                //find free cell
                int x = 0, y = 0;
                WorldCell cell;
                do
                {
                    x = Global.Random.Next(0, _worldController.Width);
                    y = Global.Random.Next(0, _worldController.Height);
                    cell = _worldController.GetCell(new Vector2int(x, y));
                } while (!cell.CanStayHere);
                //creating bot
                var bot = _fabric.CreateRandom(new Vector2int(x, y));
                AddBot(bot);
            }
        }
    }
}
