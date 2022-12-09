using BotWorld2.StandartAssembly;
using BotWorld2Core.Game.Bots;
using StandartAssembly.Bots.Components;

namespace StandartAssembly.Bots.Actions
{
    public class CreateChildAction : BotAction
    {
        private readonly IBotFabric _fabric;
        private readonly Action<BotModel> _onCreated;
        private BotStatsController _stats;

        public CreateChildAction(IBotFabric fabric, Action<BotModel> createdChildHandler) : base()
        {
            _fabric = fabric;
            _onCreated = createdChildHandler;
        }
        public override void ModelCreated()
        {
            _stats = _self.GetComponent<BotStatsController>();
        }
        public override void Execute()
        {
            if (_stats.Energy < GameSettings.ChildEnergyCost)
                return;

            if (!_fabric.CreateChild(_self, out var child))
                return;
            _onCreated.Invoke(child);
            _stats.SubtractEnergy(GameSettings.ChildEnergyCost);
        }
    }
}
