using System;
using System.Collections.Generic;
using System.Text;
using Game.Controllers;
using Game.GameResource;
using Game.Models;

namespace Game.GameModules
{
    public class Module
    {
        GameTimeController timer;

        public int id { get; set; }
        public string Name { get; set; }
        public int type { get; set; }
        public bool active { get; private set; }
        public bool infinity { get; set; }

        public int progress { get { return currentProgress; } }
        public int progressMax { get { return maxProgress; } }

        int currentProgress;
        int maxProgress;

        public delegate void ModuleAction(Module module);

        public event ModuleAction progressUpdate;
        public event ModuleAction endProgress;

        public ModuleInfo moduleInfo;
        public Module(int interval = 100)
        {
            timer = new GameTimeController();
            timer.interval = interval;
            timer.Tick += onTimerTick;

            currentProgress = 0;
            maxProgress = 100;

            moduleInfo = new ModuleInfo()
            {
                id = 0,
                Name = "",
                progress = 0,
                type = 0,
                active = false,
            };
        }
        public void Init()
        {
            moduleInfo = new Models.ModuleInfo()
            {
                id = id,
                Name = Name,
                progress = 0,
                type = type,
                active = active,
            };
        }
        public void startProgress()
        {
            if(!timer.isStarted)
            {
                Console.WriteLine($"Старт таймера {currentProgress}");
                active = true;
                currentProgress = 0;
                moduleInfo.progress = 0;
                timer.Start();
            }
        }
        public void stopProgress()
        {
            active = false;
            timer.Stop();
            setProgress(0);
        }
        public void setProgress(int progress)
        {
            currentProgress = progress;
            moduleInfo.progress = progress;
        }
        public void SetTimerInterval(int interval)
        {
            timer.interval = interval;
        }
        private void onTimerTick()
        {
            currentProgress += 1;
            moduleInfo.progress = currentProgress;
            
            Console.WriteLine($"Тик таймера {currentProgress}");

            if (progressUpdate != null)
                progressUpdate(this);

            if (currentProgress >= maxProgress && !infinity)
            {
                active = false;
                timer.Stop();
                setProgress(0);

                if (endProgress != null)
                    endProgress(this);
            }
        }
    }
}
