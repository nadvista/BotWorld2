using BotWorld2Core.Game.General;
using BotWorld2Core.Game.World;

namespace StandartAssembly.Drawing
{
    internal class SunLevelDrawer : GameDrawer
    {
        public SunLevelDrawer() : base("Sun level drawer ---------------")
        {
        }

        public override void Draw(WorldCell cell)
        {
            ConsoleColor color;
            var level = cell.SunLevel * GameSettings.SunShare;
            if (level < .1)
                color = ConsoleColor.DarkBlue;
            else if (level < .4)
                color = ConsoleColor.Blue;
            else if (level < .8)
                color = ConsoleColor.Yellow;
            else if (level < 1.2)
                color = ConsoleColor.Red;
            else if (level < 1.8)
                color = ConsoleColor.DarkRed;
            else if (level > 8)
                color = ConsoleColor.Green;
            else color = ConsoleColor.White;
            Console.ForegroundColor = color;
            Console.Write('#');
        }
    }
}
