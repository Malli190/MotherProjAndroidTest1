using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    public class GameBuildTypeModel
    {
        public int id { get; set; }
        public int type { get; set; }
        public int subType { get; set; }
        public string name { get; set; }
        public GameBuildTypeModel()
        {
            id = 0;
            type = 0;
            subType = 0;
            name = "";
        }
    }
}
