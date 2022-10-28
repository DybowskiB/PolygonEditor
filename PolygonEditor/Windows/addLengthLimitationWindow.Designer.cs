
namespace PolygonEditor
{
    partial class AddLengthLimitationWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.okButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.firstEdgeLengthTextBox = new System.Windows.Forms.TextBox();
            this.firstEdgeLengthLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.secondEdgeLengthLabel = new System.Windows.Forms.Label();
            this.secondEdgeLengthTextBox = new System.Windows.Forms.TextBox();
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(200, 144);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(120, 35);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(60, 144);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(120, 35);
            this.exitButton.TabIndex = 1;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // firstEdgeLengthTextBox
            // 
            this.firstEdgeLengthTextBox.Location = new System.Drawing.Point(200, 33);
            this.firstEdgeLengthTextBox.Name = "firstEdgeLengthTextBox";
            this.firstEdgeLengthTextBox.Size = new System.Drawing.Size(129, 27);
            this.firstEdgeLengthTextBox.TabIndex = 2;
            this.firstEdgeLengthTextBox.TextChanged += new System.EventHandler(this.firstEdgeLengthTextBox_TextChanged);
            this.firstEdgeLengthTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.firstEdgeLengthTextBox_KeyPress);
            // 
            // firstEdgeLengthLabel
            // 
            this.firstEdgeLengthLabel.AutoSize = true;
            this.firstEdgeLengthLabel.Location = new System.Drawing.Point(37, 36);
            this.firstEdgeLengthLabel.Name = "firstEdgeLengthLabel";
            this.firstEdgeLengthLabel.Size = new System.Drawing.Size(123, 20);
            this.firstEdgeLengthLabel.TabIndex = 4;
            this.firstEdgeLengthLabel.Text = "First edge length:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(-16, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(453, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "__________________________________________________________________________";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // secondEdgeLengthLabel
            // 
            this.secondEdgeLengthLabel.AutoSize = true;
            this.secondEdgeLengthLabel.Location = new System.Drawing.Point(37, 70);
            this.secondEdgeLengthLabel.Name = "secondEdgeLengthLabel";
            this.secondEdgeLengthLabel.Size = new System.Drawing.Size(145, 20);
            this.secondEdgeLengthLabel.TabIndex = 7;
            this.secondEdgeLengthLabel.Text = "Second edge length:";
            // 
            // secondEdgeLengthTextBox
            // 
            this.secondEdgeLengthTextBox.Location = new System.Drawing.Point(200, 67);
            this.secondEdgeLengthTextBox.Name = "secondEdgeLengthTextBox";
            this.secondEdgeLengthTextBox.Size = new System.Drawing.Size(129, 27);
            this.secondEdgeLengthTextBox.TabIndex = 8;
            this.secondEdgeLengthTextBox.TextChanged += new System.EventHandler(this.secondEdgeLengthTextBox_TextChanged);
            this.secondEdgeLengthTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.secondEdgeLengthTextBox_KeyPress);
            // 
            // errorProvider2
            // 
            this.errorProvider2.ContainerControl = this;
            // 
            // AddLengthLimitationWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 199);
            this.ControlBox = false;
            this.Controls.Add(this.secondEdgeLengthTextBox);
            this.Controls.Add(this.secondEdgeLengthLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.firstEdgeLengthLabel);
            this.Controls.Add(this.firstEdgeLengthTextBox);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximumSize = new System.Drawing.Size(400, 250);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 250);
            this.Name = "AddLengthLimitationWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add length limitation";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.TextBox firstEdgeLengthTextBox;
        private System.Windows.Forms.Label firstEdgeLengthLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox secondEdgeLengthTextBox;
        private System.Windows.Forms.Label secondEdgeLengthLabel;
        private System.Windows.Forms.ErrorProvider errorProvider2;
    }
}