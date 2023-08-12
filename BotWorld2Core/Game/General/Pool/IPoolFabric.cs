namespace BotWorld2Core.Game.General.Pool
{
    public interface IPoolFabric<T> where T: IPoolElement
    {
        public T CreateNew();
    }
}