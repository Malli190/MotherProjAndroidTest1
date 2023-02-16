using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game
{
    public static class StaticMethods
    {
        public static int GetNextModelId(List<GameModules.Module> modules)
        {
            List<int> ids = new List<int>();

            for (int i = 0; i < modules.Count; i++)
                ids.Add(modules[i].id);
            
            int result = ids.Count > 0 ? ids.Max() : 0;

            return result + 1;
        }
        public static int GetNextQueryId(List<Models.BuildQueryModel> models)
        {
            int result = 0;

            for (int i = 0; i < models.Count; i++)
            {
                if (models[i].query_id >= result)
                    result = models[i].query_id;
            }
            result += 1;

            return result;
        }
        public static string GetRandomMapName(int chars, int numbers)
        {
            return $"{getRandString(chars)}-{getRandString(numbers, true)}";
        }
        public static string GetRandomPlayerID()
        {
            string[][] names =
            {
                new string[]
                {
                    getRandString(2),
                    getRandString(1, true),
                    getRandString(1),
                },
                new string[]
                {
                    getRandString(3),
                    getRandString(1, true),
                },
                new string[]
                {
                    getRandString(1),
                    getRandString(3, true),
                },
                new string[]
                {
                    getRandString(1, true),
                    getRandString(1),
                    getRandString(1, true),
                    getRandString(1),
                    getRandString(2, true),
                }
            };
            string result = string.Empty;
            for (int i = 0; i < names.Length; i++)
            {
                for (int a = 0; a < names[i].Length; a++)
                    result += names[i][a];
            
                if (i < names.Length - 1)
                    result += "-";
            }

            return result;
        }
        public static string getRandString(int count, bool numeric = false)
        {
            Random rand = new Random();
            string result = "";
            for (int i = 0; i < count; i++)
            {
                int stroka = numeric ? 0 : 1;

                int lower = rand.Next(0, 2);

                int position = rand.Next(0, symb[stroka].Count - 1);

                string symbol = symb[stroka][position];

                if (!numeric && lower == 1)
                    symbol = symbol.ToUpper();


                result += symbol;
            }
            return result;
        }
        static List<List<string>> symb = new List<List<string>>
        {
            new List<string>(){ "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" },
            new List<string>(){ "q", "w", "e", "r", "t", "y", "u", "i", "o", "o", "a", "s", "d", "f", "g", "h", "j", "k", "l", "z", "x", "c", "v", "b", "n", "m" },
            //new List<string>(){ "!", "@", "#", "$", "%", "(", "&" },
        };
    }
}
