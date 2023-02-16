using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Game.GameModules
{
    public class ModuleEngine : Module
    {
        public int engineType { get; set; }
        public ModuleEngine(int interval = 100) : base(interval)
        {
            engineType = 0;

            SetTimerInterval(interval);
        }
    }
}
