namespace BotWorld2Core.Game.Bots.Actions
{
    internal class RotateRigthBotAction : BotAction
    {
        public override void Execute()
        {
            _self.Forward.RotateRight();
        }
    }
}
