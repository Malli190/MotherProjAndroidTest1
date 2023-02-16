using Game.GameObjects;
using Game.MapSystem.Models;
using Game.Models;
using Game.SaveManager;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Game.GameData.ShipManager
{
    public class ShipsManager
    {
        ShipsSaver shipSaver;

        ShipEditor shipEditor;

        MainShipManager mainShipManager;

        List<GameShip> playerShips;
        public int shipsCount { get { return playerShips.Count; } }
        public ShipsManager()
        {

            shipEditor = new ShipEditor();

            playerShips = new List<GameShip>();

            shipSaver = new ShipsSaver(shipEditor, playerShips);
            
        }
        public void InitMainShipManager(Label[] labels)
        {
            mainShipManager = new MainShipManager(labels);
            mainShipManager.SetShip(GetShipById(0));
        }
        public void UpdateMainShipinfo() => mainShipManager.UpdateInfo();
        public void EditShip(Ship ship)
        {
            for (int i = 0; i < playerShips.Count; i++)
            {
                if (playerShips[i].mainShip.id == ship.id)
                {
                    playerShips[i].mainShip = ship;

                    break;
                }
            }
            SaveShips();
        }
        public void NewGame(GeneralChunk gChunk)
        {
            RemoveAllShips();

            shipSaver.NewGame();

            Ship shp = GetShipById(0);
            Random rand = new Random();
            
            shp.position = new Location(rand.Next(0, 9), rand.Next(0, 9));
            shp.location = new MapLocation(rand.Next(0, gChunk.chunks.Count), rand.Next(0, gChunk.regionWidth), rand.Next(0, 9));

            shipSaver.SaveShips();
        }
        void RemoveAllShips()
        {
            for (int i = 0; i < playerShips.Count; i++)
                playerShips[i].StopAllAction();

            playerShips.Clear();
        }
        public void RemoveShip(Ship ship)
        {
            GameShip sh = null;
            for (int i = 0; i < playerShips.Count; i++)
            {
                if (playerShips[i].mainShip.id == ship.id)
                {
                    sh = playerShips[i];

                    break;
                }
            }
            if (sh != null)
            {
                if (sh.mainShip.state != Ship.ShipState.stay)
                    sh.StopAllAction();

                playerShips.Remove(sh);

                SaveShips();
            }
        }
        public void SaveShips() => shipSaver.SaveShips();
        public List<ShipInfo> GetShipsInfo()
        {
            List<ShipInfo> result = new List<ShipInfo>();

            for(int i = 1; i < playerShips.Count; i++)
            {
                ShipInfo info = new ShipInfo()
                {
                    id = playerShips[i].mainShip.id,
                    name = playerShips[i].mainShip.name,
                    state = playerShips[i].mainShip.state.ToString()
                };
                result.Add(info);
            }

            return result;
        }
        public Ship GetShipById(int shipId)
        {
            Ship result = null;

            for(int i = 0; i < playerShips.Count; i++)
            {
                if (playerShips[i].mainShip.id == shipId)
                {
                    result = playerShips[i].mainShip;
                    break;
                }
            }

            return result;
        }
    }
}
