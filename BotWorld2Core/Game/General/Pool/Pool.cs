using System.Collections.Generic;
using System.Linq;

namespace BotWorld2Core.Game.General.Pool
{
    public class Pool<T> where T: IPoolElement
    {
        private Queue<T> _freeElements = new Queue<T>();
        private List<T> _unfreeElements = new List<T>();
        private IPoolFabric<T> _fabric;

        public Pool(IPoolFabric<T> fabric, int initializeCount)
        {
            _fabric = fabric;
            CreateElements(initializeCount);
        }

        public T Take()
        {
            T element;
            if(_freeElements.Count > 0)
                element = _freeElements.Dequeue();
            else
            {
                element = _unfreeElements.FirstOrDefault(e => e.IsElementFree());
                if(element == null)
                    element = CreateOne();
            }
            element.OnTake();
            return element;
        }

        private void CreateElements(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var element = CreateOne();
                _freeElements.Enqueue(element);
            }
        }
        private T CreateOne()
        {
            var element = _fabric.CreateNew();
            element.OnCreate();
            return element;
        }
    }
}