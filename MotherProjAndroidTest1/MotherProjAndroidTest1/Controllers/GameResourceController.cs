using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.GameResource;

namespace Game.Controllers
{
    public class GameResourceController
    {
        public List<Resource> oreList = new List<Resource>();
        public List<Resource> metallList = new List<Resource>();
        public List<Resource> materialList = new List<Resource>();

        SaveManager.MainSaver saver;
        public GameResourceController()
        {
            saver = new SaveManager.MainSaver();

            saver.SetFileName("Resources.s");
            saver.SetFolderName("Data");

            if (!saver.LoadParam())
            {
                GenerateNewResources();

                saver.AddParameter(new Models.SaveParameterModel() { id = 0, parameterName = "ore", parameterValue = saver.GetStringByObject(oreList) });
                saver.AddParameter(new Models.SaveParameterModel() { id = 1, parameterName = "metall", parameterValue = saver.GetStringByObject(metallList) });
                saver.AddParameter(new Models.SaveParameterModel() { id = 2, parameterName = "material", parameterValue = saver.GetStringByObject(materialList) });

                saver.SaveParam();
            }
            else
            {
                oreList = saver.GetObjectByString<List<Resource>>(saver.GetParameterByName("ore").parameterValue);
                metallList = saver.GetObjectByString<List<Resource>>(saver.GetParameterByName("metall").parameterValue);
                materialList = saver.GetObjectByString<List<Resource>>(saver.GetParameterByName("material").parameterValue);
            }
        }
        public void GenerateNewResources(bool cl = false)
        {
            for(int i = 0; i < 3; i++)
                GetResourceByType(i).Clear();

            addOre(new Resource() { id = 0, Name = "Руда", SubName = "Железо", type = 0, subType = 0, Count = 10 });
            addOre(new Resource() { id = 1, Name = "Руда", SubName = "Медь", type = 0, subType = 1, Count = 0 });

            addMetall(new Resource() { id = 0, Name = "Металл", SubName = "Железо", type = 1, subType = 0, Count = 0 });
            addMetall(new Resource() { id = 1, Name = "Металл", SubName = "Медь", type = 1, subType = 1, Count = 0 });
            addMetall(new Resource() { id = 2, Name = "Металл", SubName = "Сталь", type = 1, subType = 2, Count = 0 });
            addMetall(new Resource() { id = 3, Name = "Металл", SubName = "Алюминий", type = 1, subType = 3, Count = 0 });

            addMaterial(new Resource() { id = 0, Name = "Корпус", SubName = "Сталь", type = 2, subType = 0, Count = 0 });

            if (cl)
                UpdateSaverData();
        }
        public void addOre(Resource res)
        {
            oreList.Add(res);
        }
        public void addMetall(Resource res)
        {
            metallList.Add(res);
        }
        public void addMaterial(Resource res)
        {
            materialList.Add(res);
        }
        public Resource getMetall(int id)
        {
            return metallList.Where(x => x.id == id).First();
        }
        public Resource getMetalByName(string name)
        {
            return metallList.Where(x => x.Name == name).First();
        }
        public List<string> getMetallNames()
        {
            List<string> res = new List<string>();
            
            for (int i = 0; i < metallList.Count; i++)
                res.Add(metallList[i].SubName);
            
            return res;
        }
        public List<Resource> GetResourceByType(int type)
        {
            List<Resource> result = null;

            switch(type)
            {
                case 0:
                    result = oreList;
                    break;
                case 1:
                    result = metallList;
                    break;
                case 2:
                    result = materialList;
                    break;
            }
            return result;
        }
        public void UpdateSaverData()
        {
            saver.GetParameterByName("ore").parameterValue = saver.GetStringByObject(oreList);
            saver.GetParameterByName("metall").parameterValue = saver.GetStringByObject(metallList);
            saver.GetParameterByName("material").parameterValue = saver.GetStringByObject(materialList);

            saver.SaveParam();
        }
        public void clearLsit()
        {
            metallList.Clear();
        }
    }
}
