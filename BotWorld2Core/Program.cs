using BotWorld2Core.Game.General;
using BotWorld2Core.Game.World;
using System;

namespace BotWorld2Core
{
    public class Program
    {
        private static GameManager _manager;
        public static void Main()
        {
            _manager = new GameManager();
            _manager.OnCellUpdated += RedrawCell;
            Console.SetWindowSize(1, 1);
            Console.SetBufferSize(500, 65);
            Console.SetWindowSize(500, 65);
            RedrawAll();
            while (true)
            {
                _manager.Update();
                WriteInfo();
            }
        }

        private static void WriteInfo()
        {
            Console.SetCursorPosition(0, GameSettings.WorldHeight + 2);
            
            Console.WriteLine($"Current step {_manager.Step}--------------------------------------");
            Console.WriteLine($"BotsAlive {_manager.BotsAlive}--------------------------------------");
        }
        private static void RedrawAll()
        {
            for(int x = 0; x < GameSettings.WorldWidth; x++)
            {
                for(int y = 0; y < GameSettings.WorldHeight; y++)
                {
                    RedrawCell(new Vector2int(x, y));
                }
            }
        }
        private static void RedrawCell(WorldCell cell) => RedrawCell(new Vector2int(cell.X, cell.Y));
        private static void RedrawCell(Vector2int cellPos)
        {
            Console.SetCursorPosition(cellPos.X, cellPos.Y);
            var cell = _manager.GetCell(cellPos);
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
    }
}
