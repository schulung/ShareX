using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareX.HelpersLib.Helpers
{
    class CursorPosition
    {
        public static Point GetCursorPosition()
        {
            POINT point;

            if (NativeMethods.GetCursorPos(out point))
            {
                return (Point)point;
            }

            return Point.Empty;
        }

        public static Point GetZeroBasedMousePosition()
        {
            return ScreenToClient(GetCursorPosition());
        }

        public static void SetCursorPosition(int x, int y)
        {
            NativeMethods.SetCursorPos(x, y);
        }

        public static void SetCursorPosition(Point position)
        {
            SetCursorPosition(position.X, position.Y);
        }
    }
}
