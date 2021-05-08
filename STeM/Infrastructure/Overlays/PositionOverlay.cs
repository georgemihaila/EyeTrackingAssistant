using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace STeM.Infrastructure.Overlays
{
    public class PositionOverlay : IOverlay
    {
        private Vector2 _lastPosition = new Vector2();
        private TextBlock _overlayTextBlock = new TextBlock()
        {
            Background = Brushes.White
        };
        private bool _addedToCanvas = false;

        public void DrawOn(ref Canvas source)
        {
            if (!_addedToCanvas)
            {
                _addedToCanvas = true;

                source.Children.Add(source);
                Canvas.SetTop(_overlayTextBlock, 0);
                Canvas.SetLeft(_overlayTextBlock, 0);
            }
            _overlayTextBlock.Text = $"X: {_lastPosition.X}\nY: {_lastPosition.Y}";
        }

        public void OnEyePositionChanged(Vector2 position)
        {
            _lastPosition = position;
        }
    }
}
