using System.Collections.Generic;
using System;
using BotWorld2Core.Game.Bots;
using BotWorld2Core.Game.World;
using System.Linq;

namespace BotWorld2Core.Game.General
{
    public class GameManager
    {
        public event Action<WorldCell> OnCellUpdated;
        public int BotsAlive => _bots.Count;

        protected readonly IWorldController _worldController;
        protected readonly GameCycleController _gameCycleController;
        protected readonly IBotFabric _fabric;
        protected readonly List<BotModel> _bots = new List<BotModel>();
        protected readonly List<Script> _scripts = new List<Script>();

        public GameManager(IWorldController worldController, GameCycleController cycleController, IBotFabric fabric)
        {
            _worldController = worldController;
            _worldController.CellUpdated += e => OnCellUpdated?.Invoke(e);
            _gameCycleController = cycleController;
            _fabric = fabric;
        }
        public virtual void Reset()
        {
            _bots.Clear();
            _worldController.Reset();
            _scripts.ForEach(e => e.Reset());
        }
        public void AddScript(Script script)
        {
            script.SetCycleController(_gameCycleController);
            _scripts.Add(script);
        }
        public WorldCell GetCell(Vector2int cellPos)
        {
            return _worldController.GetCell(cellPos);
        }
        public T GetScript<T>() where T : Script
        {
            return _scripts.First(e => e is T) as T;
        }
    }
}
