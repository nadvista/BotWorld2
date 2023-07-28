namespace BotWorld2Core.Pool
{
    public class ObjectsPool<T> where T : IPoolElement
    {
        private Stack<T> _active = new();
        private List<T> _inactive = new();

        private readonly IPoolFabric<T> _fabric;

        public ObjectsPool(IPoolFabric<T> fabric, int initializeCount)
        {
            _fabric = fabric;
            CreateElements(initializeCount);
        }

        public void CreateElements(int count)
        {
            for(int i = 0; i < count; i++)
                CreateElement();
        }
        public void CreateElement()
        {
            var element = _fabric.CreateNew();
            element.OnFree += SetActive;
            SetActive(element);
        }

        public T GetFree()
        {
            if(_active.Count == 0)
                CreateElement();
            return _active.Pop();
        }

        private void SetActive(IPoolElement element)
        {
            var elementOfT = (T)element;
            if(_active.Contains(elementOfT))
                return;
            if(_inactive.Contains(elementOfT))
                _inactive.Remove(elementOfT);
            _active.Push(elementOfT);

        }
    }
}