using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolygonEditor
{
    /// <summary>
    /// Class of PolygonEditor main window.
    /// </summary>
    public partial class MainWindow : Form
    {
        // Object that is used to control the drawing process.
        private Drawing drawing;
 
        public MainWindow()
        {
            InitializeComponent();
            drawing = new Drawing();
            drawing.setGraphics(pictureBox);

            // Examples 
            List<Polygon> polygons = new List<Polygon>();

            // First example
            List<Edge> edges1 = new List<Edge>() { new Edge(new PointF(200, 400), new PointF(300, 400)),
                                                   new Edge(new PointF(300, 400), new PointF(175, 800)),
                                                   new Edge(new PointF(175, 800), new PointF(315, 800)),
                                                   new Edge(new PointF(315, 800), new PointF(200, 400)) };
            Polygon polygon1 = new Polygon(edges1);
            polygons.Add(polygon1);

            LengthLimitation lengthLimitation1 = new LengthLimitation(polygon1, 0);
            lengthLimitation1.Size = 100;
            edges1[0].addLengthLimitation(lengthLimitation1);

            LengthLimitation lengthLimitation2 = new LengthLimitation(polygon1, 2);
            lengthLimitation2.Size = 150;
            edges1[2].addLengthLimitation(lengthLimitation2);

            ParallelLimitation parallelLimitation1 = new ParallelLimitation(polygon1, 0);
            edges1[0].addParallelLimitation(parallelLimitation1);
            parallelLimitation1.addEdge((polygon1, 2));
            edges1[2].addParallelLimitation(parallelLimitation1);

            // Second example
            List<Edge> edges2 = new List<Edge>() { new Edge(new PointF(175, 100), new PointF(315, 100)),
                                                   new Edge(new PointF(315, 100), new PointF(315, 300)),
                                                   new Edge(new PointF(315, 300), new PointF(175, 300)),
                                                   new Edge(new PointF(175, 300), new PointF(175, 100)) };
            Polygon polygon2 = new Polygon(edges2);
            polygons.Add(polygon2);

            edges2[0].addParallelLimitation(parallelLimitation1);
            parallelLimitation1.addEdge((polygon2, 0));
            edges2[2].addParallelLimitation(parallelLimitation1);
            parallelLimitation1.addEdge((polygon2, 2));

            // Third example
            List<Edge> edges3 = new List<Edge>() { new Edge(new PointF(785, 135), new PointF(934, 237)),
                                                   new Edge(new PointF(934, 237.105148F), new PointF(970, 434)),
                                                   new Edge(new PointF(970, 434), new PointF(816, 577)),
                                                   new Edge(new PointF(816, 577), new PointF(661, 492)),
                                                   new Edge(new PointF(661, 492), new PointF(624, 285)),
                                                   new Edge(new PointF(624, 285), new PointF(785, 135))};
            Polygon polygon3 = new Polygon(edges3);
            polygons.Add(polygon3);

            LengthLimitation lengthLimitation3 = new LengthLimitation(polygon3, 1);
            lengthLimitation3.Size = edges3[1].getLengthF();
            edges3[1].addLengthLimitation(lengthLimitation3);

            LengthLimitation lengthLimitation4 = new LengthLimitation(polygon3, 2);
            lengthLimitation4.Size = edges3[2].getLengthF();
            edges3[2].addLengthLimitation(lengthLimitation4);

            LengthLimitation lengthLimitation5 = new LengthLimitation(polygon3, 4);
            lengthLimitation5.Size = edges3[4].getLengthF();
            edges3[4].addLengthLimitation(lengthLimitation5);

            LengthLimitation lengthLimitation6 = new LengthLimitation(polygon3, 5);
            lengthLimitation6.Size = edges3[5].getLengthF();
            edges3[5].addLengthLimitation(lengthLimitation6);

            ParallelLimitation parallelLimitation2 = new ParallelLimitation(polygon3, 2);
            edges3[2].addParallelLimitation(parallelLimitation2);
            parallelLimitation2.addEdge((polygon3, 5));
            edges3[5].addParallelLimitation(parallelLimitation2);

            // Fourth example
            List<Edge> edges4 = new List<Edge>() { new Edge(new PointF(1211, 597), new PointF(1491, 609)),
                                                   new Edge(new PointF(1491, 609), new PointF(1400, 773)),
                                                   new Edge(new PointF(1400, 773), new PointF(1120, 761)),
                                                   new Edge(new PointF(1120, 761), new PointF(1211, 597))};
            Polygon polygon4 = new Polygon(edges4);
            polygons.Add(polygon4);

            LengthLimitation lengthLimitation7 = new LengthLimitation(polygon4, 0);
            lengthLimitation7.Size = 280;
            edges4[0].addLengthLimitation(lengthLimitation7);
            lengthLimitation7.applyLimitation(polygons, new List<int>());

            LengthLimitation lengthLimitation8 = new LengthLimitation(polygon4, 2);
            lengthLimitation8.Size = 280;
            edges4[2].addLengthLimitation(lengthLimitation8);
            lengthLimitation8.applyLimitation(polygons, new List<int>());

            ParallelLimitation parallelLimitation3 = new ParallelLimitation(polygon4, 1);
            edges4[1].addParallelLimitation(parallelLimitation3);
            parallelLimitation3.addEdge((polygon4, 3));
            edges4[3].addParallelLimitation(parallelLimitation3);
            parallelLimitation3.applyLimitation(polygons, new List<int>());

            drawing.setPolygons(polygons);
        }


        // Events
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right ||
                e.Button == System.Windows.Forms.MouseButtons.Middle) return;
            switch (drawing.getState())
            {
                case Drawing.State.None:
                        drawing.setState(Drawing.State.Draw);
                        drawing.setPoint((e.X, e.Y));
                        drawing.setCurrentPolygon(new Polygon());
                    break;
                case Drawing.State.Draw:
                    if(addVertexRadioButton.Checked)
                    {
                        drawing.addVertexToEdge(e.Location);
                        pictureBox.Invalidate();
                        break;
                    }
                    if (!drawing.isPolygonClosed(e.Location))
                    {
                        drawing.drawLine(e.Location);
                        break;
                    }
                    if (drawing.getCurrentPolygon().getEdges().Count + 1 > 2)
                    {
                        drawing.drawLastEdge();
                        drawing.addPolygon(drawing.getCurrentPolygon());
                        drawing.setCurrentPolygon(null);
                        drawing.setState(Drawing.State.None);
                    }
                    break;
                case Drawing.State.Blocked:
                    if (moveRadioButton.Checked)
                    {
                        drawing.findHighlightedElement(e.Location);
                        drawing.setStartingMovePoint(e.Location);
                        drawing.setMoveFlag();
                    }
                    else if (removeRadioButton.Checked)
                    {
                        drawing.findHighlightedElement(e.Location);
                        drawing.removeHighlightedElement(this);
                    }
                    else if (selectRadioButton.Checked)
                    {
                        drawing.findHighlightedEdge(e.Location);
                        drawing.selectEdges();
                        if (drawing.getSelectedEdgesSize() > 0)
                        {
                            addLengthLimitationButton.Enabled = true;
                            if (drawing.getSelectedEdgesSize() == 2)
                                addParallelLimitationButton.Enabled = true;
                        }
                        else if(drawing.getSelectedEdgesSize() == 0)
                        {
                            addLengthLimitationButton.Enabled = false;
                            addParallelLimitationButton.Enabled = false;
                        }
                    }
                    pictureBox.Invalidate();
                    break;
                default:
                    break;
            }
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (addVertexRadioButton.Checked) return;
            switch (drawing.getState())
            {
                case Drawing.State.Draw:
                    if (!BresenhamAlgorithmCheckBox.Checked)
                        drawing.getGraphics().DrawLine(drawing.getLinePen(), drawing.getPoint(), e.Location);
                    else
                        Algorithms.BresenhamLineAlgorithm(drawing.getPoint(), e.Location,
                            drawing.getBitmap(), drawing.getLinePen().Color);
                    drawing.setPotentialEdge(new Edge(drawing.getPoint(), e.Location));
                    pictureBox.Invalidate();
                    break;
                case Drawing.State.Blocked:
                    if (drawing.getMoveFlag()) drawing.moveHighlightedElement(e.Location);
                    else if (selectRadioButton.Checked) drawing.findHighlightedEdge(e.Location);
                    else drawing.findHighlightedElement(e.Location);
                    pictureBox.Invalidate();
                    break;
                default:
                    break;
            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (drawing.getMoveFlag())
            {
                drawing.moveHighlightedElement(e.Location);
                drawing.removeMoveFlag();
            }
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            drawing.setGraphics(pictureBox);
            Edge potentialEdge = drawing.getPotentialEdge();
            if (potentialEdge != null)
            {
                if (!BresenhamAlgorithmCheckBox.Checked)
                    drawing.getGraphics().DrawLine(drawing.getLinePen(),
                        potentialEdge.getPointA(), potentialEdge.getPointB());
                else
                    Algorithms.BresenhamLineAlgorithm(potentialEdge.getPointA(), potentialEdge.getPointB(),
                        drawing.getBitmap(), drawing.getLinePen().Color);
                drawing.drawVertex(potentialEdge.getGraphicalPointRectA());
            }
            drawing.redraw();
        }

        private void drawRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (drawRadioButton.Checked && drawing.getState() == Drawing.State.Blocked)
                drawing.setState(Drawing.State.None);
            if(drawRadioButton.Checked)
                pictureBox.Cursor = Cursors.Cross;
        }

        private void addVertexRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (addVertexRadioButton.Checked)
            {
                drawing.setCurrentPolygon(null);
                drawing.setPotentialEdge(null);
                drawing.redraw();
                drawing.setState(Drawing.State.Draw);
                pictureBox.Cursor = Cursors.Cross;
            }
            else if(drawRadioButton.Checked)
            {
                drawing.setState(Drawing.State.None);
            }
        }

        private void selectRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (selectRadioButton.Checked)
            {
                drawing.setCurrentPolygon(null);
                drawing.setPotentialEdge(null);
                drawing.redraw();
                drawing.setState(Drawing.State.Blocked);
                pictureBox.Cursor = Cursors.Hand;
            }
            else
            {
                drawing.clearSelectedEdges();
                addLengthLimitationButton.Enabled = false;
                addParallelLimitationButton.Enabled = false;
            }
            }

        private void moveRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (moveRadioButton.Checked)
            {
                drawing.setCurrentPolygon(null);
                drawing.setPotentialEdge(null);
                drawing.redraw();
                drawing.setState(Drawing.State.Blocked);
                pictureBox.Cursor = Cursors.SizeAll;
            }
        }

        private void removeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (removeRadioButton.Checked)
            {
                drawing.setCurrentPolygon(null);
                drawing.setPotentialEdge(null);
                drawing.redraw();
                drawing.setState(Drawing.State.Blocked);
                pictureBox.Cursor = Cursors.Hand;
            }
        }

        private void BresenhamAlgorithmCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (BresenhamAlgorithmCheckBox.Checked) drawing.setBresenhamAlgorithm();
            else drawing.removeBresenhamAlgorithm();
        }

        private void addLengthLimitationButton_Click(object sender, EventArgs e)
        {
            var selectedEdgesList = drawing.getSelectedEdgesIndex();
            LengthLimitation lengthLimitation1, lengthLimitation2 = null;
            Edge edge1, edge2 = null;
            lengthLimitation1 = new LengthLimitation(selectedEdgesList[0].polygon, 
                selectedEdgesList[0].index);
            edge1 = selectedEdgesList[0].polygon.getEdges()[selectedEdgesList[0].index];
            string length2 = null;
            if (selectedEdgesList.Count == 2)
            {
                lengthLimitation2 = new LengthLimitation(selectedEdgesList[1].polygon,
                selectedEdgesList[1].index);
                edge2 = selectedEdgesList[1].polygon.getEdges()[selectedEdgesList[1].index];
                length2 = edge2.getLengthS();
            }
            AddLengthLimitationWindow addLengthLimitationWindow = new AddLengthLimitationWindow(
                edge1.getLengthS(), length2, lengthLimitation1, 
                lengthLimitation2, edge1, edge2, this, drawing.getPolygons());
            this.Enabled = false;
            addLengthLimitationWindow.Show();
        }

        private void addParallelLimitation_Click(object sender, EventArgs e)
        {
            var selectedEdgesList = drawing.getSelectedEdgesIndex();
            Polygon polygon1 = selectedEdgesList[0].polygon;
            Polygon polygon2 = selectedEdgesList[1].polygon;
            int edgeIndex1 = selectedEdgesList[0].index;
            int edgeIndex2 = selectedEdgesList[1].index;
            Edge edge1 = polygon1.getEdges()[edgeIndex1];
            Edge edge2 = polygon2.getEdges()[edgeIndex2];

            if (!ParallelLimitation.checkPossibiltyToApply(polygon1, edgeIndex1, polygon2, edgeIndex2))
            {
                MessageBox.Show("Cannot apply parallel limitations on adjacent edges.",
                       "Parallel limitations error", MessageBoxButtons.OK, MessageBoxIcon.Information,
                       MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
            else
            {
                var polygons_c = Limitation.getPolygonsCopies(drawing.getPolygons());
                List<int> hashList = new List<int>();

                if (!edge1.hasParallelLimitation() && !edge2.hasParallelLimitation())
                {
                    ParallelLimitation parallelLimitation = new ParallelLimitation(polygon1, edgeIndex1);
                    parallelLimitation.addEdge((polygon2, edgeIndex2));
                    edge1.addParallelLimitation(parallelLimitation);
                    edge2.addParallelLimitation(parallelLimitation);
                    if (!parallelLimitation.applyLimitation(drawing.getPolygons(), hashList))
                    {
                        drawing.setPolygons(polygons_c);
                        MessageBox.Show("Cannot apply such limitation - polygon doesn't exist",
                            "Parallel limitations error", MessageBoxButtons.OK, MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    }
                }
                else if (edge1.hasParallelLimitation() && !edge2.hasParallelLimitation())
                {
                    ParallelLimitation parallelLimitation = edge1.getParallelLimitation();
                    edge2.addParallelLimitation(parallelLimitation);
                    parallelLimitation.addEdge((polygon2, edgeIndex2));
                    if (!parallelLimitation.applyLimitation(drawing.getPolygons(), hashList))
                    { 
                        drawing.setPolygons(polygons_c);
                        MessageBox.Show("Cannot apply such limitation - polygon doesn't exist",
                            "Parallel limitations error", MessageBoxButtons.OK, MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    }
                }
                else if (!edge1.hasParallelLimitation() && edge2.hasParallelLimitation())
                {
                    ParallelLimitation parallelLimitation = edge2.getParallelLimitation();
                    edge1.addParallelLimitation(parallelLimitation);
                    parallelLimitation.addEdge((polygon1, edgeIndex1));
                    if (!parallelLimitation.applyLimitation(drawing.getPolygons(), hashList))
                    {
                        drawing.setPolygons(polygons_c);
                        MessageBox.Show("Cannot apply such limitation - polygon doesn't exist",
                            "Parallel limitations error", MessageBoxButtons.OK, MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    }
                }
                else if (edge1.hasParallelLimitation() && edge2.hasParallelLimitation())
                {
                    ParallelLimitation parallelLimitation1 = edge1.getParallelLimitation();
                    ParallelLimitation parallelLimitation2 = edge2.getParallelLimitation();
                    List<(Polygon polygon, int edgeIndex)> edges = parallelLimitation2.getEdges();
                    foreach (var edgeData in edges)
                    {
                        hashList = new List<int>();
                        Edge edge = edgeData.polygon.getEdges()[edgeData.edgeIndex];
                        edge.addParallelLimitation(parallelLimitation1);
                        parallelLimitation1.addEdge(edgeData);
                        if (!parallelLimitation1.applyLimitation(drawing.getPolygons(), hashList))
                        {
                            edge.removeParallelLimitation();
                            drawing.setPolygons(polygons_c);
                            MessageBox.Show("Cannot apply such limitation - polygon doesn't exist",
                                "Parallel limitations error", MessageBoxButtons.OK, MessageBoxIcon.Information,
                                MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        }
                    }
                }
            }
        }

        private void deleteLastPolygonButton_Click(object sender, EventArgs e)
        {
            if(drawing.getCurrentPolygon() != null)
            {
                drawing.setCurrentPolygon(null);
            }
            else
            {
                var polygons = drawing.getPolygons();
                if(polygons != null && polygons.Count > 0)
                {
                    polygons.RemoveAt(polygons.Count - 1);
                }
            }
            drawing.setPotentialEdge(null);
            drawing.setState(Drawing.State.None);
            pictureBox.Invalidate();
        }

        private void deleteLastEdgeButton_Click(object sender, EventArgs e)
        {
            Polygon currentPolygon = drawing.getCurrentPolygon();
            List<Polygon> polygons = drawing.getPolygons();
            if (currentPolygon != null && currentPolygon.getEdges().Count > 0)
            {
                List<Edge> edges = currentPolygon.getEdges();
                edges[edges.Count - 1].removeParallelLimitation();
                edges.RemoveAt(edges.Count - 1);
                drawing.setPotentialEdge(null);
                if (edges.Count == 0)
                {
                    drawing.setState(Drawing.State.None);
                    if (polygons.Contains(currentPolygon)) polygons.Remove(currentPolygon);
                    drawing.setCurrentPolygon(null);
                }
                else drawing.setPoint((edges[edges.Count - 1].getPointB().X, edges[edges.Count - 1].getPointB().Y));
            }
            else if (polygons.Count > 0)
            {
                List<Edge> edges = polygons[polygons.Count - 1].getEdges();
                edges.RemoveAt(edges.Count - 1);
                drawing.setPotentialEdge(null);
                drawing.setPoint((edges[edges.Count - 1].getPointB().X, edges[edges.Count - 1].getPointB().Y));
                drawing.setCurrentPolygon(polygons[polygons.Count - 1]);
                polygons.RemoveAt(polygons.Count - 1);
                drawing.setState(Drawing.State.Draw);
                drawRadioButton.Checked = true;

            }
            else drawing.setState(Drawing.State.None);
            pictureBox.Invalidate();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            drawing.clearPolygons();
            drawing.setPotentialEdge(null);
            drawing.setGraphics(pictureBox);
            if (drawing.getState() == Drawing.State.Draw)
                drawing.setState(Drawing.State.None);
            ParallelLimitation.parallelLimitationNumber = 0;
        }
    }
}
