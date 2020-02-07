namespace ymapmover
{
    partial class EdgeFixer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EdgeFixer));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAllItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearSelectedItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.clearAllRPFsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAllYBNsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAllYDRsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAllYDDsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAllYFTsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CurrentList = new System.Windows.Forms.ListBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.FilesAddedLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.StripStatusFiller = new System.Windows.Forms.ToolStripStatusLabel();
            this.TimeLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.testLabel = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFolderToolStripMenuItem,
            this.addItemsToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // addFolderToolStripMenuItem
            // 
            this.addFolderToolStripMenuItem.Name = "addFolderToolStripMenuItem";
            this.addFolderToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.addFolderToolStripMenuItem.Text = "Add Folder";
            this.addFolderToolStripMenuItem.Click += new System.EventHandler(this.addFolderToolStripMenuItem_Click);
            // 
            // addItemsToolStripMenuItem
            // 
            this.addItemsToolStripMenuItem.Name = "addItemsToolStripMenuItem";
            this.addItemsToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.addItemsToolStripMenuItem.Text = "Add Item(s)";
            this.addItemsToolStripMenuItem.Click += new System.EventHandler(this.addItemsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(133, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearAllItemsToolStripMenuItem,
            this.clearSelectedItemsToolStripMenuItem,
            this.toolStripSeparator2,
            this.clearAllRPFsToolStripMenuItem,
            this.clearAllYBNsToolStripMenuItem,
            this.clearAllYDRsToolStripMenuItem,
            this.clearAllYDDsToolStripMenuItem,
            this.clearAllYFTsToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // clearAllItemsToolStripMenuItem
            // 
            this.clearAllItemsToolStripMenuItem.Name = "clearAllItemsToolStripMenuItem";
            this.clearAllItemsToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.clearAllItemsToolStripMenuItem.Text = "Clear All Items";
            this.clearAllItemsToolStripMenuItem.Click += new System.EventHandler(this.clearAllItemsToolStripMenuItem_Click);
            // 
            // clearSelectedItemsToolStripMenuItem
            // 
            this.clearSelectedItemsToolStripMenuItem.Name = "clearSelectedItemsToolStripMenuItem";
            this.clearSelectedItemsToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.clearSelectedItemsToolStripMenuItem.Text = "Clear Selected Item(s)";
            this.clearSelectedItemsToolStripMenuItem.Click += new System.EventHandler(this.clearSelectedItemsToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(185, 6);
            // 
            // clearAllRPFsToolStripMenuItem
            // 
            this.clearAllRPFsToolStripMenuItem.Name = "clearAllRPFsToolStripMenuItem";
            this.clearAllRPFsToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.clearAllRPFsToolStripMenuItem.Text = "Clear All RPFs";
            this.clearAllRPFsToolStripMenuItem.Click += new System.EventHandler(this.clearAllRPFsToolStripMenuItem_Click);
            // 
            // clearAllYBNsToolStripMenuItem
            // 
            this.clearAllYBNsToolStripMenuItem.Name = "clearAllYBNsToolStripMenuItem";
            this.clearAllYBNsToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.clearAllYBNsToolStripMenuItem.Text = "Clear All YBNs";
            this.clearAllYBNsToolStripMenuItem.Click += new System.EventHandler(this.clearAllYBNsToolStripMenuItem_Click);
            // 
            // clearAllYDRsToolStripMenuItem
            // 
            this.clearAllYDRsToolStripMenuItem.Name = "clearAllYDRsToolStripMenuItem";
            this.clearAllYDRsToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.clearAllYDRsToolStripMenuItem.Text = "Clear All YDRs";
            this.clearAllYDRsToolStripMenuItem.Click += new System.EventHandler(this.clearAllYDRsToolStripMenuItem_Click);
            // 
            // clearAllYDDsToolStripMenuItem
            // 
            this.clearAllYDDsToolStripMenuItem.Name = "clearAllYDDsToolStripMenuItem";
            this.clearAllYDDsToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.clearAllYDDsToolStripMenuItem.Text = "Clear All YDDs";
            this.clearAllYDDsToolStripMenuItem.Click += new System.EventHandler(this.clearAllYDDsToolStripMenuItem_Click_1);
            // 
            // clearAllYFTsToolStripMenuItem
            // 
            this.clearAllYFTsToolStripMenuItem.Name = "clearAllYFTsToolStripMenuItem";
            this.clearAllYFTsToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.clearAllYFTsToolStripMenuItem.Text = "Clear All YFTs";
            this.clearAllYFTsToolStripMenuItem.Click += new System.EventHandler(this.clearAllYFTsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // CurrentList
            // 
            this.CurrentList.FormattingEnabled = true;
            this.CurrentList.Location = new System.Drawing.Point(12, 27);
            this.CurrentList.Name = "CurrentList";
            this.CurrentList.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.CurrentList.Size = new System.Drawing.Size(776, 264);
            this.CurrentList.TabIndex = 2;
            // 
            // cancelButton
            // 
            this.cancelButton.Enabled = false;
            this.cancelButton.Location = new System.Drawing.Point(91, 294);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(73, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // startButton
            // 
            this.startButton.Enabled = false;
            this.startButton.Location = new System.Drawing.Point(12, 294);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(73, 23);
            this.startButton.TabIndex = 4;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FilesAddedLabel,
            this.StripStatusFiller,
            this.TimeLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 321);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // FilesAddedLabel
            // 
            this.FilesAddedLabel.Name = "FilesAddedLabel";
            this.FilesAddedLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.FilesAddedLabel.Size = new System.Drawing.Size(95, 17);
            this.FilesAddedLabel.Text = "No File(s) Added";
            // 
            // StripStatusFiller
            // 
            this.StripStatusFiller.Name = "StripStatusFiller";
            this.StripStatusFiller.Size = new System.Drawing.Size(690, 17);
            this.StripStatusFiller.Spring = true;
            // 
            // TimeLabel
            // 
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "*";
            this.openFileDialog1.Filter = "RPF Files|*.rpf|YBN Files|*.ybn|YDR Files|*.ydr|YDD Files|*.ydd|YFT Files|*.yft";
            this.openFileDialog1.Multiselect = true;
            // 
            // testLabel
            // 
            this.testLabel.AutoSize = true;
            this.testLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.testLabel.Location = new System.Drawing.Point(528, 27);
            this.testLabel.Name = "testLabel";
            this.testLabel.Size = new System.Drawing.Size(0, 20);
            this.testLabel.TabIndex = 7;
            // 
            // EdgeFixer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 343);
            this.Controls.Add(this.testLabel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.CurrentList);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "EdgeFixer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Poly Edge Fixer";
            this.Load += new System.EventHandler(this.EdgeFixer_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ListBox CurrentList;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel FilesAddedLabel;
        private System.Windows.Forms.ToolStripStatusLabel StripStatusFiller;
        private System.Windows.Forms.ToolStripStatusLabel TimeLabel;
        private System.Windows.Forms.ToolStripMenuItem addItemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFolderToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem clearAllItemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearSelectedItemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem clearAllYDRsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearAllYBNsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearAllYFTsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearAllYDDsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearAllRPFsToolStripMenuItem;
        private System.Windows.Forms.Label testLabel;
    }
}