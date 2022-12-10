using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace STeM.Infrastructure.Hooks
{
    public class MouseHook
    {
        static MouseHook()
        {
            var hook = new GlobalLowLevelInputHooks.MouseHook();
            hook.MouseMove += Hook_MouseMove; 
            _notMovedForStopwatch.Restart();
        }

        private static void Hook_MouseMove(GlobalLowLevelInputHooks.MouseInfo obj)
        {
            _notMovedForStopwatch.Restart();
        }

        [DllImport("User32.Dll")]
        public static extern long SetCursorPos(int x, int y);
        public static void SetCursorPos(Vector2 position) => SetCursorPos((int)position.X, (int)position.Y);

        [DllImport("User32.Dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref POINT point);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;

            public POINT(int X, int Y)
            {
                x = X;
                y = Y;
            }
        }

        public static TimeSpan NotMovedFor
        {
            get
            {
                return _notMovedForStopwatch.Elapsed;
            }
        }
        private static Stopwatch _notMovedForStopwatch = new Stopwatch();
    }
}
