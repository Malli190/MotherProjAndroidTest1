using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Pages.ShipPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class sDeckRoom : ContentPage
    {
        Models.Deck _deck;

        Models.DeckRoom currentRoom
        {
            get
            {
                Models.DeckRoom result = null;
                for(int i = 0; i < _deck.decks.Count; i++)
                {
                    if (_deck.decks[i].id == selectRoom)
                    {
                        result = _deck.decks[i];
                        break;
                    }
                }
                return result;
            }
        }

        gShipsPage shipsPage;
        public int selectRoom { get; set; }
        public sDeckRoom(gShipsPage sp)
        {
            InitializeComponent();

            shipsPage = sp;
        }
        public void SetDeck(Models.Deck d) => _deck = d;
        public void UpdateDeckRoomInfo()
        {
            _labelHead.Text = $"Каюта {currentRoom.name}";

            _labelRoomId.Text = $"id: {currentRoom.id}";
            _labelArmor.Text = $"Прочность: {currentRoom.health}";
            _labelPeople.Text = $"Экипаж: {currentRoom.people}";
            _labelMaxPeople.Text = $"Макс. экипаж: {currentRoom.peopleMax}";
        }
        private void buttonEditRoomClick(object o, EventArgs e)
        {
            sDeckRoomEdit editPage = (sDeckRoomEdit)shipsPage.GetDeckRoomEditPage();

            editPage.SetRoom(currentRoom);
            editPage.UpdateInfo();

            Navigation.PushModalAsync(editPage, false);
        }
        private void buttonBackClick(object o, EventArgs e)
        {
            var deckPage = (sDeck)shipsPage.GetDeckPage();

            deckPage.UpdateDeckInfo();

            Navigation.PopModalAsync(false);
        }
    }
}