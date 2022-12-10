using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Windows.Controls;

namespace STeM.Infrastructure.Overlays
{
    public class SquareOverlay : OverlayBase
    {

        LinkedList<Vector2> _lastPositions = new LinkedList<Vector2>();

        Vector2 _correction = new Vector2(0, 0);
    }
}
