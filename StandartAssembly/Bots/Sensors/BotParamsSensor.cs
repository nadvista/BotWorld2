using BotWorld2Core.Game.Bots;
using BotWorld2Core.Game.General;
using StandartAssembly.Bots.Components;

namespace StandartAssembly.Bots.Sensors
{
    public class BotParamsSensor : BotSensor
    {
        private BotStatsController _stats;
        public override double[] GetData()
        {
            return new double[] { _stats.Health / GameSettings.MaxHealth, _stats.Energy / GameSettings.MaxEnergy };
        }
        public override int GetDataSize()
        {
            return 2;
        }
        public override void ModelCreated()
        {
            _stats = _self.GetComponent<BotStatsController>();
        }
    }
}
