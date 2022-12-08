using System.Collections.Generic;
using System.Threading.Tasks;

namespace BotWorld2Core.Game.General
{
    public class GameCycleController
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
            List<Task> tasks = new List<Task>(_updatables.Count);
            for (int i = 0; i < _updatables.Count; i++)
            {
                var updatable = _updatables[i];
                var updatableArgument = updatable;
                var task = Task.Run(() => updatableArgument.ThreadUpdate());

                tasks.Add(task);
            }
            Task.WaitAll(tasks.ToArray());
            for (int i = 0; i < _updatables.Count; i++)
            {
                _updatables[i].Update();
            }

            foreach (var remove in _toRemove)
                _updatables.Remove(remove);
            _toRemove.Clear();
            foreach (var add in _toAdd)
                _updatables.Add(add);
            _toAdd.Clear();
        }
    }
}
