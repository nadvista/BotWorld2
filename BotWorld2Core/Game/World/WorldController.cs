using BotWorld2Core.Game.General;
using BotWorld2Core.Game.World.Schemes;

namespace BotWorld2Core.Game.World
{
    internal class WorldController
    {
        private static object locker = new();

        public event Action<WorldCell> CellUpdated;

        public readonly int Width, Height;
        private WorldCell[,] _world;
        private int _foodCount = 0;
        public WorldController(IWorldCreationScheme scheme, Vector2int size)
        {
            Width = size.X;
            Height = size.Y;
            _world = new WorldCell[Width, Height];
            InitializeCells(scheme);
        }
        public void Reset()
        {
            _foodCount = 0;
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    _world[x, y].Reset();
                }
            }
        }
        public void TakeFood(Vector2int pos)
        {
            if (GetCell(pos).HasFood)
                _foodCount--;
            GetCell(pos).TakeFood();
        }
        public bool HasFood(Vector2int pos) => GetCell(pos).HasFood;
        public void PlaceFood()
        {
            const int maxCycle = 25000;
            for (int i = 0; i < GameSettings.FoodPlaceByStep; i++)
            {
                if (_foodCount >= GameSettings.FoodMaxCount)
                    return;
                int x = 0;
                int y = 0;
                WorldCell cell;
                int cycle = 0;
                do
                {
                    x = Global.Random.Next(0, GameSettings.WorldWidth);
                    y = Global.Random.Next(0, GameSettings.WorldHeight);
                    cell = GetCell(new Vector2int(x, y));
                    if (++cycle == maxCycle) break;
                } while (cell.IsWall && cell.HasFood);

                if (cycle < maxCycle)
                    cell.PlaceFood();
                else cycle = 0;

                _foodCount++;
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
        public WorldCell GetCell(Vector2int pos)
        {
            lock (locker)
            {
                if (pos.X < 0)
                    pos.X = Width + pos.X;
                if (pos.Y < 0)
                    pos.Y = Width + pos.Y;
                pos.X %= Width;
                pos.Y %= Height;
                return _world[pos.X, pos.Y];
            }
        }
    }
}
