using BotWorld2Core.Game.General.Pool;

namespace BotWorld2Core.Game.Ai
{
    public class Neuron : IPoolElement
    {
        private double _lastValue;
        private bool _isFree;

        public virtual double Activate()
        {
            return 1 / (1 + System.Math.Pow(System.Math.E, -_lastValue));
        }
        public void AddInput(double value)
        {
            _lastValue += value;
        }

        public bool IsElementFree()
        {
            return _isFree;
        }

        public void OnCreate()
        {
            _isFree = true;
        }

        public void OnTake()
        {
            Reset();
            _isFree = false;
        }

        public void Reset()
        {
            _lastValue = 0;
        }

        public void ReturnToPool()
        {
            _isFree = true;
        }
    }
}