using BotWorld2Core.Game.General;
using BotWorld2Core.Game.World;

namespace BotWorld2Core.Game.Bots.Actions
{
    internal class GetEnergyBotAction : BotAction
    {
        private readonly WorldController _world;

        public GetEnergyBotAction(WorldController world)
        {
            _world = world;
        }

        public override void Execute()
        {
            var cell = _world.GetCell(_self.Position);
            var bonusEnergy = cell.SunLevel * GameSettings.SunEnergyBonusMultiplyer;
            var bonusHealth = cell.SunLevel * GameSettings.SunEnergyBonusMultiplyer;

            _self.Energy += bonusEnergy;
            _self.Health += bonusHealth;
        }
    }
}
