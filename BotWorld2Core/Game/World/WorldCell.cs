using BotWorld2Core.Game.Bots;

namespace BotWorld2Core.Game.World
{
    internal class WorldCell
    {
        public event Action<WorldCell> Updated;
        public bool HasBot => _currentBot != null;
        public bool CanStayHere => !IsWall && !HasBot;
        public bool HasFood { get; private set; }

        public readonly bool IsWall;
        public readonly float SunLevel;
        public readonly int X, Y;

        private BotModel _currentBot;

        public WorldCell(bool isWall, bool hasFood, float sunLevel, int x, int y)
        {
            IsWall = isWall;
            HasFood = hasFood;
            SunLevel = sunLevel;
            X = x;
            Y = y;
        }
        public BotModel GetBot() => _currentBot;
        public void PlaceBot(BotModel model)
        {
            object locker = new();
            lock (locker)
            {
                _currentBot = model;
                Updated?.Invoke(this);
            }
        }
        public void RemoveBot()
        {
            object locker = new();
            lock (locker)
            {
                _currentBot = null;
                Updated?.Invoke(this);
            }
        }
        public void PlaceFood()
        {
            object locker = new();
            lock (locker)
            {
                HasFood = true;
                Updated?.Invoke(this);
            }
        }
        public void TakeFood()
        {
            object locker = new();
            lock (locker)
            {
                HasFood = false;
                Updated?.Invoke(this);
            }
        }
        public void Reset()
        {
            object locker = new();
            lock (locker)
            {
                _currentBot = null;
                HasFood = false;
                Updated?.Invoke(this);
            }
        }
    }
}
