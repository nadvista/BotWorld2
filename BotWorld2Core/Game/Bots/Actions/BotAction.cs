namespace BotWorld2Core.Game.Bots.Actions
{
    public abstract class BotAction : BotComponent
    {
        public abstract void Execute();
        public virtual bool FreezeThread => true;
    }
}
