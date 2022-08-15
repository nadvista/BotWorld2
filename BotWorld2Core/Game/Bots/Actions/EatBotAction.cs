using BotWorld2Core.Game.General;
using BotWorld2Core.Game.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotWorld2Core.Game.Bots.Actions
{
    internal class EatBotAction : BotAction
    {
        private readonly WorldController _world;

        public EatBotAction(WorldController world)
        {
            _world = world;
        }

        public override void Execute()
        {
            var targetCell = _world.GetCell(_self.Position + _self.Forward);
            if(targetCell.HasBot)
            {
                var targetBot = targetCell.GetBot();
                var bonusHealth = Math.Min(GameSettings.EatBotHealthBonus, targetBot.Health);
                var bonusEnergy = Math.Min(GameSettings.EatBotEnergyBonus, targetBot.Energy);

                targetBot.Health -= bonusHealth;
                targetBot.Energy -= bonusEnergy;

                _self.Health += bonusHealth;
                _self.Energy += bonusEnergy;
            }
            else if(targetCell.HasFood)
            {
                targetCell.TakeFood();
                _self.Health += GameSettings.EatFoodHealthBonus;
                _self.Energy += GameSettings.EatFoodEnergyBonus;
            }
            
        }
    }
}
