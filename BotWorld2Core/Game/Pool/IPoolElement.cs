namespace BotWorld2Core.Pool
{
    public interface IPoolElement
    {
        event Action<IPoolElement> OnFree;
        void Reset();
    }
}