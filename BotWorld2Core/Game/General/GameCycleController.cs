namespace BotWorld2Core.Game.General
{
    internal class GameCycleController
    {
        private List<Updatable> _updatables = new List<Updatable>();
        private List<Updatable> _toRemove = new List<Updatable>();
        private List<Updatable> _toAdd = new List<Updatable>();

        public void AddUpdatable(Updatable updatable)
        {
            _toAdd.Add(updatable);
        }
        public void RemoveUpdatable(Updatable updatable)
        {
            _toRemove.Add(updatable);
        }
        public void Update()
        {
            foreach (var updatable in _updatables)
            {
                var updatableArgument = updatable;
                var task = Task.Run(() => updatableArgument.ThreadUpdate());
            }
            Task.WaitAll();
            _updatables.ForEach(e => e.Update()); //360.8

            foreach (var remove in _toRemove)
                _updatables.Remove(remove);
            _toRemove.Clear();
            foreach (var add in _toAdd)
                _updatables.Add(add);
            _toAdd.Clear();
        }
    }
}
