using BotWorld2Core.Game.General;
using BotWorld2Core.Game.World.Schemes;

namespace BotWorld2Core.Game.World
{
    internal class WorldController
    {
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
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    _world[x, y].Reset();
                }
            }
        }
        public void PlaceFood()
        {
            for (int i = 0; i < GameSettings.FoodPlaceByStep; i++)
            {
                if (_foodCount >= GameSettings.FoodMaxCount)
                    return;
                int x = 0;
                int y = 0;
                WorldCell cell;
                do
                {
                    x = Global.Random.Next(0, GameSettings.WorldWidth);
                    y = Global.Random.Next(0, GameSettings.WorldHeight);
                    cell = GetCell(new Vector2int(x, y));
                } while (cell.CanStayHere && !cell.HasFood);
                cell.PlaceFood();

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
            if (pos.X >= 0 && pos.X < Width && pos.Y >= 0 && pos.Y < Height)
                return _world[pos.X, pos.Y];
            throw new ArgumentException();
        }
    }
}
