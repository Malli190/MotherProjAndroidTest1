using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Game.Models
{
    public class Location
    {
        public float x { get; set; }
        public float y { get; set; }
        public static Location zero { get { return new Location(); } }
        public Location()
        {
            x = 0;
            y = 0;
        }
        public Location(float nX, float nY)
        {
            x = nX;
            y = nY;
        }
        public static int Distance(Location a, Location b)
        {
            float dx = a.x - b.x;
            float dy = a.y - b.y;

            return (int)Math.Sqrt(dx * dx + dy * dy);
        }
        public override string ToString()
        {
            return $"{{X={x}, Y={y}}}";
        }
        public static Location operator +(Location o1, Location o2) => new Location(o1.x + o2.x, o1.y + o2.y);
        public static Location operator -(Location o1, Location o2) => new Location(o1.x - o2.x, o1.y - o2.y);
        public static Location operator *(Location o1, Location o2) => new Location(o1.x * o2.x, o1.y * o2.y);
        public static Location operator /(Location o1, Location o2) => new Location(o1.x / o2.x, o1.y / o2.y);

        //public static bool operator ==(Location o1, Location o2)
        //{
        //    return (o1.x == o2.x && o1.y == o2.y);
        //}
        //public static bool operator !=(Location o1, Location o2)
        //{
        //    return (o1.x != o2.x && o1.y != o2.y);
        //}
    }
}
