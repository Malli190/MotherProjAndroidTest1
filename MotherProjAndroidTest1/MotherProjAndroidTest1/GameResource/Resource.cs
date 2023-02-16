using System;
using System.Collections.Generic;
using System.Text;

namespace Game.GameResource
{
    public class Resource
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string SubName { get; set; }
        public int type { get; set; }
        public int subType { get; set; }
        public int Count { get; set; }
        public Resource()
        {
            id = 0;
            Name = "noname";
            SubName = "-";
            type = 0;
            subType = 0;
            Count = 1;
        }
        public static Resource operator +(Resource res1, Resource res2) => new Resource() { id = res1.id, type = res1.type, Name = res1.Name, Count = res1.Count + res2.Count };
        public static Resource operator -(Resource res1, Resource res2) => new Resource() { id = res1.id, type = res1.type, Name = res1.Name, Count = res1.Count - res2.Count };
        public static Resource operator *(Resource res1, Resource res2) => new Resource() { id = res1.id, type = res1.type, Name = res1.Name, Count = res1.Count * res2.Count };
        public static Resource operator /(Resource res1, Resource res2) => new Resource() { id = res1.id, type = res1.type, Name = res1.Name, Count = res1.Count / res2.Count };
    }
}
