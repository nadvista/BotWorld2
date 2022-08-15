namespace BotWorld2Core.Game.General
{
    internal class GameSettings
    {
        #region Bots
        public const int BotHiddenLayersCount = 3;
        public const int BotHiddenLayersLenght = 3;

        public const float MaxHealth = 100;
        public const float StartHealth = 10;

        public const float MaxEnergy = 100;
        public const float StartEnergy = 10;
        #endregion

        #region World
        public const int WorldWidth = 500, WorldHeight = 60;
        #endregion

        #region Rules
        public const float EatBotHealthBonus = 10;
        public const float EatFoodHealthBonus = 10;
        public const float EatBotEnergyBonus = 10;
        public const float EatFoodEnergyBonus = 10;

        public const float SunEnergyBonusMultiplyer = 10;
        public const float SunHealthBonusMultiplyer = 10;

        public const int ChildEnergyCost = 10;
        public const float MutationChance = .05f;

        public const int StartBotsCount = 500;

        public const int FoodMaxCount = 900;
        public const int FoodPlaceByStep = 100;
        #endregion

        #region Program
        public const int ThreadsCount = 12;
        #endregion
    }
}
