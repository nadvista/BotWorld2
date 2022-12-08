using BotWorld2Core.Game.World;
using StandartAssembly.Bots.Components;

namespace StandartAssembly.Drawing
{
    internal class AgressiveDrawer : GameDrawer
    {
        public AgressiveDrawer() : base("Agressive ---------------")
        {
        }

        public override void Draw(WorldCell cell)
        {
            if (!cell.HasBot)
            {
                Console.Write(' ');
                return;
            }
            var aggr = cell.GetBot().GetComponent<BotStatistics>().BotAte;
            ConsoleColor color = ConsoleColor.White;

            if (aggr < 1)
                color = ConsoleColor.DarkBlue;
            else if (aggr < 3)
                color = ConsoleColor.Blue;
            else if (aggr < 5)
                color = ConsoleColor.Yellow;
            else if (aggr < 7)
                color = ConsoleColor.Red;
            else if (aggr < 9)
                color = ConsoleColor.DarkRed;
            else if (aggr > 11)
                color = ConsoleColor.Yellow;
            Console.ForegroundColor = color;
            Console.Write('B');
        }
    }
}
