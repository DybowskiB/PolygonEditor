using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PolygonEditor
{
    public class LengthLimitation : Limitation
    {
        private float size;
        public float Size
        {
            set { size = value; }
            get { return size; }
        }

        public LengthLimitation(Polygon polygon1, int edgeIndex1) 
            :base(polygon1, edgeIndex1)
        {
            this.size = 0;
        }

        public LengthLimitation(Polygon polygon1, int edgeIndex1, int id)
            : base(polygon1, edgeIndex1, id)
        {
            this.size = 0;
        }


        /// <summary>
        /// Recursive function that applies the limitation.
        /// </summary>
        /// <param name="polygons">The set of polygons.</param>
        /// <param name="hashList">List of already applied limitations.</param>
        /// <returns>True if it is possible to apply limitation otherwise false.</returns>
        public override bool applyLimitation(List<Polygon> polygons, List<int> hashList)
        {
            if (hashList.Contains(id))
                return false;
            hashList.Add(id);

            PointF pointA = polygon1.getEdges()[edgeIndex1].getPointA();
            PointF pointB = polygon1.getEdges()[edgeIndex1].getPointB();
            PointF[] points;
            if (Algorithms.findCommonPointBetweenCircleAndLine(pointA, pointB, pointA, Size, out points))
            {
                float d1 = Algorithms.getLineLength(pointB, points[0]);
                float d2 = Algorithms.getLineLength(pointB, points[1]);
                polygon1.getEdges()[edgeIndex1].setPointB(d1 < d2 ? points[0] : points[1]);
                return repairAfterLimitationApplication(polygon1, polygon1.getEdges(), edgeIndex1, polygons,
                    hashList);
            }
            else return false;
        }
    }
}
