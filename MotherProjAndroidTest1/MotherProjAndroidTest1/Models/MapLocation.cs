namespace Game.Models
{
    public class MapLocation
    {
        public int G { get; set; }
        public int A { get; set; }
        public int R { get; set; }
        public MapLocation()
        {
            G = 0;
            A = 0;
            R = 0;
        }
        public MapLocation(int g, int a, int r)
        {
            G = g;
            A = a;
            R = r;
        }
        public override string ToString()
        {
            return $"(общ: {G} рн: {A} уч: {R})";
        }
    }
}
