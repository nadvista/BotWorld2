using BotWorld2Core.Game.Bots.Components;
using BotWorld2Core.Game.General;
using BotWorld2Core.Game.World;
using System;

namespace BotWorld2Core.Game.Bots.Actions
{
    public class EatBotAction : BotAction
    {
        private readonly IWorldController _world;
        private BotPositionController _pos;
        private BotStatsController _stats;
        private BotStatistics _statistics;

        public EatBotAction(IWorldController world) : base()
        {
            _world = world;
        }
        public override void ModelCreated()
        {
            _pos = _self.GetComponent<BotPositionController>();
            _stats = _self.GetComponent<BotStatsController>();
            _statistics = _self.GetComponent<BotStatistics>();
        }
        public override void Execute()
        {
            var targetPos = _pos.Position + _pos.Forward;
            var targetCell = _world.GetCell(targetPos);
            if (targetCell.HasBot)
            {
                var targetBot = targetCell.GetBot().GetComponent<BotStatsController>();
                var bonusHealth = Math.Min(GameSettings.EatBotHealthBonus, targetBot.Health);
                var bonusEnergy = Math.Min(GameSettings.EatBotEnergyBonus, targetBot.Energy);

                targetBot.TakeHit(bonusHealth);
                targetBot.SubtractEnergy(bonusEnergy);

                _stats.AddHealth(bonusHealth);
                _stats.AddEnergy(bonusEnergy);
                _statistics.EatBot();
            }
            else if (targetCell.HasFood.Value)
            {
                targetCell.TakeFood();
                _stats.AddHealth(GameSettings.EatFoodHealthBonus);
                _stats.AddEnergy(GameSettings.EatFoodEnergyBonus);
            }
            else if (targetCell.HealthFoodBug > 0 || targetCell.EnergyFoodBug > 0)
            {
                var healthBonus = Math.Min(Math.Min(GameSettings.MaxHealth - _stats.Health, targetCell.HealthFoodBug), GameSettings.MaxEatBugByStep);
                var energyBonus = Math.Min(Math.Min(GameSettings.MaxHealth - _stats.Energy, targetCell.EnergyFoodBug), GameSettings.MaxEatBugByStep);
                targetCell.EnergyFoodBug -= energyBonus;
                targetCell.HealthFoodBug -= healthBonus;

                _stats.AddHealth(healthBonus);
                _stats.AddEnergy(energyBonus);

            }

        }
    }
}
