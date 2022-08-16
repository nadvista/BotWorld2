namespace BotWorld2Core.Game.General
{
    internal abstract class Updatable
    {
        protected GameCycleController _gameCycleController;
        public Updatable(GameCycleController cycleController)
        {
            _gameCycleController = cycleController;
            _gameCycleController.AddUpdatable(this);
        }
        public abstract void ThreadUpdate();
        public abstract void Update();
        protected void RemoveUpdatable() => _gameCycleController.RemoveUpdatable(this);
    }
}
