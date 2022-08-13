namespace BotWorld2Core.Game.Bots
{
    internal class BotComponent
    {
        protected BotModel _self { get; private set; }
        public void SetBot(BotModel self)
        {
            _self = self;
        }
    }
}
