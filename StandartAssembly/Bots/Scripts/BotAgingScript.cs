using BotWorld2Core.Game.Bots;
using StandartAssembly.Bots.Components;

namespace StandartAssembly.Bots.Scripts
{
    public class BotAgingScript : BotScript
    {
        private const int AGE_PER_STEP = 1;
        private BotStatistics _statistics;

        public override void ModelCreated()
        {
            _statistics = _self.GetComponent<BotStatistics>();
        }
        public override void Update()
        {
            _statistics.AddAge(AGE_PER_STEP);
        }
    }
}
