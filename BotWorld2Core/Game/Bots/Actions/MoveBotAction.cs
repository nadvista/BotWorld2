using BotWorld2Core.Game.World;

namespace BotWorld2Core.Game.Bots.Actions
{
    internal class MoveBotAction : BotAction
    {
        private readonly WorldController _world;

        public MoveBotAction(WorldController world)
        {
            _world = world;
        }

        public override void Execute()
        {
            var currentCell = _world.GetCell(_self.Position);
            var targetCell = _world.GetCell(_self.Position + _self.Forward);
            if (targetCell.CanStayHere)
            {
                currentCell.RemoveBot();
                targetCell.PlaceBot(_self);
                _self.Position += _self.Forward;
            }
        }
    }
}
