using BotWorld2Core.Game.Bots;
using StandartAssembly.Bots.Components;

namespace StandartAssembly.Bots.Actions
{
    public class RotateRigthBotAction : BotAction
    {
        public override bool FreezeThread => false;
        private BotPositionController _pos;
        public override void ModelCreated()
        {
            _pos = _self.GetComponent<BotPositionController>();
        }

        public override void Execute()
        {
            var rotation = _pos.Forward;
            rotation.RotateRight();
            _pos.SetDirection(rotation);
        }
    }
}
