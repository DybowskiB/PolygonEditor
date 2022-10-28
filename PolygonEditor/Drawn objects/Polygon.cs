using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace PolygonEditor
{
    public class Polygon
    {
        private List<Edge> edges;


        public Polygon(){
            this.edges = new List<Edge>();
        }

        public Polygon(List<Edge> edges)
        {
            this.edges = edges;
        }

        public Polygon deepCopy()
        {
            List<Edge> copyEdges = new List<Edge>();
            foreach(Edge edge in edges)
            {
                copyEdges.Add(edge.deepCopy());
            }
            return new Polygon(copyEdges);
        }

        public List<Edge> getEdges()
        {
            return this.edges;
        }

        public void addEdge(Edge edge)
        {
            this.edges.Add(edge);
        }

        public void addVertex(PointF pointA, PointF pointB, PointF point, Edge edge, int index)
        {
            edge.removeParallelLimitation();
            point = Algorithms.getClosestPointToLine(pointA, pointB, point);
            Edge newEdge1 = new Edge(edge.getPointA(), point);
            Edge newEdge2 = new Edge(point, edge.getPointB());
            edges.RemoveAt(index);
            edges.Insert(index, newEdge1);
            edges.Insert(index + 1, newEdge2);
        }

        public PointF[] getVertices()
        {
            List<PointF> vertices = new List<PointF>();
            foreach(var edge in edges)
            {
                vertices.Add(edge.getPointA());
            }
            return vertices.ToArray();
        }


        // Functions used during move mode:
        public void moveVertex(int index, PointF point, List<Polygon> polygons)
        {
            Edge edge = edges[index];
            int beforeIndex = index - 1 >= 0 ? index - 1 : edges.Count - 1;
            Edge beforeEdge = edges[beforeIndex];
            bool hasEdgeLengthLimitation = edge.hasLengthLimitation();
            bool hasEdgeParallelLimitation = edge.hasParallelLimitation();
            bool hasBeforeEdgeLengthLimitation = beforeEdge.hasLengthLimitation();
            bool hasBeforeEdgeParallelLimitation = beforeEdge.hasParallelLimitation();

            if(!hasEdgeLengthLimitation && !hasEdgeParallelLimitation &&
                !hasBeforeEdgeLengthLimitation && !hasBeforeEdgeParallelLimitation)
            {
                beforeEdge.setPointB(point);
                edge.setPointA(point);
                return;
            }

            if (hasEdgeLengthLimitation && !hasEdgeParallelLimitation &&
                !hasBeforeEdgeLengthLimitation && !hasBeforeEdgeParallelLimitation)
            {
                moveVertexWithLengthLimitation(point, beforeEdge, edge, edge.getPointB(), edge.getLengthF());
                return;
            }

            if (!hasEdgeLengthLimitation && hasEdgeParallelLimitation &&
                !hasBeforeEdgeLengthLimitation && !hasBeforeEdgeParallelLimitation)
            {
                moveVertexWithParallelLimitation(point, beforeEdge, edge, index, polygons, 
                    edge.getParallelLimitation());
                return;
            }

            if (hasEdgeLengthLimitation && hasEdgeParallelLimitation &&
                !hasBeforeEdgeLengthLimitation && !hasBeforeEdgeParallelLimitation)
            {
                moveVertexWithLengthAndParallelLimitations(point, beforeEdge, edge, edge.getPointB(), edge.getLengthF(),
                    index, polygons, edge.getParallelLimitation());
                return;
            }

            if (!hasEdgeLengthLimitation && !hasEdgeParallelLimitation &&
                hasBeforeEdgeLengthLimitation && !hasBeforeEdgeParallelLimitation)
            {
                moveVertexWithLengthLimitation(point, beforeEdge, edge, beforeEdge.getPointA(),
                    beforeEdge.getLengthF());
                return;
            }

            if (hasEdgeLengthLimitation && !hasEdgeParallelLimitation &&
                hasBeforeEdgeLengthLimitation && !hasBeforeEdgeParallelLimitation)
            {
                PointF[] points = Algorithms.getCircleIntersections(beforeEdge.getPointA(), edge.getPointB(), beforeEdge.getLengthF(),
                    edge.getLengthF());

                if (points != null && points.Length == 2 &&
                    Algorithms.IsPointInsidePolygon(new PointF[] { beforeEdge.getPointA(), points[0],
                        edge.getPointB() }, point))
                {
                    beforeEdge.setPointB(points[0]);
                    edge.setPointA(points[0]);
                }

                else if (points != null && points.Length == 2 &&
                    Algorithms.IsPointInsidePolygon(new PointF[] { beforeEdge.getPointA(), points[1],
                        edge.getPointB() }, point))
                {
                    beforeEdge.setPointB(points[1]);
                    edge.setPointA(points[1]);
                }

                else if (points != null)
                {
                    pullDuringMove(edge.getPointA(), beforeIndex, index, point, polygons);
                }

                return;
            }

            if (!hasEdgeLengthLimitation && hasEdgeParallelLimitation &&
                hasBeforeEdgeLengthLimitation && !hasBeforeEdgeParallelLimitation)
            {
                PointF startingPoint = edge.getPointA();
                moveVertexWithLengthLimitation(point, beforeEdge, edge, beforeEdge.getPointA(), 
                    beforeEdge.getLengthF());
                List<Polygon> polygons_c = Limitation.getPolygonsCopies(polygons);
                if(!edge.getParallelLimitation().applyLimitation(polygons, new List<int>()))
                {
                    polygons = polygons_c;
                    beforeEdge.setPointB(startingPoint);
                    edge.setPointA(startingPoint);
                }
                return;
            }

            if (hasEdgeLengthLimitation && hasEdgeParallelLimitation &&
                hasBeforeEdgeLengthLimitation && !hasBeforeEdgeParallelLimitation)
            {
                List<Polygon> polygons_c = Limitation.getPolygonsCopies(polygons);
                PointF[] points = Algorithms.getCircleIntersections(beforeEdge.getPointA(), edge.getPointB(), beforeEdge.getLengthF(),
                    edge.getLengthF());

                if (points != null && points.Length == 2 &&
                    Algorithms.IsPointInsidePolygon(new PointF[] { beforeEdge.getPointA(), points[0],
                        edge.getPointB() }, point))
                {
                    beforeEdge.setPointB(points[0]);
                    edge.setPointA(points[0]);
                }

                else if (points != null && points.Length == 2 &&
                    Algorithms.IsPointInsidePolygon(new PointF[] { beforeEdge.getPointA(), points[1],
                        edge.getPointB() }, point))
                {
                    beforeEdge.setPointB(points[1]);
                    edge.setPointA(points[1]);
                }

                else if (points != null)
                {
                    pullDuringMove(edge.getPointA(), beforeIndex, index, point, polygons);
                }

                ParallelLimitation parallelLimitation = edge.getParallelLimitation();
                parallelLimitation.setEdge1((this, index));
                if(!parallelLimitation.applyLimitation(polygons, new List<int>()))
                {
                    polygons = polygons_c;
                }

                return;
            }

            if (!hasEdgeLengthLimitation && !hasEdgeParallelLimitation &&
                !hasBeforeEdgeLengthLimitation && hasBeforeEdgeParallelLimitation)
            {
                moveVertexWithParallelLimitation(point, beforeEdge, edge, beforeIndex, polygons, 
                    beforeEdge.getParallelLimitation());
                return;
            }

            if (hasEdgeLengthLimitation && !hasEdgeParallelLimitation &&
                !hasBeforeEdgeLengthLimitation && hasBeforeEdgeParallelLimitation)
            {
                PointF startingPoint = edge.getPointA();
                moveVertexWithLengthLimitation(point, beforeEdge, edge, edge.getPointB(),
                    edge.getLengthF());
                List<Polygon> polygons_c = Limitation.getPolygonsCopies(polygons);
                if (!beforeEdge.getParallelLimitation().applyLimitation(polygons, new List<int>()))
                {
                    polygons = polygons_c;
                    beforeEdge.setPointB(startingPoint);
                    edge.setPointA(startingPoint);
                }
                return;
            }

            if (!hasEdgeLengthLimitation && hasEdgeParallelLimitation &&
                !hasBeforeEdgeLengthLimitation && hasBeforeEdgeParallelLimitation)
            {
                List<Polygon> polygons_c = Limitation.getPolygonsCopies(polygons);

                beforeEdge.setPointB(point);
                edge.setPointA(point);

                ParallelLimitation parallelLimitation1 = beforeEdge.getParallelLimitation();
                parallelLimitation1.setEdge1((this, beforeIndex));
                if (!parallelLimitation1.applyLimitation(polygons, new List<int>()))
                    polygons = Limitation.getPolygonsCopies(polygons_c);

                ParallelLimitation parallelLimitation2 = edge.getParallelLimitation();
                parallelLimitation2.setEdge1((this, index));
                if (!parallelLimitation2.applyLimitation(polygons, new List<int>()))
                    polygons = polygons_c;

                return;
            }

            if (hasEdgeLengthLimitation && hasEdgeParallelLimitation &&
                !hasBeforeEdgeLengthLimitation && hasBeforeEdgeParallelLimitation)
            {
                List<Polygon> polygons_c = Limitation.getPolygonsCopies(polygons);

                moveVertexWithLengthLimitation(point, beforeEdge, edge, edge.getPointB(), edge.getLengthF());

                ParallelLimitation parallelLimitation1 = beforeEdge.getParallelLimitation();
                parallelLimitation1.setEdge1((this, beforeIndex));
                if (!parallelLimitation1.applyLimitation(polygons, new List<int>()))
                    polygons = Limitation.getPolygonsCopies(polygons_c);

                ParallelLimitation parallelLimitation2 = edge.getParallelLimitation();
                parallelLimitation2.setEdge1((this, index));
                if (!parallelLimitation2.applyLimitation(polygons, new List<int>()))
                    polygons = polygons_c;

                return;
            }

            if (!hasEdgeLengthLimitation && !hasEdgeParallelLimitation &&
                hasBeforeEdgeLengthLimitation && hasBeforeEdgeParallelLimitation)
            {
                moveVertexWithLengthAndParallelLimitations(point, beforeEdge, edge, beforeEdge.getPointA(), 
                    beforeEdge.getLengthF(), beforeIndex, polygons, beforeEdge.getParallelLimitation());
                return;
            }

            if (hasEdgeLengthLimitation && !hasEdgeParallelLimitation &&
                hasBeforeEdgeLengthLimitation && hasBeforeEdgeParallelLimitation)
            {
                List<Polygon> polygons_c = Limitation.getPolygonsCopies(polygons);
                PointF[] points = Algorithms.getCircleIntersections(beforeEdge.getPointA(), edge.getPointB(), beforeEdge.getLengthF(),
                    edge.getLengthF());

                if (points != null && points.Length == 2 &&
                    Algorithms.IsPointInsidePolygon(new PointF[] { beforeEdge.getPointA(), points[0],
                        edge.getPointB() }, point))
                {
                    beforeEdge.setPointB(points[0]);
                    edge.setPointA(points[0]);
                }

                else if (points != null && points.Length == 2 &&
                    Algorithms.IsPointInsidePolygon(new PointF[] { beforeEdge.getPointA(), points[1],
                        edge.getPointB() }, point))
                {
                    beforeEdge.setPointB(points[1]);
                    edge.setPointA(points[1]);
                }

                else if (points != null)
                {
                    pullDuringMove(edge.getPointA(), beforeIndex, index, point, polygons);
                }

                ParallelLimitation parallelLimitation = beforeEdge.getParallelLimitation();
                parallelLimitation.setEdge1((this, beforeIndex));
                if (!parallelLimitation.applyLimitation(polygons, new List<int>()))
                {
                    polygons = polygons_c;
                }

                return;
            }

            if (!hasEdgeLengthLimitation && hasEdgeParallelLimitation &&
                hasBeforeEdgeLengthLimitation && hasBeforeEdgeParallelLimitation)
            {
                List<Polygon> polygons_c = Limitation.getPolygonsCopies(polygons);

                moveVertexWithLengthLimitation(point, beforeEdge, edge, beforeEdge.getPointA(), 
                    beforeEdge.getLengthF());

                ParallelLimitation parallelLimitation1 = beforeEdge.getParallelLimitation();
                parallelLimitation1.setEdge1((this, beforeIndex));
                if (!parallelLimitation1.applyLimitation(polygons, new List<int>()))
                    polygons = Limitation.getPolygonsCopies(polygons_c);

                ParallelLimitation parallelLimitation2 = edge.getParallelLimitation();
                parallelLimitation2.setEdge1((this, index));
                if (!parallelLimitation2.applyLimitation(polygons, new List<int>()))
                    polygons = polygons_c;

                return;
            }

            if (hasEdgeLengthLimitation && hasEdgeParallelLimitation &&
                hasBeforeEdgeLengthLimitation && hasBeforeEdgeParallelLimitation)
            {
                List<Polygon> polygons_c = Limitation.getPolygonsCopies(polygons);
                PointF[] points = Algorithms.getCircleIntersections(beforeEdge.getPointA(), edge.getPointB(), beforeEdge.getLengthF(),
                    edge.getLengthF());

                if (points != null && points.Length == 2 &&
                    Algorithms.IsPointInsidePolygon(new PointF[] { beforeEdge.getPointA(), points[0],
                        edge.getPointB() }, point))
                {
                    beforeEdge.setPointB(points[0]);
                    edge.setPointA(points[0]);

                    ParallelLimitation parallelLimitation1 = beforeEdge.getParallelLimitation();
                    ParallelLimitation parallelLimitation2 = edge.getParallelLimitation();
                    if(!parallelLimitation1.applyLimitation(polygons, new List<int>()))
                    {
                        polygons = Limitation.getPolygonsCopies(polygons_c);
                    }
                    if(!parallelLimitation2.applyLimitation(polygons, new List<int>()))
                    {
                        polygons = Limitation.getPolygonsCopies(polygons_c);
                    }
                }

                else if (points != null && points.Length == 2 &&
                    Algorithms.IsPointInsidePolygon(new PointF[] { beforeEdge.getPointA(), points[1],
                        edge.getPointB() }, point))
                {
                    beforeEdge.setPointB(points[1]);
                    edge.setPointA(points[1]);

                    ParallelLimitation parallelLimitation1 = beforeEdge.getParallelLimitation();
                    ParallelLimitation parallelLimitation2 = edge.getParallelLimitation();
                    if (!parallelLimitation1.applyLimitation(polygons, new List<int>()))
                    {
                        polygons = Limitation.getPolygonsCopies(polygons_c);
                    }
                    if (!parallelLimitation2.applyLimitation(polygons, new List<int>()))
                    {
                        polygons = Limitation.getPolygonsCopies(polygons_c);
                    }
                }

                else if (points != null)
                {
                    pullDuringMove(edge.getPointA(), beforeIndex, index, point, polygons);
                }

                ParallelLimitation parallelLimitation = edge.getParallelLimitation();
                parallelLimitation.setEdge1((this, index));
                if (!parallelLimitation.applyLimitation(polygons, new List<int>()))
                {
                    polygons = polygons_c;
                }

                return;
            }
        }

        public void moveEdge(int index, PointF point, PointF? startingMovePoint, List<Polygon> polygons)
        {
            Edge edge = edges[index];
            int beforeIndex = index - 1 >= 0 ? index - 1 : edges.Count - 1;
            Edge beforeEdge = edges[beforeIndex];
            int nextIndex = index + 1 < edges.Count ? index + 1 : 0;
            Edge nextEdge = edges[nextIndex];

            ParallelLimitation parallelLimitation1, parallelLimitation2;

            if (!beforeEdge.hasLengthLimitation() && !nextEdge.hasLengthLimitation())
            {

                PointF moveVector = new PointF(point.X - startingMovePoint.Value.X, point.Y - startingMovePoint.Value.Y);
                PointF pointA = edge.getPointA();
                PointF pointB = edge.getPointB();
                pointA = new PointF(pointA.X + moveVector.X, pointA.Y + moveVector.Y);
                pointB = new PointF(pointB.X + moveVector.X, pointB.Y + moveVector.Y);

                beforeEdge.setPointB(pointA);
                edge.setPointA(pointA);
                edge.setPointB(pointB);
                nextEdge.setPointA(pointB);
            }
            else if(beforeEdge.hasLengthLimitation() && nextEdge.hasLengthLimitation() &&
                beforeEdge.hasParallelLimitation() && nextEdge.hasParallelLimitation() &&
                beforeEdge.getLengthLimitation().Size == nextEdge.getLengthLimitation().Size &&
                beforeEdge.getParallelLimitation() == nextEdge.getParallelLimitation())
            {
                float vectorX = point.X - startingMovePoint.Value.X;
                float vectorY = point.Y - startingMovePoint.Value.Y;
                PointF beforeEdgePointA = beforeEdge.getPointA();
                PointF nextEdgePointB = nextEdge.getPointB();
                PointF tempPoint = new PointF(beforeEdge.getPointB().X + vectorX, 
                    beforeEdge.getPointB().Y + vectorY);
                PointF[] points;
                if(Algorithms.findCommonPointBetweenCircleAndLine(beforeEdgePointA, tempPoint, beforeEdgePointA, 
                    beforeEdge.getLengthF(), out points))
                {
                    float d1 = Algorithms.getLineLength(point, points[0]);
                    float d2 = Algorithms.getLineLength(point, points[1]);
                    PointF newPointA = d1 < d2 ? points[0] : points[1];
                    float moveVectorX = newPointA.X - beforeEdgePointA.X;
                    float moveVectorY = newPointA.Y - beforeEdgePointA.Y;
                    PointF newPointB = new PointF(nextEdgePointB.X + moveVectorX, nextEdgePointB.Y + moveVectorY);

                    beforeEdge.setPointB(newPointA);
                    edge.setPointA(newPointA);
                    edge.setPointB(newPointB);
                    nextEdge.setPointA(newPointB);
                }

            }
            else if(!beforeEdge.hasLengthLimitation() && nextEdge.hasLengthLimitation())
            {
                pullDuringMove((PointF)startingMovePoint, index, nextIndex, point, polygons);
            }
            else if (beforeEdge.hasLengthLimitation() && !nextEdge.hasLengthLimitation())
            {
                pullDuringMove((PointF)startingMovePoint, beforeIndex, index, point, polygons);
            }
            else
            {
                pullDuringMove((PointF)startingMovePoint, beforeIndex, nextIndex, point, polygons);
            }


            parallelLimitation1 = null;
            if(beforeEdge.hasParallelLimitation())
            {
                parallelLimitation1 = beforeEdge.getParallelLimitation();
                parallelLimitation1.setEdge1((this, beforeIndex));
                parallelLimitation1.applyLimitation(polygons, new List<int>());
            }

            if(nextEdge.hasParallelLimitation())
            {
                parallelLimitation2 = nextEdge.getParallelLimitation();
                if (parallelLimitation1 != null && parallelLimitation1.id == parallelLimitation2.id)
                    return;
                parallelLimitation2.setEdge1((this, nextIndex));
                parallelLimitation2.applyLimitation(polygons, new List<int>());
            }
        }

        public void movePolygon(PointF point, PointF? startingMovePoint)
        {
            PointF moveVector = new PointF(point.X - startingMovePoint.Value.X, point.Y - startingMovePoint.Value.Y);
            foreach(var edge in edges)
            {
                PointF pointA = edge.getPointA();
                PointF pointB = edge.getPointB();
                edge.setPointA(new PointF(pointA.X + moveVector.X, pointA.Y + moveVector.Y));
                edge.setPointB(new PointF(pointB.X + moveVector.X, pointB.Y + moveVector.Y));
            }
        }

        private void moveVertexWithLengthLimitation(PointF point, Edge beforeEdge, Edge edge, PointF circleCenter,
            float radius)
        {
            PointF[] points;
            if (Algorithms.findCommonPointBetweenCircleAndLine(circleCenter, point, circleCenter,
                 radius, out points))
            {
                float d1 = Algorithms.getLineLength(point, points[0]);
                float d2 = Algorithms.getLineLength(point, points[1]);
                PointF newPoint = d1 < d2 ? points[0] : points[1];
                beforeEdge.setPointB(newPoint);
                edge.setPointA(newPoint);
            }
        }

        private void moveVertexWithParallelLimitation(PointF point, Edge beforeEdge, Edge edge, int index,
            List<Polygon> polygons, ParallelLimitation parallelLimitation)
        {
            PointF startPoint = beforeEdge.getPointB();
            parallelLimitation.setEdge1((this, index));
            beforeEdge.setPointB(point);
            edge.setPointA(point);
            List<int> hashList = new List<int>();
            if(!parallelLimitation.applyLimitation(polygons, hashList))
            {
                beforeEdge.setPointB(startPoint);
                edge.setPointA(startPoint);
            }
        }

        private void moveVertexWithLengthAndParallelLimitations(PointF point, Edge beforeEdge, Edge edge, 
            PointF circleCenter, float radius, int index, List<Polygon> polygons, ParallelLimitation parallelLimitation)
        {
            PointF startPoint = beforeEdge.getPointB();

            PointF[] points;
            if (Algorithms.findCommonPointBetweenCircleAndLine(circleCenter, point, circleCenter,
                 radius, out points))
            {
                float d1 = Algorithms.getLineLength(point, points[0]);
                float d2 = Algorithms.getLineLength(point, points[1]);
                PointF newPoint = d1 < d2 ? points[0] : points[1];
                beforeEdge.setPointB(newPoint);
                edge.setPointA(newPoint);
            }

            parallelLimitation.setEdge1((this, index));
            List<Polygon> polygons_c = Limitation.getPolygonsCopies(polygons);
            if (!parallelLimitation.applyLimitation(polygons, new List<int>()))
            {
                polygons = polygons_c;
                beforeEdge.setPointB(startPoint);
                edge.setPointA(startPoint);
            }
        }
        
        private void pullDuringMove(PointF startingPoint, int beforeIndex, int index, PointF point, List<Polygon> polygons)
        {
            int i = beforeIndex - 1 >= 0 ? beforeIndex - 1 : edges.Count - 1;
            while (i != index && edges[i].hasLengthLimitation())
                i = i - 1 >= 0 ? i - 1 : edges.Count - 1;

            int j = index + 1 < edges.Count ? index + 1 : 0;
            while (j != beforeIndex && j != i && edges[j].hasLengthLimitation())
                j = j + 1 < edges.Count ? j + 1 : 0;

            if (i == j || i == index || j == beforeIndex) return;

            float vectorX = point.X - startingPoint.X;
            float vectorY = point.Y - startingPoint.Y;

            ParallelLimitation parallelLimitation1 = null, parallelLimitation2 = null;

            PointF pointA = edges[j].getPointA();
            edges[j].setPointA(new PointF(pointA.X + vectorX, pointA.Y + vectorY));
            if (edges[j].hasParallelLimitation())
            {
                parallelLimitation1 = edges[j].getParallelLimitation();
                parallelLimitation1.setEdge1((this, j));
                if (!parallelLimitation1.applyLimitation(polygons, new List<int>()))
                {
                    edges[j].setPointA(pointA);
                    return;
                }
            }

            PointF pointB = edges[i].getPointB();
            edges[i].setPointB(new PointF(pointB.X + vectorX, pointB.Y + vectorY));
            if (edges[i].hasParallelLimitation())
            {
                parallelLimitation2 = edges[i].getParallelLimitation();
                if (parallelLimitation1 != parallelLimitation2)
                {
                    parallelLimitation2.setEdge1((this, i));
                    if (!parallelLimitation2.applyLimitation(polygons, new List<int>()))
                    {
                        edges[i].setPointA(pointB);
                        return;
                    }
                }
            }
            for (int ii = (i + 1 < edges.Count ? i + 1 : 0);
                ii != j; ii = ii + 1 < edges.Count ? ii + 1 : 0)
            {
                pointA = edges[ii].getPointA();
                pointB = edges[ii].getPointB();
                edges[ii].setPointA(new PointF(pointA.X + vectorX, pointA.Y + vectorY));
                edges[ii].setPointB(new PointF(pointB.X + vectorX, pointB.Y + vectorY));
            }
        }

    }
}
