using BotWorld2Core.Game.General;
using BotWorld2Core.Game.World;

namespace StandartAssembly.World
{
    public class WorldController : IWorldController
    {
        public event Action<WorldCell> CellUpdated;

        protected WorldCell[,] _world;

        public int Width => _width;
        public int Height => _height;

        private readonly int _width, _height;

        public WorldController(IWorldCreationScheme scheme, Vector2int size)
        {
            _width = size.X;
            _height = size.Y;
            _world = new WorldCell[Width, Height];
            InitializeCells(scheme);
        }
        public WorldCell GetCell(Vector2int pos)
        {
            if (pos.X < 0)
                pos.X = Width + pos.X;
            if (pos.Y < 0)
                pos.Y = Height + pos.Y;
            pos.X %= Width;
            pos.Y %= Height;
            return _world[pos.X, pos.Y];
        }
        public void Reset()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    _world[x, y].Reset();
                }
            }
        }

        private void InitializeCells(IWorldCreationScheme scheme)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    var cell = scheme.GetCell(x, y);
                    cell.Updated += e => CellUpdated?.Invoke(e);
                    _world[x, y] = cell;
                }
            }
        }
    }
}
