using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PolygonEditor
{
    /// <summary>
    /// Class of the window used during deleting the edge.
    /// </summary>
    public partial class RemoveChoiceWindow : Form
    {
        private List<Edge> edges;
        private int index;
        private Edge edge;
        private MainWindow mainWindow;

        // ComboBox options
        private const string remLen = "Remove length limitation";
        private const string remPar = "Remove parallel limitation";
        private const string remAll = "Remove all limitations";
        private const string remEdge = "Remove edge";

        public RemoveChoiceWindow(List<Edge> edges, int index, MainWindow mainWindow)
        {
            this.edges = edges;
            this.index = index;
            this.edge = edges[index];
            this.mainWindow = mainWindow;
            InitializeComponent();

            // Adding needed options to comboBox
            if(edge.hasLengthLimitation())
                whatRemoveComboBox.Items.Add(remLen);
            if(edge.hasParallelLimitation())
                whatRemoveComboBox.Items.Add(remPar);
            if (edge.hasLengthLimitation() && edge.hasParallelLimitation())
                whatRemoveComboBox.Items.Add(remAll);
            whatRemoveComboBox.Items.Add(remEdge);
            whatRemoveComboBox.SelectedIndex = whatRemoveComboBox.Items.Count - 1;

            mainWindow.Enabled = false;
        }

        /// <summary>
        /// Function that remove limitations and edges after user choice.
        /// </summary>
        private void accept()
        {
            if ((string)whatRemoveComboBox.SelectedItem == remLen)
                edge.removeLengthLimitation();
            else if ((string)whatRemoveComboBox.SelectedItem == remPar)
                edge.removeParallelLimitation();
            else if ((string)whatRemoveComboBox.SelectedItem == remAll)
            {
                edge.removeLengthLimitation();
                edge.removeParallelLimitation();
            }
            else
            {
                Edge nextEdge = edges[index + 1 < edges.Count ? index + 1 : 0];
                nextEdge.removeLengthLimitation();
                nextEdge.removeParallelLimitation();
                nextEdge.setPointA(edge.getPointA());
                edge.removeParallelLimitation();
                edges.Remove(edge);
            }
            mainWindow.Enabled = true;
            this.Close();
        }

        // Events
        private void okButton_Click(object sender, EventArgs e)
        {
            accept();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            mainWindow.Enabled = true;
            this.Close();
        }

        private void whatRemoveComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                accept();
        }
    }
}
