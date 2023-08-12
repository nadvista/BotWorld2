using System;
using System.Linq;
using BotWorld2Core.Game.General;

namespace BotWorld2Core.Game.Bots
{
    public class BotController : Updatable
    {
        private BotAction _lastAction;
        private bool _lastActionExecuted;
        private readonly BotModel _model;

        public BotController(GameCycleController cycle, BotModel model) : base(cycle)
        {
            _model = model;
        }

        public override void ThreadUpdate()
        {
            if (!_model.Enabled)
                return;

            _lastActionExecuted = false;

            var datas = GetSensorsData();
            var answer = GetBrainAnswer(datas);

            var index = Array.IndexOf(answer, answer.Max());

            _lastAction = _model.Actions[index];

            if (!_lastAction.FreezeThread)
            {
                _lastAction.Execute();
                _lastActionExecuted = true;
            }
        }
        public void Remove()
        {
            RemoveUpdatable();
        }
        public override void Update()
        {
            if (!_model.Enabled)
                return;

            if (!_lastActionExecuted)
            {
                _lastAction.Execute();
            }

            for (int i = 0; i < _model.Scripts.Length; i++)
            {
                if (!_model.Enabled)
                    return;
                _model.Scripts[i].Update();
            }
        }

        private double[] GetBrainAnswer(double[] inputs) => _model.Brain.Calculate(inputs);
        private double[] GetSensorsData() => _model.Sensors.SelectMany(e => e.GetData()).ToArray();
    }
}
