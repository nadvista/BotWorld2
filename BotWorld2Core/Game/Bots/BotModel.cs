using BotWorld2Core.Game.Ai;
using BotWorld2Core.Game.Bots.Actions;
using BotWorld2Core.Game.Bots.Scripts;
using BotWorld2Core.Game.Bots.Sensors;
using BotWorld2Core.Game.General;
using System;
using System.Linq;

namespace BotWorld2Core.Game.Bots
{
    public class BotModel
    {
        public readonly NeuronNetwork Brain;
        public readonly BotSensor[] Sensors;
        public readonly BotAction[] Actions;
        public readonly BotScript[] Scripts;
        public readonly BotComponent[] Components;
        public readonly BotController Controller;

        public BotModel(GameCycleController cycleController, NeuronNetwork brain, BotSensor[] sensors, BotAction[] actions, BotScript[] scripts, BotComponent[] components)
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

            Controller = new BotController(cycleController, this);
        }
        public T GetComponent<T>() where T : BotComponent => (T)Components.First(e => e is T);

        private void BindComponents(BotComponent[] components)
        {
            for (int i = 0; i < components.Length; i++)
                components[i].SetBot(this);
        }
    }
}
