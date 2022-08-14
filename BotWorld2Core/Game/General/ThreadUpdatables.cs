namespace BotWorld2Core.Game.General
{
    internal class ThreadUpdatables
    {
        private Thread _thread;
        private Updatable[] _updatables;
        public ThreadUpdatables(Updatable[] updatables)
        {
            _thread = new Thread(Update);
            _updatables = updatables;

        }
        public void Run()
        {
            _thread.Start();
        }
        public void Update()
        {
            for (int i = 0; i < _updatables.Length; i++)
                _updatables[i].Update();
        }
        public bool IsStopped() => _thread.ThreadState != ThreadState.Running;
    }
}
