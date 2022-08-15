using BotWorld2Core.Game.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotWorld2Core.Game.Bots.Actions
{
    internal class CreateChildAction : BotAction
    {
        private BotFabric _fabric;
        private GameManager _manager;

        public CreateChildAction(BotFabric fabric, GameManager manager)
        {
            _fabric = fabric;
            _manager = manager;
        }

        public override void Execute()
        {
            if (_self.Energy < GameSettings.ChildEnergyCost)
                return;

            if (!_fabric.CreateChild(_self, out var child))
                return;
            _manager.AddBot(child);
            _self.Energy -= GameSettings.ChildEnergyCost;
        }   
    }
}
