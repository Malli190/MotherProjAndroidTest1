using Game.GameObjects;
using Game.Models;
using System.Collections.Generic;

namespace Game.GameData.ShipManager
{
    class ShipEditor
    {
        public ShipEditor()
        {

        }
        public GameShip GetStartMotherShip()
        {
            GameShip motherShip = new GameShip();

            Ship motherModel = new Ship()
            {
                id = 0,
                health = 100,
                people = 15,
                name = "mother",
                state = Ship.ShipState.stay,
                position = Location.zero,
                location = new MapLocation(0, 0, 0),
                type = 0,
                decks = new List<Deck>()
                {
                    new Deck()
                    {
                        id = 0,
                        name = "Главная",
                        level = 0,
                        decks = new List<DeckRoom>()
                        {
                            new DeckRoom(){ id = 0, name = "Мостик", peopleMax = 3 },
                            new DeckRoom(){ id = 1, name = "Экипаж", peopleMax = 12 },
                            new DeckRoom(){ id = 2, name = "Инженерная", peopleMax = 8, },
                        },
                    },
                },
                modules = new List<ModuleInfo>()
                {
                    new ModuleInfo(){ id = 0, Name = "Перерабатывающий", type = 0 },
                    new ModuleInfo(){ id = 1, Name = "Двигающий", type = 2 },
                },
            };
            motherShip.Init(motherModel);

            return motherShip;
        }
    }
}
