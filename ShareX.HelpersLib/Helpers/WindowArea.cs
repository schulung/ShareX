using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareX.HelpersLib.Helpers
{
    class WindowArea
    {
        public static Rectangle GetWindowRectangle(IntPtr handle)
        {
            Rectangle rect = Rectangle.Empty;

            if (NativeMethods.IsDWMEnabled())
            {
                Rectangle tempRect;

                if (NativeMethods.GetExtendedFrameBounds(handle, out tempRect))
                {
                    rect = tempRect;
                }
            }

            if (rect.IsEmpty)
            {
                rect = NativeMethods.GetWindowRect(handle);
            }

            if (!Helpers.IsWindows10OrGreater() && NativeMethods.IsZoomed(handle))
            {
                rect = NativeMethods.MaximizedWindowFix(handle, rect);
            }

            return rect;
        }

        public static Rectangle GetActiveWindowRectangle()
        {
            IntPtr handle = NativeMethods.GetForegroundWindow();
            return GetWindowRectangle(handle);
        }

        public static Rectangle GetActiveWindowClientRectangle()
        {
            IntPtr handle = NativeMethods.GetForegroundWindow();
            return NativeMethods.GetClientRect(handle);
        }
    }
}
