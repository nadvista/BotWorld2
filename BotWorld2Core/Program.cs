using BotWorld2Core.Drawing;
using BotWorld2Core.Game.Bots;
using BotWorld2Core.Game.General;
using BotWorld2Core.Game.Scripts;
using BotWorld2Core.Game.World;
using BotWorld2Core.Game.World.Schemes;
using System;
using System.Collections.Generic;

namespace BotWorld2Core
{
    public class Program
    {
        private static WorldController _world;
        private static GameManager _manager;
        private static StepsCounterScript _stepsCounter;
        private static GameCycleController _cycleController;

        private static List<GameDrawer> _drawers = new List<GameDrawer>();
        private static GameDrawer _currentDrawer => _drawers[_currentDrawerIndex];
        private static int _currentDrawerIndex;

        private static Dictionary<ConsoleKey, Action> _handlers = new Dictionary<ConsoleKey, Action>();

        private static bool _showOutput = true;
        private static bool _pause = false;

        public static void Main()
        {
            _world = new WorldController(new IslandCreationScheme(), new Vector2int(GameSettings.WorldWidth, GameSettings.WorldHeight));
            _cycleController = new GameCycleController();
            _manager = new GameManager(_world, _cycleController, new BotFabric(_world,_cycleController));
            _stepsCounter = new StepsCounterScript();

            _manager.OnCellUpdated += RedrawCell;
            SetupConsole();

            _manager.AddScript(new SunScript());
            _manager.AddScript(new FoodDispenserScript(_world));
            _manager.AddScript(_stepsCounter);

            AddDrawers();
            AddHandlers();

            RedrawAll();
            StartGame();
        }

        private static void AddHandlers()
        {
            _handlers.Add(ConsoleKey.UpArrow, SelectNextDrawer);
            _handlers.Add(ConsoleKey.DownArrow, SelectPreviousDrawer);
            _handlers.Add(ConsoleKey.Q, SwitchOutputMode);
            _handlers.Add(ConsoleKey.P, SwitchPause);
        }

        private static void AddDrawers()
        {
            _drawers.Add(new SunLevelDrawer());
            _drawers.Add(new SimpleMapDrawer());
            _drawers.Add(new HealthDrawer());
            _drawers.Add(new AgeDrawer());
            _drawers.Add(new AgressiveDrawer());
        }

        private static void StartGame()
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

        private static void SetupConsole()
        {
            Console.SetWindowSize(1, 1);
            Console.SetBufferSize(480, 65);
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
        }

        private static void SelectNextDrawer()
        {
            _currentDrawerIndex++;
            if (_currentDrawerIndex == _drawers.Count)
            {
                _currentDrawerIndex = 0;
            }
            _manager.OnCellUpdated += RedrawCell;
            RedrawAll();
        }
        private static void SelectPreviousDrawer()
        {
            _currentDrawerIndex--;
            if (_currentDrawerIndex == -1)
            {
                _currentDrawerIndex = _drawers.Count - 1;
            }
            _manager.OnCellUpdated += RedrawCell;
            RedrawAll();
        }
        private static void SwitchOutputMode()
        {
            _showOutput = !_showOutput;
            if (_showOutput) RedrawAll();
        }
        private static void SwitchPause()
        {
            _pause = !_pause;
        }

        private static void WriteInfo()
        {
            Console.SetCursorPosition(0, GameSettings.WorldHeight + 2);

            Console.WriteLine($"Current step {_stepsCounter.Step}--------------------------------------");
            Console.WriteLine($"BotsAlive {_manager.BotsAlive}--------------------------------------");
            Console.WriteLine($"Map mode - {_currentDrawer.DrawerName}");
        }
        private static void RedrawAll()
        {
            for (int x = 0; x < GameSettings.WorldWidth; x++)
            {
                for (int y = 0; y < GameSettings.WorldHeight; y++)
                {
                    RedrawCell(new Vector2int(x, y));
                }
            }
        }
        private static void RedrawCell(WorldCell cell)
        {
            Console.ResetColor();
            if (!_showOutput) return;
            Console.SetCursorPosition(cell.X, cell.Y);
            _currentDrawer.Draw(cell);
            Console.ResetColor();
        }
        private static void RedrawCell(Vector2int cellPos)
        {
            var cell = _manager.GetCell(cellPos);
            RedrawCell(cell);
        }

    }
}
