using BotWorld2Core.Game.General;
using BotWorld2Core.Game.World;

namespace BotWorld2Core
{
    public class Program
    {
        private static GameManager _manager = new GameManager();

        private static List<GameDrawer> _drawers = new List<GameDrawer>();
        private static GameDrawer _currentDrawer => _drawers[_currentDrawerIndex];
        private static int _currentDrawerIndex;

        private static Dictionary<ConsoleKeyInfo, Action> _handlers = new Dictionary<ConsoleKeyInfo, Action>();

        private static bool _showOutput = true;
        private static bool _pause = false;

        public static void Main()
        {
            _manager.OnCellUpdated += RedrawCell;
            Console.SetWindowSize(1, 1);
            Console.SetBufferSize(480, 65);
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

            RedrawAll();
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    if (_handlers.ContainsKey(key))
                        _handlers[key].Invoke();
                }

                if (_pause)
                {
                    return;
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

            Console.WriteLine($"Current step {_manager.Step}--------------------------------------");
            Console.WriteLine($"BotsAlive {_manager.BotsAlive}--------------------------------------");
            Console.WriteLine(_currentDrawer.DrawerName);
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
