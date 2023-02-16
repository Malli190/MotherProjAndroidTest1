using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Game.Models;
using System.Text;
using Xamarin.Forms;

namespace Game.Controllers
{
    public class LogController
    {
        ObservableCollection<GameLogItem> nList = new ObservableCollection<GameLogItem>();

        ListView log;
        List<GameLogItem> logMessages;

        public LogController(ListView view)
        {
            log = view;
            logMessages = new List<GameLogItem>();
            log.ItemsSource = nList;
        }
        public void Write(string text)
        {
            string time = DateTime.Now.ToString();
            string result = $"{time} - {text}";
            GameLogItem nItem = new GameLogItem(result);
            nList.Add(nItem);
            log.ScrollTo(nItem, ScrollToPosition.Start, true);
        }
        public void ClearLog() 
        {
            nList.Clear(); 
        }
        void UpdateLog()
        {
            log.ItemsSource = nList;
        }
        public static string getStatusName()
        {
            string result = GameTimeController.started ? "Ост." : "Нач.";

            return result;
        }
    }
}
