using System;
using System.Collections.Generic;
using System.Text;
using Game.Models;

namespace Game.MapSystem.Models
{
    public class Chunk
    {
        public int id { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public Location position;

        public Chunk()
        {
            id = 0;
            width = 10;
            height = 10;

            position = Location.zero;
        }
        public void SetPosition(Location nPos) => position = nPos;
        public void SetSize(Location size)
        {
            width = (int)size.x;
            height = (int)size.y;
        }
    }
}
