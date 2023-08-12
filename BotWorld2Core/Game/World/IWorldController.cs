using System;
using BotWorld2Core.Game.General;

namespace BotWorld2Core.Game.World
{
    public interface IWorldController
    {
        public int Width { get; }
        public int Height { get; }
        public event Action<WorldCell> CellUpdated;
        public WorldCell GetCell(Vector2int position);
        public void Reset();
    }
}
