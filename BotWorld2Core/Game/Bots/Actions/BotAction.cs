namespace BotWorld2Core.Game.Bots.Actions
{
    internal abstract class BotAction : BotComponent
    {
        public abstract void Execute();
        public virtual bool StopThread => true;
    }
}
