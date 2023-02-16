using Game.MapSystem.Models;
using Game.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class sMapSettingsPage : ContentPage
    {
        ObservableCollection<MapChunkRegionModel> regions;

        sMapPage _mapPage;

        GeneralChunk selectMapChunk;
        public sMapSettingsPage(sMapPage mapPage)
        {
            InitializeComponent();

            _mapPage = mapPage;

            regions = new ObservableCollection<MapChunkRegionModel>();

            chunkRegionList.ItemsSource = regions;
        }
        public void SetGeneralChunk(GeneralChunk chunk)
        {
            selectMapChunk = chunk;
        }
        public void UpdateMapInfo()
        {
            int areaCount = 0;
            int areaPerReg = 0;

            for (int i = 0; i < selectMapChunk.chunks.Count; i++)
            {
                areaCount += selectMapChunk.chunks[i].chunks.Count;
                areaPerReg = selectMapChunk.chunks[i].chunks.Count;
            }
            headerLabel.Text = $"Карта: {selectMapChunk.name}";

            label_id.Text = $"id: {selectMapChunk.id}";

            label_width.Text = $"Ширина: {selectMapChunk.width}";
            label_height.Text = $"Высота: {selectMapChunk.height}";

            label_region_count.Text = $"Кол-регионов: {selectMapChunk.chunks.Count}";
            label_areaPerRegion_count.Text = $"Участков на регион: {areaPerReg}";
            label_area_count.Text = $"Кол-участков: {areaCount}";

            UpdateRegionList();
        }
        void UpdateRegionList()
        {
            regions.Clear();

            for (int i = 0; i < selectMapChunk.chunks.Count; i++)
            {
                AreaChunk areaChunk = selectMapChunk.chunks[i];
                
                int cCount = /*areaChunk.chunks.Count*/0;
                
                for (int r = 0; r < areaChunk.chunks.Count; r++)
                    cCount += areaChunk.chunks[r].chunks.Count;
                
                MapChunkRegionModel regionM = new MapChunkRegionModel()
                {
                    tab_id = i,
                    label_id = $"id: {i} x: {selectMapChunk.chunks[i].position.x} y: {selectMapChunk.chunks[i].position.y}",
                    label_reg = $"Рег. id: {selectMapChunk.chunks[i].id}",
                    name = $"Наз.:{selectMapChunk.chunks[i].name}",
                    areas = $"Учст.: {cCount}",
                    text = $"ш. {selectMapChunk.chunks[i].width}, в. {selectMapChunk.chunks[i].height} регионов: {areaChunk.chunks.Count}"
                };

                regions.Add(regionM);
            }
            
        }
        private void onButtonUseClick(object o, EventArgs e)
        {
            _mapPage.SelectPlayerMap(selectMapChunk.id);
            _mapPage.UpdateMapList();

            buttonBackClick(o, e);
        }
        private void onButtonDeleteClick(object o, EventArgs e)
        {
            _mapPage.RemoveMap(selectMapChunk);
            _mapPage.UpdateMapList();

            buttonBackClick(o, e);
        }
        private void buttonBackClick(object o, EventArgs e)
        {
            Navigation.PopModalAsync(false);
        }
    }
}