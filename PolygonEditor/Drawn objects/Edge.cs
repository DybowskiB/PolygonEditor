using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PolygonEditor
{
    public class Edge
    {
        private PointF pointA;
        private PointF pointB;
        private RectangleF graphicalPointRectA;
        private RectangleF graphicalPointRectB;
        private SizeF graphicalPointSize;
        private LengthLimitation lengthLimitation;
        private ParallelLimitation parallelLimitation;


        public Edge(PointF pointA, PointF pointB){
            this.graphicalPointSize = new SizeF(12F, 12F);
            setPointA(pointA);
            setPointB(pointB);
        }

        public Edge deepCopy()
        {
            return new Edge(new PointF(this.pointA.X, this.pointA.Y), new PointF(this.pointB.X, this.pointB.Y));
        }

        public PointF getPointA()
        {
            return pointA;
        }

        public void setPointA(PointF pointA)
        {
            this.pointA = pointA;
            PointF leftCornerA = new PointF(pointA.X - graphicalPointSize.Width / 2,
               pointA.Y - graphicalPointSize.Height / 2);
            this.graphicalPointRectA = new RectangleF(leftCornerA, graphicalPointSize);
        }

        public PointF getPointB()
        {
            return pointB;
        }

        public void setPointB(PointF pointB)
        {
            this.pointB = pointB;
            PointF leftCornerB = new PointF(pointB.X - graphicalPointSize.Width / 2,
                pointB.Y - graphicalPointSize.Height / 2);
            this.graphicalPointRectB = new RectangleF(leftCornerB, graphicalPointSize);
        }

        public RectangleF getGraphicalPointRectA()
        {
            return this.graphicalPointRectA;
        }

        public RectangleF getGraphicalPointRectB()
        {
            return this.graphicalPointRectB;
        }

        public SizeF getGraphicalPointSize()
        {
            return this.graphicalPointSize;
        }

        public void addLengthLimitation(LengthLimitation sizeLimitation)
        {
            this.lengthLimitation = sizeLimitation;
        }

        public void removeLengthLimitation()
        {
            lengthLimitation = null;
        }

        public void addParallelLimitation(ParallelLimitation parallelLimitation)
        {
            this.parallelLimitation = parallelLimitation;
        }

        public ParallelLimitation getParallelLimitation()
        {
            return parallelLimitation;
        }

        public void removeParallelLimitation()
        {
            if (parallelLimitation != null)
            {
                var edges = parallelLimitation.getEdges();
                for(int  i = 0; i < edges.Count; ++i)
                {
                    var edge = edges[i].polygon.getEdges()[edges[i].edgeIndex];
                    if (edge == this)
                    {
                        var polEdg = parallelLimitation.getEdge1();
                        Edge markedEdge = polEdg.polygon.getEdges()[polEdg.edgeIndex];
                        edges.RemoveAt(i);
                        if (markedEdge == this)
                        {
                            parallelLimitation.setEdge1(edges[0]);
                        }
                        parallelLimitation = null;
                        break;
                    }
                }
                if(edges.Count == 1)
                {
                    var edge = edges[0].polygon.getEdges()[edges[0].edgeIndex];
                    edge.parallelLimitation = null;
                }
            }
        }

        public bool hasLengthLimitation()
        {
            return !(lengthLimitation == null);
        }

        public bool hasParallelLimitation()
        {
            return !(parallelLimitation == null);
        }

        public LengthLimitation getLengthLimitation()
        {
            return this.lengthLimitation;
        }

        public string getLengthS()
        {
            if (lengthLimitation != null) return ((int)lengthLimitation.Size).ToString();
            return (Algorithms.getLineLength(pointA, pointB)).ToString();
        }

        public float getLengthF()
        {
            if (lengthLimitation != null) return lengthLimitation.Size;
            return Algorithms.getLineLength(pointA, pointB);
        }
    }
}
