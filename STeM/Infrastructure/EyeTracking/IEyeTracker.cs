using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace STeM.Infrastructure.EyeTracking
{
    /// <summary>
    /// Represents an eye tracker
    /// </summary>
    public interface IEyeTracker
    {
        /// <summary>
        /// Occurs when the eye position changes.
        /// </summary>
        public event EventHandler<Vector2> EyePositionChanged;
    }
}
