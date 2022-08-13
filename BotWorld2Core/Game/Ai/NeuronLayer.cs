using BotWorld2Core.Game.General;

namespace BotWorld2Core.Game.Ai
{
    public class NeuronLayer
    {
        public int Length => _neurons.Length;

        private Neuron[] _neurons;
        private double[,] _weights;
        private NeuronLayer _nextLayer;

        public NeuronLayer(int neuronCount)
        {
            _neurons = new Neuron[neuronCount];
            for (int i = 0; i < neuronCount; i++)
            {
                _neurons[i] = new Neuron();
            }
        }
        public NeuronLayer(params Neuron[] neurons)
        {
            _neurons = neurons;
        }

        public void SetNextLayer(NeuronLayer nextLayer)
        {
            var rnd = Global.Random;
            _weights = new double[Length, nextLayer.Length];
            _nextLayer = nextLayer;
            for (int currentNeuron = 0; currentNeuron < Length; currentNeuron++)
            {
                for (int nextLayerNeuron = 0; nextLayerNeuron < nextLayer.Length; nextLayerNeuron++)
                {
                    _weights[currentNeuron, nextLayerNeuron] = rnd.NextDouble() * 2 - 1;
                }
            }
        }

        public void AddNeuronInput(int neuron, double input)
        {
            _neurons[neuron].AddInput(input);
        }
        public void SetLayerInput(double[] input)
        {
            if (input.Length != Length)
                throw new ArgumentException();

            for (int i = 0; i < input.Length; i++)
            {
                _neurons[i].Reset();
                _neurons[i].AddInput(input[i]);
            }
        }
        public double[] GetLayerOutput()
        {
            var answer = new double[_nextLayer != null ? _nextLayer.Length : _neurons.Length];
            for (int current = 0; current < Length; current++)
            {
                if (_nextLayer == null)
                {
                    answer[current] = _neurons[current].Activate();
                    continue;
                }

                for (int next = 0; next < _nextLayer.Length; next++)
                {
                    var weight = _weights[current, next];
                    var currentValue = _neurons[current].Activate();
                    answer[next] += weight * currentValue;
                }
            }
            return answer;
        }
        public void Clear()
        {
            foreach (var neuron in _neurons)
                neuron.Reset();
        }
        public double[,] ExportWeights() => _weights;
        public Type GetNeuronType(int neuron) => _neurons[neuron].GetType();
        public void ImportWeights(double[,] weights)
        {
            if (_nextLayer == null)
                return;
            if (Length != weights.GetLength(0) || _nextLayer.Length != weights.GetLength(1))
                throw new ArgumentException();
            _weights = weights;
        }
    }
}