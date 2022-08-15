namespace BotWorld2Core.Game.Bots.Sensors
{
    internal abstract class BotSensor : BotComponent
    {
        public abstract double[] GetData();
        public abstract int GetDataSize();
    }
}
