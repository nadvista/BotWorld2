namespace BotWorld2Core.Game.Bots
{
    internal class BotComponent
    {
#pragma warning disable CS8618 // свойство "_self", не допускающий значения NULL, должен содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить свойство как допускающий значения NULL.
        protected BotModel _self { get; private set; }
#pragma warning restore CS8618 // свойство "_self", не допускающий значения NULL, должен содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить свойство как допускающий значения NULL.
        public void SetBot(BotModel self)
        {
            _self = self;
        }
    }
}
