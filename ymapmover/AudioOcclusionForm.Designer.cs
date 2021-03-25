namespace ymapmover
{
    partial class AudioOcclusionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AudioOcclusionForm));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.addYmapButton = new System.Windows.Forms.Button();
            this.ymapLocationText = new System.Windows.Forms.TextBox();
            this.generatedHashText = new System.Windows.Forms.TextBox();
            this.outputLabel = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "*";
            this.openFileDialog1.Filter = "YMAP Files|*.ymap";
            // 
            // addYmapButton
            // 
            this.addYmapButton.Location = new System.Drawing.Point(12, 24);
            this.addYmapButton.Name = "addYmapButton";
            this.addYmapButton.Size = new System.Drawing.Size(75, 20);
            this.addYmapButton.TabIndex = 0;
            this.addYmapButton.Text = "Add YMAP";
            this.addYmapButton.UseVisualStyleBackColor = true;
            this.addYmapButton.Click += new System.EventHandler(this.addYmapButton_Click);
            // 
            // ymapLocationText
            // 
            this.ymapLocationText.BackColor = System.Drawing.SystemColors.Window;
            this.ymapLocationText.Location = new System.Drawing.Point(93, 24);
            this.ymapLocationText.Name = "ymapLocationText";
            this.ymapLocationText.ReadOnly = true;
            this.ymapLocationText.Size = new System.Drawing.Size(495, 20);
            this.ymapLocationText.TabIndex = 1;
            // 
            // generatedHashText
            // 
            this.generatedHashText.BackColor = System.Drawing.SystemColors.Window;
            this.generatedHashText.Location = new System.Drawing.Point(93, 50);
            this.generatedHashText.Name = "generatedHashText";
            this.generatedHashText.ReadOnly = true;
            this.generatedHashText.Size = new System.Drawing.Size(147, 20);
            this.generatedHashText.TabIndex = 2;
            // 
            // outputLabel
            // 
            this.outputLabel.AutoSize = true;
            this.outputLabel.Location = new System.Drawing.Point(17, 53);
            this.outputLabel.Name = "outputLabel";
            this.outputLabel.Size = new System.Drawing.Size(70, 13);
            this.outputLabel.TabIndex = 3;
            this.outputLabel.Text = "Output Hash:";
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(432, 48);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 22);
            this.startButton.TabIndex = 4;
            this.startButton.Text = "Generate";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(513, 48);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(75, 22);
            this.clearButton.TabIndex = 5;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Import the interior placement ymap";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.DefaultExt = "*";
            this.openFileDialog2.Filter = "YNV Files|*.ynv";
            // 
            // AudioOcclusionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 77);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.outputLabel);
            this.Controls.Add(this.generatedHashText);
            this.Controls.Add(this.ymapLocationText);
            this.Controls.Add(this.addYmapButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AudioOcclusionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Audio Occlusion Hash Generator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button addYmapButton;
        private System.Windows.Forms.TextBox ymapLocationText;
        private System.Windows.Forms.TextBox generatedHashText;
        private System.Windows.Forms.Label outputLabel;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
    }
}