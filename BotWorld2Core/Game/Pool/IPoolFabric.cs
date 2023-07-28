namespace BotWorld2Core.Pool
{
    public interface IPoolFabric<T> where T : IPoolElement
    {
        T CreateNew();
    }
}