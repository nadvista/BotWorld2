using System;
using System.Collections.Generic;
using System.Linq;
using BotWorld2Core.Game.General.Pool;

namespace BotWorld2Core.Game.Ai
{
    public class NeuronNetwork : IPoolElement
    {
        private static Pool<Neuron> _neuronsPool = new Pool<Neuron>(new NeuronsFabric(), 10000);
        private static Pool<RandomNeuron> _randomNeuronsPool = new Pool<RandomNeuron>(new RandomNeuronsFabric(), 10000);
        private static Pool<NeuronLayer> _neuronLayersPool = new Pool<NeuronLayer>(new NeuronLayerFabric(), 10000);

        private const int MAX_MEMORY_LEN = 300;

        public int InputLayerLength { get; private set; }
        public int OutputLayerLength { get; private set; }

        private NeuronLayer[] _layers;
        private Dictionary<string, double[]> _memory = new Dictionary<string, double[]>(300);
        private bool _isFree;

        public NeuronNetwork() {}
        public NeuronNetwork(NetworkCreationScheme scheme)
        {
            Setup(scheme);
        }
        public void Setup(NetworkCreationScheme scheme)
        {
            _layers = new NeuronLayer[scheme.NeuronTypes.Length];
            //creating layers
            for (int layerIndex = 0; layerIndex < scheme.NeuronTypes.Length; layerIndex++)
            {
                var layerNeurons = new Neuron[scheme.NeuronTypes[layerIndex].Length];
                for (int neuronIndex = 0; neuronIndex < layerNeurons.Length; neuronIndex++)
                {
                    //layerNeurons[neuronIndex] = (Neuron)Activator.CreateInstance(scheme.NeuronTypes[layerIndex][neuronIndex]);
                    var neuron = scheme.NeuronTypes[layerIndex][neuronIndex] == typeof(Neuron) ? _neuronsPool.Take() : _randomNeuronsPool.Take();
                    layerNeurons[neuronIndex] = neuron;
                }
                var layer = _neuronLayersPool.Take();
                layer.Setup(layerNeurons);
                _layers[layerIndex] = layer;
            }
            //binding layers
            for (int layerIndex = 0; layerIndex < _layers.Length - 1; layerIndex++)
            {
                var currentLayer = _layers[layerIndex];
                var nextLayer = _layers[layerIndex + 1];
                currentLayer.SetNextLayer(nextLayer);
            }

            InputLayerLength = _layers[0].Length;
            OutputLayerLength = _layers[^1].Length;
            ImportWeights(scheme.Weights);
        }
        public double[] Calculate(double[] input)
        {
            var key = string.Join("", input);
            if (_memory.ContainsKey(key))
                return (double[])_memory[key].Clone();

            if (input.Length != InputLayerLength)
                throw new ArgumentException();
            if (_layers.Length < 2)
                throw new ArgumentException();

            _layers[0].SetLayerInput(input);
            for (int layerIndex = 0; layerIndex < _layers.Length - 1; layerIndex++)
            {
                var currentLayer = _layers[layerIndex];
                var nextLayer = _layers[layerIndex + 1];

                var currentLayerOut = currentLayer.GetLayerOutput();
                nextLayer.SetLayerInput(currentLayerOut);
            }
            var output = _layers[^1].GetLayerOutput();

            if (_memory.Count < MAX_MEMORY_LEN && !_memory.ContainsKey(key))
                _memory.Add(key, output);

            return output;
        }
        public void Clear()
        {
            foreach (var layer in _layers)
                layer.Clear();
        }
        public double[][,] ExportWeights()
        {
            return (double[][,])_layers.Select(e => e.ExportWeights()).ToArray().Clone();
        }
        public void ImportWeights(double[][,] weights)
        {
            for (int layerIndex = 0; layerIndex < _layers.Length; layerIndex++)
            {
                var layer = _layers[layerIndex];
                layer.ImportWeights(weights[layerIndex]);
            }
        }
        public NetworkCreationScheme GetScheme()
        {
            var neuronsTypes = new Type[_layers.Length][];
            for (int layerIndex = 0; layerIndex < _layers.Length; layerIndex++)
            {
                neuronsTypes[layerIndex] = new Type[_layers[layerIndex].Length];
                var layer = _layers[layerIndex];
                for (int neuronIndex = 0; neuronIndex < layer.Length; neuronIndex++)
                    neuronsTypes[layerIndex][neuronIndex] = layer.GetNeuronType(neuronIndex);
            }
            return new NetworkCreationScheme(ExportWeights(), neuronsTypes);
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
            _isFree = false;
        }

        public void ReturnToPool()
        {
            _isFree = true;
            foreach(var layer in _layers)
                layer.ReturnToPool();
        }
    }
}