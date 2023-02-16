using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.MapSystem;
using Game.MapSystem.Models;
using Game.Models;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class gMapPage : ContentPage
    {
        MapDrawner mapDrawner;
        SKCanvasView _canvasMap;

        public gMapPage()
        {
            InitializeComponent();

            _canvasMap = new SKCanvasView();

            mapDrawner = new MapDrawner(_canvasMap);

            mapDrawner.SetPositionsLabels(labelXPos, labelYPos);
            mapDrawner.SetSelectInfoLabels(currentAreaLabel, currentRegionLabel);

            _canvasMap.EnableTouchEvents = true;

            _canvasMap.WidthRequest = imageParentLayout.Width;
            _canvasMap.HeightRequest = imageParentLayout.Height;

            _canvasMap.PaintSurface += mapDrawner.OnCanvasViewPaintSurface;
            _canvasMap.Touch += mapDrawner.onCanvasTouch;

            imageParentLayout.Children.Add(_canvasMap);

            EnableButtons((Button)buttonsPanel.Children[0]);
        }
        public void SetMap(GeneralChunk chunk)
        {
            labelHead.Text = $"Карта: {chunk.name}";

            mapDrawner.SetChunk(chunk);
        }
        private void onButtonGenerallick(object o, EventArgs e)
        {
            EnableButtons((Button)o);
            mapDrawner.currentState = MapState.GENERAL;
            mapDrawner.UpdateMapCanvas();
        }
        private void onButtonAreaclick(object o, EventArgs e)
        {
            if(mapDrawner._lastSelectAreaChunk == null)
            {
                DisplayAlert("Карта", "Не выбран регион", "OK");
                return;
            }
            EnableButtons((Button)o);
            mapDrawner.currentState = MapState.AREA;
            mapDrawner.UpdateMapCanvas();
        }
        private void onButtonRegionclick(object o, EventArgs e)
        {
            if (mapDrawner._lastSelectRegionChunk == null)
            {
                DisplayAlert("Карта", "Не выбран район", "OK");
                return;
            }
            EnableButtons((Button)o);
            mapDrawner.currentState = MapState.REGION;
            mapDrawner.UpdateMapCanvas();
        }
        private void onButtonUpdateMapclick(object o, EventArgs e)
        {
            mapDrawner.UpdateMapCanvas();
        }
        private void buttonBackClick(object o, EventArgs e)
        {

            Navigation.PopModalAsync(false);
        }
        void EnableButtons(Button b)
        {
            b.IsEnabled = false;
            for(int i = 0; i < buttonsPanel.Children.Count; i++)
            {
                Button but = (Button)buttonsPanel.Children[i];

                if (but != b)
                {
                    but.IsEnabled = true;

                    //but.BackgroundColor = Color.DarkRed;
                }
            }
        }
    }
}