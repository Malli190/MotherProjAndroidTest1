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
    public partial class sDeckRoomEdit : ContentPage
    {
        gShipsPage shipsPage;

        Models.DeckRoom _room;
        public sDeckRoomEdit(gShipsPage sp)
        {
            InitializeComponent();

            shipsPage = sp;
        }
        public void SetRoom(Models.DeckRoom room) => _room = room;
        public void UpdateInfo()
        {
            _labelHead.Text = $"Редактирование каюта {_room.name}";

            _labelId.Text = $"id: {_room.id}";
            _entryRoomName.Text = $"{_room.name}";
        }
        private void buttonSaveClick(object o, EventArgs e)
        {
            _room.name = _entryRoomName.Text;

            var deckRoom = (sDeckRoom)shipsPage.GetDeckRoomPage();
            var deck = (sDeck)shipsPage.GetDeckPage();

            deck.EditDeck();

            deckRoom.UpdateDeckRoomInfo();

            Navigation.PopModalAsync(false);
        }
        private void buttonBackClick(object o, EventArgs e)
        {
            Navigation.PopModalAsync(false);
        }
    }
}