using Game.MapSystem.Models;
using Game.Models;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xamarin.Forms;

namespace Game.MapSystem
{
    class MapDrawner
    {
        SKCanvasView canvas;
        SKImageInfo lastCanvasInfo;

        GeneralChunk chunk;

        public AreaChunk _lastSelectAreaChunk;
        public RegionChunk _lastSelectRegionChunk;

        Location _lastTouchPosition;
        Location _lastGeneralPos;
        Location _lastAreaPos;
        Location _lastRegionPos;
        Location startPoint;
        Location endPoint;

        Label lastXLabel;
        Label lastYlabel;

        Label selectAreaLabel;
        Label selectRegionlabel;

        int _lastSelectX;
        int _lastSelectY;

        public MapState currentState;

        bool _DrawTouchPoint;
        public MapDrawner(SKCanvasView mainCanvaw)
        {
            canvas = mainCanvaw;

            startPoint = Location.zero;
            endPoint = new Location(125, (int)canvas.Height);

            currentState = MapState.GENERAL;
            _DrawTouchPoint = true;
        }
        public void SetChunk(GeneralChunk c) => chunk = c;
        public void SetPositionsLabels(Label xLabel, Label ylabel)
        {
            lastXLabel = xLabel;
            lastYlabel = ylabel;
        }
        public void SetSelectInfoLabels(Label l1, Label l2)
        {
            selectAreaLabel = l1;
            selectRegionlabel = l2;
        }

        public void onCanvasTouch(object sender, SKTouchEventArgs e)
        {
            _lastTouchPosition = new Location((int)e.Location.X, (int)e.Location.Y);

            Chunk sChunk = null;
            switch(currentState)
            {
                case MapState.GENERAL:
                    sChunk = chunk;

                    _lastGeneralPos = _lastTouchPosition;
                    break;
                case MapState.AREA:
                    if (_lastSelectAreaChunk == null)
                        return;

                    sChunk = _lastSelectAreaChunk;
                    
                    _lastAreaPos = _lastTouchPosition;
                    break;
                case MapState.REGION:
                    if (_lastSelectAreaChunk == null && _lastSelectRegionChunk == null)
                        return;

                    sChunk = _lastSelectRegionChunk;
                    
                    _lastRegionPos = _lastTouchPosition;
                    break;
            }

            _lastSelectX = (int)(_lastTouchPosition.x / (lastCanvasInfo.Width / sChunk.width));
            _lastSelectY = (int)(_lastTouchPosition.y / (lastCanvasInfo.Height / sChunk.height));

            lastXLabel.Text = $"x: {_lastSelectX}";
            lastYlabel.Text = $"y: {_lastSelectY}";

            switch (currentState)
            {
                case MapState.GENERAL:

                    var nch = chunk.GetChunkByPosition(new Location(_lastSelectX, _lastSelectY));

                    if (_lastSelectAreaChunk != nch)
                    {
                        _lastSelectRegionChunk = null;

                        _lastAreaPos = null;
                        _lastRegionPos = null;
                    }
                    _lastSelectAreaChunk = nch;
                    break;
                case MapState.AREA:

                    var nach = _lastSelectAreaChunk.GetChunkByPosition(new Location(_lastSelectX, _lastSelectY));

                    if (_lastSelectRegionChunk != nach)
                    {
                        _lastRegionPos = null;
                    }
                    _lastSelectRegionChunk = nach;
                    break;
                case MapState.REGION:
                    break;
            }
            selectAreaLabel.Text = $"Выбранный район: {_lastSelectAreaChunk.name}";
            
            if (_lastSelectRegionChunk != null)
                selectRegionlabel.Text = $"Выбранный регион: {_lastSelectRegionChunk.name}";
            else
                selectRegionlabel.Text = $"Выбранный регион: -";

            UpdateMapCanvas();
        }
        public void UpdateMapCanvas()
        {
            canvas.InvalidateSurface();
        }
        public void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            lastCanvasInfo = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear(Color.LightGray.ToSKColor());

            switch(currentState)
            {
                case MapState.GENERAL:
                    DrawGeneralGrid(canvas, lastCanvasInfo);

                    _lastTouchPosition = _lastGeneralPos;
                    break;
                case MapState.AREA:
                    if (_lastSelectAreaChunk != null)
                        DrawAreaGrid(_lastSelectAreaChunk, canvas, lastCanvasInfo);

                    _lastTouchPosition = _lastAreaPos;
                    break;
                case MapState.REGION:
                    if (_lastSelectRegionChunk != null)
                        DrawAreaGrid(_lastSelectRegionChunk, canvas, lastCanvasInfo);
                    
                    _lastTouchPosition = _lastRegionPos;
                    break;
            }
            

            if (_DrawTouchPoint)
                DrawPoint(canvas, lastCanvasInfo);
        }
        void DrawGeneralGrid(SKCanvas canvas, SKImageInfo info) // Отрисовка основной сетки карты
        {
            SKPaint paint = new SKPaint
            {
                Color = Color.Black.ToSKColor(),
                StrokeWidth = 1,
                Style = SKPaintStyle.Fill,
            };
            startPoint.x = 0;
            startPoint.y = 0;
            endPoint.x = info.Width;
            endPoint.y = info.Height;

            float xPos = 0;
            float yPos = 0;

            float rWidth = info.Width / chunk.width;
            float rHeight = info.Height / chunk.height;

            for (int w = 0; w < chunk.width; w++)
            {
                xPos += rWidth;


                for(int h = 0; h < chunk.height; h++)
                {
                    yPos += rHeight;

                    canvas.DrawLine(new SKPoint(startPoint.x, yPos), new SKPoint(endPoint.x, yPos), paint);
                }
                canvas.DrawLine(new SKPoint(xPos, startPoint.y), new SKPoint(xPos, endPoint.y), paint); // По вертикали
            }
        }
        void DrawAreaGrid(Chunk chunk, SKCanvas canvas, SKImageInfo info)
        {
            SKPaint paint = new SKPaint
            {
                Color = Color.Black.ToSKColor(),
                StrokeWidth = 1,
                Style = SKPaintStyle.Fill,
            };
            startPoint.x = 0;
            startPoint.y = 0;
            endPoint.x = info.Width;
            endPoint.y = info.Height;

            float xPos = 0;
            float yPos = 0;

            float rWidth = info.Width / chunk.width;
            float rHeight = info.Height / chunk.height;

            for (int w = 0; w < chunk.width; w++)
            {
                xPos += rWidth;


                for (int h = 0; h < chunk.height; h++)
                {
                    yPos += rHeight;

                    canvas.DrawLine(new SKPoint(startPoint.x, yPos), new SKPoint(endPoint.x, yPos), paint);
                }
                canvas.DrawLine(new SKPoint(xPos, startPoint.y), new SKPoint(xPos, endPoint.y), paint); // По вертикали
            }
        }
        void DrawPoint(SKCanvas canvas, SKImageInfo info)
        {
            if (_lastTouchPosition == null) return;

            SKPaint paint = new SKPaint
            {
                Color = Color.Red.ToSKColor(),
                StrokeWidth = 1,
                Style = SKPaintStyle.Fill,
            };
            canvas.DrawCircle(new SKPoint(_lastTouchPosition.x, _lastTouchPosition.y), 5, paint);
        }
    }
    public enum MapState
    {
        GENERAL = 0,
        AREA = 1,
        REGION = 2
    }
}
