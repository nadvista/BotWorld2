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

        #region World
        public const int WorldWidth = 100, WorldHeight = 100;
        #endregion

        #region Program
        public const int ThreadsCount;
        #endregion
    }
}
