using OxyPlot;

using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Windows.Controls;

namespace STeM.Infrastructure.Overlays
{
    public interface IOverlay
    {
        public void OnEyePositionChanged(Vector2 position);
        public void Update(Canvas parent);
    }
}
