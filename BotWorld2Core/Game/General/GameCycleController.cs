namespace BotWorld2Core.Game.General
{
    internal class GameCycleController
    {
        private List<Updatable> _updatables = new List<Updatable>();
        public void AddUpdatable(Updatable updatable)
        {
            _updatables.Add(updatable);
        }

        private ThreadUpdatables[] _threads = new ThreadUpdatables[GameSettings.ThreadsCount];

        public bool TryCallUpdate()
        {
            SetupThreads();
            if (!_threads.All(e => e == null || e.IsStopped())) 
                return false;
            for (int i = 0; i < _threads.Length; i++)
            {
                var thread = _threads[i];
                thread.Update();
            }
            return true;
        }

        private void SetupThreads()
        {
            var updatablesPerThreadFloat = _updatables.Count / (float)GameSettings.ThreadsCount;
            var updatablesPerThreadInt = (int)Math.Ceiling(updatablesPerThreadFloat);
            var updatablesAdded = 0;

            for (int i = 0; i < GameSettings.ThreadsCount - 1; i++)
            {
                var currentUpdatables = _updatables.GetRange(i * updatablesPerThreadInt, updatablesPerThreadInt).ToArray();
                var threadUpdatables = new ThreadUpdatables(currentUpdatables);
                updatablesAdded += currentUpdatables.Length;

                _threads[i] = threadUpdatables;
            }
            if (updatablesAdded > 0)
            {
                var lastUpdatables = _updatables.GetRange(updatablesAdded - 1, _updatables.Count - updatablesAdded).ToArray();
                _threads[^1] = new ThreadUpdatables(lastUpdatables);
            }

        }
    }
}
