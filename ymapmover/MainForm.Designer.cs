namespace ymapmover
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yMAPsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yBNsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAllItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearSelectedItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.clearAllYBNsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAllYMAPsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CurrentList = new System.Windows.Forms.ListBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.FilesAddedLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.StripStatusFiller = new System.Windows.Forms.ToolStripStatusLabel();
            this.TimeLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.zMove = new System.Windows.Forms.TextBox();
            this.yMove = new System.Windows.Forms.TextBox();
            this.xMove = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.outdatedLabel = new System.Windows.Forms.Label();
            this.legacyToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jSONToYMAPAndYTYPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.howToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.legacyToolsToolStripMenuItem});
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
            this.addFolderToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.yFilesToolStripMenuItem,
            this.yMAPsToolStripMenuItem,
            this.yBNsToolStripMenuItem});
            this.addFolderToolStripMenuItem.Name = "addFolderToolStripMenuItem";
            this.addFolderToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.addFolderToolStripMenuItem.Text = "Add Folder";
            // 
            // yFilesToolStripMenuItem
            // 
            this.yFilesToolStripMenuItem.Name = "yFilesToolStripMenuItem";
            this.yFilesToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.yFilesToolStripMenuItem.Text = "All Files (.ymap and .ybn)";
            this.yFilesToolStripMenuItem.Click += new System.EventHandler(this.yFilesToolStripMenuItem_Click);
            // 
            // yMAPsToolStripMenuItem
            // 
            this.yMAPsToolStripMenuItem.Name = "yMAPsToolStripMenuItem";
            this.yMAPsToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.yMAPsToolStripMenuItem.Text = "YMAP Files";
            this.yMAPsToolStripMenuItem.Click += new System.EventHandler(this.yMAPsToolStripMenuItem_Click);
            // 
            // yBNsToolStripMenuItem
            // 
            this.yBNsToolStripMenuItem.Name = "yBNsToolStripMenuItem";
            this.yBNsToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.yBNsToolStripMenuItem.Text = "YBN Files";
            this.yBNsToolStripMenuItem.Click += new System.EventHandler(this.yBNsToolStripMenuItem_Click);
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
            this.clearAllYBNsToolStripMenuItem,
            this.clearAllYMAPsToolStripMenuItem});
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
            // clearAllYBNsToolStripMenuItem
            // 
            this.clearAllYBNsToolStripMenuItem.Name = "clearAllYBNsToolStripMenuItem";
            this.clearAllYBNsToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.clearAllYBNsToolStripMenuItem.Text = "Clear All YBNs";
            this.clearAllYBNsToolStripMenuItem.Click += new System.EventHandler(this.clearAllYBNsToolStripMenuItem_Click);
            // 
            // clearAllYMAPsToolStripMenuItem
            // 
            this.clearAllYMAPsToolStripMenuItem.Name = "clearAllYMAPsToolStripMenuItem";
            this.clearAllYMAPsToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.clearAllYMAPsToolStripMenuItem.Text = "Clear All YMAPs";
            this.clearAllYMAPsToolStripMenuItem.Click += new System.EventHandler(this.clearAllYMAPsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.howToToolStripMenuItem,
            this.toolStripSeparator3,
            this.helpToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // CurrentList
            // 
            this.CurrentList.FormattingEnabled = true;
            this.CurrentList.Location = new System.Drawing.Point(12, 27);
            this.CurrentList.Name = "CurrentList";
            this.CurrentList.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.CurrentList.Size = new System.Drawing.Size(776, 264);
            this.CurrentList.TabIndex = 1;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "*";
            this.openFileDialog1.Filter = "YMAP Files|*.ymap|YBN Files|*.ybn";
            this.openFileDialog1.Multiselect = true;
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
            this.statusStrip1.TabIndex = 2;
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
            // zMove
            // 
            this.zMove.Location = new System.Drawing.Point(715, 296);
            this.zMove.Name = "zMove";
            this.zMove.Size = new System.Drawing.Size(73, 20);
            this.zMove.TabIndex = 6;
            this.zMove.Text = "0.0";
            this.zMove.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.zMove.TextChanged += new System.EventHandler(this.zMove_TextChanged);
            // 
            // yMove
            // 
            this.yMove.Location = new System.Drawing.Point(636, 296);
            this.yMove.Name = "yMove";
            this.yMove.Size = new System.Drawing.Size(73, 20);
            this.yMove.TabIndex = 5;
            this.yMove.Text = "0.0";
            this.yMove.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.yMove.TextChanged += new System.EventHandler(this.yMove_TextChanged);
            // 
            // xMove
            // 
            this.xMove.Location = new System.Drawing.Point(557, 296);
            this.xMove.Name = "xMove";
            this.xMove.Size = new System.Drawing.Size(73, 20);
            this.xMove.TabIndex = 4;
            this.xMove.Text = "0.0";
            this.xMove.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.xMove.TextChanged += new System.EventHandler(this.xMove_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(445, 296);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Offset X, Y, Z:";
            // 
            // startButton
            // 
            this.startButton.Enabled = false;
            this.startButton.Location = new System.Drawing.Point(12, 294);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(73, 23);
            this.startButton.TabIndex = 2;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Enabled = false;
            this.cancelButton.Location = new System.Drawing.Point(91, 294);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(73, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // outdatedLabel
            // 
            this.outdatedLabel.AutoSize = true;
            this.outdatedLabel.BackColor = System.Drawing.Color.Transparent;
            this.outdatedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outdatedLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.outdatedLabel.Location = new System.Drawing.Point(170, 296);
            this.outdatedLabel.Name = "outdatedLabel";
            this.outdatedLabel.Size = new System.Drawing.Size(0, 20);
            this.outdatedLabel.TabIndex = 9;
            // 
            // legacyToolsToolStripMenuItem
            // 
            this.legacyToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.jSONToYMAPAndYTYPToolStripMenuItem});
            this.legacyToolsToolStripMenuItem.Name = "legacyToolsToolStripMenuItem";
            this.legacyToolsToolStripMenuItem.Size = new System.Drawing.Size(87, 20);
            this.legacyToolsToolStripMenuItem.Text = "Legacy Tools";
            this.legacyToolsToolStripMenuItem.Visible = false;
            // 
            // jSONToYMAPAndYTYPToolStripMenuItem
            // 
            this.jSONToYMAPAndYTYPToolStripMenuItem.Name = "jSONToYMAPAndYTYPToolStripMenuItem";
            this.jSONToYMAPAndYTYPToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.jSONToYMAPAndYTYPToolStripMenuItem.Text = "JSON to YMAP and YTYP";
            // 
            // howToToolStripMenuItem
            // 
            this.howToToolStripMenuItem.Enabled = false;
            this.howToToolStripMenuItem.Name = "howToToolStripMenuItem";
            this.howToToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.howToToolStripMenuItem.Text = "How to use";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(177, 6);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 343);
            this.Controls.Add(this.outdatedLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.xMove);
            this.Controls.Add(this.yMove);
            this.Controls.Add(this.zMove);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.CurrentList);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "YMAP & YBN Mover";
            this.Load += new System.EventHandler(this.MainForm_Load);
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
        private System.Windows.Forms.ToolStripMenuItem addItemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearAllItemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearSelectedItemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem clearAllYBNsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearAllYMAPsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ListBox CurrentList;
        private System.Windows.Forms.ToolStripMenuItem addFolderToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel FilesAddedLabel;
        private System.Windows.Forms.TextBox zMove;
        private System.Windows.Forms.TextBox yMove;
        private System.Windows.Forms.TextBox xMove;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.ToolStripStatusLabel StripStatusFiller;
        private System.Windows.Forms.ToolStripStatusLabel TimeLabel;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ToolStripMenuItem yMAPsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yBNsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yFilesToolStripMenuItem;
        private System.Windows.Forms.Label outdatedLabel;
        private System.Windows.Forms.ToolStripMenuItem legacyToolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jSONToYMAPAndYTYPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem howToToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}

