using Game.MapSystem.Models;
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
    public partial class sNewMapPage : ContentPage
    {
        sMapPage _sMap;

        int _lastWidthValue;
        int _lastWidthRegValue;
        int _lastHeightValue;
        int _lastHeightRegValue;
        public sNewMapPage(sMapPage sMap)
        {
            InitializeComponent();

            _sMap = sMap;
        }
        void UpdateFinalMapInfo()
        {
            int finalW = _lastWidthValue * _lastWidthRegValue * 10;
            int finalH = _lastHeightValue * _lastHeightRegValue * 10;
            finalWidthLabel.Text = $"Ширина карты: {finalW}";
            finalHeightLabel.Text = $"Высота карты: {finalH}";
        }
        private void onButtonGenerateMapNameClick(object o, EventArgs e)
        {
            string name = StaticMethods.GetRandomMapName(4, 3);

            mapNameInput.Text = name;
        }
        private void onButtonCreateMapClick(object o, EventArgs e)
        {
            GeneralChunk chunk = new GeneralChunk(new Models.Location(_lastWidthValue, _lastHeightValue));

            chunk.name = mapNameInput.Text;
            chunk.SetRegionSize(new Models.Location(_lastWidthRegValue, _lastHeightRegValue));
            chunk.GenerateChunks();

            _sMap.CreateNewMap(chunk);
            _sMap.UpdateMapList();

            Navigation.PopModalAsync(false);
        }
        private void onEntryWFiledChancheValue(object sender, TextChangedEventArgs e)
        {
            changeValueChunk(entryWInputField, stepperWidth, ref _lastWidthValue);
        }
        private void onEntryWRegFiledChancheValue(object sender, TextChangedEventArgs e)
        {
            changeValueChunk(entryWRegInputField, stepperWidthReg, ref _lastWidthRegValue);
        }
        private void onEntryHFiledChancheValue(object sender, TextChangedEventArgs e)
        {
            changeValueChunk(entryHInputField, stepperHeight, ref _lastHeightValue);
        }
        private void onEntryHRegFiledChancheValue(object sender, TextChangedEventArgs e)
        {
            changeValueChunk(entryHRegInputField, stepperHeightReg, ref _lastHeightRegValue);
        }
        private void onWidthStepperChanche(object o, EventArgs e)
        {
            Stepper stp = (Stepper)o;

            int stpValue = (int)stp.Value;

            changeChunkValueText(stpValue, ref _lastWidthValue, entryWInputField);
        }
        private void onWidthRegStepperChanche(object o, EventArgs e)
        {
            Stepper stp = (Stepper)o;

            int stpValue = (int)stp.Value;

            changeChunkValueText(stpValue, ref _lastWidthRegValue, entryWRegInputField);
        }
        private void onHeightStepperChanche(object o, EventArgs e)
        {
            Stepper stp = (Stepper)o;

            int stpValue = (int)stp.Value;

            changeChunkValueText(stpValue, ref _lastHeightValue, entryHInputField);
        }
        private void onHeightRegStepperChanche(object o, EventArgs e)
        {
            Stepper stp = (Stepper)o;

            int stpValue = (int)stp.Value;

            changeChunkValueText(stpValue, ref _lastHeightRegValue, entryHRegInputField);
        }
        private void buttonBackClick(object o, EventArgs e)
        {
            Navigation.PopModalAsync(false);
        }
        void changeValueChunk(Entry inputField, Stepper stp, ref int lastValue)
        {
            int nWSize;

            if (inputField.Text.Length == 0)
                return;

            try
            {
                nWSize = int.Parse(inputField.Text);

                if (nWSize > stp.Maximum)
                    throw new Exception();

                lastValue = nWSize;

                stp.Value = nWSize;

                inputField.Text = lastValue.ToString();

                UpdateFinalMapInfo();
            }
            catch
            {
                DisplayAlert("Карта", $"Ошибка ввода\nдолжно быть целое число в диапазоне от 0 до {stp.Maximum}", "OK");
            }
        }
        void changeChunkValueText(int parseValue, ref int lastValue, Entry inputField)
        {
            if (parseValue > lastValue)
            {
                lastValue += 1;
            }
            else if (parseValue < lastValue)
            {
                lastValue -= 1;
            }
            else if (parseValue == lastValue)
            {

            }
            inputField.Text = lastValue.ToString();

            UpdateFinalMapInfo();

            lastValue = parseValue;
        }
    }
}