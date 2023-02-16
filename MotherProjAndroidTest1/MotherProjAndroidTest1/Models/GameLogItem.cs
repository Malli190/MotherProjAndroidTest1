using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    public class GameLogItem
    {
        public string Text { get; set; }
        public GameLogItem() { }
        public GameLogItem(string text) { Text = text; }
    }
}
