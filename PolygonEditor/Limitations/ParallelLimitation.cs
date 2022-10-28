using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace PolygonEditor
{
    public class ParallelLimitation : Limitation
    {
        public static int parallelLimitationNumber = 0;
        private List<(Polygon polygon, int edgeIndex)> edges;
        private int number;

        public ParallelLimitation(Polygon polygon1, int edgeIndex1) 
            : base(polygon1, edgeIndex1)
        {
            edges = new List<(Polygon, int)>();
            edges.Add((polygon1, edgeIndex1));
            number = parallelLimitationNumber++;
        }

        public ParallelLimitation(Polygon polygon1, int edgeIndex1, int id, int number)
            : base(polygon1, edgeIndex1, id)
        {
            edges = new List<(Polygon, int)>();
            edges.Add((polygon1, edgeIndex1));
            this.number = number;
        }

        public ParallelLimitation deepCopy(List<Polygon> copyPolygons, int polygonIndex, List<Polygon> polygons,
            out List<(Polygon, int)> limitedEdges)
        {
            ParallelLimitation parallelLimitation = new ParallelLimitation(copyPolygons[polygonIndex], edgeIndex1,
                this.id, this.number);
            foreach(var edgeData in edges)
            {
                int i = 0;
                for (i = 0; i < polygons.Count; i++)
                    if (edgeData.polygon == polygons[i])
                        break;
                parallelLimitation.addEdge((copyPolygons[i], edgeData.edgeIndex));
            }
            limitedEdges = parallelLimitation.getEdges();
            return parallelLimitation;
        }

        public void setEdge1((Polygon polygon, int edgeIndex) edge)
        {
            polygon1 = edge.polygon;
            edgeIndex1 = edge.edgeIndex;
        }

        public List<(Polygon polygon, int edgeIndex)> getEdges()
        {
            return this.edges;
        }

        public void addEdge((Polygon, int) edge)
        {
            this.edges.Add(edge);
        }

        public int getNumber()
        {
            return number;
        }


        /// <summary>
        /// Recursive function that applies the limitation.
        /// </summary>
        /// <param name="polygons">The set of polygons.</param>
        /// <param name="hashList">List of already applied limitations.</param>
        /// <returns>True if it is possible to apply limitation otherwise false.</returns>
        public override bool applyLimitation(List<Polygon> polygons, List<int> hashList)
        {
            if (hashList.Contains(this.id))
                return false;
            hashList.Add(this.id);

            Edge parallelEdge = polygon1.getEdges()[edgeIndex1];
            for (int i = 0; i < edges.Count; ++i)
            {
                int edgeIndex = edges[i].edgeIndex;
                Polygon edgePolygon = edges[i].polygon;
                Edge edge = edgePolygon.getEdges()[edgeIndex];
                if (edge == parallelEdge) continue;
                PointF parEdgePointA = parallelEdge.getPointA(), parEdgePointB = parallelEdge.getPointB();
                PointF newPoint = new PointF(), edgePointA = edge.getPointA();
                (float X, float Y) vector = (parEdgePointB.X - parEdgePointA.X, parEdgePointB.Y - parEdgePointA.Y);
                float k = 0;
                if (vector.X != 0)
                {
                    newPoint.X = edge.getPointB().X;
                    k = (newPoint.X - edgePointA.X) / vector.X;
                    newPoint.Y = k * vector.Y + edgePointA.Y;
                }
                else
                {
                    newPoint.Y = edge.getPointB().Y;
                    k = (newPoint.Y - edgePointA.Y) / vector.Y;
                    newPoint.X = k * vector.X + edgePointA.X;
                }
                PointF[] points;
                if (Algorithms.findCommonPointBetweenCircleAndLine(edge.getPointA(), newPoint, edge.getPointA(),
                    edge.getLengthF(), out points))
                {
                    float d1 = Algorithms.getLineLength(points[0], edge.getPointB());
                    float d2 = Algorithms.getLineLength(points[1], edge.getPointB());
                    edgePolygon.getEdges()[edgeIndex].setPointB(d1 < d2 ? points[0] : points[1]);
                    if (!repairAfterLimitationApplication(edgePolygon, edgePolygon.getEdges(), edgeIndex, polygons,
                    hashList))
                        return false;
                }
                else
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Function checks if limitation application is possible. Use function check indexes in order to check
        /// if indexes are correct.
        /// </summary>
        /// <param name="polygon1">First edge polygon.</param>
        /// <param name="edgeIndex1">First edge index.</param>
        /// <param name="polygon2">Second edge polygon.</param>
        /// <param name="edgeIndex2">Second edge index.</param>
        /// <returns></returns>
        public static bool checkPossibiltyToApply(Polygon polygon1, int edgeIndex1, Polygon polygon2, int edgeIndex2)
        {
            Edge edge1 = polygon1.getEdges()[edgeIndex1];
            Edge edge2 = polygon2.getEdges()[edgeIndex2];

            if (!edge1.hasParallelLimitation() && !edge2.hasParallelLimitation())
            {
                if (!checkIndexes(polygon1, polygon2, edgeIndex1, edgeIndex2))
                    return false;
            }
            else if (edge1.hasParallelLimitation() && !edge2.hasParallelLimitation())
            {
                ParallelLimitation parallelLimitation = edge1.getParallelLimitation();
                List<(Polygon polygon, int edgeIndex)> edges = parallelLimitation.getEdges();
                foreach (var edgeData in edges)
                {
                    if (!checkIndexes(polygon2, edgeData.polygon, edgeIndex2, edgeData.edgeIndex))
                        return false;
                }
            }
            else if (!edge1.hasParallelLimitation() && edge2.hasParallelLimitation())
            {
                ParallelLimitation parallelLimitation = edge2.getParallelLimitation();
                List<(Polygon polygon, int edgeIndex)> edges = parallelLimitation.getEdges();
                foreach (var edgeData in edges)
                {
                    if (!checkIndexes(polygon1, edgeData.polygon, edgeIndex1, edgeData.edgeIndex))
                        return false;
                }
            }
            else if (edge1.hasParallelLimitation() && edge2.hasParallelLimitation())
            {
                ParallelLimitation parallelLimitation1 = edge1.getParallelLimitation();
                List<(Polygon polygon, int edgeIndex)> edges1 = parallelLimitation1.getEdges();
                ParallelLimitation parallelLimitation2 = edge2.getParallelLimitation();
                List<(Polygon polygon, int edgeIndex)> edges2 = parallelLimitation2.getEdges();
                foreach (var edgeData1 in edges1)
                {
                    foreach (var edgeData2 in edges2)
                    {
                        if (!checkIndexes(edgeData1.polygon, edgeData2.polygon, edgeData1.edgeIndex, edgeData2.edgeIndex))
                            return false;
                    }
                }
            }
            return true;
        }

        public static bool checkIndexes(Polygon polygon1, Polygon polygon2, int edgeIndex1, int edgeIndex2)
        {
            if (polygon1 == polygon2 && (Math.Abs(edgeIndex1 - edgeIndex2) == 1 ||
                    (edgeIndex1 == polygon1.getEdges().Count - 1 && edgeIndex2 == 0) ||
                    (edgeIndex1 == 0 && edgeIndex2 == polygon2.getEdges().Count - 1)))
                return false;
            return true;
        }
    }
}
