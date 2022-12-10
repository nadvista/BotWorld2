namespace BotWorld2Core.Game.General
{
    public class GameCycleController
    {
        private readonly List<Updatable> _updatables = new List<Updatable>();
        private readonly List<Updatable> _toRemove = new List<Updatable>();
        private readonly List<Updatable> _toAdd = new List<Updatable>();

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
            foreach (var remove in _toRemove)
                _updatables.Remove(remove);
            _toRemove.Clear();
            foreach (var add in _toAdd)
                _updatables.Add(add);
            _toAdd.Clear();

            Parallel.ForEach<Updatable>(_updatables, e => e.ThreadUpdate());
            for (int i = 0; i < _updatables.Count; i++)
            {
                _updatables[i].Update();
            }
        }
    }
}
