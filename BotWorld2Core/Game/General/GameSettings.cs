using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotWorld2Core.Game.General
{
    internal class GameSettings
    {
        #region Bots
        public const int BotHiddenLayersCount = 3;
        public const int BotHiddenLayersLenght = 3;

        public const float MaxHealth = 10;
        public const float StartHealth = 10;

        public const float MaxEnergy = 10;
        public const float StartEnergy = 10;

        public const float MutationChance = .05f;
        #endregion
    }
}
