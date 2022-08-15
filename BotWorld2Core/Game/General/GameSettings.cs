namespace BotWorld2Core.Game.General
{
    internal class GameSettings
    {
        #region Bots
        public const int BotHiddenLayersCount = 3;
        public const int BotHiddenLayersLenght = 3;

        public const float MaxHealth = 1000;
        public const float StartHealth = 100;

        public const float MaxEnergy = 40;
        public const float StartEnergy = 0;
        #endregion

        #region World
        public const int WorldWidth = 400, WorldHeight = 80;
        #endregion

        #region Rules
        public const float EatBotHealthBonus = 4;
        public const float EatFoodHealthBonus = 4;
        public const float EatBotEnergyBonus = 25;
        public const float EatFoodEnergyBonus = 10;

        public const float SunEnergyBonusMultiplyer = 1f;
        public const float SunHealthBonusMultiplyer = .1f;

        public const int ChildEnergyCost = 15;
        public const float MutationChance = .05f;

        public const int StartBotsCount = 1000;

        public const int FoodMaxCount = 20000;
        public const int FoodPlaceByStep = 500;
        #endregion

        #region Program
        public const int ThreadsCount = 12;
        #endregion
    }
}
