#region License Information (GPL v3)

/*
    ShareX - A program that allows you to take screenshots and share any file type
    Copyright (c) 2007-2018 ShareX Team

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/

#endregion License Information (GPL v3)

using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ShareX.HelpersLib
{
    public static class CaptureHelpers
    {

        public static Rectangle CreateRectangle(int x, int y, int x2, int y2)
        {
            int width, height;

            if (x <= x2)
            {
                width = x2 - x + 1;
            }
            else
            {
                width = x - x2 + 1;
                x = x2;
            }

            if (y <= y2)
            {
                height = y2 - y + 1;
            }
            else
            {
                height = y - y2 + 1;
                y = y2;
            }

            return new Rectangle(x, y, width, height);
        }

        public static Rectangle CreateRectangle(Point pos, Point pos2)
        {
            return CreateRectangle(pos.X, pos.Y, pos2.X, pos2.Y);
        }

        public static Point ProportionalPosition(Point pos, Point pos2)
        {
            Point newPosition = Point.Empty;
            int min;

            if (pos.X < pos2.X)
            {
                if (pos.Y < pos2.Y)
                {
                    min = Math.Min(pos2.X - pos.X, pos2.Y - pos.Y);
                    newPosition.X = pos.X + min;
                    newPosition.Y = pos.Y + min;
                }
                else
                {
                    min = Math.Min(pos2.X - pos.X, pos.Y - pos2.Y);
                    newPosition.X = pos.X + min;
                    newPosition.Y = pos.Y - min;
                }
            }
            else
            {
                if (pos.Y > pos2.Y)
                {
                    min = Math.Min(pos.X - pos2.X, pos.Y - pos2.Y);
                    newPosition.X = pos.X - min;
                    newPosition.Y = pos.Y - min;
                }
                else
                {
                    min = Math.Min(pos.X - pos2.X, pos2.Y - pos.Y);
                    newPosition.X = pos.X - min;
                    newPosition.Y = pos.Y + min;
                }
            }

            return newPosition;
        }

        public static Point SnapPositionToDegree(Point pos, Point pos2, float degree, float startDegree)
        {
            float angle = MathHelpers.LookAtRadian(pos, pos2);
            float startAngle = MathHelpers.DegreeToRadian(startDegree);
            float snapAngle = MathHelpers.DegreeToRadian(degree);
            float newAngle = (float)Math.Round((angle + startAngle) / snapAngle) * snapAngle - startAngle;
            float distance = MathHelpers.Distance(pos, pos2);
            return (Point)(pos + MathHelpers.RadianToVector2(newAngle, distance));
        }

        public static Point CalculateNewPosition(Point posOnClick, Point posCurrent, Size size)
        {
            if (posCurrent.X > posOnClick.X)
            {
                if (posCurrent.Y > posOnClick.Y)
                {
                    return new Point(posOnClick.X + size.Width - 1, posOnClick.Y + size.Height - 1);
                }
                else
                {
                    return new Point(posOnClick.X + size.Width - 1, posOnClick.Y - size.Height + 1);
                }
            }
            else
            {
                if (posCurrent.Y > posOnClick.Y)
                {
                    return new Point(posOnClick.X - size.Width + 1, posOnClick.Y + size.Height - 1);
                }
                else
                {
                    return new Point(posOnClick.X - size.Width + 1, posOnClick.Y - size.Height + 1);
                }
            }
        }

        public static Rectangle CalculateNewRectangle(Point posOnClick, Point posCurrent, Size size)
        {
            Point newPosition = CalculateNewPosition(posOnClick, posCurrent, size);
            return CreateRectangle(posOnClick, newPosition);
        }

        public static Rectangle EvenRectangleSize(Rectangle rect)
        {
            rect.Width -= rect.Width & 1;
            rect.Height -= rect.Height & 1;
            return rect;
        }
    }
}