using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ShareX.HelpersLib.Helpers
{
    class ScreenInformation
    {
        public static Rectangle GetScreenBounds()
        {
            return SystemInformation.VirtualScreen;
        }

        public static Rectangle GetScreenWorkingArea()
        {
            return Screen.AllScreens.Select(x => x.WorkingArea).Combine();
        }

        public static Rectangle GetScreenBounds2()
        {
            Point topLeft = Point.Empty;
            Point bottomRight = Point.Empty;

            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.Bounds.X < topLeft.X) topLeft.X = screen.Bounds.X;
                if (screen.Bounds.Y < topLeft.Y) topLeft.Y = screen.Bounds.Y;
                if ((screen.Bounds.X + screen.Bounds.Width) > bottomRight.X) bottomRight.X = screen.Bounds.X + screen.Bounds.Width;
                if ((screen.Bounds.Y + screen.Bounds.Height) > bottomRight.Y) bottomRight.Y = screen.Bounds.Y + screen.Bounds.Height;
            }

            return new Rectangle(topLeft.X, topLeft.Y, bottomRight.X + Math.Abs(topLeft.X), bottomRight.Y + Math.Abs(topLeft.Y));
        }

        public static Rectangle GetScreenBounds3()
        {
            Point topLeft = Point.Empty;
            Point bottomRight = Point.Empty;

            foreach (Screen screen in Screen.AllScreens)
            {
                topLeft.X = Math.Min(topLeft.X, screen.Bounds.X);
                topLeft.Y = Math.Min(topLeft.Y, screen.Bounds.Y);
                bottomRight.X = Math.Max(bottomRight.X, screen.Bounds.Right);
                bottomRight.Y = Math.Max(bottomRight.Y, screen.Bounds.Bottom);
            }

            return new Rectangle(topLeft.X, topLeft.Y, bottomRight.X + Math.Abs(topLeft.X), bottomRight.Y + Math.Abs(topLeft.Y));
        }

        public static Rectangle GetScreenBounds4()
        {
            return Screen.AllScreens.Select(x => x.Bounds).Combine();
        }

        public static Rectangle GetActiveScreenBounds()
        {
            return Screen.FromPoint(CursorPosition.GetCursorPosition()).Bounds;
        }

        public static Rectangle GetActiveScreenWorkingArea()
        {
            return Screen.FromPoint(CursorPosition.GetCursorPosition()).WorkingArea;
        }

        public static Rectangle GetPrimaryScreenBounds()
        {
            return Screen.PrimaryScreen.Bounds;
        }

        public static Rectangle GetScreenBounds0Based()
        {
            return ScreenCoordinateConverter.ScreenToClient(GetScreenBounds());
        }

        public static Rectangle GetActiveScreenBounds0Based()
        {
            return ScreenCoordinateConverter.ScreenToClient(GetActiveScreenBounds());
        }

        public static Rectangle GetPrimaryScreenBounds0Based()
        {
            return ScreenCoordinateConverter.ScreenToClient(GetPrimaryScreenBounds());
        }
    }
}
