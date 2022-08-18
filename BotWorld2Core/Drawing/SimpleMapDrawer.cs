using BotWorld2Core.Game.World;

namespace BotWorld2Core.Drawing
{
    internal class SimpleMapDrawer : GameDrawer
    {
        public SimpleMapDrawer() : base("Simple map ---------------")
        {
        }

        public override void Draw(WorldCell cell)
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
    }
}
