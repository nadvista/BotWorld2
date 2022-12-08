using BotWorld2Core.Game.World;

namespace StandartAssembly.Drawing
{
    internal abstract class GameDrawer
    {
        public readonly string DrawerName;
        public GameDrawer(string drawerName)
        {
            DrawerName = drawerName;
        }

        public abstract void Draw(WorldCell cell);
    }
}
