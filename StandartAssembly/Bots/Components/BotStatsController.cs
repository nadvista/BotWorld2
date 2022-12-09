using BotWorld2.StandartAssembly;
using BotWorld2Core.Game.Bots;
using BotWorld2Core.Game.World;

namespace StandartAssembly.Bots.Components
{
    public class BotStatsController : BotComponent
    {
        public event Action<BotModel> OnDead;
        public float Health { get; private set; } = GameSettings.StartHealth;
        public float Energy { get; private set; } = GameSettings.StartEnergy;
        private readonly IWorldController _worldController;

        public BotStatsController(IWorldController worldController) : base()
        {
            _worldController = worldController;
        }
        public void TakeHit(float damage)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException($"Value {nameof(damage)} must be >= 0");
            Health = Math.Max(0, Health - damage);
            if (Health == 0)
            {
                OnDead?.Invoke(_self);
                _self.Controller.Remove();
                _worldController.GetCell(_self.GetComponent<BotPositionController>().Position).RemoveBot();
            }
        }
        public void AddHealth(float value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException($"Value {nameof(value)} must be >= 0");
            Health = Math.Min(GameSettings.MaxHealth, Health + value);
        }
        public void SubtractEnergy(float damage)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException($"Value {nameof(damage)} must be >= 0");
            Energy = Math.Max(0, Energy - damage);
        }
        public void AddEnergy(float value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException($"Value {nameof(value)} must be >= 0");
            Energy = Math.Min(GameSettings.MaxHealth, Energy + value);
        }
    }
}