using BotWorld2Core.Game.Bots;
using StandartAssembly.Bots.Components;

namespace StandartAssembly.Bots.Scripts
{
    public class BotDamageDoerScript : BotScript
    {
        private const float DAMAGE_PER_STEP = 1;
        private BotStatsController _stats;

        public override void ModelCreated()
        {
            _stats = _self.GetComponent<BotStatsController>();
        }
        public override void Update()
        {
            _stats.TakeHit(DAMAGE_PER_STEP);
        }
    }
}
