using BotWorld2Core.Game.General;

namespace BotWorld2Core.Game
{
    public abstract class Script : Updatable
    {
        public Script(GameCycleController controller) : base(controller)
        { }

        public abstract void Reset();
        public override void ThreadUpdate()
        {

        }
    }
}
