using Game.PlayerSystem;
using Game.MapSystem.Models;
using Game.Models;
using Game.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Game.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class sMapPage : ContentPage // Страница настроек карты
    {
        Player _selectPlayer;

        gSettingsPage _mainSettPage;

        sNewMapPage newMapPage;
        sMapSettingsPage mapSettingsPage;
        MapSystem.MapLoader mapLoader;

        ObservableCollection<MapChunkModel> maps = new ObservableCollection<MapChunkModel>();
        public sMapPage(gSettingsPage settPage)
        {
            InitializeComponent();

            _mainSettPage = settPage;

            newMapPage = new sNewMapPage(this);
            mapSettingsPage = new sMapSettingsPage(this);

            mapLoader = new MapSystem.MapLoader();

            mapList.ItemsSource = maps;
        }
        public void UpdateMapList()
        {
            maps.Clear();

            for(int i = 0; i < mapLoader.chunks.Count; i++)
            {
                GeneralChunk gChunk = mapLoader.chunks[i];

                int regCount = gChunk.chunks.Count;

                maps.Add(new MapChunkModel()
                {
                    tabindex = gChunk.id,
                    id = $"id: {gChunk.id}",
                    name = gChunk.name,
                    chunkCount = $"Карт: {regCount}"
                });
            }
            if (maps.Count > 0)
            {
                var map = mapLoader.GetChunkByID(_selectPlayer.map_id);

                string mapName = map != null ? map.name : _selectPlayer.map_id.ToString();
                _currentMapLabel.Text = $"Текущая карта: {mapName}";
            }
            else
                _currentMapLabel.Text = $"Текущая карта: -";
        }
        public void SelectPlayer(Player pl) => _selectPlayer = pl;
        public void SelectPlayerMap(int map_id)
        {
            _selectPlayer.map_id = map_id;

            _mainSettPage.onPlayerEdit(_selectPlayer);
        }
        public void CreateNewMap(GeneralChunk nChunk) => mapLoader.AddChunk(nChunk);
        public void RemoveMap(GeneralChunk chunk)
        {
            if(_selectPlayer.map_id == chunk.id)
            {
                for(int i = 0; i < mapLoader.chunks.Count; i++)
                {
                    if (mapLoader.chunks[i].id != chunk.id)
                    {
                        SelectPlayerMap(mapLoader.chunks[i].id);
                        break;
                    }
                }
            }
            mapLoader.RemoveChunkById(chunk.id);
        }
        public GeneralChunk GetChunkById(int id)
        {
            return mapLoader.GetChunkByID(id);
        }
        private void onMapSClick(object o, EventArgs e)
        {
            var butt = (Button)o;

            GeneralChunk sChunk = mapLoader.GetChunkByID(butt.TabIndex);

            Console.WriteLine($"id: {sChunk.id} n: {sChunk.chunks.Count}");

            mapSettingsPage.SetGeneralChunk(sChunk);
            mapSettingsPage.UpdateMapInfo();

            Navigation.PushModalAsync(mapSettingsPage, false);
        }
        private void onButtonNewMapClick(object o, EventArgs e)
        {
            Navigation.PushModalAsync(newMapPage, false);
        }
        private void onButtonClearMapClick(object o, EventArgs e)
        {
            for (int i = 0; i < mapLoader.chunks.Count; i++)
            {
                mapLoader.RemoveChunkById(mapLoader.chunks[i].id);
            }
            UpdateMapList();
        }
        private void buttonBackClick(object o, EventArgs e)
        {
            _mainSettPage.UpdateResourceInfo();

            Navigation.PopModalAsync(false);
        }
    }
}