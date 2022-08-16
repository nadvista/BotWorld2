namespace BotWorld2Core.Game.Bots.Actions
{
    internal class RotateLeftBotAction : BotAction
    {
        public override bool FreezeThread => false;

        public override void Execute()
        {
            _self.Forward.RotateLeft();
        }
    }
}
