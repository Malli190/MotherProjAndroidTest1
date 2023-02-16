using Game.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.CompilerServices;

namespace Game.SaveManager
{
    public class MainSaver
    {
        public string FolderName { get; private set; }
        public string FileBName { get; private set; }
        public int parametersCount { get { return _parameters.Count; } }

        List<SaveParameterModel> _parameters;
        public MainSaver()
        {
            _parameters = new List<SaveParameterModel>();
        }
        public void SetFileName(string name) => FileBName = name;
        public void SetFolderName(string name) => FolderName = $"{Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData))}/{name}";
        public void AddParameter(SaveParameterModel param)
        {
            _parameters.Add(param);
        }
        public void RemoveParameter(SaveParameterModel param)
        {
            _parameters.Remove(param);
        }
        public SaveParameterModel GetParameterById(int id)
        {
            return _parameters[id];
        }
        public SaveParameterModel GetParameterByName(string name)
        {
            SaveParameterModel result = null;
            for (int i = 0; i < _parameters.Count; i++)
            {
                if (_parameters[i].parameterName.ToLower() == name.ToLower())
                {
                    result = _parameters[i];
                    break;
                }
            }
            return result;
        }
        
        public void SaveParam() // Сохранение настроек
        {
            if (!Directory.Exists(FolderName))
                Directory.CreateDirectory(FolderName);

            string filePath = $"{FolderName}/{FileBName}";

            if (!File.Exists(filePath))
                File.Create(filePath).Close();

            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.Default))
            {
                string result = JsonConvert.SerializeObject(_parameters);
                sw.WriteLine(result);
                sw.Close();

                for (int i = 0; i < _parameters.Count; i++)
                    Console.WriteLine($"{_parameters[i].parameterValue}");
            }
        }
        public bool LoadParam()
        {
            string filePath = $"/{FolderName}/{FileBName}";

            if (!File.Exists(filePath))
            {

                return false;
            }
            using (StreamReader sr = new StreamReader(filePath, Encoding.Default, true))
            {
                string result = sr.ReadLine();
                _parameters = JsonConvert.DeserializeObject<List<SaveParameterModel>>(result);

                sr.Close();

                Console.WriteLine($"Файл: {FileBName} Данные: {result}");

                if (_parameters.Count == 0)
                    return false;
            }
            return true;
        }
        public string GetStringByObject<T>(T importObject) where T: class
        {
            return JsonConvert.SerializeObject(importObject);
        }
        public T GetObjectByString<T>(string importString) where T: class
        {
            return JsonConvert.DeserializeObject<T>(importString);
        }
    }
}
