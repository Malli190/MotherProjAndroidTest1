using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    public class BuildModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<GameResource.Resource> cost { get; set; }
        public int buildTime { get; set; }

        public BuildModel()
        {

        }
    }
}
