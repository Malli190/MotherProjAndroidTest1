using System;
using System.Collections.Generic;
using System.Text;

namespace Game.GameModules
{
    public class ModuleCollect : Module
    {
        public int collectSpeed { get; set; }
        public ModuleCollect(int interval = 100) : base(interval)
        {
            collectSpeed = 2;

            SetTimerInterval(interval);
        }
    }
}
