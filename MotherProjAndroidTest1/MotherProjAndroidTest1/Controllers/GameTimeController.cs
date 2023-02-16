using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Game.Controllers
{
    public class GameTimeController
    {
        //public Action Tick;

        public int interval;
        int time;
        public bool isStarted { get; set; }

        public static bool started { get; private set; }

        bool abort;

        public delegate void TimerAction();

        public event TimerAction Tick;
        public GameTimeController()
        {
            interval = 100;
            time = 0;

            abort = false;
            isStarted = false;
            started = false;
        }
        public void Start()
        {
            started = true;
            Task.Run(() => process());
        }
        public void Stop()
        {
            abort = true;
            started = false;
            isStarted = false;
        }
        async void process()
        {
            if (isStarted)
                return;

            Console.WriteLine($"Запуск таймера");

            isStarted = true;
            time = 0;
            while(true)
            {
                await Task.Delay(1);

                time += 1;
                
                if (abort)
                {
                    abort = false;
                    time = 0;
                    break;
                }
                if (time >= interval)
                {
                    time = 0;

                    if (Tick != null)
                        Tick();
                }
            }
            isStarted = false;
            Console.WriteLine($"Таймер остановлен");
        }
    }
}
