namespace BotWorld2Core.Game.Bots.Sensors
{
    internal class PositionRotationSensor : BotSensor
    {

        public override double[] GetData()
        {
            return new double[] { _self.Position.X, _self.Position.Y, _self.Forward.X, _self.Forward.Y };
        }

        public override int GetDataSize()
        {
            return 4;
        }
    }
}
