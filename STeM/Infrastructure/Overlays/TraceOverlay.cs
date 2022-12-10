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
        private readonly int _gazeLengthMilliseconds;
        private readonly Polyline _gazePath = new Polyline()
        {
            Stroke = Brushes.White,
            StrokeThickness = 5
        };
        private bool _pathInitialized = false;
        public TraceOverlay(int gazeLengthMilliseconds)
        {
            _gazeLengthMilliseconds = gazeLengthMilliseconds;
        }

        private readonly List<TimedVector2> _lastIntervalEyeGazePositions = new List<TimedVector2>();
        public override void OnEyePositionChanged(Vector2 position)
        {
            _lastIntervalEyeGazePositions.RemoveAll(x => DateTime.Now.Subtract(x.Time).TotalMilliseconds > _gazeLengthMilliseconds);
            _lastIntervalEyeGazePositions.Add(new TimedVector2(DateTime.Now, position));
        }

        public override void Update(Canvas parent)
        {
            if (!_pathInitialized)
            {
                parent.Children.Add(_gazePath);
                _pathInitialized = true;
            }

            if (!_lastIntervalEyeGazePositions.Select(x => x.Vector2).IsWhiteNoise())
            {
                _gazePath.Points = new PointCollection(_lastIntervalEyeGazePositions.Select(x => new System.Windows.Point(x.Vector2.X, x.Vector2.Y)));
            }
        }
    }
}
