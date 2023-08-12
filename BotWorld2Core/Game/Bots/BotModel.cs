using System;
using System.Linq;
using BotWorld2Core.Game.Ai;
using BotWorld2Core.Game.General;
using BotWorld2Core.Game.General.Pool;

namespace BotWorld2Core.Game.Bots
{
    public class BotModel : IPoolElement
    {
        public readonly Guid Id = Guid.NewGuid();
        public NeuronNetwork Brain { get; private set; }
        public BotSensor[] Sensors{ get; private set; }
        public BotAction[] Actions{ get; private set; }
        public BotScript[] Scripts{ get; private set; }
        public BotComponent[] Components{ get; private set; }
        public BotController Controller{ get; private set; }

        public bool Enabled => _enabled;

        private bool _enabled;
        private bool _isFree;

        public BotModel(GameCycleController cycleController) 
        {
            Controller = new BotController(cycleController, this);
        }
        public BotModel(GameCycleController cycleController, NeuronNetwork brain, BotSensor[] sensors, BotAction[] actions, BotScript[] scripts, BotComponent[] components) : this(cycleController)
        {
            Setup(cycleController, brain, sensors, actions, scripts, components);
        }
        public void Setup(GameCycleController cycleController, NeuronNetwork brain, BotSensor[] sensors, BotAction[] actions, BotScript[] scripts, BotComponent[] components)
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
            Scripts = scripts;
            Components = components;

            BindComponents(sensors);
            BindComponents(actions);
            BindComponents(scripts);
            BindComponents(components);

            foreach (var sens in sensors)
                sens.ModelCreated();
            foreach (var act in actions)
                act.ModelCreated();
            foreach (var script in scripts)
                script.ModelCreated();
        }
        public T GetComponent<T>() where T : BotComponent => (T)Components.First(e => e is T);
        public void Enable() => _enabled = true;
        public void Disable() => _enabled = false;

        private void BindComponents(BotComponent[] components)
        {
            for (int i = 0; i < components.Length; i++)
                components[i].SetBot(this);
        }

        public bool IsElementFree()
        {
            return _isFree;
        }

        public void OnCreate()
        {
            _isFree = true;
        }

        public void OnTake()
        {
            _isFree = false;
        }

        public void ReturnToPool()
        {
            _isFree = true;
            foreach(var component in Components)
                component.ReturnToPool();
            foreach(var script in Scripts)
                script.ReturnToPool();
            foreach(var sensor in Sensors)
                sensor.ReturnToPool();
            foreach(var action in Actions)
                action.ReturnToPool();
        }
    }
}
