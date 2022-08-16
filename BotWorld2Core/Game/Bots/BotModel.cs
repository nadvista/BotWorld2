using BotWorld2Core.Game.Ai;
using BotWorld2Core.Game.Bots.Actions;
using BotWorld2Core.Game.Bots.Sensors;
using BotWorld2Core.Game.General;

namespace BotWorld2Core.Game.Bots
{
    internal class BotModel
    {
        public event Action<BotModel> OnDead;

        public float MaxHealth => GameSettings.MaxHealth;
        public float MaxEnergy => GameSettings.MaxEnergy;
        public int Age { get; set; }
        public int BotAte { get; set; }
        public float Health
        {
            get => _health;
            set
            {
                _health = Math.Clamp(value, 0f, MaxHealth);
                if (_health == 0)
                    OnDead?.Invoke(this);
            }
        }
        public float Energy
        {
            get => _energy;
            set
            {
                _energy = Math.Clamp(value, 0f, MaxEnergy);
            }
        }

        public readonly int Birthday;
        public readonly int Color;
        public readonly NeuronNetwork Brain;
        public readonly BotSensor[] Sensors;
        public readonly BotAction[] Actions;

        public Vector2int Position;
        public Vector2int Forward;

        private float _health;
        private float _energy;
        private BotController _controller;

        public BotModel(GameCycleController cycleController, NeuronNetwork brain, BotSensor[] sensors, BotAction[] actions, Vector2int position, int birthday = 0, int color = 0)
        {
            if (brain == null
                || sensors == null || sensors.Any(e => e == null)
                || actions == null || actions.Any(e => e == null))
                throw new ArgumentNullException();
            if (brain.InputLayerLength != sensors.Sum(e => e.GetDataSize())
                || brain.OutputLayerLength != actions.Length)
                throw new ArgumentException();

            Brain = brain;
            Sensors = sensors;
            Actions = actions;
            Birthday = birthday;

            _health = GameSettings.StartHealth;
            _energy = GameSettings.StartEnergy;

            Position = position;
            Forward = new Vector2int(1, 0);

            BindComponents(sensors);
            BindComponents(actions);

            _controller = new BotController(cycleController, this);
            Color = color;
        }

        private void BindComponents(BotComponent[] sensors)
        {
            for (int i = 0; i < sensors.Length; i++)
                sensors[i].SetBot(this);
        }
    }
}
