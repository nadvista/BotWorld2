using BotWorld2Core.Game.General;

namespace BotWorld2Core.Game.Bots
{
    internal class BotController : Updatable
    {
        private BotModel _model;

        public BotController(BotModel model) : base()
        {
            _model = model;
        }

        public override void Update()
        {
            var datas = GetSensorsData();
            var answer = GetBrainAnswer((double[])datas);

            var commandIndex = Array.IndexOf(answer, answer.Max());
            _model.Actions[commandIndex].Execute();
            //безопасно, т.к при создании модели происходит проверка совпадения длины выходного уровня НС и кол-ва  Actions

        }
        private double[] GetBrainAnswer(double[] inputs) => _model.Brain.Calculate(inputs);
        private double[] GetSensorsData() => _model.Sensors.SelectMany(e => e.GetData()).ToArray();
    }
}
