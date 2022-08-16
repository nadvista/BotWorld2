using BotWorld2Core.Game.Bots.Actions;
using BotWorld2Core.Game.General;

namespace BotWorld2Core.Game.Bots
{
    internal class BotController : Updatable
    {
        private BotAction _lastAction;
        private bool _lastActionExecuted;
        private BotModel _model;

        public BotController(GameCycleController cycle, BotModel model) : base(cycle)
        {
            _model = model;
            _model.OnDead += OnDeadHandler;
        }

        public override void ThreadUpdate()
        {
            _lastActionExecuted = false;

            var datas = GetSensorsData();
            var answer = GetBrainAnswer(datas);

            var index = Array.IndexOf(answer, answer.Max());
            _lastAction = _model.Actions[index];

            if(!_lastAction.FreezeThread)
            {
                _lastAction.Execute();
                _lastActionExecuted = true;
            }    
        }
        public void OnDeadHandler(BotModel model)
        {
            RemoveUpdatable();
        }
        public override void Update()
        {
            if (!_lastActionExecuted)
                _lastAction.Execute();

            //безопасно, т.к при создании модели происходит проверка совпадения длины выходного уровня НС и кол-ва  Actions
            _model.Health--;
            _model.Age++;
        }

        private double[] GetBrainAnswer(double[] inputs) => _model.Brain.Calculate(inputs);
        private double[] GetSensorsData() => _model.Sensors.SelectMany(e => e.GetData()).ToArray();
    }
}
