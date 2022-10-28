
namespace PolygonEditor
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.Menu = new System.Windows.Forms.GroupBox();
            this.addLengthLimitationButton = new System.Windows.Forms.Button();
            this.addParallelLimitationButton = new System.Windows.Forms.Button();
            this.deleteLastEdgeButton = new System.Windows.Forms.Button();
            this.deleteLastPolygonButton = new System.Windows.Forms.Button();
            this.modeGroupBox = new System.Windows.Forms.GroupBox();
            this.selectRadioButton = new System.Windows.Forms.RadioButton();
            this.removeRadioButton = new System.Windows.Forms.RadioButton();
            this.moveRadioButton = new System.Windows.Forms.RadioButton();
            this.addVertexRadioButton = new System.Windows.Forms.RadioButton();
            this.drawRadioButton = new System.Windows.Forms.RadioButton();
            this.clearButton = new System.Windows.Forms.Button();
            this.BresenhamAlgorithmCheckBox = new System.Windows.Forms.CheckBox();
            this.panel = new System.Windows.Forms.Panel();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel.SuspendLayout();
            this.Menu.SuspendLayout();
            this.modeGroupBox.SuspendLayout();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.Menu, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.panel, 1, 0);
            this.tableLayoutPanel.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 550F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(977, 547);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // Menu
            // 
            this.Menu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Menu.AutoSize = true;
            this.Menu.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.Menu.Controls.Add(this.addLengthLimitationButton);
            this.Menu.Controls.Add(this.addParallelLimitationButton);
            this.Menu.Controls.Add(this.deleteLastEdgeButton);
            this.Menu.Controls.Add(this.deleteLastPolygonButton);
            this.Menu.Controls.Add(this.modeGroupBox);
            this.Menu.Controls.Add(this.clearButton);
            this.Menu.Controls.Add(this.BresenhamAlgorithmCheckBox);
            this.Menu.Location = new System.Drawing.Point(3, 3);
            this.Menu.Name = "Menu";
            this.Menu.Size = new System.Drawing.Size(144, 544);
            this.Menu.TabIndex = 0;
            this.Menu.TabStop = false;
            this.Menu.Text = "Menu";
            // 
            // addLengthLimitationButton
            // 
            this.addLengthLimitationButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.addLengthLimitationButton.AutoSize = true;
            this.addLengthLimitationButton.Enabled = false;
            this.addLengthLimitationButton.Location = new System.Drawing.Point(12, 257);
            this.addLengthLimitationButton.Name = "addLengthLimitationButton";
            this.addLengthLimitationButton.Size = new System.Drawing.Size(117, 50);
            this.addLengthLimitationButton.TabIndex = 6;
            this.addLengthLimitationButton.Text = "Add limitation\r\non length";
            this.addLengthLimitationButton.UseVisualStyleBackColor = true;
            this.addLengthLimitationButton.Click += new System.EventHandler(this.addLengthLimitationButton_Click);
            // 
            // addParallelLimitationButton
            // 
            this.addParallelLimitationButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.addParallelLimitationButton.AutoSize = true;
            this.addParallelLimitationButton.Enabled = false;
            this.addParallelLimitationButton.Location = new System.Drawing.Point(12, 313);
            this.addParallelLimitationButton.Name = "addParallelLimitationButton";
            this.addParallelLimitationButton.Size = new System.Drawing.Size(117, 50);
            this.addParallelLimitationButton.TabIndex = 5;
            this.addParallelLimitationButton.Text = "Add limitation\r\non parallelism";
            this.addParallelLimitationButton.UseVisualStyleBackColor = true;
            this.addParallelLimitationButton.Click += new System.EventHandler(this.addParallelLimitation_Click);
            // 
            // deleteLastEdgeButton
            // 
            this.deleteLastEdgeButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.deleteLastEdgeButton.AutoSize = true;
            this.deleteLastEdgeButton.Location = new System.Drawing.Point(12, 369);
            this.deleteLastEdgeButton.Name = "deleteLastEdgeButton";
            this.deleteLastEdgeButton.Size = new System.Drawing.Size(117, 50);
            this.deleteLastEdgeButton.TabIndex = 4;
            this.deleteLastEdgeButton.Text = "Delete last \r\nedge";
            this.deleteLastEdgeButton.UseVisualStyleBackColor = true;
            this.deleteLastEdgeButton.Click += new System.EventHandler(this.deleteLastEdgeButton_Click);
            // 
            // deleteLastPolygonButton
            // 
            this.deleteLastPolygonButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.deleteLastPolygonButton.AutoSize = true;
            this.deleteLastPolygonButton.Location = new System.Drawing.Point(12, 425);
            this.deleteLastPolygonButton.Name = "deleteLastPolygonButton";
            this.deleteLastPolygonButton.Size = new System.Drawing.Size(117, 50);
            this.deleteLastPolygonButton.TabIndex = 3;
            this.deleteLastPolygonButton.Text = "Delete last \r\npolygon";
            this.deleteLastPolygonButton.UseVisualStyleBackColor = true;
            this.deleteLastPolygonButton.Click += new System.EventHandler(this.deleteLastPolygonButton_Click);
            // 
            // modeGroupBox
            // 
            this.modeGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.modeGroupBox.Controls.Add(this.selectRadioButton);
            this.modeGroupBox.Controls.Add(this.removeRadioButton);
            this.modeGroupBox.Controls.Add(this.moveRadioButton);
            this.modeGroupBox.Controls.Add(this.addVertexRadioButton);
            this.modeGroupBox.Controls.Add(this.drawRadioButton);
            this.modeGroupBox.Location = new System.Drawing.Point(6, 26);
            this.modeGroupBox.Name = "modeGroupBox";
            this.modeGroupBox.Size = new System.Drawing.Size(132, 175);
            this.modeGroupBox.TabIndex = 0;
            this.modeGroupBox.TabStop = false;
            this.modeGroupBox.Text = "Mode";
            // 
            // selectRadioButton
            // 
            this.selectRadioButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.selectRadioButton.AutoSize = true;
            this.selectRadioButton.Location = new System.Drawing.Point(6, 84);
            this.selectRadioButton.Name = "selectRadioButton";
            this.selectRadioButton.Size = new System.Drawing.Size(114, 24);
            this.selectRadioButton.TabIndex = 4;
            this.selectRadioButton.Text = "Select edges";
            this.selectRadioButton.UseVisualStyleBackColor = true;
            this.selectRadioButton.CheckedChanged += new System.EventHandler(this.selectRadioButton_CheckedChanged);
            // 
            // removeRadioButton
            // 
            this.removeRadioButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.removeRadioButton.AutoSize = true;
            this.removeRadioButton.Location = new System.Drawing.Point(6, 144);
            this.removeRadioButton.Name = "removeRadioButton";
            this.removeRadioButton.Size = new System.Drawing.Size(84, 24);
            this.removeRadioButton.TabIndex = 3;
            this.removeRadioButton.Text = "Remove";
            this.removeRadioButton.UseVisualStyleBackColor = true;
            this.removeRadioButton.CheckedChanged += new System.EventHandler(this.removeRadioButton_CheckedChanged);
            // 
            // moveRadioButton
            // 
            this.moveRadioButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.moveRadioButton.AutoSize = true;
            this.moveRadioButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.moveRadioButton.Location = new System.Drawing.Point(6, 114);
            this.moveRadioButton.Name = "moveRadioButton";
            this.moveRadioButton.Size = new System.Drawing.Size(67, 24);
            this.moveRadioButton.TabIndex = 2;
            this.moveRadioButton.Text = "Move";
            this.moveRadioButton.UseVisualStyleBackColor = true;
            this.moveRadioButton.CheckedChanged += new System.EventHandler(this.moveRadioButton_CheckedChanged);
            // 
            // addVertexRadioButton
            // 
            this.addVertexRadioButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.addVertexRadioButton.AutoSize = true;
            this.addVertexRadioButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.addVertexRadioButton.Location = new System.Drawing.Point(6, 55);
            this.addVertexRadioButton.Name = "addVertexRadioButton";
            this.addVertexRadioButton.Size = new System.Drawing.Size(112, 24);
            this.addVertexRadioButton.TabIndex = 1;
            this.addVertexRadioButton.Text = "Add vertices";
            this.addVertexRadioButton.UseVisualStyleBackColor = true;
            this.addVertexRadioButton.CheckedChanged += new System.EventHandler(this.addVertexRadioButton_CheckedChanged);
            // 
            // drawRadioButton
            // 
            this.drawRadioButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.drawRadioButton.AutoSize = true;
            this.drawRadioButton.Checked = true;
            this.drawRadioButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.drawRadioButton.Location = new System.Drawing.Point(6, 26);
            this.drawRadioButton.Name = "drawRadioButton";
            this.drawRadioButton.Size = new System.Drawing.Size(65, 24);
            this.drawRadioButton.TabIndex = 0;
            this.drawRadioButton.TabStop = true;
            this.drawRadioButton.Text = "Draw";
            this.drawRadioButton.UseVisualStyleBackColor = true;
            this.drawRadioButton.CheckedChanged += new System.EventHandler(this.drawRadioButton_CheckedChanged);
            // 
            // clearButton
            // 
            this.clearButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.clearButton.AutoSize = true;
            this.clearButton.Location = new System.Drawing.Point(12, 481);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(117, 50);
            this.clearButton.TabIndex = 2;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // BresenhamAlgorithmCheckBox
            // 
            this.BresenhamAlgorithmCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.BresenhamAlgorithmCheckBox.AutoSize = true;
            this.BresenhamAlgorithmCheckBox.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BresenhamAlgorithmCheckBox.Location = new System.Drawing.Point(16, 207);
            this.BresenhamAlgorithmCheckBox.Name = "BresenhamAlgorithmCheckBox";
            this.BresenhamAlgorithmCheckBox.Size = new System.Drawing.Size(108, 44);
            this.BresenhamAlgorithmCheckBox.TabIndex = 1;
            this.BresenhamAlgorithmCheckBox.Text = "Bresenham \r\nalgorithm";
            this.BresenhamAlgorithmCheckBox.UseVisualStyleBackColor = false;
            this.BresenhamAlgorithmCheckBox.CheckedChanged += new System.EventHandler(this.BresenhamAlgorithmCheckBox_CheckedChanged);
            // 
            // panel
            // 
            this.panel.AutoScroll = true;
            this.panel.Controls.Add(this.pictureBox);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(150, 0);
            this.panel.Margin = new System.Windows.Forms.Padding(0);
            this.panel.Name = "panel";
            this.tableLayoutPanel.SetRowSpan(this.panel, 2);
            this.panel.Size = new System.Drawing.Size(827, 550);
            this.panel.TabIndex = 1;
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.pictureBox.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pictureBox.Location = new System.Drawing.Point(1, 2);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(100, 72);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 553);
            this.Controls.Add(this.tableLayoutPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "MainWindow";
            this.Text = "Polygon Editor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.Menu.ResumeLayout(false);
            this.Menu.PerformLayout();
            this.modeGroupBox.ResumeLayout(false);
            this.modeGroupBox.PerformLayout();
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.GroupBox Menu;
        private System.Windows.Forms.CheckBox BresenhamAlgorithmCheckBox;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.GroupBox modeGroupBox;
        private System.Windows.Forms.RadioButton removeRadioButton;
        private System.Windows.Forms.RadioButton moveRadioButton;
        private System.Windows.Forms.RadioButton addVertexRadioButton;
        private System.Windows.Forms.RadioButton drawRadioButton;
        private System.Windows.Forms.Button deleteLastPolygonButton;
        private System.Windows.Forms.Button deleteLastEdgeButton;
        private System.Windows.Forms.Button addLengthLimitationButton;
        private System.Windows.Forms.Button addParallelLimitationButton;
        private System.Windows.Forms.RadioButton selectRadioButton;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.PictureBox pictureBox;
    }
}

