using BotWorld2.StandartAssembly;
using BotWorld2.StandartAssembly.App;
using BotWorld2Core.Game.General;
using BotWorld2Core.Game.World;
using StandartAssembly.Bots;
using StandartAssembly.Drawing;
using StandartAssembly.Scripts;
using StandartAssembly.World;
using StandartAssembly.World.Schemes;

namespace StandartAssembly
{
    public class BotWordlApp
    {
        private BotWorldManager _manager;
        private StepsCounterScript _stepsCounter;
        private WorldController world;

        private readonly List<GameDrawer> _drawers = new List<GameDrawer>();
        private GameDrawer _currentDrawer => _drawers[_currentDrawerIndex];
        private int _currentDrawerIndex;

        private readonly Dictionary<ConsoleKey, Action> _handlers = new Dictionary<ConsoleKey, Action>();

        private bool _showOutput = true;
        private bool _pause = false;

        public void Run()
        {
            world = new WorldController(new IslandCreationScheme(), new Vector2int(GameSettings.WorldWidth, GameSettings.WorldHeight));
            var cycleController = new GameCycleController();
            _manager = new BotWorldManager(world, cycleController, new BotFabric(world, cycleController));
            _stepsCounter = new StepsCounterScript();

            _manager.OnCellUpdated += RedrawCell;
            SetupConsole();

            _manager.AddScript(new SunScript());
            _manager.AddScript(new FoodDispenserScript(world));
            _manager.AddScript(_stepsCounter);

            AddDrawers();
            AddHandlers();

            RedrawAll();
            StartGame();
        }

        private void AddHandlers()
        {
            _handlers.Add(ConsoleKey.UpArrow, SelectNextDrawer);
            _handlers.Add(ConsoleKey.DownArrow, SelectPreviousDrawer);
            _handlers.Add(ConsoleKey.Q, SwitchOutputMode);
            _handlers.Add(ConsoleKey.P, SwitchPause);
            _handlers.Add(ConsoleKey.R, GetRealCount);
        }
        private void AddDrawers()
        {
            _drawers.Add(new SunLevelDrawer());
            _drawers.Add(new SimpleMapDrawer());
            _drawers.Add(new HealthDrawer());
            _drawers.Add(new AgeDrawer());
            _drawers.Add(new AgressiveDrawer());
        }
        private void StartGame()
        {
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    if (_handlers.ContainsKey(key))
                        _handlers[key].Invoke();
                }

                if (_pause)
                {
                    continue;
                }
                _manager.Update();
                WriteInfo();

                if (_manager.BotsAlive < 4)
                {
                    _manager.Reset();
                    RedrawAll();
                }
            }
        }
        private void SetupConsole()
        {
            Console.SetWindowSize(1, 1);
            Console.SetBufferSize(480, 65);
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
        }
        private void SelectNextDrawer()
        {
            _currentDrawerIndex++;
            if (_currentDrawerIndex == _drawers.Count)
            {
                _currentDrawerIndex = 0;
            }
            _manager.OnCellUpdated += RedrawCell;
            RedrawAll();
        }
        private void SelectPreviousDrawer()
        {
            _currentDrawerIndex--;
            if (_currentDrawerIndex == -1)
            {
                _currentDrawerIndex = _drawers.Count - 1;
            }
            _manager.OnCellUpdated += RedrawCell;
            RedrawAll();
        }
        private void SwitchOutputMode()
        {
            _showOutput = !_showOutput;
            if (_showOutput) RedrawAll();
        }
        private void SwitchPause()
        {
            _pause = !_pause;
        }
        private void GetRealCount()
        {
            var count = world.GetRealBotsCount();
        }
        private void WriteInfo()
        {
            Console.SetCursorPosition(0, GameSettings.WorldHeight + 2);

            Console.WriteLine($"Current step {_stepsCounter.Step}--------------------------------------");
            Console.WriteLine($"BotsAlive {_manager.BotsAlive}--------------------------------------");
            Console.WriteLine($"Map mode - {_currentDrawer.DrawerName}");
        }
        private void RedrawAll()
        {
            for (int x = 0; x < GameSettings.WorldWidth; x++)
            {
                for (int y = 0; y < GameSettings.WorldHeight; y++)
                {
                    RedrawCell(new Vector2int(x, y));
                }
            }
        }
        private void RedrawCell(WorldCell cell)
        {
            Console.ResetColor();
            if (!_showOutput) return;
            Console.SetCursorPosition(cell.X, cell.Y);
            _currentDrawer.Draw(cell);
            Console.ResetColor();
        }
        private void RedrawCell(Vector2int cellPos)
        {
            var cell = _manager.GetCell(cellPos);
            RedrawCell(cell);
        }
    }
}
