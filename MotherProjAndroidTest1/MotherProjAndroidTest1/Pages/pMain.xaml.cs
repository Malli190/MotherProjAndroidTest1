using System;

using Game.Controllers;
using Game.GameData.ShipManager;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class pMain : ContentPage
    {
        static Modules.Loading loadingPage;
        static INavigation stNavigation;

        gSettingsPage settingPage;
        gMapPage mapPage;
        gBuildPage buildPage;
        gShipsPage shipsPage;
        gPerPage perPage;

        //LogController logController;
        public GameModuleController moduleController;
        GameTimeController mainTimer;
        GameResourceController resourceController;
        GameBuildController buildController;

        ShipsManager shipsManager;

        public pMain()
        {
            InitializeComponent();

            stNavigation = Navigation;

            loadingPage = new Modules.Loading();
            //logController = new LogController(logList);

            resourceController = new GameResourceController();
            
            moduleController = new GameModuleController(moduleList);

            buildController = new GameBuildController();

            shipsManager = new ShipsManager();
            shipsManager.InitMainShipManager(new[] 
            {
                _labelShipMapLocation, _labelShipPosition, _labelShipName, _labelShipPeople, _labelShipDecks, _labelShipModules 
            });
            
            settingPage = new gSettingsPage(this, resourceController);
            mapPage = new gMapPage();
            buildPage = new gBuildPage(buildController, resourceController, this);
            shipsPage = new gShipsPage(shipsManager);
            perPage = new gPerPage(moduleController, resourceController);



            mainTimer = new GameTimeController();
            mainTimer.interval = 600;
            mainTimer.Tick += onTimerTick;

            settingPage.SetShipsPage(shipsPage);

            NavigationPage.SetHasNavigationBar(this, false);
            
        }
        public void UpdateShipInfo() => shipsManager.UpdateMainShipinfo();
        private void onButtonTestClick(object o, EventArgs e) // Тестовая кнопка
        {
            
        }
        private void onButtonMapPageClick(object o, EventArgs e) // Экран карта
        {
            mapPage.SetMap(settingPage.GetCurrentMap());

            Navigation.PushModalAsync(mapPage, false);
        }
        private void onButtonPageSettingsClick(object o, EventArgs e) // Переход на экран настроек
        {
            settingPage.UpdateResourceInfo();

            Navigation.PushModalAsync(settingPage, false);
        }
        private void onButtonShipInfoPage(object o, EventArgs e) // Экран инфы главного корабля
        {
            shipsPage.SetParent(this);

            Navigation.PushModalAsync(shipsPage.OpenMainShipInfoPage(), false);
        }
        private void onButtonShipsPage(object o, EventArgs e) // Экран корабли
        {
            shipsPage.UpdateShipList();

            Navigation.PushModalAsync(shipsPage, false);
        }
        private void onButtonBuildPage(object o, EventArgs e)
        {
            buildPage.UpdateResourceInfo();

            Navigation.PushModalAsync(buildPage, false);
        }
        private void onButtonPerPageClick(object o, EventArgs e)
        {
            perPage.updateResourceInfo();

            Navigation.PushModalAsync(perPage, false);
        }
        private void onTimerTick()
        {
            //logController.Write("timer tick");
        }
    }
    public class LogItem
    {
        public string message { get; set; }
    }
}