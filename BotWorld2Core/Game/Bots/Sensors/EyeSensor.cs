using BotWorld2Core.Game.World;
using System.Collections.Generic;

namespace BotWorld2Core.Game.Bots.Sensors
{
    public class EyeSensor : BotSensor
    {
        private WorldController _world;

        public EyeSensor(WorldController world)
        {
            _world = world;
        }
        public override double[] GetData()
        {
            var forward = _self.Forward;
            var cell = _world.GetCell(_self.Position + _self.Forward);
            var answer = new List<double> { 
                cell.HasBot ? 1 : 0,
                cell.HasFood ? 1 : 0,
                cell.SunLevel,
                cell.CanStayHere ? 1 : 0};

            forward.RotateLeft();
            var cell2 = _world.GetCell(_self.Position + forward);
            answer.Add(cell2.HasBot ? 1 : 0);
            answer.Add(cell2.HasFood ? 1 : 0);

            forward.RotateLeft();
            var cell3 = _world.GetCell(_self.Position + forward);
            answer.Add(cell3.HasBot ? 1 : 0);
            answer.Add(cell3.HasFood ? 1 : 0);

            forward.RotateLeft();
            var cell4 = _world.GetCell(_self.Position + forward);
            answer.Add(cell4.HasBot ? 1 : 0);
            answer.Add(cell4.HasFood ? 1 : 0);


            return answer.ToArray(); ;
        }
        public override int GetDataSize()
        {
            return 10;
        }
    }
}
