using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Numerics;
using System.Text;
using System.Windows.Forms;

namespace PolygonEditor
{
    /// <summary>
    /// Static class with algorithms.
    /// </summary>
    public static class Algorithms
    {
        /// <summary>
        /// Function checks if point belongs to simple rectangle.
        /// </summary>
        /// <param name="firstVertex">Rectangle</param>
        /// <param name="point">Checked point</param>
        /// <returns>True if point belongs to rectangle otherwise false.</returns>
        public static bool pointBelongsToRectangle(RectangleF firstVertex, PointF point)
        {
            if (firstVertex.Left <= point.X && firstVertex.Right >= point.X
                && firstVertex.Top <= point.Y && firstVertex.Bottom >= point.Y)
                return true;
            return false;
        }

        /// <summary>
        /// Function checks if points lies on specified line.
        /// </summary>
        /// <param name="point1">The beginning of the line.</param>
        /// <param name="point2">The end of the line.</param>
        /// <param name="point">Checked point.</param>
        /// <param name="tolerantWidth">Acceptable measurement error.</param>
        /// <returns>True if point is located on line otherwise false.</returns>
        public static bool pointOnLine(PointF point1, PointF point2, PointF point, float tolerantWidth)
        {
            using (var path = new GraphicsPath())
            {
                using (var pen = new Pen(Brushes.White, tolerantWidth))
                {
                    path.AddLine(point1, point2);
                    return path.IsOutlineVisible(point, pen);
                }
            }
        }

        /// <summary>
        /// Function gets the projection of point on line.
        /// </summary>
        /// <param name="pointA">The beginning of the line.</param>
        /// <param name="pointB">The end of the line.</param>
        /// <param name="point"></param>
        /// <returns>The new point that is projection of point on line.</returns>
        public static PointF getClosestPointToLine(PointF pointA, PointF pointB, PointF point)
        {
            Vector2 AP = new Vector2(point.X - pointA.X, point.Y - pointA.Y);
            Vector2 AB = new Vector2(pointB.X - pointA.X, pointB.Y - pointA.Y);

            float magnitudeAB = AB.LengthSquared();
            float ABAPproduct = Vector2.Dot(AP, AB);
            float distance = ABAPproduct / magnitudeAB;

            if (distance < 0)
                return pointA;
            if (distance > 1)
                return pointB;
            return new PointF(pointA.X + AB.X * distance, pointA.Y + AB.Y * distance);
        }

        /// <summary>
        /// Function checks if points is located inside polygon.
        /// </summary>
        /// <param name="polygon">The table of polygon's vertices.</param>
        /// <param name="point">Checked point.</param>
        /// <returns>True if point is located inside polygon otherwise false.</returns>
        public static bool IsPointInsidePolygon(PointF[] polygon, PointF point)
        {
            using (var path = new GraphicsPath())
            {
                path.AddPolygon(polygon);
                using (var region = new Region(path))
                {
                    return region.IsVisible(point);
                }
            }
        }

        /// <summary>
        /// Implementation of Bresenham Line Algorithm that draws a line.
        /// </summary>
        /// <param name="pointA">The beginning of the line.</param>
        /// <param name="pointB">The end of the line.</param>
        /// <param name="bitmap">Drawing surface.</param>
        /// <param name="color">Line color.</param>
        public static void BresenhamLineAlgorithm(PointF pointA, PointF pointB, Bitmap bitmap, Color color)
        {
            int x1 = (int) pointA.X;
            int y1 = (int) pointA.Y;
            int x2 = (int) pointB.X;
            int y2 = (int) pointB.Y;

            int w = x2 - x1;
            int h = y2 - y1;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (!(longest > shortest))
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            int numerator = longest >> 1;
            for (int i = 0; i <= longest; i++)
            {
                if(x1 >= 0 && y1 >= 0 && x1 < bitmap.Width && y1 < bitmap.Height) 
                    bitmap.SetPixel(x1, y1, color);
                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    x1 += dx1;
                    y1 += dy1;
                }
                else
                {
                    x1 += dx2;
                    y1 += dy2;
                }
            }
        }

        /// <summary>
        /// Function return the length of the line.
        /// </summary>
        /// <param name="pointA">First point of the line.</param>
        /// <param name="pointB">Second point of the line (pointA != pointB).</param>
        /// <returns>Returns line length.</returns>
        public static float getLineLength(PointF pointA, PointF pointB)
        {
            return (float)Math.Sqrt(Math.Pow((pointB.X - pointA.X), 2) + Math.Pow((pointB.Y - pointA.Y), 2));
        }

        /// <summary>
        /// Function finds the center of line.
        /// </summary>
        /// <param name="pointA">First point of the line.</param>
        /// <param name="pointB">Second point of the line (pointA != pointB).</param>
        /// <returns>Returns line center.</returns>
        public static PointF getEdgeCenter(PointF pointA, PointF pointB)
        {
            float x = (pointB.X - pointA.X) / 2;
            float y = (pointB.Y - pointA.Y) / 2;
            return new PointF(pointA.X + x, pointA.Y + y);
        }

        /// <summary>
        /// Gets the intersection point of circle and line.
        /// </summary>
        /// <param name="pointA">First point of the line.</param>
        /// <param name="pointB">Second point of the line (pointA != pointB).</param>
        /// <param name="circleCenter">The circle center.</param>
        /// <param name="size">Circle's radius.</param>
        /// <param name="points">Found points.</param>
        /// <returns>True if point(s) are found otherwise false.</returns>
        public static bool findCommonPointBetweenCircleAndLine(PointF pointA, PointF pointB, PointF circleCenter,
            float size, out PointF[] points)
        {
            double a = 0, b = 0, A = 0, B = 0, C = 0;
            if (pointB.X - pointA.X != 0)
            {
                a = (pointB.Y - pointA.Y) / (pointB.X - pointA.X);
                b = pointA.Y - a * pointA.X;
                A = 1 + Math.Pow(a, 2);
                B = 2 * a * b - 2 * circleCenter.X - 2 * a * circleCenter.Y;
                C = Math.Pow(circleCenter.X, 2) + Math.Pow(b, 2) - 2 * circleCenter.Y * b + 
                    Math.Pow(circleCenter.Y, 2) - Math.Pow(size, 2);
            }
            else
            {
                a = 1;
                b = -pointA.X;
                A = 1;
                B = -2 * circleCenter.Y;
                C = Math.Pow(circleCenter.Y, 2) - Math.Pow(size, 2);
            }
            double delta = Math.Pow(B, 2) - 4 * A * C;
            points = null;
            if (delta > 0)
            {
                points = new PointF[2];
                points[0].X = (float)((-B - Math.Sqrt(delta)) / (2 * A));
                points[1].X = (float)((-B + Math.Sqrt(delta)) / (2 * A));
                points[0].Y = (float)(a * points[0].X + b);
                points[1].Y = (float)(a * points[1].X + b);
                return true;
            }
            else if (delta == 0)
            {
                points = new PointF[1];
                points[0].X = (float)(-B / (2 * A));
                points[0].Y = (float)(a * points[0].X + b);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Gets the line equation (a*x + b*y = c).
        /// </summary>
        /// <param name="pointA">First point that belongs to line.</param>
        /// <param name="pointB">Second point that belongs to line (point1 != point2).</param>
        /// <returns>Returns coefficients: a, b, c.</returns>
        public static (float a, float b, float c) getLineEquation(PointF pointA, PointF pointB)
        {
            float a = pointA.Y - pointB.Y;
            float b = pointB.X - pointA.X;
            float c = (pointA.X - pointB.X) * pointA.Y + (pointB.Y - pointA.Y) * pointA.X;
            return (a, b, c);
        }

        /// <summary>
        /// Gets the intersection point of two lines.
        /// </summary>
        /// <param name="point1A">First point that belongs to first line.</param>
        /// <param name="point1B">Second point that belongs to first line.</param>
        /// <param name="point2A">First point that belongs to second line.</param>
        /// <param name="point2B">Second point that belongs to second line.</param>
        /// <param name="point">The result.</param>
        /// <returns>If a point is found, the returns true and that point, otherwise false.</returns>
        public static bool getIntersectionPointOfLines(PointF point1A, PointF point1B, PointF point2A, PointF point2B, 
            out PointF point)
        {
            point = new PointF();
            (float a1, float b1, float c1) = getLineEquation(point1A, point1B);
            (float a2, float b2, float c2) = getLineEquation(point2A, point2B);

            float delta = a1 * b2 - a2 * b1;

            if (delta == 0) return false;

            point.X = (b1 * c2 - b2 * c1) / delta;
            point.Y = (a2 * c1 - a1 * c2) / delta;
            return true;
        }

        /// <summary>
        /// Gets the intersections of two circles
        /// </summary>
        /// <param name="center1">The first circle's center</param>
        /// <param name="center2">The second circle's center</param>
        /// <param name="radius1">The first circle's radius</param>
        /// <param name="radius2">The second circle's radius. If omitted, assumed to equal the first circle's radius</param>
        /// <returns>An array of intersection points. May have zero, one, or two values</returns>
        public static PointF[] getCircleIntersections(PointF center1, PointF center2, double radius1, double radius2)
        {

            var (r1, r2) = (radius1, radius2);
            (double x1, double y1, double x2, double y2) = (center1.X, center1.Y, center2.X, center2.Y);
            // d = distance from center1 to center2
            double d = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
            // Return an empty array if there are no intersections
            if (!(Math.Abs(r1 - r2) <= d && d <= r1 + r2)) { return null; }

            // Intersections i1 and possibly i2 exist
            double dsq = d * d;
            var (r1sq, r2sq) = (r1 * r1, r2 * r2);
            double r1sq_r2sq = r1sq - r2sq;
            double a = r1sq_r2sq / (2 * dsq);
            double c = Math.Sqrt(2 * (r1sq + r2sq) / dsq - (r1sq_r2sq * r1sq_r2sq) / (dsq * dsq) - 1);

            double fx = (x1 + x2) / 2 + a * (x2 - x1);
            double gx = c * (y2 - y1) / 2;

            double fy = (y1 + y2) / 2 + a * (y2 - y1);
            double gy = c * (x1 - x2) / 2;

            PointF i1 = new PointF((float)(fx + gx), (float)(fy + gy));
            PointF i2 = new PointF((float)(fx - gx), (float)(fy - gy));

            return i1 == i2 ? new PointF[] { i1 } : new PointF[] { i1, i2 };
        }
    }
}
