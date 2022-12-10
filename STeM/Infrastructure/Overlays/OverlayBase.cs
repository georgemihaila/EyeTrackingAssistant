using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace STeM.Infrastructure.Overlays
{
    public abstract class OverlayBase : UIElement
    {
        private bool _initialized = false;
        public abstract void OnEyePositionChanged(Vector2 position);

        public abstract void Update(Canvas parent);

        protected void AddIfNeeded(Canvas parent)
        {
            if (!_initialized)
            {
                parent.Children.Add(this);
                _initialized = true;
            }
        }
    }
}