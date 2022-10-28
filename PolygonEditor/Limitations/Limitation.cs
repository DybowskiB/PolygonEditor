using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace PolygonEditor
{
    public abstract class Limitation
    {
        private static int idCounter = 0;
        protected Polygon polygon1;
        protected int edgeIndex1;
        public int id {get;}

        public Limitation(Polygon polygon1, int edgeIndex1)
        {
            this.polygon1 = polygon1;
            this.edgeIndex1 = edgeIndex1;
            id = idCounter++;
        }

        public Limitation(Polygon polygon1, int edgeIndex1, int id)
        {
            this.polygon1 = polygon1;
            this.edgeIndex1 = edgeIndex1;
            this.id = id;
        }

        public (Polygon polygon, int edgeIndex) getEdge1()
        {
            return (polygon1, edgeIndex1);
        }


        /// <summary>
        /// Recursive function that applies the limitation.
        /// </summary>
        /// <param name="polygons">The set of polygons.</param>
        /// <param name="hashList">List of already applied limitations.</param>
        /// <returns>True if it is possible to apply limitation otherwise false.</returns>
        public abstract bool applyLimitation(List<Polygon> polygons, List<int> hashList);

        /// <summary>
        /// Function that repair the polygon's structure after limitation application.
        /// </summary>
        /// <param name="polygon">Repaired polygon.</param>
        /// <param name="edges">Edges from repaired polygon.</param>
        /// <param name="index">Index of first edge that needs to be repaired.</param>
        /// <param name="polygons">The set of all polygons.</param>
        /// <param name="hashList">List of already applied limitations.</param>
        /// <returns>True if polygon's structure is repaired otherwise false.</returns>
        public bool repairAfterLimitationApplication(Polygon polygon, List<Edge> edges, int index, List<Polygon> polygons,
            List<int> hashList)
        {
            if (repaired(edges))
                return true;

            int polygonIndex = 0;
            for (int j = 0; j < polygons.Count; ++j)
                if (polygons[j] == polygon)
                    polygonIndex = j;

            int i = index;
            Edge edge, nextEdge;
            do
            {
                edge = edges[i];
                int i_next = i + 1 > edges.Count - 1 ? 0 : i + 1;
                nextEdge = edges[i_next];
                bool edgeParLim = edge.hasParallelLimitation();
                bool edgeLenLim = edge.hasLengthLimitation();
                bool nextEdgeParLim = nextEdge.hasParallelLimitation();
                bool nextEdgeLenLim = nextEdge.hasLengthLimitation();

                if (!edgeLenLim && edgeParLim && !nextEdgeLenLim && !nextEdgeParLim)
                {
                    polygon.getEdges()[i_next].setPointA(edge.getPointB());
                    return true;
                }

                else if (edgeLenLim && !edgeParLim && !nextEdgeLenLim && !nextEdgeParLim)
                {
                    polygon.getEdges()[i_next].setPointA(edge.getPointB());
                    return true;
                }

                else if (!edgeLenLim && edgeParLim && !nextEdgeLenLim && nextEdgeParLim)
                {
                    PointF point;
                    if (Algorithms.getIntersectionPointOfLines(edge.getPointA(), edge.getPointB(),
                        nextEdge.getPointA(), nextEdge.getPointB(), out point))
                    {
                        edge.setPointB(point);
                        nextEdge.setPointA(point);
                        return true;
                    }
                }

                else if (!edgeLenLim && edgeParLim && nextEdgeLenLim && !nextEdgeParLim)
                {
                    PointF[] points;
                    if(Algorithms.findCommonPointBetweenCircleAndLine(edge.getPointA(), edge.getPointB(),
                         nextEdge.getPointB(), nextEdge.getLengthF(), out points))
                    {
                        edge.setPointB(points[0]);
                        nextEdge.setPointA(points[0]);
                        return true;
                    }
                    else
                    {
                        List<Polygon> polygons_c = getPolygonsCopies(polygons);
                        List<int> hashList_c = getHashCopy(hashList);
                        edge.setPointB(nextEdge.getPointA());
                        ParallelLimitation parallelLimitation = edge.getParallelLimitation();
                        parallelLimitation.setEdge1((polygon, i));
                        if (parallelLimitation.applyLimitation(polygons, hashList))
                            return true;
                        else
                        {
                            polygons = polygons_c;
                            hashList = hashList_c;
                            polygon = polygons[polygonIndex];
                            edges = polygon.getEdges();
                            edge = polygons[polygonIndex].getEdges()[i];
                            nextEdge = polygons[polygonIndex].getEdges()[i_next];
                            moveEdge(nextEdge, edge.getPointB(), nextEdge.getPointA(), nextEdge.getPointB());
                        }
                    }
                }

                else if (edgeLenLim && !edgeParLim && !nextEdgeLenLim && nextEdgeParLim)
                {
                    PointF[] points;
                    if (Algorithms.findCommonPointBetweenCircleAndLine(nextEdge.getPointA(), nextEdge.getPointB(),
                         edge.getPointA(), edge.getLengthF(), out points))
                    {
                        edge.setPointB(points[0]);
                        nextEdge.setPointA(points[0]);
                        return true;
                    }
                    else if(isFreeEdge(i, edges))
                    {
                        moveEdge(nextEdge, edge.getPointB(), nextEdge.getPointA(), nextEdge.getPointB());
                        i = i_next;
                        continue;
                    }
                    else
                    {
                        List<Polygon> polygons_c = getPolygonsCopies(polygons);
                        List<int> hashList_c = getHashCopy(hashList);
                        nextEdge.setPointA(edge.getPointB());
                        ParallelLimitation parallelLimitation = nextEdge.getParallelLimitation();
                        parallelLimitation.setEdge1((polygon, i_next));
                        if (parallelLimitation.applyLimitation(polygons, hashList))
                            return true;
                        polygons = polygons_c;
                        hashList = hashList_c;
                        polygon = polygons[polygonIndex];
                        edges = polygon.getEdges();
                        edge = polygons[polygonIndex].getEdges()[i];
                        nextEdge = polygons[polygonIndex].getEdges()[i_next];
                    }
                    moveEdge(nextEdge, edge.getPointB(), nextEdge.getPointA(), nextEdge.getPointB());
                }

                else if (edgeLenLim && !edgeParLim && nextEdgeLenLim && !nextEdgeParLim)
                {
                    PointF[] points = Algorithms.getCircleIntersections(edge.getPointA(), nextEdge.getPointB(),
                        edge.getLengthF(), nextEdge.getLengthF());
                    if(points == null)
                    {
                        moveEdge(nextEdge, edge.getPointB(), nextEdge.getPointA(), nextEdge.getPointB());
                    }
                    else if(points.Length == 1)
                    {
                        edge.setPointB(points[0]);
                        nextEdge.setPointA(points[0]);
                    }
                    else
                    {
                        float d1 = Algorithms.getLineLength(points[0], nextEdge.getPointA());
                        float d2 = Algorithms.getLineLength(points[1], nextEdge.getPointA());
                        PointF point = d1 < d2 ? points[0] : points[1];
                        edge.setPointB(point);
                        nextEdge.setPointA(point);
                    }
                }

                else if (!edgeLenLim && edgeParLim && nextEdgeLenLim && nextEdgeParLim)
                {
                    List<Polygon> polygons_c = getPolygonsCopies(polygons);
                    List<int> hashList_c = getHashCopy(hashList);

                    edge.setPointB(nextEdge.getPointA());
                    ParallelLimitation parallelLimitation = edge.getParallelLimitation();
                    parallelLimitation.setEdge1((polygon, i));
                    if (parallelLimitation.applyLimitation(polygons, hashList))
                        return true;
                    polygons = getPolygonsCopies(polygons_c);
                    hashList = getHashCopy(hashList_c);
                    polygon = polygons[polygonIndex];
                    edges = polygon.getEdges();
                    edge = polygons[polygonIndex].getEdges()[i];
                    nextEdge = polygons[polygonIndex].getEdges()[i_next];

                    PointF[] points;
                    if (Algorithms.findCommonPointBetweenCircleAndLine(edge.getPointA(), edge.getPointB(),
                         nextEdge.getPointB(), nextEdge.getLengthF(), out points))
                    {
                        edge.setPointB(points[0]);
                        nextEdge.setPointA(points[0]);
                        ParallelLimitation parallelLimitation1 = edge.getParallelLimitation();
                        parallelLimitation1.setEdge1((polygon, i));
                        ParallelLimitation parallelLimitation2 = nextEdge.getParallelLimitation();
                        parallelLimitation2.setEdge1((polygon, i_next));
                        if (parallelLimitation1.applyLimitation(polygons, hashList) &&
                            parallelLimitation2.applyLimitation(polygons, hashList))
                            return true;
                        polygons = getPolygonsCopies(polygons_c);
                        hashList = getHashCopy(hashList_c);
                        polygon = polygons[polygonIndex];
                        edges = polygon.getEdges();
                        edge = polygons[polygonIndex].getEdges()[i];
                        nextEdge = polygons[polygonIndex].getEdges()[i_next];

                        if (points.Length > 1)
                        {
                            edge.setPointB(points[1]);
                            nextEdge.setPointA(points[1]);
                            parallelLimitation1 = edge.getParallelLimitation();
                            parallelLimitation1.setEdge1((polygon, i));
                            parallelLimitation2 = nextEdge.getParallelLimitation();
                            parallelLimitation2.setEdge1((polygon, i_next));
                            if (parallelLimitation1.applyLimitation(polygons, hashList) &&
                                parallelLimitation2.applyLimitation(polygons, hashList))
                                return true;
                            polygons = polygons_c;
                            hashList = hashList_c;
                            polygon = polygons[polygonIndex];
                            edges = polygon.getEdges();
                            edge = polygons[polygonIndex].getEdges()[i];
                            nextEdge = polygons[polygonIndex].getEdges()[i_next];
                        }
                    }
                    moveEdge(nextEdge, edge.getPointB(), nextEdge.getPointA(), nextEdge.getPointB());
                }

                else if (edgeLenLim && !edgeParLim && nextEdgeLenLim && nextEdgeParLim)
                {
                    List <Polygon> polygons_c = getPolygonsCopies(polygons);
                    List<int> hashList_c = getHashCopy(hashList);

                    PointF[] points = Algorithms.getCircleIntersections(edge.getPointA(), nextEdge.getPointB(),
                        edge.getLengthF(), nextEdge.getLengthF());
                    if(points != null && points.Length == 1)
                    {
                        edge.setPointB(points[0]);
                        nextEdge.setPointA(points[0]);
                        ParallelLimitation parallelLimitation = nextEdge.getParallelLimitation();
                        parallelLimitation.setEdge1((polygon, i_next));
                        if (parallelLimitation.applyLimitation(polygons, hashList))
                            return true;
                        polygons = polygons_c;
                        hashList = hashList_c;
                        polygon = polygons[polygonIndex];
                        edges = polygon.getEdges();
                        edge = polygons[polygonIndex].getEdges()[i];
                        nextEdge = polygons[polygonIndex].getEdges()[i_next];
                    }
                    else if(points != null)
                    {
                        edge.setPointB(points[0]);
                        nextEdge.setPointA(points[0]);
                        ParallelLimitation parallelLimitation = nextEdge.getParallelLimitation();
                        parallelLimitation.setEdge1((polygon, i_next));
                        if (parallelLimitation.applyLimitation(polygons, hashList))
                            return true;
                        polygons = getPolygonsCopies(polygons_c);
                        hashList = getHashCopy(hashList);
                        edge = polygons[polygonIndex].getEdges()[i];
                        nextEdge = polygons[polygonIndex].getEdges()[i_next];

                        edge.setPointB(points[1]);
                        nextEdge.setPointA(points[1]);
                        parallelLimitation = nextEdge.getParallelLimitation();
                        parallelLimitation.setEdge1((polygon, i_next));
                        if (parallelLimitation.applyLimitation(polygons, hashList))
                            return true;
                        polygons = polygons_c;
                        hashList = hashList_c;
                        polygon = polygons[polygonIndex];
                        edges = polygon.getEdges();
                        edge = polygons[polygonIndex].getEdges()[i];
                        nextEdge = polygons[polygonIndex].getEdges()[i_next];
                    }
                    moveEdge(nextEdge, edge.getPointB(), nextEdge.getPointA(), nextEdge.getPointB());
                }

                else if (edgeLenLim && edgeParLim && !nextEdgeLenLim && nextEdgeParLim)
                {
                    List<Polygon> polygons_c = getPolygonsCopies(polygons);
                    List<int> hashList_c = getHashCopy(hashList);

                    nextEdge.setPointA(edge.getPointB());
                    ParallelLimitation parallelLimitation = nextEdge.getParallelLimitation();
                    parallelLimitation.setEdge1((polygon, i_next));
                    if (parallelLimitation.applyLimitation(polygons, hashList))
                        return true;
                    polygons = getPolygonsCopies(polygons_c);
                    hashList = getHashCopy(hashList);
                    polygon = polygons[polygonIndex];
                    edges = polygon.getEdges();
                    edge = polygons[polygonIndex].getEdges()[i];
                    nextEdge = polygons[polygonIndex].getEdges()[i_next];

                    PointF[] points;
                    if (Algorithms.findCommonPointBetweenCircleAndLine(nextEdge.getPointA(), nextEdge.getPointB(),
                         edge.getPointA(), edge.getLengthF(), out points))
                    {
                        edge.setPointB(points[0]);
                        nextEdge.setPointA(points[0]);
                        ParallelLimitation parallelLimitation1 = edge.getParallelLimitation();
                        parallelLimitation1.setEdge1((polygon, i));
                        ParallelLimitation parallelLimitation2 = nextEdge.getParallelLimitation();
                        parallelLimitation2.setEdge1((polygon, i_next));
                        if (parallelLimitation1.applyLimitation(polygons, hashList) &&
                            parallelLimitation2.applyLimitation(polygons, hashList))
                            return true;
                        polygons = getPolygonsCopies(polygons_c);
                        hashList = getHashCopy(hashList);
                        polygon = polygons[polygonIndex];
                        edges = polygon.getEdges();
                        edge = polygons[polygonIndex].getEdges()[i];
                        nextEdge = polygons[polygonIndex].getEdges()[i_next];

                        if (points.Length > 1)
                        {
                            edge.setPointB(points[1]);
                            nextEdge.setPointA(points[1]);
                            parallelLimitation1 = edge.getParallelLimitation();
                            parallelLimitation1.setEdge1((polygon, i));
                            parallelLimitation2 = nextEdge.getParallelLimitation();
                            parallelLimitation2.setEdge1((polygon, i_next));
                            if (parallelLimitation1.applyLimitation(polygons, hashList) &&
                                parallelLimitation2.applyLimitation(polygons, hashList))
                                return true;
                            polygons = polygons_c;
                            hashList = hashList_c;
                            polygon = polygons[polygonIndex];
                            edges = polygon.getEdges();
                            edge = polygons[polygonIndex].getEdges()[i];
                            nextEdge = polygons[polygonIndex].getEdges()[i_next];
                        }
                    }
                    moveEdge(nextEdge, edge.getPointB(), nextEdge.getPointA(), nextEdge.getPointB());
                }

                else if (edgeLenLim && edgeParLim && nextEdgeLenLim && !nextEdgeParLim)
                {
                    List<Polygon> polygons_c = getPolygonsCopies(polygons);
                    List<int> hashList_c = getHashCopy(hashList);

                    PointF[] points = Algorithms.getCircleIntersections(edge.getPointA(), nextEdge.getPointB(),
                        edge.getLengthF(), nextEdge.getLengthF());
                    if (points != null && points.Length == 1)
                    {
                        edge.setPointB(points[0]);
                        nextEdge.setPointA(points[0]);
                        ParallelLimitation parallelLimitation = edge.getParallelLimitation();
                        parallelLimitation.setEdge1((polygon, i));
                        if (parallelLimitation.applyLimitation(polygons, hashList))
                            return true;
                        polygons = polygons_c;
                        hashList = hashList_c;
                        polygon = polygons[polygonIndex];
                        edges = polygon.getEdges();
                        edge = polygons[polygonIndex].getEdges()[i];
                        nextEdge = polygons[polygonIndex].getEdges()[i_next];
                    }
                    else if (points != null)
                    {
                        edge.setPointB(points[0]);
                        nextEdge.setPointA(points[0]);
                        ParallelLimitation parallelLimitation = edge.getParallelLimitation();
                        parallelLimitation.setEdge1((polygon, i));
                        if (parallelLimitation.applyLimitation(polygons, hashList))
                            return true;
                        polygons = getPolygonsCopies(polygons_c);
                        hashList = getHashCopy(hashList);
                        polygon = polygons[polygonIndex];
                        edges = polygon.getEdges();
                        edge = polygons[polygonIndex].getEdges()[i];
                        nextEdge = polygons[polygonIndex].getEdges()[i_next];

                        edge.setPointB(points[1]);
                        nextEdge.setPointA(points[1]);
                        parallelLimitation = edge.getParallelLimitation();
                        parallelLimitation.setEdge1((polygon, i));
                        if (parallelLimitation.applyLimitation(polygons, hashList))
                            return true;
                        polygons = polygons_c;
                        hashList = hashList_c;
                        polygon = polygons[polygonIndex];
                        edges = polygon.getEdges();
                        edge = polygons[polygonIndex].getEdges()[i];
                        nextEdge = polygons[polygonIndex].getEdges()[i_next];
                    }
                    moveEdge(nextEdge, edge.getPointB(), nextEdge.getPointA(), nextEdge.getPointB());
                }

                else if (edgeLenLim && edgeParLim && !nextEdgeLenLim && !nextEdgeParLim)
                {
                    nextEdge.setPointA(edge.getPointB());
                    return true;
                }

                else if (edgeLenLim && edgeParLim && nextEdgeLenLim && nextEdgeParLim)
                {
                    moveEdge(nextEdge, edge.getPointB(), nextEdge.getPointA(), nextEdge.getPointB());
                }

                i = i_next;

            } while (i != index);

            if (Math.Abs(edge.getPointB().X - nextEdge.getPointA().X) < 0.001 &&
                    Math.Abs(edge.getPointB().Y - nextEdge.getPointA().Y) < 0.001)
            {
                edge.setPointB(nextEdge.getPointA());
                return true;
            }
            return false;
        }

        /// <summary>
        /// Function checks if polygon is already repaired.
        /// </summary>
        /// <param name="edges">The set of edges in polygon.</param>
        /// <returns>True if polyogn is repaired otherwise false.</returns>
        public bool repaired(List<Edge> edges)
        {
            int i_next = 1;
            for(int i = 0; i < edges.Count; ++i)
            {
                i_next = i + 1 < edges.Count ? i + 1 : 0;
                if(Math.Abs(edges[i].getPointB().X - edges[i_next].getPointA().X) < 0.001 &&
                    Math.Abs(edges[i].getPointB().Y - edges[i_next].getPointA().Y) < 0.001)
                {
                    edges[i].setPointB(edges[i_next].getPointA());
                    continue;
                }
                if (edges[i].getPointB().X != edges[i_next].getPointA().X ||
                    edges[i].getPointB().Y != edges[i_next].getPointA().Y)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Function that moves the edge maintaining limitations.
        /// </summary>
        /// <param name="nextEdge">the adjacent edge.</param>
        /// <param name="edgePointB">The end of the edge.</param>
        /// <param name="nextEdgePointA">The beginning of adjacent edge.</param>
        /// <param name="nextEdgePointB">The end of adjacent edge.</param>
        public void moveEdge(Edge nextEdge, PointF edgePointB, PointF nextEdgePointA, PointF nextEdgePointB)
        {
            float vectorx = edgePointB.X - nextEdgePointA.X;
            float vectory = edgePointB.Y - nextEdgePointA.Y;
            nextEdge.setPointA(new PointF(nextEdgePointA.X + vectorx, nextEdgePointA.Y + vectory));
            nextEdge.setPointB(new PointF(nextEdgePointB.X + vectorx, nextEdgePointB.Y + vectory));
        }

        public bool isFreeEdge(int i, List<Edge> edges)
        {
            for (int j = i + 1 < edges.Count ? i + 1 : 0; j != i; j = j + 1 < edges.Count ? j + 1 : 0)
                if (!edges[j].hasLengthLimitation() &&
                    !edges[j].hasParallelLimitation())
                    return true;
            return false;
        }

        /// <summary>
        /// Function gets deep copy of polygons' set.
        /// </summary>
        /// <param name="polygons">The set of polygons.</param>
        /// <returns>The new set of polygons.</returns>
        public static List<Polygon> getPolygonsCopies(List<Polygon> polygons)
        {
            List<Polygon> copyPolygons = new List<Polygon>();

            foreach(Polygon polygon in polygons)
            {
                copyPolygons.Add(polygon.deepCopy());
            }

            for(int i = 0; i < copyPolygons.Count; ++i)
            {
                var copyEdges = copyPolygons[i].getEdges();
                var edges = polygons[i].getEdges();
                for (int j = 0; j < edges.Count; ++j)
                {
                    if (edges[j].hasLengthLimitation())
                    {
                        LengthLimitation lengthLimitation = new LengthLimitation(copyPolygons[i], j,
                            edges[j].getLengthLimitation().id);
                        copyEdges[j].addLengthLimitation(lengthLimitation);
                    }
                    if(edges[j].hasParallelLimitation() && !copyEdges[j].hasParallelLimitation())
                    {
                        ParallelLimitation parallelLimitation = edges[j].getParallelLimitation();
                        List<(Polygon, int)> limitedEdges;
                        ParallelLimitation copyParallelLimitation = parallelLimitation.deepCopy(copyPolygons,
                            i, polygons, out limitedEdges);
                        foreach((Polygon polygon, int edgeIndex) edgeData in limitedEdges)
                        {
                            Edge edge = edgeData.polygon.getEdges()[edgeData.edgeIndex];
                            edge.addParallelLimitation(copyParallelLimitation);
                        }
                    }
                }
            }

            return copyPolygons;
        }

        /// <summary>
        /// Function gets the deep copy of hash list.
        /// </summary>
        /// <param name="hashList">The set of limitation ids.</param>
        /// <returns>New set of limitations ids with same values.</returns>
        public static List<int> getHashCopy(List<int> hashList)
        {
            List<int> copyHashList = new List<int>();
            foreach(int id in hashList)
            {
                copyHashList.Add(id);
            }
            return copyHashList;
        }
    }
}
