using BotWorld2Core.Game.Ai;
using BotWorld2Core.Game.Bots.Actions;
using BotWorld2Core.Game.Bots.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BotWorld2Core.Game.Bots
{
    internal class BotFabric
    {
        private static Type[] neuronsTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(e => typeof(Neuron).IsAssignableFrom(e))
            .ToArray();
        private Random _random = Global.Random;

        public BotModel CreateRandom(int posX, int posY)
        {
            var layers = new NeuronLayer[2 + GameSettings.BotHiddenLayersCount];
            //создаем скрытые слои
            for (int i = 1; i < layers.Length - 1; i++)
            {
                var layerNeurons = new Neuron[GameSettings.BotHiddenLayersLenght];
                for (int neuronI = 0; neuronI < layerNeurons.Length; neuronI++)
                    layerNeurons[neuronI] = (Neuron)Activator.CreateInstance(neuronsTypes[_random.Next(0, neuronsTypes.Length)]);
                layers[i] = new NeuronLayer(layerNeurons);
            }
            var sensors = CreateSensors();
            var actions = CreateActions();

            layers[0] = new NeuronLayer(sensors.Length);
            layers[1] = new NeuronLayer(actions.Length);

            var network = new NeuronNetwork(layers);

            var bot = new BotModel(network, sensors, actions, posX, posY);
            return bot;
        }
        public BotModel CreateChild(BotModel parent)
        {
            var parentBrainScheme = parent.Brain.GetScheme();
        }

        private BotSensor[] CreateSensors()
        {
            throw new NotImplementedException();
        }

        private BotAction[] CreateActions()
        {
            throw new NotImplementedException();
        }
    }
}
