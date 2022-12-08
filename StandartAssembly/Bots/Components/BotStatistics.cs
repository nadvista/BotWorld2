using BotWorld2Core.Game.Bots;
using BotWorld2Core.Game.General;

namespace StandartAssembly.Bots.Components
{
    public class BotStatistics : BotComponent
    {
        public int Age { get; private set; }
        public int BotAte { get; private set; }
        public void AddAge(int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException($"Value {nameof(value)} must be >= 0");
            Age += value;
        }
        public void EatBot()
        {
            BotAte++;
        }
    }
}