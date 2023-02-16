using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Game.Models
{
    public class GameRecTicketModel
    {
        public int id { get; set; }
        public string perName { get; set; }
        public string perSubName { get; set; }
        public int resType { get; set; }
        public int resSubType { get; set; }
        public int resCount { get; set; }
        public int progress { get; set; }
        public GameRecTicketModel()
        {
        }
    }
}
