using Game.GameModules;
using Game.Models;
using System;
using System.Collections.Generic;

namespace Game.GameObjects
{
    public class GameShip
    {
        public Ship mainShip;

        public float speed { get; set; }

        public List<Module> modules;

        public Location lastTarget;
        public Location currentTarget;

        public GameShip()
        {
            mainShip = new Ship();

            speed = 2;

            modules = new List<Module>();

            //GenerateModules();
        }
        public void Init(Ship shp)
        {
            mainShip = shp;

            ReloadModules();
        }
        public void SetPosition(Location nPos)
        {
            mainShip.position = nPos;
        }
        public void MoveShip(Location target)
        {
            if (mainShip.state == Ship.ShipState.stay)
            {
                mainShip.state = Ship.ShipState.move;
                
                currentTarget = target;

                GetModuleByType(2).startProgress();
            }
        }
        public void StopMove()
        {
            mainShip.state = Ship.ShipState.stay;

            GetModuleByType(2).stopProgress();
        }
        public void StopAllAction()
        {
            for(int i = 0; i < modules.Count; i++)
                modules[i].stopProgress();

            mainShip.state = Ship.ShipState.stay;
        }
        void onModuleUpdate(Module module)
        {
            // Определяем модуль
            switch(module.type)
            {
                case 0: // Перерабатывающий
                    break;
                case 1: // Добывающий

                    break;
                case 2: // Двигательный
                    if(currentTarget != Location.zero)
                    {
                        var dis = Location.Distance(mainShip.position, currentTarget);
                        if (dis > 0)
                        {
                            var angle = MathHelper.AngleVector(mainShip.position, currentTarget);

                            mainShip.position.x += (int)(speed * Math.Sin(angle));
                            mainShip.position.y -= (int)(speed * Math.Cos(angle));

                            Console.WriteLine($"поз: {mainShip.position.x}/{mainShip.position.y} дис: {dis}");
                        }
                        else
                        {
                            mainShip.state = Ship.ShipState.stay;
                            module.stopProgress();
                        }
                    }
                    break;
                case 3: // Исследовательский
                    break;
            }
        }
        void onModuleEndUpdate(Module module)
        {
            lastTarget = currentTarget;

            currentTarget = Location.zero;
        }
        public void ReloadModules()
        {
            RemoveAllModules();

            for (int i = 0; i < mainShip.modules.Count; i++)
            {
                var nMod = GetNewModuleType(mainShip.modules[i].type);
                
                nMod.id = mainShip.modules[i].id;
                nMod.Name = mainShip.modules[i].Name;
                nMod.type = mainShip.modules[i].type;
                nMod.setProgress(mainShip.modules[i].progress);
                nMod.Init();

                if (nMod.type == 2)
                    nMod.infinity = true;

                nMod.progressUpdate += onModuleUpdate;
                nMod.endProgress += onModuleEndUpdate;

                modules.Add(nMod);
            }
        }
        void RemoveAllModules()
        {
            for (int i = 0; i < modules.Count; i++)
            {
                if (modules[i].active)
                    modules[i].stopProgress();

                modules[i].progressUpdate -= onModuleUpdate;
                modules[i].endProgress -= onModuleEndUpdate;
            }
            modules.Clear();
        }
        Module GetNewModuleType(int type)
        {
            Module result = null;
            switch(type)
            {
                case 0:
                    result = new ModuleResource();
                    break;
                case 1:
                    result = new ModuleCollect();
                    break;
                case 2:
                    result = new ModuleEngine();
                    break;
                case 3:
                    break;
            }
            return result;
        }
        Module GetModuleByType(int type)
        {
            Module result = null;

            for(int i = 0; i < modules.Count; i++)
            {
                if (modules[i].type == type)
                {
                    result = modules[i];
                    break;
                }    
            }

            return result;
        }
    }
}
