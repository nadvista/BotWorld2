namespace BotWorld2Core.Game.Bots
{
    public class BotComponent
    {
        protected BotModel _self { get; private set; }

        public void SetBot(BotModel self)
        {
            _self = self;
        }
    }
}
