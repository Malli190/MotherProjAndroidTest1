using Game.PlayerSystem;
using Game.MapSystem.Models;
using Game.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Game.MapSystem
{
    class MapLoader
    {
        SaveManager.MainSaver mapSaver;

        public List<GeneralChunk> chunks;
        public MapLoader()
        {
            mapSaver = new SaveManager.MainSaver();

            mapSaver.SetFileName("_map.m");
            mapSaver.SetFolderName("Data");

            chunks = new List<GeneralChunk>();

            if(!mapSaver.LoadParam())
            {
                Console.WriteLine($"Карта не загружена, создаю новую");
                GenerateNewMap(new Location(2, 2));
                
                mapSaver.SaveParam();
            }
            else
            {
                for(int i = 0; i < mapSaver.parametersCount; i++)
                {
                    var param = mapSaver.GetParameterById(i);

                    Console.WriteLine(param.parameterValue);

                    GeneralChunk chunk = mapSaver.GetObjectByString<GeneralChunk>(param.parameterValue);

                    chunks.Add(chunk);
                    Console.WriteLine($"Параметр загружен c: {chunk.chunks.Count}");
                }
            }
        }
        public void GenerateNewMap(Location size)
        {
            GeneralChunk gChunk = new GeneralChunk(size)
            {
                id = 0,
                name = "test_0",
            };
            gChunk.SetRegionSize(new Location(2, 2));
            gChunk.GenerateChunks();

            chunks.Add(gChunk);
            AddChunkToParam(gChunk);
        }
        public void AddChunk(GeneralChunk chunk)
        {
            chunk.id = GetNextChunkID();

            chunks.Add(chunk);
            AddChunkToParam(chunk);
            Console.WriteLine($"Сохранаяю {chunk.name} c: {chunk.chunks.Count}");
            mapSaver.SaveParam();
        }
        void AddChunkToParam(GeneralChunk chunk)
        {
            mapSaver.AddParameter(new SaveParameterModel()
            {
                id = chunk.id,
                parameterName = chunk.name,
                parameterValue = mapSaver.GetStringByObject(chunk)
            });
        }
        public GeneralChunk GetChunkByName(string name)
        {
            GeneralChunk result = null;
            for(int i = 0; i < chunks.Count; i++)
            {
                if (chunks[i].name == name)
                {
                    result = chunks[i];
                    break;
                }    
            }
            return result;
        }
        public GeneralChunk GetChunkByID(int id)
        {
            GeneralChunk result = null;
            for (int i = 0; i < chunks.Count; i++)
            {
                if (chunks[i].id == id)
                {
                    result = chunks[i];
                    break;
                }
            }
            return result;
        }
        public void RemoveChunkByName(string name)
        {
            for(int i = 0; i < chunks.Count; i++)
            {
                if(chunks[i].name == name)
                {
                    RemoveChunkById(i);
                    break;
                }
            }
        }
        public void RemoveChunkById(int id)
        {
            RemoveChunk(GetChunkByID(id));
        }
        void RemoveChunk(GeneralChunk chunk)
        {
            RemoveChunkParam(chunk);
            chunks.Remove(chunk);
        }
        void RemoveChunkParam(GeneralChunk chunk)
        {
            mapSaver.RemoveParameter(mapSaver.GetParameterByName(chunk.name));
            mapSaver.SaveParam();
        }
        int GetNextChunkID()
        {
            int result = 0;
            
            List<int> ids = new List<int>();
            
            for(int i = 0; i < chunks.Count; i++)
                ids.Add(chunks[i].id);

            if (ids.Count > 0)
                result = ids.Max();

            return result + 1;
        }
    }
}
