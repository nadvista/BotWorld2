using System;
using System.Collections.Generic;
using System.Linq;

namespace BotWorld2Core.Game.Ai
{
    public class NeuronNetwork
    {
        private NeuronLayer[] _layers;
        public readonly int InputLayerLength;
        public readonly int OutputLayerLength;

        public NeuronNetwork(params NeuronLayer[] layers)
        {
            if(layers.Length < 2)
                throw new ArgumentException();

            _layers = new NeuronLayer[layers.Length];

            for (int i = 0; i < layers.Length-1; i++)
            {
                var currentLayer = layers[i];
                var nextLayer = layers[i + 1];
                currentLayer.SetNextLayer(nextLayer);
                _layers[i] = currentLayer;
            }
            _layers[^1] = layers[^1];
            InputLayerLength = _layers[0].Length;
            OutputLayerLength = _layers[^1].Length;
        }
        public NeuronNetwork(NetworkCreationScheme scheme)
        {
            _layers = new NeuronLayer[scheme.NeuronTypes.Length];
            //creating layers
            for (int layerIndex= 0; layerIndex < scheme.NeuronTypes.Length; layerIndex++)
            {
                var layerNeurons = new Neuron[scheme.NeuronTypes[layerIndex].Length];
                for (int neuronIndex = 0; neuronIndex < layerNeurons.Length; neuronIndex++)
                {
                    layerNeurons[neuronIndex] = (Neuron)Activator.CreateInstance(scheme.NeuronTypes[layerIndex][neuronIndex]);
                }
                _layers[layerIndex] = new NeuronLayer(layerNeurons);
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
            if(input.Length != InputLayerLength)
                throw new ArgumentException();
            if(_layers.Length < 2)
                throw new ArgumentException();

            _layers[0].SetLayerInput(input);
            for (int layerIndex = 0; layerIndex < _layers.Length - 1; layerIndex++)
            {
                var currentLayer = _layers[layerIndex];
                var nextLayer = _layers[layerIndex+1];

                var currentLayerOut = currentLayer.GetLayerOutput();
                nextLayer.SetLayerInput(currentLayerOut);
            }
            return _layers[^1].GetLayerOutput();
        }
        public void Clear()
        {
            foreach(var layer in _layers)
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
    }
}