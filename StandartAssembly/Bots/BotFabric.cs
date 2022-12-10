using BotWorld2.StandartAssembly;
using BotWorld2Core.Game.Ai;
using BotWorld2Core.Game.Bots;
using BotWorld2Core.Game.General;
using BotWorld2Core.Game.World;
using StandartAssembly.Bots.Actions;
using StandartAssembly.Bots.Components;
using StandartAssembly.Bots.Scripts;
using StandartAssembly.Bots.Sensors;
using System.Reflection;

namespace StandartAssembly.Bots
{
    public class BotFabric : IBotFabric
    {
        public event Action<BotModel> BotCreated;
        public event Action<BotModel> BotRemoved;

        private static readonly Type[] neuronsTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(e => typeof(Neuron).IsAssignableFrom(e))
            .ToArray();

        private readonly Random _random = Global.Random;
        private readonly GameCycleController _cycleController;
        private readonly IWorldController _world;

        public BotFabric(IWorldController worldController, GameCycleController cycleController)
        {
            _world = worldController;
            _cycleController = cycleController;
        }
        public BotModel CreateRandom(Vector2int position, int Step = 0)
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
            var components = CreateComponents();
            var sensors = CreateSensors();
            var actions = CreateActions();
            var scripts = CreateScripts();

            layers[0] = new NeuronLayer(sensors.Sum(e => e.GetDataSize()));
            layers[^1] = new NeuronLayer(actions.Length);

            var network = new NeuronNetwork(layers);
            var color = Global.Random.Next(GameSettings.BotColorsCount);

            var bot = new BotModel(_cycleController, network, sensors, actions, scripts, components);
            bot.Enable();
            SetupComponents(bot, position);
            return bot;
        }
        public bool CreateChild(BotModel parent, out BotModel child, int Step = 0)
        {
            var parentBrainScheme = parent.Brain.GetScheme();

            var parentPositionController = parent.GetComponent<BotPositionController>();
            //find child position
            var offset = parentPositionController.Forward;
            var parentPosition = parentPositionController.Position;
            var positionFound = false;
            for (int i = 0; i < 8; i++)
            {
                var cell = _world.GetCell(offset + parentPosition);
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
            var childPostion = parentPosition + offset;

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
            var components = CreateComponents();
            var sensors = CreateSensors();
            var actions = CreateActions();
            var scripts = CreateScripts();

            var childBrainScheme = new NetworkCreationScheme(weights, neurons);
            var network = new NeuronNetwork(childBrainScheme);

            child = new BotModel(_cycleController, network, sensors, actions, scripts, components);
            SetupComponents(child, childPostion);
            child.Enable();
            return true;
        }
        private void BotDeadHandler(BotModel model)
        {
            BotRemoved?.Invoke(model);
            model.GetComponent<BotStatsController>().OnDead -= BotDeadHandler;
        }
        private void OnChildCreatedHandler(BotModel model)
        {
            BotCreated?.Invoke(model);
        }
        private BotSensor[] CreateSensors()
        {
            return new BotSensor[] {
                new EyeSensor(_world),
                new PositionRotationSensor(),
                new BotParamsSensor()
            };
        }
        private BotAction[] CreateActions()
        {
            return new BotAction[] {
                new MoveBotAction(_world),
                new RotateLeftBotAction(),
                new RotateRigthBotAction(),
                new EatBotAction(_world),
                new GetEnergyBotAction(_world),
                new CreateChildAction(this, OnChildCreatedHandler),
                new PlaceFoodAction(_world)
            };
        }
        private BotScript[] CreateScripts()
        {
            return new BotScript[]
            {
                new BotDamageDoerScript(),
                new BotAgingScript(),
            };
        }
        private BotComponent[] CreateComponents()
        {
            return new BotComponent[]
            {
                new BotPositionController(_world),
                new BotStatistics(),
                new BotStatsController(_world)
            };
        }
        private void SetupComponents(BotModel bot, Vector2int pos)
        {
            var position = bot.GetComponent<BotPositionController>();
            position.SetPosition(pos);
            position.SetDirection(new Vector2int(1, 0));

            var stats = bot.GetComponent<BotStatsController>();
            stats.OnDead += BotDeadHandler;
        }
    }
}
