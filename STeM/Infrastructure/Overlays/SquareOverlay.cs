using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Windows.Controls;

namespace STeM.Infrastructure.Overlays
{
    public class SquareOverlay : IOverlay
    {

        LinkedList<Vector2> _lastPositions = new LinkedList<Vector2>();

        Vector2 _correction = new Vector2(0, 0);

        public void DrawOn(ref Canvas source)
        {

        }

        public void OnEyePositionChanged(Vector2 position)
        {

        }
    }
}
