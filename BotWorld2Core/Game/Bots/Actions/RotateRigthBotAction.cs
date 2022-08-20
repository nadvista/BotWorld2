namespace BotWorld2Core.Game.Bots.Actions
{
    public class RotateRigthBotAction : BotAction
    {
        public override bool FreezeThread => false;

        public override void Execute()
        {
            _self.Forward.RotateRight();
        }
    }
}
