using BotWorld2Core.Game.General;
using BotWorld2Core.Game.World;

namespace BotWorld2Core.Game.Bots.Actions
{
    public class GetEnergyBotAction : BotAction
    {
        public override bool FreezeThread => false;

        private readonly IWorldController _world;

        public GetEnergyBotAction(IWorldController world)
        {
            _world = world;
        }

        public override void Execute()
        {
            var cell = _world.GetCell(_self.Position);
            var bonusEnergy = cell.SunLevel * GameSettings.SunEnergyBonusMultiplyer;
            var bonusHealth = cell.SunLevel * GameSettings.SunEnergyBonusMultiplyer;

            var oldHealth = _self.Health;
            _self.Energy += bonusEnergy;
            _self.Health += bonusHealth;
        }
    }
}
