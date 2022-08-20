using BotWorld2Core.Game.General;

namespace BotWorld2Core.Game.Bots.Actions
{
    public class CreateChildAction : BotAction
    {
        private IBotFabric _fabric;
        private Action<BotModel> _onCreated;

        public CreateChildAction(IBotFabric fabric, Action<BotModel> createdChildHandler)
        {
            _fabric = fabric;
            _onCreated = createdChildHandler;
        }

        public override void Execute()
        {
            if (_self.Energy < GameSettings.ChildEnergyCost)
                return;

            if (!_fabric.CreateChild(_self, out var child))
                return;
            _onCreated.Invoke(child);
            _self.Energy -= GameSettings.ChildEnergyCost;
        }
    }
}
