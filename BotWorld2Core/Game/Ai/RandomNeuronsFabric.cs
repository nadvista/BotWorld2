using BotWorld2Core.Game.General.Pool;

namespace BotWorld2Core.Game.Ai
{
    public class RandomNeuronsFabric : IPoolFabric<RandomNeuron>
    {
        public RandomNeuron CreateNew()
        {
            return new RandomNeuron();
        }
    }
}