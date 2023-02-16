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
    public partial class sPlayerPage : ContentPage
    {
        Player selectPlayer;

        gSettingsPage _settPage;
        sPlayerEditPage _editPlayerPage;
        public sPlayerPage(gSettingsPage settingsPage)
        {
            InitializeComponent();

            _settPage = settingsPage;
            _editPlayerPage = new sPlayerEditPage(this);
        }
        public void SelectPlayer(Player pl)
        {
            selectPlayer = pl;

            _playerLabel_id.Text = $"Id: {selectPlayer.id}";
            _playerLabel_name.Text = $"Имя: {selectPlayer.name}";
            _playerLabel_map.Text = $"Карта id: {selectPlayer.map_id}";
        }
        public void onPlayerEdit(Player pl)
        {
            _settPage.onPlayerEdit(pl);
        }
        private void onButtonPlayerEditClick(object o, EventArgs e)
        {
            _editPlayerPage.SelectPlayer(selectPlayer);

            Navigation.PushModalAsync(_editPlayerPage, false);
        }
        private void buttonBackClick(object o, EventArgs e)
        {
            _settPage.UpdateResourceInfo();

            Navigation.PopModalAsync(false);
        }
    }
}