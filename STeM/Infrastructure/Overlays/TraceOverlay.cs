using STeM.Infrastructure.Extensions;
using STeM.Infrastructure.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;

namespace STeM.Infrastructure.Overlays
{
    /// <summary>
    /// Creates an overlay displaying the path the user's eyes had gazed over
    /// </summary>
    public class TraceOverlay : OverlayBase
    {
        private Brush _brush;
        private const double OPACITY = 0.8;
        private readonly Polyline _gazePath = new Polyline()
        {
            StrokeThickness = 5
        };
        public TraceOverlay()
        {
            _brush = new SolidColorBrush(Colors.White);
            _gazePath.Stroke = _brush;
            RegisterElementsIfNeeded(_gazePath);
        }
        protected override void OnUpdateInternal()
        {

            _gazePath.Points = new PointCollection(_lastIntervalEyeGazePositions.Select(x => new System.Windows.Point(x.Vector2.X, x.Vector2.Y)));
        }
        protected override void OnFocus(Vector2 position)
        {
            _brush.Opacity = 0;
        }

        protected override void OnDefocus(Vector2 position)
        {
            _brush.Opacity = OPACITY;
        }
    }
}
