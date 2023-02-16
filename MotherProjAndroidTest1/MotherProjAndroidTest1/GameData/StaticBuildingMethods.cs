using Game.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    public static class StaticBuildingMethods
    {
        public static string[] BuildTypeNames =
        {
            "Корабль",
            "Модуль",
            "Деталь"
        };
        public static List<BuildModel> subTypeShips = new List<BuildModel>() // Типы кораблей
        {
            new BuildModel()
            {
                id = 0,
                name = "Добывающий",
                cost = new List<GameResource.Resource>()
                {
                    new GameResource.Resource() { type = 2, subType = 0, Name = "Корпус", Count = 120  },
                    new GameResource.Resource() { type = 1, subType = 2, Name = "Сталь", Count = 100  },
                    new GameResource.Resource() { type = 1, subType = 3, Name = "Алюминий", Count = 140  },
                },
                buildTime = 100
            },
            new BuildModel()
            {
                id = 1,
                name = "Военный",
                cost = new List<GameResource.Resource>()
                {
                    new GameResource.Resource() { type = 2, subType = 0, Name = "Корпус", Count = 250  },
                    new GameResource.Resource() { type = 1, subType = 2, Name = "Сталь", Count = 130  },
                    new GameResource.Resource() { type = 1, subType = 3, Name = "Алюминий", Count = 300  },
                },
                buildTime = 200
            },
            new BuildModel()
            {
                id = 2,
                name = "Исследовательский",
                cost = new List<GameResource.Resource>()
                {
                    new GameResource.Resource() { type = 2, subType = 0, Name = "Корпус", Count = 80  },
                    new GameResource.Resource() { type = 1, subType = 2, Name = "Сталь", Count = 30  },
                    new GameResource.Resource() { type = 1, subType = 3, Name = "Алюминий", Count = 60  },
                },
                buildTime = 80
            },
        };
        public static List<BuildModel> subTypeModules = new List<BuildModel>() // Типы модулей
        {
            new BuildModel()
            {
                id = 0,
                name = "Перерабатывающий",
                cost = new List<GameResource.Resource>()
                {
                    new GameResource.Resource() { type = 1, subType = 0, Name = "Железо", Count = 20  },
                },
                buildTime = 40
            },
            new BuildModel()
            {
                id = 1,
                name = "Добывающий",
                cost = new List<GameResource.Resource>()
                {
                    new GameResource.Resource() { type = 1, subType = 0, Name = "Железо", Count = 20  },
                    new GameResource.Resource() { type = 1, subType = 2, Name = "Сталь", Count = 10  },
                },
                buildTime = 60
            },
            new BuildModel()
            {
                id = 2,
                name = "Двигающий",
                cost = new List<GameResource.Resource>()
                {
                    new GameResource.Resource() { type = 2, subType = 0, Name = "Корпус", Count = 10  },
                    new GameResource.Resource() { type = 1, subType = 3, Name = "Алюминий", Count = 20  },
                    new GameResource.Resource() { type = 1, subType = 2, Name = "Сталь", Count = 20  },
                },
                buildTime = 80
            },
            new BuildModel()
            {
                id = 3,
                name = "Модуль тип 4",
                cost = new List<GameResource.Resource>()
                {
                    new GameResource.Resource() { type = 2, subType = 0, Name = "Корпус", Count = 5  },
                    new GameResource.Resource() { type = 1, subType = 3, Name = "Алюминий", Count = 14  },
                    new GameResource.Resource() { type = 1, subType = 2, Name = "Сталь", Count = 10  },
                },
                buildTime = 60
            },
        };
        public static List<BuildModel> subTypeMaterials = new List<BuildModel>() // Типы материалов
        {
            new BuildModel()
            {
                id = 0,
                name = "Корпус",
                cost = new List<GameResource.Resource>()
                {
                    new GameResource.Resource() { type = 1, subType = 3, Name = "Алюминий", Count = 10  },
                },
                buildTime = 20
            },
            new BuildModel()
            {
                id = 1,
                name = "Электродеталь",
                cost = new List<GameResource.Resource>()
                {
                    new GameResource.Resource() { type = 1, subType = 3, Name = "Алюминий", Count = 10  },
                    new GameResource.Resource() { type = 1, subType = 1, Name = "Медь", Count = 10  },
                },
                buildTime = 20
            },
        };
    }
}
