using BotWorld2Core.Game.General.Pool;

namespace BotWorld2Core.Game.Bots
{
    public class BotComponent : IPoolElement
    {
        protected BotModel _self { get; private set; }

        private bool _isFree;

        public void SetBot(BotModel self)
        {
            _self = self;
        }
        public virtual void ModelCreated() { }

        public bool IsElementFree()
        {
            return _isFree;
        }

        public void OnCreate()
        {
            _isFree = true;
        }

        public void OnTake()
        {
            _isFree = false;
        }

        public void ReturnToPool()
        {
            _isFree = true;
        }
    }
}
