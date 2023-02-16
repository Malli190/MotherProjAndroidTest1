using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    public class ModuleInfo
    {
        public int id { get; set; }
        public string Name { get; set; }
        public int type { get; set; }
        public int progress { get; set; }
        public bool active { get; set; }
        public ModuleInfo() { }
    }
}
