using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    public class Deck // Палуба
    {
        public int id { get; set; }
        public string name { get; set; }
        public int level { get; set; }
        public int people { get; set; }
        public List<DeckRoom> decks { get; set; }
        public Deck()
        {
            id = 0;
            level = 0;
            people = 0;

            decks = new List<DeckRoom>();
        }
    }
}
