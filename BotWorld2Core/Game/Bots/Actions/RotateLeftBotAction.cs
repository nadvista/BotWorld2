using BotWorld2Core.Game.Bots.Components;

namespace BotWorld2Core.Game.Bots.Actions
{
    public class RotateLeftBotAction : BotAction
    {
        public override bool FreezeThread => false;
        private BotPositionController _pos;
        public override void ModelCreated()
        {
            _pos = _self.GetComponent<BotPositionController>();
        }
        public override void Execute()
        {
            _pos.Forward.RotateLeft();
        }
    }
}
