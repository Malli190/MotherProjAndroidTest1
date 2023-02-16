using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Pages.ShipPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class sInfo : ContentPage
    {
        pMain mainPage;
        gShipsPage shipsPage;

        Models.Ship selectShip;

        ObservableCollection<Models.DeckInfo> decks = new ObservableCollection<Models.DeckInfo>();

        public bool isParentSend;
        public sInfo(gShipsPage sp)
        {
            InitializeComponent();

            shipsPage = sp;

            deckList.ItemsSource = decks;
        }
        public void SetMainPage(pMain mp)
        {
            mainPage = mp;

            isParentSend = true;
        }
        public void SetShip(Models.Ship ship)
        {
            selectShip = ship;
        }
        public void UpdateShipinfo()
        {
            _labelId.Text = $"id: {selectShip.id}";
            _labelName.Text = $"Имя: {selectShip.name}";
            _labelPeople.Text = $"Экипаж: {selectShip.people}";
            _labelDecks.Text = $"Палубы: {selectShip.decks.Count}";
            _labelModules.Text = $"Модули: {selectShip.modules.Count}";

            UpdateDeckInfo();
        }
        void UpdateDeckInfo()
        {
            decks.Clear();

            for (int i = 0; i < selectShip.decks.Count; i++)
            {
                Models.DeckInfo dInfo = new Models.DeckInfo()
                {
                    id = $"id: {selectShip.decks[i].id}",
                    tab_id = selectShip.decks[i].id,
                    name = $"{selectShip.decks[i].name}",
                    level = $"Уровень {selectShip.decks[i].level}",
                    people = $"кают: {selectShip.decks[i].decks.Count}"
                };
                decks.Add(dInfo);
            }
        }
        private void onButtonDeckClick(object o, EventArgs e)
        {
            int id = ((Button)o).TabIndex;

            var shipDeckPage = (sDeck)shipsPage.GetDeckPage();

            shipDeckPage.SetShip(selectShip);
            shipDeckPage.selectDeck = id;
            shipDeckPage.UpdateDeckInfo();

            Navigation.PushModalAsync(shipDeckPage, false);
        }
        private async void onButtonNewDeckClick(object o, EventArgs e)
        {
            string[] bO = { "Уровевь выше", "Уровень ниже" };
            string result = await DisplayActionSheet("Новая палуба", "отмена", null, bO);

            Models.Deck nDeck = new Models.Deck();

            nDeck.name = "новая";
            nDeck.id = GetNextDeckId();

            if (result == bO[0])
                nDeck.level = GetNextDeckUPID(selectShip.decks, true);
            else if (result == bO[1])
                nDeck.level = GetNextDeckUPID(selectShip.decks, false);
            else if (result == "отмена" || result == "" || result == null)
                return;
            
            selectShip.decks.Add(nDeck);
            shipsPage.EditShip(selectShip);

            UpdateShipinfo();
        }
        public void RemoveDeck(Models.Deck deck)
        {
            selectShip.decks.Remove(deck);

            shipsPage.EditShip(selectShip);

            UpdateShipinfo();
        }
        private void buttonBackClick(object o, EventArgs e)
        {
            if (!isParentSend)
                shipsPage.UpdateShipList();
            else
                mainPage.UpdateShipInfo();

            Navigation.PopModalAsync(false);
        }
        int GetNextDeckId()
        {
            List<int> ids = new List<int>();

            for (int i = 0; i < selectShip.decks.Count; i++)
                ids.Add(selectShip.decks[i].id);

            int result = ids.Count > 0 ? ids.Max() : 0;

            result += 1;

            return result;
        }
        int GetNextDeckUPID(List<Models.Deck> decks, bool levelUp)
        {
            List<int> ids = new List<int>();

            for (int i = 0; i < decks.Count; i++)
                ids.Add(decks[i].level);

            int result = ids.Count > 0 ? levelUp ? ids.Max() : ids.Min() : 0;

            if (levelUp)
                result += 1;
            else
                result -= 1;

            return result;
        }
    }
}