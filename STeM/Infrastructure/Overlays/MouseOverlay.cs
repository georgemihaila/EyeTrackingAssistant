using STeM.Infrastructure.Hooks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Windows.Input;

namespace STeM.Infrastructure.Overlays
{
    public class MouseOverlay : OverlayBase
    {
        protected override void OnFocus(Vector2 position)
        {
            if (MouseHook.NotMovedFor > TimeSpan.FromMilliseconds(3000))
            {
                MouseHook.SetCursorPos(position);
            }
        }

        protected override void OnDefocus(Vector2 position)
        {
            Mouse.SetCursor(Cursors.None);
        }
    }
}
