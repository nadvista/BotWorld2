using BotWorld2Core.Game.General;
using BotWorld2Core.Game.World.Schemes;
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

        public WorldController(IWorldCreationScheme scheme, Vector2int size)
        {
            Width = size.X;
            Height = size.Y;
            _world = new WorldCell[Width, Height];
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
        public WorldCell GetCell(Vector2int pos)
        {
            if (pos.X >= 0 && pos.X < Width && pos.Y >= 0 && pos.Y < Height)
                return _world[pos.X, pos.Y];
            throw new ArgumentException();
        }
        private void CellUpdatedHandler(WorldCell cell)
        {
            CellUpdated?.Invoke(cell);
        }
    }
}
