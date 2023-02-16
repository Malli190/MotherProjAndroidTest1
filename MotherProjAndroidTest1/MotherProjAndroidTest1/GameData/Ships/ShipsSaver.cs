using Game.GameObjects;
using Game.Models;
using Game.SaveManager;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.GameData.ShipManager
{
    class ShipsSaver
    {
        ShipEditor shipEditor;

        MainSaver shipSaver;
        List<GameShip> playerShips;
        public ShipsSaver(ShipEditor se, List<GameShip> ps)
        {
            shipEditor = se;
            playerShips = ps;

            shipSaver = new MainSaver();

            shipSaver.SetFileName("playerShip.s");
            shipSaver.SetFolderName("Data");

            if (!shipSaver.LoadParam())
            {
                GameShip motherShip = shipEditor.GetStartMotherShip();

                playerShips.Add(motherShip);

                List<Ship> ships = new List<Ship>();

                for (int i = 0; i < playerShips.Count; i++)
                    ships.Add(playerShips[i].mainShip);

                shipSaver.AddParameter(new SaveParameterModel() { id = 0, parameterName = "playerShips", parameterValue = shipSaver.GetStringByObject(ships) });
                shipSaver.SaveParam();
            }
            else
            {
                List<Ship> lShips = shipSaver.GetObjectByString<List<Ship>>(shipSaver.GetParameterByName("playerShips").parameterValue);

                for (int i = 0; i < lShips.Count; i++)
                {
                    Ship sModel = lShips[i];

                    GameShip nShip = new GameShip();

                    nShip.Init(sModel);

                    playerShips.Add(nShip);
                }
            }
        }
        public void NewGame()
        {
            playerShips.Add(shipEditor.GetStartMotherShip());

            //SaveShips();
        }
        public void SaveShips()
        {
            List<Ship> ships = new List<Ship>();

            for (int i = 0; i < playerShips.Count; i++)
            {
                Ship sModel = playerShips[i].mainShip;

                ships.Add(sModel);
            }
            shipSaver.GetParameterByName("playerShips").parameterValue = shipSaver.GetStringByObject(ships);
            shipSaver.SaveParam();
        }
    }
}
