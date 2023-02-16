using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Game.GameModules;
using Game.Models;
using Xamarin.Forms;

namespace Game.Controllers
{
    public class GameModuleController
    {
        ObservableCollection<ModuleInfo> moduleList = new ObservableCollection<Models.ModuleInfo>();

        ListView moduleView;
        List<Module> modules;

        GameRecTicketModel _localTicket;
        public int moduleCount { get { return moduleList.Count; } }

        public delegate void ModuleAction(Module module, GameRecTicketModel ticket);
        
        public event ModuleAction onModuleUpdate;
        public event ModuleAction onModuleActionEnd;
        public GameModuleController(ListView view)
        {
            moduleView = view;
            modules = new List<Module>();

            InitModules();
            addModule(new ModuleInfo() { id = StaticMethods.GetNextModelId(modules), Name = "Перерабатывающий", progress = 0, type = 0 });
        }
        public void SetLocalTicket(GameRecTicketModel ticket) => _localTicket = ticket;
        public void startModuleTask(int moduleID)
        {
            modules[moduleID - 1].startProgress();
        }
        public void stopModuleTask(int moduleID)
        {
            modules[moduleID].stopProgress();
        }
        void InitModules()
        {
            moduleView.ItemsSource = moduleList;
        }
        public void addModule(ModuleInfo nItem)
        {
            moduleList.Add(nItem);

            ModuleResource nMod = new ModuleResource(30);
            
            nMod.id = nItem.id;
            nMod.Name = nItem.Name;
            nMod.type = nItem.type;
            nMod.setProgress(nItem.progress);
            nMod.Init();
            nMod.endProgress += onModuleEndProgress;
            nMod.progressUpdate += onModuleProgressUpdate;

            modules.Add(nMod);
        }
        public void RemoveModule(int id) // Уничтожаем модуль
        {
            var module = modules[id];

            if (module.active)
                module.stopProgress();

            module.progressUpdate -= onModuleProgressUpdate;
            module.endProgress -= onModuleEndProgress;

            modules.Remove(module);
        }
        public List<Module> GetModulesByType(int type)
        {
            List<Module> result = new List<Module>();
            for(int i = 0; i < modules.Count; i++)
            {
                if (modules[i].type == type && !modules[i].active)
                    result.Add(modules[i]);
            }
            return result;
        }
        void onModuleEndProgress(Module module)
        {
            if (onModuleActionEnd != null)
                onModuleActionEnd(module, _localTicket);
        }
        void onModuleProgressUpdate(Module module)
        {
            _localTicket.progress = module.progress;

            if (onModuleUpdate != null)
                onModuleUpdate(module, _localTicket);
        }
    }
}
