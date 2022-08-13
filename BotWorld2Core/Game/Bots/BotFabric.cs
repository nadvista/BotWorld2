using BotWorld2Core.Game.Ai;
using BotWorld2Core.Game.Bots.Actions;
using BotWorld2Core.Game.Bots.Sensors;
using BotWorld2Core.Game.General;
using BotWorld2Core.Game.World;
using System.Reflection;

namespace BotWorld2Core.Game.Bots
{
    internal class BotFabric
    {
        private static Type[] neuronsTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(e => typeof(Neuron).IsAssignableFrom(e))
            .ToArray();
        private Random _random = Global.Random;
        private WorldController _world;
        public BotFabric(WorldController worldController)
        {
            _world = worldController;
        }
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

            var bot = new BotModel(network, sensors, actions, new Vector2int(posX, posY));
            return bot;
        }
        public bool CreateChild(BotModel parent, out BotModel child)
        {
            var parentBrainScheme = parent.Brain.GetScheme();
            //find child position
            var offset = parent.Forward;
            var parentPosition = parent.Position;
            var positionFound = false;
            for (int i = 0; i < 8; i++)
            {
                var cell = _world.GetCell(offset + parent.Position);
                if (cell != null && cell.CanStayHere)
                {
                    positionFound = true;
                    break;
                }
                offset.RotateRight();
            }
            if (!positionFound)
            {
                child = null;
                return false;
            }
            var childPostion = parent.Position + offset;
            //mutate child weights
            var weights = new double[parentBrainScheme.Weights.Length][,];
            for (int layer = 0; layer < parentBrainScheme.Weights.Length - 1; layer++)
            {
                var layerWeights = parentBrainScheme.Weights[layer];
                weights[layer] = new double[layerWeights.GetLength(0), layerWeights.GetLength(1)];
                for (int layerNeuron = 0; layerNeuron < layerWeights.GetLength(0); layerNeuron++)
                {
                    for (int nextNeuron = 0; nextNeuron < layerWeights.GetLength(1); nextNeuron++)
                    {
                        var weight = layerWeights[layerNeuron, nextNeuron];
                        var chance = _random.NextDouble();
                        if (chance <= GameSettings.MutationChance)
                            weight = _random.NextDouble() * 2 - 1;
                        weights[layer][layerNeuron, nextNeuron] = weight;
                    }
                }
            }
            //mutate child neurons
            var neurons = new Type[parentBrainScheme.NeuronTypes.Length][];
            for (int layer = 0; layer < parentBrainScheme.NeuronTypes.Length; layer++)
            {
                neurons[layer] = new Type[parentBrainScheme.NeuronTypes[layer].Length];
                for (int neuron = 0; neuron < parentBrainScheme.NeuronTypes[layer].Length; neuron++)
                {
                    var type = parentBrainScheme.NeuronTypes[layer][neuron];
                    var chance = _random.NextDouble();
                    if (chance <= GameSettings.MutationChance)
                        type = neuronsTypes[_random.Next(0, neuronsTypes.Length)];
                    neurons[layer][neuron] = type;
                }
            }
            //add components
            var sensors = CreateSensors();
            var actions = CreateActions();

            var childBrainScheme = new NetworkCreationScheme(weights, neurons);
            var network = new NeuronNetwork(childBrainScheme);

            child = new BotModel(network, sensors, actions, childPostion);
            return true;
        }

        private BotSensor[] CreateSensors()
        {
            return new BotSensor[] {
                new EyeSensor(_world),
                new PositionRotationSensor()
            };
        }

        private BotAction[] CreateActions()
        {
            return new BotAction[] {
                new MoveBotAction(_world)
            };
        }
    }
}
