namespace BotWorld2Core.Game.General.Pool
{
    public interface IPoolElement
    {
        public bool IsElementFree();
        public void OnCreate();
        public void OnTake();
        public void ReturnToPool();
    }
}