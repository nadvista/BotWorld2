namespace BotWorld2Core.Game.General
{
    internal abstract class Updatable
    {
        protected GameCycleController _gameCycleController;
        public Updatable(GameCycleController cycleController)
        {
            _gameCycleController = cycleController;
            _gameCycleController.RemoveUpdatable(this);
        }
        public abstract void Update();
    }
}
