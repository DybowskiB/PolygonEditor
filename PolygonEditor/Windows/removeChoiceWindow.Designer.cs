
namespace PolygonEditor
{
    partial class RemoveChoiceWindow
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
            this.hintLabel = new System.Windows.Forms.Label();
            this.whatRemoveComboBox = new System.Windows.Forms.ComboBox();
            this.exitButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // hintLabel
            // 
            this.hintLabel.AutoSize = true;
            this.hintLabel.Location = new System.Drawing.Point(30, 33);
            this.hintLabel.Name = "hintLabel";
            this.hintLabel.Size = new System.Drawing.Size(236, 20);
            this.hintLabel.TabIndex = 0;
            this.hintLabel.Text = "Choose, what you want to remove.";
            // 
            // whatRemoveComboBox
            // 
            this.whatRemoveComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.whatRemoveComboBox.FormattingEnabled = true;
            this.whatRemoveComboBox.Location = new System.Drawing.Point(72, 70);
            this.whatRemoveComboBox.Name = "whatRemoveComboBox";
            this.whatRemoveComboBox.Size = new System.Drawing.Size(237, 28);
            this.whatRemoveComboBox.TabIndex = 1;
            this.whatRemoveComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.whatRemoveComboBox_KeyPress);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(60, 144);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(120, 35);
            this.exitButton.TabIndex = 2;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(200, 144);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(120, 35);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(-16, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(453, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "__________________________________________________________________________";
            // 
            // RemoveChoiceWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 199);
            this.ControlBox = false;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.whatRemoveComboBox);
            this.Controls.Add(this.hintLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximumSize = new System.Drawing.Size(400, 250);
            this.MinimumSize = new System.Drawing.Size(400, 250);
            this.Name = "RemoveChoiceWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Remove";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label hintLabel;
        private System.Windows.Forms.ComboBox whatRemoveComboBox;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label label3;
    }
}