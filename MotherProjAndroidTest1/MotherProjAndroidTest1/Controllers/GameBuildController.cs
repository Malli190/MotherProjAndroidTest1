using Game.GameModules;
using Game.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Game.Controllers
{
    public class GameBuildController
    {
        Module buildModule;

        GameBuildTypeModel _buildModel;
        BuildQueryModel _lastbModel;

        public bool active;

        public delegate void ModuleAction(Module module, BuildQueryModel qModel);
        
        public event ModuleAction onModuleUpdate;
        public event ModuleAction onModuleActionEnd;
        public GameBuildController()
        {
            buildModule = new Module(50);
            buildModule.id = 0;
            buildModule.Name = "Строительный";
            buildModule.type = 1;
            buildModule.setProgress(0);
            buildModule.Init();

            buildModule.progressUpdate += onModuleProgressUpdate;
            buildModule.endProgress += onModuleEndProgress;

            active = false;
        }
        public void StartBuild(GameBuildTypeModel model, BuildQueryModel bModel)
        {
            if (buildModule.active)
                return;

            _buildModel = model;
            _lastbModel = bModel;
            
            _lastbModel.status = "на старте";

            buildModule.startProgress();

            active = true;
        }
        public void AbortBuild()
        {
            _lastbModel.status = "отменен";

            buildModule.stopProgress();

            active = false;
        }
        void onModuleEndProgress(Module module)
        {
            Console.WriteLine($"Обновление завершено");

            _lastbModel.status = "завершен";
            _lastbModel.progress = module.progress;
            
            active = false;

            if (onModuleActionEnd != null)
                onModuleActionEnd(module, _lastbModel);

            _lastbModel = null;
        }
        void onModuleProgressUpdate(Module module)
        {
            //Console.WriteLine($"Обновление модуля строительства {module.progress}/{module.progressMax}");

            _lastbModel.status = "в процессе";
            _lastbModel.progress = module.progress;

            if (onModuleUpdate != null)
                onModuleUpdate(module, _lastbModel);
        }
    }
}
