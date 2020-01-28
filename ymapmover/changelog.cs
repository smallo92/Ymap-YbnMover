using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace ymapmover
{
    public partial class changelog : Form
    {
        public changelog()
        {
            InitializeComponent();
        }

        private void changelog_Load(object sender, EventArgs e)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string VersionNum = fvi.FileVersion.ToString();
            mainTextbox.Rtf = Properties.Resources.fullchangelog;
            groupBox1.Text = "Changelog - Current Version: " + VersionNum;
        }
    }
}
