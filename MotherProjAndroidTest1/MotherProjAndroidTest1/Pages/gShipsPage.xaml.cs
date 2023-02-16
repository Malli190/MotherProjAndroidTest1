using Game.GameData.ShipManager;
using Game.MapSystem.Models;
using Game.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class gShipsPage : ContentPage
    {
        ShipsManager shipsManager;

        ShipPages.sInfo sInfoPage;
        ShipPages.sDeck sDeckPage;
        ShipPages.sDeckEdit sDeckEditPage;
        ShipPages.sDeckRoom sDeckRoomPage;
        ShipPages.sDeckRoomEdit sDeckRoomEdit;

        ObservableCollection<ShipInfo> shipInfo = new ObservableCollection<ShipInfo>();
        public gShipsPage(ShipsManager sm)
        {
            InitializeComponent();

            shipsManager = sm;

            sInfoPage = new ShipPages.sInfo(this);
            sDeckPage = new ShipPages.sDeck(this);
            sDeckEditPage = new ShipPages.sDeckEdit(this);
            sDeckRoomPage = new ShipPages.sDeckRoom(this);
            sDeckRoomEdit = new ShipPages.sDeckRoomEdit(this);

            sDeckPage.SetShipInfoP(sInfoPage);

            shipsList.ItemsSource = shipInfo;
        }
        public void UpdateShipList()
        {
            shipInfo.Clear();

            var sList = shipsManager.GetShipsInfo();

            foreach (var info in sList)
                shipInfo.Add(info);

            labelAllShips.Text = $"Всего кораблей: {shipsManager.shipsCount}";
        }
        public void NewGame(GeneralChunk g) => shipsManager.NewGame(g);
        public void EditShip(Ship shp) => shipsManager.EditShip(shp);
        public void SetParent(pMain mp) => sInfoPage.SetMainPage(mp);
        public Page OpenMainShipInfoPage()
        {
            var selectShip = shipsManager.GetShipById(0);

            sInfoPage.SetShip(selectShip);
            sInfoPage.UpdateShipinfo();

            return sInfoPage;
        }
        public Page GetsInfoPage() { return sInfoPage; }
        public Page GetDeckPage() { return sDeckPage; }
        public Page GetDeckRoomPage() { return sDeckRoomPage; }
        public Page GetDeckEditPage() { return sDeckEditPage; }
        public Page GetDeckRoomEditPage() { return sDeckRoomEdit; }
        private void buttonShipInfoClick(object o, EventArgs e)
        {
            Button button = (Button)o;

            int shipId = button.TabIndex;

            var selectShip = shipsManager.GetShipById(shipId);

            sInfoPage.isParentSend = false;
            sInfoPage.SetShip(selectShip);
            sInfoPage.UpdateShipinfo();

            Navigation.PushModalAsync(sInfoPage, false);
        }
        private void buttonBackClick(object o, EventArgs e)
        {
            Navigation.PopModalAsync(false);
        }
    }
}