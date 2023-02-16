using Game.GameModules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.GameModules
{
    public class ModuleResource : Module
    {
        public string resourceName { get; set; }
        public int resourceType { get; set; }
        public int resourceOutputType { get; set; }

        public ModuleResource(int interval = 100) : base(interval)
        {
            resourceName = StaticResourceMethods.ResourceMetallNames[0];
            resourceType = 0;
            
            SetTimerInterval(interval);
        }
        public void RunProcessing()
        {
            startProgress();
        }
    }
}
