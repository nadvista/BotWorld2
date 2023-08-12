using System;
using BotWorld2.StandartAssembly;
using BotWorld2Core.Game;

namespace StandartAssembly.Scripts
{
    public class SunScript : Script
    {
        private readonly float startEnergyBonus = GameSettings.SunEnergyBonusMultiplyer;
        private readonly float startHealthBonus = GameSettings.SunHealthBonusMultiplyer;

        private int _timer = 0;
        private readonly int _cycleDuration = 1500;

        public SunScript()
        {
        }

        public override void Update()
        {
            var currentShare = Math.Abs(1 - _timer / (float)(_cycleDuration / 2));

            GameSettings.SunShare = currentShare;

            GameSettings.SunEnergyBonusMultiplyer = startEnergyBonus * currentShare;
            GameSettings.SunHealthBonusMultiplyer = startHealthBonus * currentShare;

            if (_timer != _cycleDuration)
                _timer++;
        }
        public override void Reset()
        {
            GameSettings.SunEnergyBonusMultiplyer = startEnergyBonus;
            GameSettings.SunHealthBonusMultiplyer = startHealthBonus;
            _timer = 0;
        }
    }
}
