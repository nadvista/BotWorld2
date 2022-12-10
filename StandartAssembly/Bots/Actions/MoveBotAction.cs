using BotWorld2.StandartAssembly;
using BotWorld2Core.Game.Bots;
using BotWorld2Core.Game.World;
using StandartAssembly.Bots.Components;

namespace StandartAssembly.Bots.Actions
{
    public class MoveBotAction : BotAction
    {
        private readonly IWorldController _world;
        private BotPositionController _pos;

        public MoveBotAction(IWorldController world) : base()
        {
            _world = world;
        }
        public override void ModelCreated()
        {
            _pos = _self.GetComponent<BotPositionController>();
        }
        public override void Execute()
        {
            _pos.SetPosition(_pos.Position + _pos.Forward);
        }
    }
}
