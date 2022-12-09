using BotWorld2Core.Game;

namespace StandartAssembly.Scripts
{
    internal class StepsCounterScript : Script
    {
        public int Step { get; private set; } = 0;

        public override void Reset()
        {
            Step = 0;
        }

        public override void Update()
        {
            Step++;
        }
    }
}
