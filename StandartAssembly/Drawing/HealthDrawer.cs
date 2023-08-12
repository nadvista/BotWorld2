using System;
using BotWorld2Core.Game.World;
using StandartAssembly.Bots.Components;

namespace StandartAssembly.Drawing
{
    internal class HealthDrawer : GameDrawer
    {
        public HealthDrawer() : base("Bots health ---------------")
        {
        }

        public override void Draw(WorldCell cell)
        {
            if (!cell.HasBot)
            {
                Console.Write(' ');
                return;
            }
            var health = cell.GetBot().GetComponent<BotStatsController>().Health;
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
    }
}
