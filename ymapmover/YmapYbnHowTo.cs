using System;
using System.Windows.Forms;

namespace ymapmover
{
    public partial class YmapYbnHowTo : Form
    {
        public YmapYbnHowTo()
        {
            InitializeComponent();
        }

        private void YmapYbnHowTo_Load(object sender, EventArgs e)
        {
            richTextBox1.ScrollBars = RichTextBoxScrollBars.None;
        }
    }
}
