namespace BotWorld2Core.Game.Bots.Actions
{
    internal class RotateLeftBotAction : BotAction
    {
        public override void Execute()
        {
            _self.Forward.RotateLeft();
        }
    }
}
