using BotWorld2Core.Game.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotWorld2Core.Game.Scripts
{
    internal class SunScript : Script
    {
        private int _timer = 0;
        private int _cycleDuration = 1500;
        private readonly float startEnergyBonus = GameSettings.SunEnergyBonusMultiplyer;
        private readonly float startHealthBonus = GameSettings.SunHealthBonusMultiplyer;

        public SunScript()
        {
        }

        public override void ThreadUpdate()
        {  
        }

        public override void Update()
        {

            var currentShare = Math.Abs(_timer / (float)(_cycleDuration/2) - 1);
            GameSettings.SunShare = currentShare;

            GameSettings.SunEnergyBonusMultiplyer = startEnergyBonus*currentShare;
            GameSettings.SunHealthBonusMultiplyer = startHealthBonus*currentShare;

            _timer++;
            if (_timer == _cycleDuration)
                _timer = 0;
        }

        public override void Reset()
        {
            GameSettings.SunEnergyBonusMultiplyer = startEnergyBonus;
            GameSettings.SunHealthBonusMultiplyer = startHealthBonus;
        }
    }
}
