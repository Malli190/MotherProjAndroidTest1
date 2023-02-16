using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Controllers;
using Game.GameModules;
using Game.GameResource;
using Game.Models;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Game.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class gPerPage : ContentPage
    {
        ObservableCollection<GameRecTicketModel> resModList = new ObservableCollection<GameRecTicketModel>();

        GameModuleController moduleController;
        GameResourceController resourceController;

        Label _selectLabel;

        List<Resource> _localResource;
        public gPerPage(GameModuleController moduleController, GameResourceController resourceController)
        {
            InitializeComponent();

            this.moduleController = moduleController;
            this.resourceController = resourceController;

            this.moduleController.onModuleUpdate += onModuleActionUpdate;
            this.moduleController.onModuleActionEnd += onModuleActionEnd;

            _selectLabel = selectResourceLabel;

            perQueryList.ItemsSource = resModList;

            resourcePicker.Items.Clear();

            _localResource = resourceController.GetResourceByType(1);
            
            for (int i = 0; i < _localResource.Count; i++)
                resourcePicker.Items.Add(_localResource[i].SubName);

            resourcePicker.SelectedIndexChanged += ResourcePicker_SelectedIndexChanged;

            var modules = this.moduleController.GetModulesByType(0);

            perCountLabel.Text = modules.Count.ToString();
        }
        public void updateResourceInfo()
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
            var modules = moduleController.GetModulesByType(0);

            perCountLabel.Text = modules.Count.ToString();
        }
        private void ResourcePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectLabel.Text = $"{_localResource[resourcePicker.SelectedIndex].type} готово к переработке";
        }
        private void buttonLaunchPerClick(object p, EventArgs e) // Кнопка переработать ресурс
        {
            if(resourcePicker.SelectedItem == null)
            {
                DisplayAlert("Ресурсы", "Не выбран ресурс переработки", "OK");
                return;
            }
            // Выбрать доступные переработчики по индесу 0
            var modules = moduleController.GetModulesByType(0);

            if(modules.Count == 0)
            {
                DisplayAlert("Переработка", "Нет доступных переработчиков", "OK");
                return;
            }
            var mod = modules[0];

            GameRecTicketModel recModel = new GameRecTicketModel()
            {
                id = mod.id,
                perName = mod.Name,
                perSubName = _localResource[resourcePicker.SelectedIndex].SubName,
                resType = _localResource[resourcePicker.SelectedIndex].type,
                resSubType = _localResource[resourcePicker.SelectedIndex].subType,
                resCount = 1,
                progress = 0,
            };
            //recModel.button.Clicked += onModuleButtonCancelClick;

            resModList.Add(recModel);

            moduleController.SetLocalTicket(recModel);
            moduleController.startModuleTask(mod.id);

            updateResourceInfo();
        }
        private void onModuleButtonCancelClick(object o, EventArgs e) // Кнопка отменить на переработке
        {
            int modelIndex = ((Button)o).TabIndex - 1;

            moduleController.stopModuleTask(modelIndex);

            resModList.RemoveAt(modelIndex);

            _selectLabel.Text = $"отменен";
        }
        private void buttonBackClick(object o, EventArgs e)
        {
            Navigation.PopModalAsync(false);
        }
        private void onModuleActionUpdate(Module module, GameRecTicketModel ticket) // по обновлению прогресса модуля
        {
            //var mod = resModList.Where((x) => x.id == module.id).First();
            var mod = resModList[module.id - 1];

            // mod = recModel;

            for (int i = 0; i < resModList.Count; i++)
            {
                if (resModList[i].id == module.id)
                {
                    //GameRecTicketModel recModel = ticket;
                    GameRecTicketModel recModel = new GameRecTicketModel()
                    {
                        id = module.id,
                        perName = module.Name,
                        perSubName = resModList[i].perSubName,
                        resType = resModList[i].resType,
                        resSubType = resModList[i].resSubType,
                        resCount = resModList[i].resCount,
                        progress = module.progress,
                    };
                    resModList[i] = recModel;
                    break;
                }
            }

            Device.BeginInvokeOnMainThread(() => 
            {
                _selectLabel.Text = $"активно"; 
            });
        }
        private void onModuleActionEnd(Module module, GameRecTicketModel ticket) // По завершени работ модуля
        {
            var oreRes = resourceController.GetResourceByType(0);
            var metRes = resourceController.GetResourceByType(1);
            var matRes = resourceController.GetResourceByType(2);
            switch (ticket.resType)
            {
                case 0: // Руда
                    

                    switch (ticket.resSubType)
                    {
                        case 0:
                            oreRes[0].Count -= 1;

                            resourceController.GetResourceByType(1)[0].Count += 1;
                            break;
                        case 1:
                            break;
                    }
                    break;
                case 1: // Металлы
                    switch(ticket.resSubType)
                    {
                        case 0: // Железо
                            oreRes[0].Count -= 1;

                            metRes[0].Count += 1;
                            break;
                        case 1: // Медь
                            oreRes[1].Count -= 1;

                            metRes[1].Count += 1;
                            break;
                        case 2: // Сталь
                            oreRes[0].Count -= 1;
                            metRes[0].Count -= 1;

                            metRes[2].Count += 1;

                            break;
                        case 3: // алюминий
                            break;
                    }
                    break;
                case 2: // Материалы
                    break;
            }
            Device.BeginInvokeOnMainThread(() =>
            {
                resourceController.UpdateSaverData();
                updateResourceInfo();
                selectResourceLabel.Text = "завершено";
            });
            resModList.RemoveAt(0);
        }
    }
}