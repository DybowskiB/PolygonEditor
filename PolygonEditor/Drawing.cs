using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;
using System.Windows.Forms;

namespace PolygonEditor
{
    /// <summary>
    /// Class that manages the drawing, moving, removing and selecting the elements (vertices, edges, polygons).
    /// </summary>
    public class Drawing
    {
        // The set of drawn polygons
        List<Polygon> polygons;

        // GDI+ drawning surface
        private Graphics graphics;

        // Drawing state
        private State state;
      
        // Draw
        private Bitmap bitmap;
        private int BITMAPSIZE_X = 1761;
        private int BITMAPSIZE_Y = 980;
        private PointF point;
        private int lineWidth;
        private Pen linePen;
        private Pen vertexPen;
        private SolidBrush vertexBrush;
        private Polygon currentPolygon;
        private Edge potentialEdge;
        private Pen highlightedPen;
        private SolidBrush highlightedBrush;
        private bool isBresenhamAgorithmChecked;

        // Move
        private Polygon highlightedPolygon;
        private (Edge edge, Polygon polygon, int index) highlightedEdge;
        private (RectangleF? rect, Polygon polygon, int index) highlightedVertex;
        private bool moveFlag;
        private PointF? startingMovePoint;

        // Add limitation
        private List<Edge> selectedEdges;

        // State defines if drawing is allowed or not.
        // None - user can start the drawing.
        // Draw - user is drawing now.
        // Blocked - drawing is not allowed.
        public enum State
        {
            None = 0,
            Draw = 1,
            Blocked = 2
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Drawing()
        {
            this.polygons = new List<Polygon>();
            this.point = new PointF(-1, -1);
            this.state = State.None;
            this.lineWidth = 4;
            this.linePen = new Pen(Color.Red, this.lineWidth);
            this.vertexPen = new Pen(Color.Black, this.lineWidth / 2);
            this.vertexBrush = new SolidBrush(Color.White);
            this.currentPolygon = null;
            this.potentialEdge = null;
            this.highlightedPen = new Pen(Color.Yellow, this.lineWidth);
            this.highlightedBrush = new SolidBrush(Color.Yellow);
            this.highlightedPolygon = null;
            this.highlightedEdge = (null, null, -1);
            this.highlightedVertex = (null, null, -1);
            this.moveFlag = false;
            this.startingMovePoint = null;
            this.selectedEdges = new List<Edge>();
            this.isBresenhamAgorithmChecked = false;
            this.bitmap = null;
        }

        public List<Polygon> getPolygons()
        {
            return this.polygons;
        }

        public void setPolygons(List<Polygon> polygons)
        {
            this.polygons = polygons;
        }

        public void clearPolygons()
        {
            this.polygons = new List<Polygon>();
            this.currentPolygon = null;
        }

        public void addPolygon(Polygon polygon)
        {
            this.polygons.Add(polygon);
        }

        public void setGraphics(PictureBox picturebox)
        {
            bitmap = new Bitmap(BITMAPSIZE_X, BITMAPSIZE_Y);
            picturebox.Image = bitmap;
            this.graphics = Graphics.FromImage(picturebox.Image);
            this.graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        }

        public Graphics getGraphics()
        {
             return this.graphics;
        }

        public State getState()
        {
            return this.state;
        }

        public void setState(State state)
        {
            this.state = state;
        }

        public Bitmap getBitmap()
        {
            return this.bitmap;
        }

        public PointF getPoint()
        {
            return this.point;
        }

        public void setPoint((float X, float Y) point)
        {
            this.point.X = point.X;
            this.point.Y = point.Y;
        }

        public Pen getLinePen()
        {
            return this.linePen;
        }

        public Polygon getCurrentPolygon()
        {
            return this.currentPolygon;
        }

        public void setCurrentPolygon(Polygon polygon)
        {
            this.currentPolygon = polygon;
        }

        public Edge getPotentialEdge()
        {
            return this.potentialEdge;
        }

        public void setPotentialEdge(Edge edge)
        {
            this.potentialEdge = edge;
        }

        public void setBresenhamAlgorithm()
        {
            this.isBresenhamAgorithmChecked = true;
        }

        public void removeBresenhamAlgorithm()
        {
            this.isBresenhamAgorithmChecked = false;
        }

        public void setHighlightedPolygon(Polygon highlightedPolygon)
        {
            this.highlightedPolygon = highlightedPolygon;
        }

        public void setHighlightedEdge(Edge highlightedEdge, Polygon polygon, int index)
        {
            this.highlightedEdge = (highlightedEdge, polygon, index);
        }

        public void setHighlightedVertex(RectangleF highlightedVertex, Polygon polygon, int index)
        {
            this.highlightedVertex = (highlightedVertex, polygon, index);
        }

        public bool getMoveFlag()
        {
            return this.moveFlag;
        }

        public void setMoveFlag()
        {
            this.moveFlag = true;
        }

        public void removeMoveFlag()
        {
            this.moveFlag = false;
        }

        public PointF? getStartingMovePoint()
        {
            return this.startingMovePoint;
        }

        public void setStartingMovePoint(PointF point)
        {
            this.startingMovePoint = point;
        }

        public int getSelectedEdgesSize()
        {
            return this.selectedEdges.Count;
        }

        public List<(Polygon polygon, int index)> getSelectedEdgesIndex()
        {
            List<(Polygon polygon, int index)> selectedEdgesList = new List<(Polygon polygon, int index)>();
            foreach(var polygon in polygons)
            {
                var edges = polygon.getEdges();
                for(int i = 0; i < edges.Count; ++i)
                {
                    if (selectedEdges.Contains(edges[i]))
                        selectedEdgesList.Add((polygon, i));
                }
            }
            return selectedEdgesList;
        }


        // Draw
        public void drawVertex(RectangleF rect)
        {
            if (rect != highlightedVertex.rect) graphics.FillEllipse(vertexBrush, rect);
            else graphics.FillEllipse(highlightedBrush, rect);
            graphics.DrawEllipse(vertexPen, rect);
        }

        public void drawLine(PointF point)
        {
            if (!isBresenhamAgorithmChecked) graphics.DrawLine(linePen, this.point, point);
            else Algorithms.BresenhamLineAlgorithm(this.point, point, bitmap, linePen.Color);
            Edge newEdge = new Edge(this.point, point);
            if(currentPolygon != null) currentPolygon.addEdge(newEdge);
            drawVertex(newEdge.getGraphicalPointRectA());
            this.point = point;
        }

        public void drawLastEdge()
        {
            PointF point = currentPolygon.getEdges()[0].getPointA();
            drawLine(point);
            setPotentialEdge(null);
        }

        public void drawPolygon(Polygon polygon)
        {
            foreach (var edge in polygon.getEdges())
            {
                Pen pen = (edge == highlightedEdge.edge || selectedEdges.Contains(edge) ||
                    polygon == highlightedPolygon) ? highlightedPen : linePen;
                if (!isBresenhamAgorithmChecked) graphics.DrawLine(pen, edge.getPointA(), edge.getPointB());
                else Algorithms.BresenhamLineAlgorithm(edge.getPointA(), edge.getPointB(), bitmap, pen.Color);
                drawVertex(edge.getGraphicalPointRectA());
                drawVertex(edge.getGraphicalPointRectB());
                if(edge.hasLengthLimitation() && edge.hasParallelLimitation())
                {
                    PointF edgeCenter = Algorithms.getEdgeCenter(edge.getPointA(), edge.getPointB());
                    graphics.DrawString(edge.getLengthS() + " P" + edge.getParallelLimitation().getNumber(), 
                        new Font("Consolas", 7), Brushes.Black, edgeCenter);
                }
                else if(edge.hasLengthLimitation())
                {
                    PointF edgeCenter = Algorithms.getEdgeCenter(edge.getPointA(), edge.getPointB());
                    graphics.DrawString(edge.getLengthS(), new Font("Consolas", 7), Brushes.Black, edgeCenter);
                }
                else if(edge.hasParallelLimitation())
                {
                    PointF edgeCenter = Algorithms.getEdgeCenter(edge.getPointA(), edge.getPointB());
                    graphics.DrawString("P" + edge.getParallelLimitation().getNumber(), 
                        new Font("Consolas", 7), Brushes.Black, edgeCenter);
                }
            }
            List<Edge> edges = polygon.getEdges();
            if (edges != null && edges.Count > 0)
                drawVertex(edges[0].getGraphicalPointRectA());
        }

        public void redraw()
        {
            foreach (var polygon in polygons)
            {
                drawPolygon(polygon);
            }
            if (currentPolygon != null)
                drawPolygon(currentPolygon);
        }

        public bool isPolygonClosed(PointF point)
        {
            if (currentPolygon.getEdges() == null || currentPolygon.getEdges().Count == 0) return false;
            Edge startingEdge = currentPolygon.getEdges()[0];
            RectangleF firstVertex = startingEdge.getGraphicalPointRectA();
            return Algorithms.pointBelongsToRectangle(firstVertex, point);
        }

        public void addVertexToEdge(PointF point)
        {
            for(int i = polygons.Count - 1; i >= 0; --i)
            {
                var edges = polygons[i].getEdges();
                for (int j = 0; j < edges.Count; ++j)
                {
                    var edge = edges[j];
                    var pointA = edge.getPointA();
                    var pointB = edge.getPointB();
                    if(Algorithms.pointOnLine(pointA, pointB, point, 2 * linePen.Width))
                    {
                        polygons[i].addVertex(pointA, pointB, point, edge, j);
                        return;
                    }
                }
            }
        }


        // Move
        public void findHighlightedElement(PointF point)
        {
            highlightedPolygon = null;
            highlightedVertex = (null, null, -1);
            highlightedEdge = (null, null, -1);

            for (int i = polygons.Count - 1; i >= 0; --i)
            {
                List<Edge> edges = polygons[i].getEdges();
                for (int j = edges.Count - 1; j >= 0; --j)
                {
                    if (Algorithms.pointBelongsToRectangle(edges[j].getGraphicalPointRectA(), point))
                    {
                        setHighlightedVertex(edges[j].getGraphicalPointRectA(), polygons[i], j);
                        return;
                    }
                }

                for (int j = edges.Count - 1; j >= 0; --j)
                {
                    if (Algorithms.pointOnLine(edges[j].getPointA(), edges[j].getPointB(), point, 2 * lineWidth))
                    {
                        setHighlightedEdge(edges[j], polygons[i], j);
                        return;
                    }
                }

                if (Algorithms.IsPointInsidePolygon(polygons[i].getVertices(), point))
                {
                    highlightedPolygon = polygons[i];
                    return;
                }
            }
        }

        public void findHighlightedPolygon(PointF point)
        {
            highlightedPolygon = null;
            highlightedVertex = (null, null, -1);
            highlightedEdge = (null, null, -1);

            for (int i = polygons.Count - 1; i >= 0; --i)
            {
                if (Algorithms.IsPointInsidePolygon(polygons[i].getVertices(), point))
                {
                    highlightedPolygon = polygons[i];
                    return;
                }
            }
        }

        public void findHighlightedVertex(PointF point)
        {
            highlightedPolygon = null;
            highlightedVertex = (null, null, -1);
            highlightedEdge = (null, null, -1);

            for (int i = polygons.Count - 1; i >= 0; --i)
            {
                List<Edge> edges = polygons[i].getEdges();
                for (int j = edges.Count - 1; j >= 0; --j)
                {
                    if (Algorithms.pointBelongsToRectangle(edges[j].getGraphicalPointRectA(), point))
                    {
                        setHighlightedVertex(edges[j].getGraphicalPointRectA(), polygons[i], j);
                        return;
                    }

                }
            }
        }

        public void findHighlightedEdge(PointF point)
        {
            highlightedPolygon = null;
            highlightedVertex = (null, null, -1);
            highlightedEdge = (null, null, -1);

            for (int i = polygons.Count - 1; i >= 0; --i)
            {
                List<Edge> edges = polygons[i].getEdges();
                for (int j = edges.Count - 1; j >= 0; --j)
                {
                    if (Algorithms.pointOnLine(edges[j].getPointA(), edges[j].getPointB(), point, 2 * lineWidth))
                    {
                        setHighlightedEdge(edges[j], polygons[i], j);
                        return;
                    }
                }
            }
        }

        public void removeHighlightedElement(MainWindow mainWindow)
        {
            if(highlightedVertex.polygon != null && highlightedVertex.rect != null)
            {
                List<Edge> edges = highlightedVertex.polygon.getEdges();
                if(edges.Count == 3)
                {
                    foreach(var edge in edges)
                    {
                        edge.removeLengthLimitation();
                        edge.removeParallelLimitation();
                    }
                    polygons.Remove(highlightedVertex.polygon);
                    return;
                }
                Edge beforeEdge = edges[highlightedVertex.index - 1 >= 0 ? 
                    highlightedVertex.index - 1 : edges.Count - 1];
                edges[highlightedVertex.index].setPointA(beforeEdge.getPointA());
                edges[highlightedVertex.index].removeLengthLimitation();
                edges[highlightedVertex.index].removeParallelLimitation();
                edges.Remove(beforeEdge);
                return;
            }

            if (highlightedEdge.polygon != null && highlightedEdge.edge != null)
            {
                List<Edge> edges = highlightedEdge.polygon.getEdges();
                if (edges.Count == 3)
                {
                    foreach (var edge in edges)
                    {
                        edge.removeLengthLimitation();
                        edge.removeParallelLimitation();
                    }
                    polygons.Remove(highlightedEdge.polygon);
                    return;
                }
                Edge removedEdge = edges[highlightedEdge.index];
                if (!removedEdge.hasParallelLimitation() && !removedEdge.hasLengthLimitation())
                {
                    Edge nextEdge = edges[highlightedEdge.index + 1 < edges.Count ? highlightedEdge.index + 1 : 0];
                    nextEdge.removeLengthLimitation();
                    nextEdge.removeParallelLimitation();
                    nextEdge.setPointA(removedEdge.getPointA());
                    removedEdge.removeParallelLimitation();
                    edges.Remove(removedEdge);
                }
                else
                {
                    RemoveChoiceWindow removeChoiceWindow = new RemoveChoiceWindow(edges, highlightedEdge.index, mainWindow);
                    removeChoiceWindow.Show();
                }
                return;
            }

            if(highlightedPolygon != null)
            {
                foreach (var edge in highlightedPolygon.getEdges())
                {
                    edge.removeLengthLimitation();
                    edge.removeParallelLimitation();
                }
                polygons.Remove(highlightedPolygon);
            }
        }

        public void moveHighlightedElement(PointF point)
        {
            if (highlightedVertex.polygon != null && highlightedVertex.rect != null)
            {
                highlightedVertex.polygon.moveVertex(highlightedVertex.index, point, polygons);
                Edge edge = highlightedVertex.polygon.getEdges()[highlightedVertex.index];
                PointF leftCorner = new PointF(point.X - edge.getGraphicalPointSize().Width / 2,
                           point.Y - edge.getGraphicalPointSize().Height / 2);
                RectangleF newVertex = new RectangleF(leftCorner, edge.getGraphicalPointSize());
                setHighlightedVertex(newVertex, highlightedVertex.polygon, highlightedVertex.index);
                return;
            }

            if (highlightedEdge.polygon != null && highlightedEdge.edge != null)
            {
                highlightedEdge.polygon.moveEdge(highlightedEdge.index, point, startingMovePoint, polygons);
                startingMovePoint = point;
                return;
            }

            if (highlightedPolygon != null)
            {
                highlightedPolygon.movePolygon(point, startingMovePoint);
                startingMovePoint = point;
                polygons.Remove(highlightedPolygon);
                polygons.Add(highlightedPolygon);
            }
        }


        // Adding limitation
        public void selectEdges()
        {
            if (highlightedEdge.polygon != null)
            {
                if (selectedEdges.Count == 2 && !selectedEdges.Contains(highlightedEdge.edge))
                    selectedEdges.RemoveAt(0);
                if (!selectedEdges.Contains(highlightedEdge.edge))
                    selectedEdges.Add(highlightedEdge.edge);
            }
            else selectedEdges.Clear();
        }

        public void clearSelectedEdges()
        {
            selectedEdges.Clear();
        }
    }
}
