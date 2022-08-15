using BotWorld2Core.Game.General;
using BotWorld2Core.Game.World;

namespace BotWorld2Core
{
    public class Program
    {
#pragma warning disable CS8618 // поле "_manager", не допускающий значения NULL, должен содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающий значения NULL.
        private static GameManager _manager;
#pragma warning restore CS8618 // поле "_manager", не допускающий значения NULL, должен содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающий значения NULL.
        private static int _outputMode;
        private static bool _showOutput = true;
        private static bool _pause = false;

        public static void Main()
        {
            _manager = new GameManager();
            _manager.OnCellUpdated += RedrawCell;
            Console.SetWindowSize(1, 1);
            Console.SetBufferSize(480, 65);
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

            RedrawAll();
            while (true)
            {
                if (!_pause)
                {
                    _manager.Update();
                    WriteInfo();

                    if (_manager.BotsAlive < 4)
                    {
                        _manager.Reset();
                        RedrawAll();
                    }
                }

                if (Console.KeyAvailable)
                {
                    var keyPressed = Console.ReadKey(true);
                    switch (keyPressed.Key)
                    {
                        case ConsoleKey.DownArrow:
                            _outputMode--;
                            if(_outputMode == -1)
                            {
                                _outputMode = 3;
                            }
                            _manager.OnCellUpdated -= RedrawCell;
                            RedrawAll();
                            break;
                        case ConsoleKey.UpArrow:
                            _outputMode++;
                            if (_outputMode == 4)
                            {
                                _outputMode = 0;
                            }
                            _manager.OnCellUpdated += RedrawCell;
                            RedrawAll();
                            break;
                        case ConsoleKey.Q:
                            _showOutput = !_showOutput;
                            if (_showOutput) RedrawAll();
                            break;
                        case ConsoleKey.P:
                            _pause = !_pause;
                            break;
                    }
                }
            }
        }

        private static void WriteInfo()
        {
            Console.SetCursorPosition(0, GameSettings.WorldHeight + 2);

            Console.WriteLine($"Current step {_manager.Step}--------------------------------------");
            Console.WriteLine($"BotsAlive {_manager.BotsAlive}--------------------------------------");
            if (_outputMode == 0)
                Console.WriteLine("Output: Energy map");
            else if (_outputMode == 1)
                Console.WriteLine("Output: Standart map");
            else if (_outputMode == 2)
                Console.WriteLine("Output: Age map");
            else if (_outputMode == 3)
                Console.WriteLine("Output: Health map");
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
        private static void RedrawCell(WorldCell cell) => RedrawCell(new Vector2int(cell.X, cell.Y));
        private static void RedrawCell(Vector2int cellPos)
        {
            if (!_showOutput) return;
            Console.SetCursorPosition(cellPos.X, cellPos.Y);
            var cell = _manager.GetCell(cellPos);
            if (_outputMode == 0)
            {
                DrawEnergy(cell);
            }
            else if (_outputMode == 1)
            {
                DrawStandart(cell);
            }
            else if(_outputMode == 2)
            {
                DrawAge(cell);
            }
            else if(_outputMode == 3)
            {
                DrawHealth(cell);
            }
            Console.ResetColor();
        }

        private static void DrawHealth(WorldCell cell)
        {
            if (!cell.HasBot)
                return;
            var health = cell.GetBot().Health;
            ConsoleColor color = ConsoleColor.White;

            if (health < 50)
                color = ConsoleColor.DarkBlue;
            else if (health < 70)
                color = ConsoleColor.Blue;
            else if (health < 100)
                color = ConsoleColor.Yellow;
            else if (health < 150)
                color = ConsoleColor.Red;
            else if (health < 300)
                color = ConsoleColor.DarkRed;
            else if (health > 500)
                color = ConsoleColor.Green;
            Console.ForegroundColor = color;
            Console.Write('B');
        }
        private static void DrawAge(WorldCell cell)
        {
            if (!cell.HasBot)
                return;
            var botAge = cell.GetBot().Age;
            ConsoleColor color = ConsoleColor.White;

            if (botAge < 100)
                color = ConsoleColor.DarkBlue;
            else if (botAge < 200)
                color = ConsoleColor.Blue;
            else if (botAge < 300)
                color = ConsoleColor.Yellow;
            else if (botAge < 500)
                color = ConsoleColor.Red;
            else if (botAge < 700)
                color = ConsoleColor.DarkRed;
            else if (botAge > 1000)
                color = ConsoleColor.Green;
            Console.ForegroundColor = color;
            Console.Write('B');

        }
        private static void DrawStandart(WorldCell cell)
        {
            if (cell.IsWall)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write('#');
            }
            else if (cell.HasBot)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write('B');
            }
            else if (cell.HasFood)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write('B');
            }
            else Console.Write(' ');
        }

        private static void DrawEnergy(WorldCell cell)
        {
            ConsoleColor color;
            if (cell.SunLevel < .1)
                color = ConsoleColor.DarkBlue;
            else if (cell.SunLevel < .4)
                color = ConsoleColor.Blue;
            else if (cell.SunLevel < .8)
                color = ConsoleColor.Yellow;
            else if (cell.SunLevel < 1.2)
                color = ConsoleColor.Red;
            else if (cell.SunLevel < 1.8)
                color = ConsoleColor.DarkRed;
            else if (cell.SunLevel > 8)
                color = ConsoleColor.Green;
            else color = ConsoleColor.White;
            Console.ForegroundColor = color;
            Console.Write('#');
        }
    }
}
