using BotWorld2Core.Game.Bots;
using BotWorld2Core.Game.World;
using StandartAssembly.Bots.Components;

namespace StandartAssembly.Bots.Sensors
{
    public class EyeSensor : BotSensor
    {
        private readonly IWorldController _world;
        private BotPositionController _pos;

        public EyeSensor(IWorldController world) : base()
        {
            _world = world;
        }
        public override double[] GetData()
        {
            var forward = _pos.Forward;
            var cell = _world.GetCell(_pos.Position + _pos.Forward);
            var answer = new List<double> {
                cell.HasBot ? 1 : 0,
                cell.HasFood.Value ? 1 : 0,
                cell.SunLevel,
                cell.CanStayHere ? 1 : 0};

            forward.RotateLeft();
            var cell2 = _world.GetCell(_pos.Position + forward);
            answer.Add(cell2.HasBot ? 1 : 0);
            answer.Add(cell2.HasFood.Value ? 1 : 0);

            forward.RotateLeft();
            var cell3 = _world.GetCell(_pos.Position + forward);
            answer.Add(cell3.HasBot ? 1 : 0);
            answer.Add(cell3.HasFood.Value ? 1 : 0);

            forward.RotateLeft();
            var cell4 = _world.GetCell(_pos.Position + forward);
            answer.Add(cell4.HasBot ? 1 : 0);
            answer.Add(cell4.HasFood.Value ? 1 : 0);


            return answer.ToArray(); ;
        }
        public override int GetDataSize()
        {
            return 10;
        }
        public override void ModelCreated()
        {
            _pos = _self.GetComponent<BotPositionController>();
        }
    }
}
