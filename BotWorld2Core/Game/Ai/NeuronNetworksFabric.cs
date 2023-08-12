using BotWorld2Core.Game.General.Pool;

namespace BotWorld2Core.Game.Ai
{
    public class NeuronNetworksFabric : IPoolFabric<NeuronNetwork>
    {
        public NeuronNetwork CreateNew()
        {
            return new NeuronNetwork();
        }
    }
}