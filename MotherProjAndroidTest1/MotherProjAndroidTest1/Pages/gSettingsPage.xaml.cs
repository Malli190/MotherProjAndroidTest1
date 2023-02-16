using Game.Controllers;
using Game.MapSystem.Models;
using Game.PlayerSystem;
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
    public partial class gSettingsPage : ContentPage
    {
        pMain _mainPage;

        PlayerObject _player;

        gShipsPage _shipsPage;

        sGamePage _gamePage;
        sPlayerPage _playerPage;
        sMapPage settingsMapPage;

        GameResourceController _resourceController;

        public gSettingsPage(pMain mainPage, GameResourceController resourceController)
        {
            InitializeComponent();

            _mainPage = mainPage;

            _player = new PlayerObject();

            _gamePage = new sGamePage(this);
            _playerPage = new sPlayerPage(this);
            settingsMapPage = new sMapPage(this);

            _resourceController = resourceController;
        }
        public void SetShipsPage(gShipsPage p) => _shipsPage = p;
        public void NewGame()
        {
            _shipsPage.NewGame(GetCurrentMap());
            _resourceController.GenerateNewResources(true);
        }
        public void UpdateResourceInfo()
        {
            modulesCountLabel.Text = _mainPage.moduleController.moduleCount.ToString();
        }
        public GeneralChunk GetCurrentMap()
        {
            return settingsMapPage.GetChunkById(_player.playerModel.map_id);
        }
        private void onButtonGameClick(object o, EventArgs e)
        {
            Navigation.PushModalAsync(_gamePage, false);
        }
        public void onPlayerEdit(Models.Player pl)
        {
            _player.SetPlayer(pl);
        }
        private void onButtonPlayerClick(object o, EventArgs e)
        {
            _playerPage.SelectPlayer(_player.playerModel);

            Navigation.PushModalAsync(_playerPage, false);
        }
        private void onButtonMapSettingsClick(object o, EventArgs e)
        {
            settingsMapPage.SelectPlayer(_player.playerModel);
            settingsMapPage.UpdateMapList();

            Navigation.PushModalAsync(settingsMapPage, false);
        }
        private void buttonBackClick(object o, EventArgs e)
        {
            Navigation.PopModalAsync(false);
        }
    }
}