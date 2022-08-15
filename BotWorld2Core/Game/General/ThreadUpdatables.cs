namespace BotWorld2Core.Game.General
{
    internal class ThreadUpdatables : IComparable<ThreadUpdatables>
    {
        public int Length => _updatables.Count;
        private Thread _thread;
        private List<Updatable> _updatables;
        public ThreadUpdatables()
        {
            _thread = new Thread(Update);
            _updatables = new List<Updatable>();
        }
        public void Run()
        {
            _thread.Start();
        }
        public void Update()
        {
            for (int i = 0; i < _updatables.Count; i++)
                _updatables[i].Update();
        }
        public bool IsStopped() => _thread.ThreadState != ThreadState.Running;
        public void Add(Updatable upd) => _updatables.Add(upd);
        public void Remove(Updatable upd) => _updatables.Remove(upd);
        public bool Contains(Updatable udp) => _updatables.Contains(udp);
        public int CompareTo(ThreadUpdatables? other)
        {
            return Length.CompareTo(other.Length);
        }
    }
}
