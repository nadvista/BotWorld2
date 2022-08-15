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

#pragma warning disable CS8618 // событие "Updated", не допускающий значения NULL, должен содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить событие как допускающий значения NULL.
#pragma warning disable CS8618 // поле "_currentBot", не допускающий значения NULL, должен содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающий значения NULL.
        public WorldCell(bool isWall, bool hasFood, float sunLevel, int x, int y)
#pragma warning restore CS8618 // поле "_currentBot", не допускающий значения NULL, должен содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающий значения NULL.
#pragma warning restore CS8618 // событие "Updated", не допускающий значения NULL, должен содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить событие как допускающий значения NULL.
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
            _currentBot = model;
            Updated?.Invoke(this);
        }
        public void RemoveBot()
        {
            _currentBot = null;
            Updated?.Invoke(this);
        }
        public void PlaceFood()
        {
            HasFood = true;
            Updated?.Invoke(this);
        }
        public void TakeFood()
        {
            HasFood = false;
            Updated?.Invoke(this);
        }
        public void Reset()
        {
            _currentBot = null;
            HasFood = false;
            Updated?.Invoke(this);
        }
    }
}
