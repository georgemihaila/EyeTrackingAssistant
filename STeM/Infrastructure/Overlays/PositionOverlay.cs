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
        public PositionOverlay()
        {
            RegisterElementsIfNeeded(_overlayTextBlock);
        }
        protected override void OnUpdateInternal()
        {
            _overlayTextBlock.Text = $"X: {_lastPosition.X}\nY: {_lastPosition.Y}";
        }
    }
}
