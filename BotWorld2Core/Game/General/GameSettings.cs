namespace BotWorld2Core.Game.General
{
    public class GameSettings
    {
        #region Bots
        public const int BotHiddenLayersCount = 2;
        public const int BotHiddenLayersLenght = 5;

        public const float MaxHealth = 150;
        public const float StartHealth = 100;

        public const float MaxEnergy = 40;
        public const float StartEnergy = 0;
        public const int BotColorsCount = 10;
        #endregion

        #region World
        public const int WorldWidth = 200, WorldHeight = 80;
        #endregion

        #region Rules
        public static float EatBotHealthBonus = 25;
        public static float EatFoodHealthBonus = 15;
        public static float EatBotEnergyBonus = 25;
        public static float EatFoodEnergyBonus = 10;

        public static float SunEnergyBonusMultiplyer = .6f;
        public static float SunHealthBonusMultiplyer = 1f;
        public static float SunShare = 1;

        public static int ChildEnergyCost = 10;
        public static float MutationChance = .05f;

        public static int StartBotsCount = 2000;

        public static int FoodMaxCount = 3000;
        public static int FoodPlaceByStep = 200;

        public static float MaxEatBugByStep = 1;
        public static float MaxBugPlaceSize = 10;
        #endregion

        #region Program
        public const int ThreadsCount = 12;
        #endregion
    }
}
