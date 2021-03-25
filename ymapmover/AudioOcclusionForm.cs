using System;
using System.IO;
using System.Windows.Forms;
using CodeWalker.GameFiles;

namespace ymapmover
{
    public partial class AudioOcclusionForm : Form
    {
        public AudioOcclusionForm()
        {
            InitializeComponent();
        }

        private void addYmapButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                ymapLocationText.Text = openFileDialog1.FileName;
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            generatedHashText.Clear();
            ymapLocationText.Clear();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (ymapLocationText.Text != null)
            {
                string filename = ymapLocationText.Text;
                YmapFile ymap = new YmapFile();
                ymap.Load(File.ReadAllBytes(filename));

                if (ymap.AllEntities != null)
                {
                    foreach (YmapEntityDef yEnts in ymap.AllEntities) { 
                        if (yEnts.IsMlo) {
                            long outputNumber = (long.Parse(yEnts.Name) ^ (long)Math.Floor(yEnts.Position.X * 100) ^ (long)Math.Floor(yEnts.Position.Y * 100) ^ (long)Math.Floor(yEnts.Position.Z * 100)) & 0xffffffff;
                            generatedHashText.Text = outputNumber.ToString();
                        }
                    }
                }
            }
        }
    }
}
