namespace BotWorld2Core.Game.Bots.Sensors
{
    public abstract class BotSensor : BotComponent
    {
        public abstract double[] GetData();
        public abstract int GetDataSize();
    }
}
