using BotWorld2Core.Game.General;
using BotWorld2Core.Game.World;

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
            }
            else if (_world.HasFood(targetPos))
            {
                _world.TakeFood(targetPos);
                _self.Health += GameSettings.EatFoodHealthBonus;
                _self.Energy += GameSettings.EatFoodEnergyBonus;
            }

        }
    }
}
