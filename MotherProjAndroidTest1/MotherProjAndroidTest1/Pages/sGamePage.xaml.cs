using Game.Pages;
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
    public partial class sGamePage : ContentPage
    {
        gSettingsPage _settPage;
        public sGamePage(gSettingsPage sP)
        {
            InitializeComponent();

            _settPage = sP;
        }
        private async void onNewGameButtonClick(object o, EventArgs e)
        {
            bool answer = await DisplayAlert("Игра", "Начать новую игру?\nТекущие настройки будут сброшены", "Да", "Нет");

            if(answer)
            {
                _settPage.NewGame();
            }
        }
        private void buttonBackClick(object o, EventArgs e)
        {
            Navigation.PopModalAsync(false);
        }
    }
}