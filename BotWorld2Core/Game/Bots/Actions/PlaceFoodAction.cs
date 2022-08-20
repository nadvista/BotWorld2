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
        private WorldController _world;

        public PlaceFoodAction(WorldController world)
        {
            _world = world;
        }

        public override void Execute()
        {
            var targetCell = _world.GetCell(_self.Position + _self.Forward);
            var healthBugSize = Math.Min(GameSettings.MaxBugPlaceSize, (_self.Health - 1)> 0? _self.Health-1:0); // чтобы бот не мог убить себя таким способом, проверяем его хп
            var energyBugSize = Math.Min(GameSettings.MaxBugPlaceSize, _self.Energy);

            targetCell.HealthFoodBug += healthBugSize;
            targetCell.EnergyFoodBug += energyBugSize;

            _self.Health -= healthBugSize;
            _self.Energy -= energyBugSize;
        }
    }
}
