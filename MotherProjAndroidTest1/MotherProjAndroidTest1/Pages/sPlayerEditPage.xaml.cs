using Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class sPlayerEditPage : ContentPage
    {
        sPlayerPage _playerPage;

        Player _selectPlayer;
        public sPlayerEditPage(sPlayerPage playerPage)
        {
            InitializeComponent();

            _playerPage = playerPage;
        }
        public void SelectPlayer(Player pl)
        {
            _selectPlayer = pl;

            _editName.Text = _selectPlayer.name;
        }
        private void onButtonSaveClick(object o, EventArgs e) // Кнопка сохранить 
        {
            _playerPage.SelectPlayer(_selectPlayer);
            _playerPage.onPlayerEdit(_selectPlayer);

            buttonBackClick(o, e);
        }
        private void buttonBackClick(object o, EventArgs e)
        {
            Navigation.PopModalAsync(false);
        }

        private void onNameChanched(object sender, TextChangedEventArgs e)
        {
            if(_editName.Text.Length > 0)
            {
                _selectPlayer.name = _editName.Text;
            }
        }
    }
}