using BotWorld2Core.Game.General;

namespace BotWorld2Core.Game.Bots.Sensors
{
    internal class BotParamsSensor : BotSensor
    {
        public override double[] GetData()
        {
            return new double[] { _self.Health / GameSettings.MaxHealth, _self.Energy / GameSettings.MaxEnergy };
        }

        public override int GetDataSize()
        {
            return 2;
        }
    }
}
