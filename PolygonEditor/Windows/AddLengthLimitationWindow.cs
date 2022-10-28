using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PolygonEditor
{
    /// <summary>
    /// Class of window that is used to add length limitation.
    /// </summary>
    public partial class AddLengthLimitationWindow : Form
    {
        private LengthLimitation lengthLimitation1;
        private LengthLimitation lengthLimitation2;
        private Edge edge1;
        private Edge edge2;
        private MainWindow mainWindow;
        private bool okBlocked1 = false;
        private bool okBlocked2 = false;
        private List<Polygon> polygons;

        public AddLengthLimitationWindow(string length1, string length2, LengthLimitation lengthLimitation1, 
            LengthLimitation lengthLimitation2, Edge edge1, Edge edge2, MainWindow mainWindow, 
            List<Polygon> polygons)
        {
            this.lengthLimitation1 = lengthLimitation1;
            this.lengthLimitation2 = lengthLimitation2;
            this.mainWindow = mainWindow;
            this.edge1 = edge1;
            this.edge2 = edge2;
            this.polygons = polygons;
            InitializeComponent();
           
            // Type of the window choice (one edge ot two edge).
            firstEdgeLengthTextBox.Text = length1;
            if (length2 != null) 
                secondEdgeLengthTextBox.Text = length2;
            else
            {
                firstEdgeLengthTextBox.Location = new Point(firstEdgeLengthTextBox.Location.X, 
                    firstEdgeLengthTextBox.Location.Y + 20);
                firstEdgeLengthLabel.Location = new Point(firstEdgeLengthLabel.Location.X + 10,
                    firstEdgeLengthLabel.Location.Y + 20);
                firstEdgeLengthLabel.Text = "Edge length:";

                secondEdgeLengthLabel.Visible = false;
                secondEdgeLengthTextBox.Visible = false;
            }
        }

        /// <summary>
        /// Function that apply the limitations and infrom user when the limitations cannot be applied.
        /// </summary>
        private void accept()
        {
            if (lengthLimitation2 != null && edge2 != null)
            {
                lengthLimitation1.Size = (float)Convert.ToDouble(firstEdgeLengthTextBox.Text);
                edge1.addLengthLimitation(lengthLimitation1);
                List<Polygon> polygons_c = Limitation.getPolygonsCopies(polygons);
                List<int> hashList = new List<int>();
                if (!lengthLimitation1.applyLimitation(polygons, hashList))
                {
                    polygons = polygons_c;
                    MessageBox.Show("Cannot apply such limitation - polygon doesn't exist",
                                "Parallel limitations error", MessageBoxButtons.OK, MessageBoxIcon.Information,
                                MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }

                lengthLimitation2.Size = (float)Convert.ToDouble(secondEdgeLengthTextBox.Text);
                edge2.addLengthLimitation(lengthLimitation2);

                polygons_c = Limitation.getPolygonsCopies(polygons);
                hashList = new List<int>();
                if (!lengthLimitation2.applyLimitation(polygons, hashList))
                {
                    polygons = polygons_c;
                    MessageBox.Show("Cannot apply such limitation - polygon doesn't exist",
                                "Parallel limitations error", MessageBoxButtons.OK, MessageBoxIcon.Information,
                                MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
            }
            else
            {
                lengthLimitation1.Size = (float)Convert.ToDouble(firstEdgeLengthTextBox.Text);
                edge1.addLengthLimitation(lengthLimitation1);
                List<Polygon> polygons_c = Limitation.getPolygonsCopies(polygons);
                List<int> hashList = new List<int>();
                if (!lengthLimitation1.applyLimitation(polygons, hashList))
                {
                    polygons = polygons_c;
                    MessageBox.Show("Cannot apply such limitation - polygon doesn't exist",
                                "Parallel limitations error", MessageBoxButtons.OK, MessageBoxIcon.Information,
                                MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
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

        private void firstEdgeLengthTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '\r' &&
                e.KeyChar != '\b' && e.KeyChar != (char)Keys.Delete)
            {
                errorProvider1.SetError(firstEdgeLengthTextBox, "Allow only numeric values.");
                okButton.Enabled = false;
                okBlocked1 = true;
            }
            else if (e.KeyChar == '\r')
            {
                accept();
            }
        }

        private void secondEdgeLengthTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '\r' &&
                e.KeyChar != '\b' && e.KeyChar != (char)Keys.Delete)
            {
                errorProvider2.SetError(secondEdgeLengthTextBox, "Allow only numeric values.");
                okButton.Enabled = false;
                okBlocked2 = true;
            }
            else if (e.KeyChar == '\r')
            {
                accept();
            }
        }

        private void firstEdgeLengthTextBox_TextChanged(object sender, EventArgs e)
        {
            if(firstEdgeLengthTextBox.Text == "")
            {
                errorProvider1.SetError(firstEdgeLengthTextBox, "Allow only numeric values.");
                okButton.Enabled = false;
                okBlocked1 = true;
            }
            else
            {
                foreach (var c in firstEdgeLengthTextBox.Text)
                {
                    if (!char.IsDigit(c) && c != ',')
                    {
                        errorProvider1.SetError(firstEdgeLengthTextBox, "Allow only numeric values.");
                        okButton.Enabled = false;
                        okBlocked1 = true;
                        return;
                    }
                }
                errorProvider1.Clear();
                okBlocked1 = false;
                if (!okBlocked2) okButton.Enabled = true;
            }
        }

        private void secondEdgeLengthTextBox_TextChanged(object sender, EventArgs e)
        {
            if (secondEdgeLengthTextBox.Text == "")
            {
                errorProvider2.SetError(secondEdgeLengthTextBox, "Allow only numeric values.");
                okButton.Enabled = false;
                okBlocked2 = true;
            }
            else
            {
                foreach (var c in secondEdgeLengthTextBox.Text)
                {
                    if (!char.IsDigit(c) && c != ',')
                    {
                        errorProvider2.SetError(secondEdgeLengthTextBox, "Allow only numeric values.");
                        okButton.Enabled = false;
                        okBlocked2 = true;
                        return;
                    }
                }
                errorProvider2.Clear();
                okBlocked2 = false;
                if (!okBlocked1) okButton.Enabled = true;
            }
        }
    }
}
