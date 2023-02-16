using Game.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Game.Models.Ship;

namespace Game.Pages.ShipPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class sDeck : ContentPage
    {
        ObservableCollection<DeckRoomInfo> deckRooms = new ObservableCollection<DeckRoomInfo>();

        gShipsPage shipsPage;
        sInfo shipInfoPage;

        Ship _ship;

        Deck currentDeck
        {
            get
            {
                Deck result = null;

                for (int i = 0; i < _ship.decks.Count; i++)
                {
                    if (_ship.decks[i].id == selectDeck)
                    {
                        result = _ship.decks[i];
                        break;
                    }
                }
                return result;
            }
            set
            {
                currentDeck = value;
            }
        }

        public int selectDeck { get; set; }
        public sDeck(gShipsPage sp)
        {
            InitializeComponent();

            shipsPage = sp;

            deckRoomList.ItemsSource = deckRooms;
        }
        public void SetShipInfoP(sInfo p) => shipInfoPage = p;
        public void SetShip(Ship s) => _ship = s;
        public void UpdateDeckInfo()
        {
            _labelHead.Text = $"Палуба {currentDeck.level}";

            _labelDeckLevel.Text = $"Уровень: {currentDeck.level}";

            _labelDeckName.Text = $"п. {currentDeck.name}";
            _labelPeople.Text = $"Экипаж: {currentDeck.people}";

            UpdateDeckRooms();
        }
        void UpdateDeckRooms()
        {
            deckRooms.Clear();

            for(int i = 0; i < currentDeck.decks.Count; i++)
            {
                DeckRoom dr = currentDeck.decks[i];

                deckRooms.Add(new DeckRoomInfo() 
                { 
                    id = $"id : {dr.id}",
                    tab_id = dr.id,
                    name = $"{dr.name}",
                    health = $"прочность: {dr.health}",
                    width = $"ш.: {dr.width}",
                    height = $"в.: {dr.height}",
                    people = $"э.: {dr.people}",
                });
            }
        }
        public void EditDeck()
        {
            //currentDeck = ed;

            shipsPage.EditShip(_ship);
        }
        private void onSelectRoomClick(object o, EventArgs e)
        {
            sDeckRoom deckRoomPage = (sDeckRoom)shipsPage.GetDeckRoomPage();

            int id = ((Button)o).TabIndex;

            deckRoomPage.SetDeck(currentDeck);
            deckRoomPage.selectRoom = id;
            deckRoomPage.UpdateDeckRoomInfo();

            Navigation.PushModalAsync(deckRoomPage, false);
        }
        private void onButtonEditDeckClick(object o, EventArgs e)
        {
            sDeckEdit editPage = (sDeckEdit)shipsPage.GetDeckEditPage();

            editPage.SetDeckPage(this);
            editPage.SetDeck(currentDeck);
            editPage.UpdateDeckInfo();

            Navigation.PushModalAsync(editPage, false);
        }
        private void onButtonAddDeckRoom(object o, EventArgs e)
        {
            currentDeck.decks.Add(new DeckRoom() 
            { 
                id = GetNextDeckId(),
                name= $"новая",
            });
            EditDeck();
            UpdateDeckInfo();
        }
        private async void onButtonRemoveDeck(object o, EventArgs e)
        {
            bool answer = await DisplayAlert("Палуба", "Разобрать палубу?", "Да", "Нет");

            if (answer)
            {
                shipInfoPage.RemoveDeck(currentDeck);

                Navigation.PopModalAsync(false);
            }
        }
        private void buttonBackClick(object o, EventArgs e)
        {
            shipInfoPage.UpdateShipinfo();

            Navigation.PopModalAsync(false);
        }
        int GetNextDeckId()
        {
            List<int> ids = new List<int>();

            for (int i = 0; i < currentDeck.decks.Count; i++)
                ids.Add(currentDeck.decks[i].id);

            int result = ids.Count > 0 ? ids.Max() : 0;

            result += 1;

            return result;
        }
    }
}