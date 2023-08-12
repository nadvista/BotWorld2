using BotWorld2Core.Game.General.Pool;

namespace BotWorld2Core.Game.Ai
{
    public class NeuronsFabric : IPoolFabric<Neuron>
    {
        public Neuron CreateNew()
        {
            return new Neuron();
        }
    }
}