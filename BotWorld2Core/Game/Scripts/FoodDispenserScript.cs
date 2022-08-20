using BotWorld2Core.Game.General;
using BotWorld2Core.Game.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotWorld2Core.Game.Scripts
{
    internal class FoodDispenserScript : Script
    {
        private int _foodCount = 0;
        private IWorldController _world;

        public FoodDispenserScript(IWorldController world)
        {
            _world = world;

            for (int x = 0; x < _world.Width; x++)
            {
                for (int y = 0; y < _world.Height; y++)
                {
                    var cell = _world.GetCell(new Vector2int(x, y));
                    cell.HasFood.Changed += CellUpdateHandler;
                }
            }
        }

        public override void Reset()
        {
            _foodCount = 0;
        }

        public override void Update()
        {
            const int maxCycle = 25000;
            for (int i = 0; i < GameSettings.FoodPlaceByStep; i++)
            {
                if (_foodCount >= GameSettings.FoodMaxCount)
                    return;
                int x = 0;
                int y = 0;
                WorldCell cell;
                int cycle = 0;
                do
                {
                    x = Global.Random.Next(0, GameSettings.WorldWidth);
                    y = Global.Random.Next(0, GameSettings.WorldHeight);
                    cell = _world.GetCell(new Vector2int(x, y));
                    if (++cycle == maxCycle) break;
                } while (cell.IsWall && cell.HasFood.Value);

                if (cycle < maxCycle)
                    cell.PlaceFood();
                else cycle = 0;

                _foodCount++;
            }
        }
        private void CellUpdateHandler(ObservableVar<bool> newState)
        {
            if(!newState.Value)
                _foodCount--;
        }
    }
}
