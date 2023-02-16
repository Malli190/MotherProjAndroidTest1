using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    public class Ship
    {
        public int id { get; set; }
        public int health { get; set; }
        public int type { get; set; }
        public int people { get; set; }
        public string name { get; set; }
        public Location position { get; set; }
        public MapLocation location { get; set; }
        public ShipState state { get; set; }
        public List<Deck> decks { get; set; }
        public List<ModuleInfo> modules { get; set; }
        public Ship()
        {
            //health = 100;
            //name = "ship_";
            //position = new Location(0, 0);
            //state = ShipState.stay;

            //decks = new List<Deck>() 
            //{ 
            //    new Deck()
            //    { 
            //        id = 0,
            //        name = "Главная",
            //        level = 0,
            //        decks = new List<DeckRoom>()
            //        { 
            //            new DeckRoom(){ id = 0, name = "Мостик", peopleMax = 3 },
            //            new DeckRoom(){ id = 1, name = "Экипаж", peopleMax = 12 },
            //            new DeckRoom(){ id = 2, name = "Инженерная", peopleMax = 8, },
            //        },
            //    },
            //};
            //modules = new List<ModuleInfo>() 
            //{
            //    new ModuleInfo(){ id = 0, Name = "Двигающий", type = 2 },
            //};
        }
        public enum ShipState
        {
            stay,
            move,
            combat,
            collect,
            research
        }
    }
}
