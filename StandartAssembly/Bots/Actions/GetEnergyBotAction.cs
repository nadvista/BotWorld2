using BotWorld2Core.Game.Bots;
using BotWorld2Core.Game.General;
using BotWorld2Core.Game.World;
using StandartAssembly.Bots.Components;

namespace StandartAssembly.Bots.Actions
{
    public class GetEnergyBotAction : BotAction
    {
        public override bool FreezeThread => false;

        private readonly IWorldController _world;
        private BotPositionController _pos;
        private BotStatsController _stats;

        public GetEnergyBotAction(IWorldController world) : base()
        {
            _world = world;
        }
        public override void ModelCreated()
        {
            _pos = _self.GetComponent<BotPositionController>();
            _stats = _self.GetComponent<BotStatsController>();
        }
        public override void Execute()
        {
            var cell = _world.GetCell(_pos.Position);
            var bonusEnergy = cell.SunLevel * GameSettings.SunEnergyBonusMultiplyer;
            var bonusHealth = cell.SunLevel * GameSettings.SunEnergyBonusMultiplyer;

            var oldHealth = _stats.Health;
            _stats.AddEnergy(bonusEnergy);
            _stats.AddHealth(bonusHealth);
        }
    }
}
