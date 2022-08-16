namespace BotWorld2Core.Game.Bots.Actions
{
    internal class RotateRigthBotAction : BotAction
    {
        public override bool StopThread => false;
        public override void Execute()
        {
            _self.Forward.RotateRight();
        }
    }
}
