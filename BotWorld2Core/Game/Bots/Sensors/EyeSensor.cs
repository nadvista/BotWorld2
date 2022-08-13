using BotWorld2Core.Game.World;

namespace BotWorld2Core.Game.Bots.Sensors
{
    internal class EyeSensor : BotSensor
    {
        private WorldController _world;
        public EyeSensor(WorldController world)
        {
            _world = world;
        }
        public override double[] GetData()
        {
            var cell = _world.GetCell(_self.Position);
            return new double[] { cell.HasBot ? 1 : 0, cell.HasFood ? 1 : 0, cell.SunLevel, cell.CanStayHere ? 1 : 0 };
        }
    }
}
