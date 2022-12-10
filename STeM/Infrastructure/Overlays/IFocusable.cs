using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace STeM.Infrastructure.Overlays
{
    public interface IFocusable
    {
        protected virtual void OnFocus(Vector2 position) { }
        protected virtual void OnDefocus(Vector2 position) { }
    }
}
