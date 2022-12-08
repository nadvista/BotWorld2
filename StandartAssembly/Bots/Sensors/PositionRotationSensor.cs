using BotWorld2Core.Game.Bots;
using StandartAssembly.Bots.Components;

namespace StandartAssembly.Bots.Sensors
{
    public class PositionRotationSensor : BotSensor
    {
        private BotPositionController _pos;

        public override double[] GetData()
        {
            return new double[] { _pos.Position.X, _pos.Position.Y, _pos.Forward.X, _pos.Forward.Y };
        }
        public override int GetDataSize()
        {
            return 4;
        }
        public override void ModelCreated()
        {
            _pos = _self.GetComponent<BotPositionController>();
        }
    }
}
