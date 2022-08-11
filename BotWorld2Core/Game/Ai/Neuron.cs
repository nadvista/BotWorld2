using System;

namespace BotWorld2Core.Game.Ai
{
    public class Neuron
    {
        private double _lastValue;

        public virtual double Activate()
        {
            return 1 / (1 + System.Math.Pow(System.Math.E, -_lastValue));
        }
        public void AddInput(double value)
        {
            _lastValue += value;
        }
        public void Reset()
        {
            _lastValue = 0;
        }
    }
}