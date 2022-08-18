using BotWorld2Core.Game.World;

namespace BotWorld2Core.Drawing
{
    internal class AgeDrawer : GameDrawer
    {
        public AgeDrawer() : base("Age ---------------")
        {
        }

        public override void Draw(WorldCell cell)
        {
            if (!cell.HasBot)
            {
                Console.Write(' ');
                return;
            }

            var botAge = cell.GetBot().Age;
            ConsoleColor color = ConsoleColor.White;

            if (botAge < 50)
                color = ConsoleColor.DarkBlue;
            else if (botAge < 70)
                color = ConsoleColor.Blue;
            else if (botAge < 120)
                color = ConsoleColor.Yellow;
            else if (botAge < 160)
                color = ConsoleColor.Red;
            else if (botAge < 200)
                color = ConsoleColor.DarkRed;
            else if (botAge > 400)
                color = ConsoleColor.Green;
            Console.ForegroundColor = color;
            Console.Write('B');
        }
    }
}
