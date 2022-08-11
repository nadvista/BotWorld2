namespace BotWorld2Core.Game.Ai
{
    public class RandomNeuron : Neuron
    {
        public override double Activate()
        {
            return Global.Random.NextDouble();
        }
    }
}