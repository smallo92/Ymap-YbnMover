using System;
using System.Globalization;
using System.Windows.Forms;

namespace ymapmover
{
    public partial class JsonTool : Form
    {
        public CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
        public JsonTool()
        {
            ci.NumberFormat.CurrencyDecimalSeparator = ".";
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
