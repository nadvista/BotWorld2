namespace BotWorld2Core.Game.Ai
{
    public struct NetworkCreationScheme
    {
        public double[][,] Weights { get; private set; }
        public readonly Type[][] NeuronTypes;
        public NetworkCreationScheme(double[][,] weights, Type[][] neuronTypes)
        {
            Weights = weights;
            NeuronTypes = neuronTypes;
        }
    }
}