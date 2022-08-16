namespace BotWorld2Core.Game.Bots.Actions
{
    internal class RotateLeftBotAction : BotAction
    {
        public override bool StopThread => false;
        public override void Execute()
        {
            _self.Forward.RotateLeft();
        }
    }
}
