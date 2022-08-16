namespace BotWorld2Core.Game.General
{
    internal abstract class Updatable
    {
        protected GameCycleController _gameCycleController;

        public Updatable()
        { }
        public Updatable(GameCycleController cycleController) : base()
        {
            SetCycleController(cycleController);
        }
        public void SetCycleController(GameCycleController newController)
        {
            if (_gameCycleController != null)
                RemoveUpdatable();
            _gameCycleController = newController;
            _gameCycleController.AddUpdatable(this);
        }
        public abstract void ThreadUpdate();
        public abstract void Update();

        protected void RemoveUpdatable() => _gameCycleController.RemoveUpdatable(this);
    }
}
