using BotWorld2Core.Game.Bots;
using BotWorld2Core.Game.General;
using BotWorld2Core.Game.World;

namespace StandartAssembly.Bots.Components
{
    public class BotPositionController : BotComponent
    {
        public Vector2int Position { get; private set; }
        public Vector2int Forward { get; private set; }

        private IWorldController _world;
        private bool _initialized;

        public BotPositionController(IWorldController worldController)
        {
            _world = worldController;
        }
        public void SetPosition(int x, int y)
        {
            SetPosition(new Vector2int(x, y));
        }
        public void SetPosition(Vector2int pos)
        {
            if (Position.Equals(pos))
                return;

            var currentCell = _world.GetCell(Position);

            var targetCell = _world.GetCell(pos);
            if (targetCell.CanStayHere)
            {
                if(_initialized)
                currentCell.RemoveBot(_self);
                targetCell.PlaceBot(_self);

                Position = new Vector2int(targetCell.X, targetCell.Y);
            }
            if (!_initialized)
                _initialized = true;
        }
        public void SetDirection(int x, int y)
        {
            Forward = new Vector2int(x, y);
        }
        public void SetDirection(Vector2int forward)
        {
            Forward = forward;
        }
    }
}
