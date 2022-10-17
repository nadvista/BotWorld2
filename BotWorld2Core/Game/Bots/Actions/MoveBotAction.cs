using BotWorld2Core.Game.Bots.Components;
using BotWorld2Core.Game.General;
using BotWorld2Core.Game.World;

namespace BotWorld2Core.Game.Bots.Actions
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
            var currentCell = _world.GetCell(_pos.Position);
            var targetCell = _world.GetCell(_pos.Position + _pos.Forward);
            if (targetCell.CanStayHere)
            {
                currentCell.RemoveBot();
                targetCell.PlaceBot(_self);
                var newPos = _pos.Position;
                newPos += _pos.Forward;
                if (_pos.Position.Y < 0)
                    newPos.Y = GameSettings.WorldHeight + _pos.Position.Y;
                if (_pos.Position.X < 0)
                    newPos.X = GameSettings.WorldWidth + _pos.Position.X;
                _pos.SetPosition(newPos);
            }
        }
    }
}
