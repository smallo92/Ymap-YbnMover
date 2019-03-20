namespace ymapmover
{
    partial class CalculateVectorDifference
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CalculateVectorDifference));
            this.calculateButton = new System.Windows.Forms.Button();
            this.vector1 = new System.Windows.Forms.TextBox();
            this.vector2 = new System.Windows.Forms.TextBox();
            this.newOffset = new System.Windows.Forms.TextBox();
            this.OGLocLabel = new System.Windows.Forms.Label();
            this.newLocLabel = new System.Windows.Forms.Label();
            this.CalculatedLabel = new System.Windows.Forms.Label();
            this.InputButton = new System.Windows.Forms.Button();
            this.InstructionsLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // calculateButton
            // 
            this.calculateButton.Location = new System.Drawing.Point(83, 100);
            this.calculateButton.Name = "calculateButton";
            this.calculateButton.Size = new System.Drawing.Size(75, 23);
            this.calculateButton.TabIndex = 0;
            this.calculateButton.Text = "Calculate";
            this.calculateButton.UseVisualStyleBackColor = true;
            this.calculateButton.Click += new System.EventHandler(this.calculateButton_Click);
            // 
            // vector1
            // 
            this.vector1.Location = new System.Drawing.Point(104, 22);
            this.vector1.Name = "vector1";
            this.vector1.Size = new System.Drawing.Size(208, 20);
            this.vector1.TabIndex = 1;
            // 
            // vector2
            // 
            this.vector2.Location = new System.Drawing.Point(104, 48);
            this.vector2.Name = "vector2";
            this.vector2.Size = new System.Drawing.Size(208, 20);
            this.vector2.TabIndex = 2;
            // 
            // newOffset
            // 
            this.newOffset.Location = new System.Drawing.Point(104, 74);
            this.newOffset.Name = "newOffset";
            this.newOffset.ReadOnly = true;
            this.newOffset.Size = new System.Drawing.Size(208, 20);
            this.newOffset.TabIndex = 3;
            // 
            // OGLocLabel
            // 
            this.OGLocLabel.AutoSize = true;
            this.OGLocLabel.Location = new System.Drawing.Point(12, 25);
            this.OGLocLabel.Name = "OGLocLabel";
            this.OGLocLabel.Size = new System.Drawing.Size(89, 13);
            this.OGLocLabel.TabIndex = 4;
            this.OGLocLabel.Text = "Original Location:";
            // 
            // newLocLabel
            // 
            this.newLocLabel.AutoSize = true;
            this.newLocLabel.Location = new System.Drawing.Point(22, 51);
            this.newLocLabel.Name = "newLocLabel";
            this.newLocLabel.Size = new System.Drawing.Size(76, 13);
            this.newLocLabel.TabIndex = 5;
            this.newLocLabel.Text = "New Location:";
            // 
            // CalculatedLabel
            // 
            this.CalculatedLabel.AutoSize = true;
            this.CalculatedLabel.Location = new System.Drawing.Point(7, 77);
            this.CalculatedLabel.Name = "CalculatedLabel";
            this.CalculatedLabel.Size = new System.Drawing.Size(91, 13);
            this.CalculatedLabel.TabIndex = 6;
            this.CalculatedLabel.Text = "Calculated Offset:";
            // 
            // InputButton
            // 
            this.InputButton.Location = new System.Drawing.Point(164, 100);
            this.InputButton.Name = "InputButton";
            this.InputButton.Size = new System.Drawing.Size(75, 23);
            this.InputButton.TabIndex = 7;
            this.InputButton.Text = "Input Offset";
            this.InputButton.UseVisualStyleBackColor = true;
            this.InputButton.Click += new System.EventHandler(this.InputButton_Click);
            // 
            // InstructionsLabel
            // 
            this.InstructionsLabel.AutoSize = true;
            this.InstructionsLabel.Location = new System.Drawing.Point(231, 6);
            this.InstructionsLabel.Name = "InstructionsLabel";
            this.InstructionsLabel.Size = new System.Drawing.Size(81, 13);
            this.InstructionsLabel.TabIndex = 8;
            this.InstructionsLabel.Text = "Input as X, Y, Z";
            // 
            // CalculateVectorDifference
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 130);
            this.Controls.Add(this.InstructionsLabel);
            this.Controls.Add(this.InputButton);
            this.Controls.Add(this.CalculatedLabel);
            this.Controls.Add(this.newLocLabel);
            this.Controls.Add(this.OGLocLabel);
            this.Controls.Add(this.newOffset);
            this.Controls.Add(this.vector2);
            this.Controls.Add(this.vector1);
            this.Controls.Add(this.calculateButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CalculateVectorDifference";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Calculate Vector Difference";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button calculateButton;
        private System.Windows.Forms.TextBox vector1;
        private System.Windows.Forms.TextBox vector2;
        private System.Windows.Forms.TextBox newOffset;
        private System.Windows.Forms.Label OGLocLabel;
        private System.Windows.Forms.Label newLocLabel;
        private System.Windows.Forms.Label CalculatedLabel;
        private System.Windows.Forms.Button InputButton;
        private System.Windows.Forms.Label InstructionsLabel;
    }
}