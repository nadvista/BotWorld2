namespace BotWorld2Core.Game.General
{
    internal class GameSettings
    {
        #region Bots
        public const int BotHiddenLayersCount = 2;
        public const int BotHiddenLayersLenght = 5;

        public const float MaxHealth = 250;
        public const float StartHealth = 100;

        public const float MaxEnergy = 40;
        public const float StartEnergy = 0;
        public const int BotColorsCount = 10;
        #endregion

        #region World
        public const int WorldWidth = 400, WorldHeight = 80;
        #endregion

        #region Rules
        public static float EatBotHealthBonus = 10;
        public static float EatFoodHealthBonus = 30;
        public static float EatBotEnergyBonus = 5;
        public static float EatFoodEnergyBonus = 30;

        public static float SunEnergyBonusMultiplyer = .6f;
        public static float SunHealthBonusMultiplyer = 1.5f;
        public static float SunShare = 1;

        public static int ChildEnergyCost = 18;
        public static float MutationChance = .1f;
        
        public static int StartBotsCount = 2000;

        public static int FoodMaxCount = 2000;
        public static int FoodPlaceByStep = 50;
        #endregion

        #region Program
        public const int ThreadsCount = 12;
        #endregion
    }
}
