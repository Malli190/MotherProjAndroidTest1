using Game.Models;
using Game.Pages;
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
    public partial class sDeckEdit : ContentPage
    {
        Deck _deck;

        gShipsPage shipsPage;
        sDeck deckPage;
        public int selectRoom { get; set; }
        public sDeckEdit(gShipsPage sp)
        {
            InitializeComponent();

            shipsPage = sp;
        }
        public void SetDeckPage(sDeck d) => deckPage = d;
        public void SetDeck(Deck d) => _deck = d;
        public void UpdateDeckInfo()
        {
            _EntryDeckName.Text = _deck.name;
        }
        private void buttonSaveClick(object o, EventArgs e)
        {
            _deck.name = _EntryDeckName.Text;

            deckPage.EditDeck();
            deckPage.UpdateDeckInfo();

            Navigation.PopModalAsync(false);
        }
        private void buttonBackClick(object o, EventArgs e)
        {
            Navigation.PopModalAsync(false);
        }
    }
}