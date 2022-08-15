namespace BotWorld2Core.Game.General
{
    internal class GameCycleController
    {
        private ThreadUpdatables[] _threads = new ThreadUpdatables[GameSettings.ThreadsCount];

        public GameCycleController()
        {
            _threads = new ThreadUpdatables[GameSettings.ThreadsCount];
            for(int i = 0; i < _threads.Length; i++)
            {
                _threads[i] = new ThreadUpdatables();
            }
        }
        public void AddUpdatable(Updatable updatable)
        {
            var thread = _threads.Min();
            thread.Add(updatable);
        }

        public void RemoveUpdatable(Updatable updatable)
        {
            var thread = _threads.First(e => e.Contains(updatable));
            thread.Remove(updatable);
        }

        public bool TryCallUpdate()
        {
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
        }
    }
}
