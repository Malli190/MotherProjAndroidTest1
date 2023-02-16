using Game.GameResource;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    public class DeckRoom // Комната на палубе
    {
        public int id { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string name { get; set; }
        public int health { get; set; }
        public int people { get; set; }
        public int peopleMax { get; set; }
        public List<Resource> resource { get; set; }
        public DeckRoom()
        {
            id = 0;
            width = 10;
            height = 10;
            name = "-";
            health = 100;
            people = 0;
            peopleMax = 10;

            resource = new List<Resource>();
        }
    }
}
