using BotWorld2Core.Game.General;

namespace BotWorld2Core.Game.Bots
{
    internal class BotController : Updatable
    {
        private static object locker = new();
       
        private BotModel _model;

        public BotController(GameCycleController cycle, BotModel model) : base(cycle)
        {
            _model = model;
            _model.OnDead += OnDeadHandler;
        }

        public override void Update()
        {
            var datas = GetSensorsData();
            var answer = GetBrainAnswer(datas);

            var commandIndex = Array.IndexOf(answer, answer.Max());
            lock (locker)
            {
                _model.Actions[commandIndex].Execute();


                _model.Health--;
                //безопасно, т.к при создании модели происходит проверка совпадения длины выходного уровня НС и кол-ва  Actions
                _model.Age++;
            }

        }
        private double[] GetBrainAnswer(double[] inputs) => _model.Brain.Calculate(inputs);
        private double[] GetSensorsData() => _model.Sensors.SelectMany(e => e.GetData()).ToArray();
        public void OnDeadHandler(BotModel model)
        {
            RemoveUpdatable();
        }
    }
}
