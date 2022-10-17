using BotWorld2Core.Game.Bots.Components;
using BotWorld2Core.Game.General;
using BotWorld2Core.Game.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotWorld2Core.Game.Bots.Actions
{
    class PlaceFoodAction : BotAction
    {
        private IWorldController _world;
        private BotPositionController _pos;
        private BotStatsController _stats;
        public PlaceFoodAction(IWorldController world) : base()
        {
            _world = world;
        }

        public override void Execute()
        {
            var targetCell = _world.GetCell(_pos.Position + _pos.Forward);
            var healthBugSize = Math.Min(GameSettings.MaxBugPlaceSize, (_stats.Health - 1) > 0 ? _stats.Health - 1 : 0); // чтобы бот не мог убить себя таким способом, проверяем его хп
            var energyBugSize = Math.Min(GameSettings.MaxBugPlaceSize, _stats.Energy);

            targetCell.HealthFoodBug += healthBugSize;
            targetCell.EnergyFoodBug += energyBugSize;

            _stats.TakeHit(healthBugSize);
            _stats.SubtractEnergy(energyBugSize);
        }
        public override void ModelCreated()
        {
            _pos = _self.GetComponent<BotPositionController>();
            _stats = _self.GetComponent<BotStatsController>();
        }
    }
}
