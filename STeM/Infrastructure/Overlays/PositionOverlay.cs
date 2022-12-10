using OxyPlot;

using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace STeM.Infrastructure.Overlays
{
    public class PositionOverlay : OverlayBase
    {
        private Vector2 _lastPosition = new Vector2();
        private TextBlock _overlayTextBlock = new TextBlock()
        {
            Foreground = Brushes.White,
        };
        private bool _addedToCanvas = false;

        public override void Update(Canvas parent)
        {
            AddIfNeeded(parent);
            if (!_addedToCanvas)
            {
                _addedToCanvas = true;
                parent.Children.Add(_overlayTextBlock);
                Canvas.SetTop(_overlayTextBlock, 0);
                Canvas.SetLeft(_overlayTextBlock, 0);
            }
            _overlayTextBlock.Text = $"X: {_lastPosition.X}\nY: {_lastPosition.Y}";
        }

        public override void OnEyePositionChanged(Vector2 position)
        {
            _lastPosition = position;
        }
    }
}
