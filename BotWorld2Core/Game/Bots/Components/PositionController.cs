using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotWorld2Core.Game.Bots.Scripts;
using BotWorld2Core.Game.General;

namespace BotWorld2Core.Game.Bots.Components
{
    public class BotPositionController : BotComponent
    {
        public Vector2int Position { get; private set; }
        public Vector2int Forward { get; private set; }
        public void SetPosition(int x, int y)
        {
            Position = new Vector2int(x, y);
        }
        public void SetPosition(Vector2int pos)
        {
            Position = pos;
        }
        public void SetDirection(int x, int y)
        {
            Forward = new Vector2int(x, y);
        }
        public void SetDirection(Vector2int forward)
        {
            Forward = forward;
        }
    }
}
