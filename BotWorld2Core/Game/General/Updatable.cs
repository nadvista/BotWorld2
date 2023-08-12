using BotWorld2Core.Game.General.Pool;

namespace BotWorld2Core.Game.General
{
    public abstract class Updatable : IPoolElement
    {
        protected GameCycleController _gameCycleController;
        private bool _isFree;

        public Updatable(GameCycleController cycleController) : base()
        {
            SetCycleController(cycleController);
        }

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
            _gameCycleController.AddUpdatable(this);
        }

        public void ReturnToPool()
        {
            _isFree = true;
            RemoveUpdatable();
        }

        public void SetCycleController(GameCycleController newController)
        {
            if (_gameCycleController != null)
                RemoveUpdatable();
            _gameCycleController = newController;
            if(!_isFree)
                _gameCycleController.AddUpdatable(this);
        }
        public abstract void ThreadUpdate();
        public abstract void Update();

        protected void RemoveUpdatable()
        {
            _gameCycleController.RemoveUpdatable(this);
        }
    }
}
