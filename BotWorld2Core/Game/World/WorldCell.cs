using BotWorld2Core.Game.Bots;
using BotWorld2Core.Game.General;

namespace BotWorld2Core.Game.World
{
    public class WorldCell
    {
        public event Action<WorldCell> Updated;

        public bool HasBot => _currentBot != null;
        public bool CanStayHere => !IsWall && !HasBot;

        public ObservableVar<bool> HasFood { get; private set; }
        public float HealthFoodBug
        {
            get => _healthBug;
            set
            {
                _healthBug = Math.Max(0, value);
                Updated?.Invoke(this);
            }
        }
        public float EnergyFoodBug
        {
            get => _energyBug;
            set
            {
                _energyBug = Math.Max(0, value);
                Updated?.Invoke(this);
            }
        }

        public readonly bool IsWall;
        public readonly float SunLevel;
        public readonly int X, Y;
        private float _healthBug;
        private float _energyBug;
        private BotModel _currentBot;

        public WorldCell(bool isWall, bool hasFood, float sunLevel, int x, int y)
        {
            IsWall = isWall;
            HasFood = new ObservableVar<bool>(hasFood);
            SunLevel = sunLevel;
            X = x;
            Y = y;
        }

        public BotModel GetBot() => _currentBot;
        public void PlaceBot(BotModel model)
        {
            _currentBot = model;
            Updated?.Invoke(this);
        }
        public void RemoveBot(BotModel model)
        {
            _currentBot = null;
            Updated?.Invoke(this);
        }
        public void PlaceFood()
        {
            HasFood.Value = true;
            Updated?.Invoke(this);
        }
        public void TakeFood()
        {
            HasFood.Value = false;
            Updated?.Invoke(this);
        }
        public void Reset()
        {
            _currentBot = null;
            HasFood.Value = false;
            Updated?.Invoke(this);
            EnergyFoodBug = 0;
            HealthFoodBug = 0;
        }
    }
}
