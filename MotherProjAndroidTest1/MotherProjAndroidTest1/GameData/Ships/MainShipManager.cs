using Game.MapSystem.Models;
using Game.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Game.GameData.ShipManager
{
    class MainShipManager
    {
        Label _labelMapLocation;
        Label _labelPosition;
        Label _labelName;
        Label _labelPeople;
        Label _labelDecks;
        Label _labelModules;

        Models.Ship mainShip;
        public MainShipManager(Label[] labels)
        {
            _labelMapLocation = labels[0];
            _labelPosition = labels[1];
                
            _labelName = labels[2];
            _labelPeople = labels[3];
            _labelDecks = labels[4];
            _labelModules = labels[5];
        }
        public void SetShip(Models.Ship sp) => mainShip = sp;
        public void UpdateInfo()
        {
            _labelMapLocation.Text = $"Место: ({mainShip.location})";
            _labelPosition.Text = $"Позиция: {mainShip.position}";

            _labelName.Text = $"Корабль {mainShip.name}";
            _labelPeople.Text = $"Экипаж: {mainShip.people}";
            _labelDecks.Text = $"Палубы: {mainShip.decks.Count}";
            _labelModules.Text = $"Модули: {mainShip.modules.Count}";
        }
        string getGCName(GeneralChunk chunk, MapLocation location)
        {
            return chunk.chunks[location.R].chunks[location.A].name;
        }
    }
}
