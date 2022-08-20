using BotWorld2Core.Game.General;
using BotWorld2Core.Game.World;
using System;

namespace BotWorld2Core.Game.Bots.Actions
{
    public class EatBotAction : BotAction
    {
        private readonly IWorldController _world;

        public EatBotAction(IWorldController world)
        {
            _world = world;
        }

        public override void Execute()
        {
            var targetPos = _self.Position + _self.Forward;
            var targetCell = _world.GetCell(targetPos);
            if (targetCell.HasBot)
            {
                var targetBot = targetCell.GetBot();
                var bonusHealth = Math.Min(GameSettings.EatBotHealthBonus, targetBot.Health);
                var bonusEnergy = Math.Min(GameSettings.EatBotEnergyBonus, targetBot.Energy);

                targetBot.Health -= bonusHealth;
                targetBot.Energy -= bonusEnergy;

                _self.Health += bonusHealth;
                _self.Energy += bonusEnergy;
                _self.BotAte++;
            }
            else if (targetCell.HasFood.Value)
            {
                targetCell.TakeFood();
                _self.Health += GameSettings.EatFoodHealthBonus;
                _self.Energy += GameSettings.EatFoodEnergyBonus;
            }
            else if(targetCell.HealthFoodBug > 0 || targetCell.EnergyFoodBug > 0)
            {
                var healthBonus = Math.Min(Math.Min(GameSettings.MaxHealth - _self.Health, targetCell.HealthFoodBug), GameSettings.MaxEatBugByStep);
                var energyBonus = Math.Min(Math.Min(GameSettings.MaxHealth - _self.Energy, targetCell.EnergyFoodBug), GameSettings.MaxEatBugByStep);
                targetCell.EnergyFoodBug -= energyBonus;
                targetCell.HealthFoodBug -= healthBonus;

                _self.Health += healthBonus;
                _self.Energy += energyBonus;

            }

        }
    }
}
