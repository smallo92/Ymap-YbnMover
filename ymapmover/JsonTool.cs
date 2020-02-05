using System;
using System.Windows.Forms;

namespace ymapmover
{
    public partial class JsonTool : Form
    {
        public JsonTool()
        {
            InitializeComponent();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpForm helpForm = new HelpForm();
            helpForm.ShowDialog();
        }

        private void JsonTool_Load(object sender, EventArgs e)
        {

        }

        private void startButton_Click(object sender, EventArgs e)
        {

        }
    }
}
