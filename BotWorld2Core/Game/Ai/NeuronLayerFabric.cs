using BotWorld2Core.Game.General.Pool;

namespace BotWorld2Core.Game.Ai
{
    public class NeuronLayerFabric : IPoolFabric<NeuronLayer>
    {
        public NeuronLayer CreateNew()
        {
            return new NeuronLayer();
        }
    }
}