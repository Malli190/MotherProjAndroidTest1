using Game.Models;
using Game;
using Game.Controllers;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Game.Controllers.GameModuleController;
using System.Threading.Tasks;
using System.Linq;

namespace Game.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class gBuildPage : ContentPage
    {
        ObservableCollection<BuildQueryModel> buildTypeModels = new ObservableCollection<BuildQueryModel>();

        pMain _mainPage;

        GameBuildController buildController;
        GameResourceController resourceController;

        GameBuildTypeModel _selectBuildTypeModel;

        public gBuildPage(GameBuildController bController, GameResourceController rController, pMain mainPage)
        {
            InitializeComponent();

            _mainPage = mainPage;

            buildController = bController;
            resourceController = rController;

            var buildTypeNames = StaticBuildingMethods.BuildTypeNames;

            buildTypeNamesPicker.Items.Clear();
            buildSeledSubTypePicker.Items.Clear();
            buildTypeModels.Clear();

            //buildQueryList.ItemTemplate = new DataTemplate();
            buildQueryList.ItemsSource = buildTypeModels;

            for (int i = 0; i < buildTypeNames.Length; i++)
                buildTypeNamesPicker.Items.Add(buildTypeNames[i]);

            buildController.onModuleUpdate += onBuildProgressUpdate;
            buildController.onModuleActionEnd += onBuildEndProgress;
            
            buildTypeNamesPicker.SelectedIndexChanged += onBuildTypePickerSelect;
            buildSeledSubTypePicker.SelectedIndexChanged += onBuildSubTypePickerSelect;

            buttonBuildCancel.IsEnabled = false;
        }
        private void onBuildTypePickerSelect(object sender, EventArgs e) // Когда выбрал тип строительства
        {
            selectedBuildTypeLabel.Text = $"Тип {buildTypeNamesPicker.SelectedItem}";
            
            // Собирается список суб-типа из выбранного типа
            SetBuildSubTypeParameter(buildTypeNamesPicker.SelectedIndex);
            
            selectBuildLabel.Text = $"Выбран: {buildTypeNamesPicker.SelectedItem} | -";
        }
        private void onBuildSubTypePickerSelect(object sender, EventArgs e)// Когда выбрал суб тип строительства
        {
            selectBuildLabel.Text = $"Выбран: {buildTypeNamesPicker.SelectedItem} | {buildSeledSubTypePicker.SelectedItem}";

            GameBuildTypeModel typeModel = new GameBuildTypeModel()
            {
                id = 0,
                name = $"{buildTypeNamesPicker.SelectedItem} | {buildSeledSubTypePicker.SelectedItem}",
                type = buildTypeNamesPicker.SelectedIndex,
                subType = buildSeledSubTypePicker.SelectedIndex
            };
            _selectBuildTypeModel = typeModel;
        }
        private void buttonBuildClick(object o, EventArgs e) // Кнопка построить
        {
            if(buildSeledSubTypePicker.SelectedItem == null || buildTypeNamesPicker.SelectedItem == null)
            {
                DisplayAlert("Строительство", "Не выбран тип/подтип строительства", "OK");
                return;
            }

            buttonBuildCancel.IsEnabled = true;

            BuildQueryModel bModel = new BuildQueryModel() 
            {
                query_id = StaticMethods.GetNextQueryId(buildTypeModels.ToList()),
                name = _selectBuildTypeModel.name,
                status = "в очереди",
                progress = 0,
                gameModel = _selectBuildTypeModel,
            };
            buildTypeModels.Add(bModel);

            if (!buildController.active)
            {
                buildController.StartBuild(_selectBuildTypeModel, bModel);
            }
        }
        private void buttonBuildCancelClick(object o, EventArgs e) // Отменить строительство
        {

            buildController.AbortBuild();

            buildTypeModels.RemoveAt(0);

            if (buildTypeModels.Count == 0)
                buttonBuildCancel.IsEnabled = false;
            else
                Task.Run(() => WaitAndCheckBuildQuery());
        }
        void onBuildEndProgress(GameModules.Module module, BuildQueryModel qModel) // Событие по окончанию
        {
            //buildTypeModels[0] = qModel;
            buildTypeModels.Remove(qModel);

            Task.Run(() => WaitAndCheckBuildQuery());
        }
        void onBuildProgressUpdate(GameModules.Module module, BuildQueryModel qModel)// Событие по тику
        {
            //buildTypeModels[0] = qModel;


            for (int i = 0; i < buildTypeModels.Count; i++)
            {
                if (buildTypeModels[i].query_id == qModel.query_id)
                {
                    buildTypeModels[i] = qModel;
                    break;
                }
            }
        }
        public void buttonBackClick(object o, EventArgs e)
        {
            _mainPage.UpdateShipInfo();

            Navigation.PopModalAsync(false);
        }
        async void WaitAndCheckBuildQuery()
        {
            await Task.Delay(300);

            if (buildTypeModels.Count > 0)
            {
                BuildQueryModel bModel = buildTypeModels[0];

                buildController.StartBuild(bModel.gameModel, bModel);
            }
            else
            {
                if (buildController.active)
                    buildController.active = false;

                Device.BeginInvokeOnMainThread(() => 
                {
                    buttonBuildCancel.IsEnabled = false;
                });
            }
        }
        void SetBuildSubTypeParameter(int idx)
        {
            buildSeledSubTypePicker.Items.Clear();
            switch (idx)
            {
                case 0:
                    var shipNames = StaticBuildingMethods.subTypeShips;

                    for (int i = 0; i < shipNames.Count; i++)
                        buildSeledSubTypePicker.Items.Add(shipNames[i].name);
                    break;
                case 1:
                    var moduleNames = StaticBuildingMethods.subTypeModules;

                    for (int i = 0; i < moduleNames.Count; i++)
                        buildSeledSubTypePicker.Items.Add(moduleNames[i].name);
                    break;

                case 2:
                    var detailNames = StaticBuildingMethods.subTypeMaterials;

                    for (int i = 0; i < detailNames.Count; i++)
                        buildSeledSubTypePicker.Items.Add(detailNames[i].name);
                    break;
                case 3:
                    break;
            }
        }
        public void UpdateResourceInfo()
        {
            var resOre = resourceController.GetResourceByType(0);
            var resMetalls = resourceController.GetResourceByType(1);
            var resMaterials = resourceController.GetResourceByType(2);

            _OreGrid.Children.Clear();
            _MetallGrid.Children.Clear();
            _MaterialGrid.Children.Clear();

            for (int i = 0; i < resOre.Count; i++)
            {
                var label = new Label() { Text = $"{resOre[i].SubName}: {resOre[i].Count}" };

                Grid.SetColumn(label, i);
                _OreGrid.Children.Add(label);
            }
            for (int i = 0; i < resMetalls.Count; i++)
            {
                var label = new Label() { Text = $"{resMetalls[i].SubName}: {resMetalls[i].Count}" };

                Grid.SetColumn(label, i);
                _MetallGrid.Children.Add(label);
            }
            for (int i = 0; i < resMaterials.Count; i++)
            {
                var label = new Label() { Text = $"{resMaterials[i].Name}/{resMaterials[i].SubName}: {resMaterials[i].Count}" };

                Grid.SetColumn(label, i);
                _MaterialGrid.Children.Add(label);
            }
        }
    }
}