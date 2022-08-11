using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotWorld2Core.Game.World
{
    internal class WorldController
    {
        public event Action<WorldCell> CellUpdated;
        public readonly int Width, Height;
        private WorldCell[,] _world;

        public WorldController(IWorldCreationScheme scheme, int width, int height)
        {
            _world = new WorldCell[width, height];
            Width = width;
            Height = height;
            InitializeCells(scheme);
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
                    cell.Updated += CellUpdatedHandler;
                    _world[x, y] = cell;
                }
            }
        }
        public WorldCell GetCell(int x, int y)
        {
            if (x >= 0 && x < Width && y >= 0 && y < Height)
                return _world[x, y];
            throw new ArgumentException();
        }
        private void CellUpdatedHandler(WorldCell cell)
        {
            CellUpdated?.Invoke(cell);
        }
    }
}
