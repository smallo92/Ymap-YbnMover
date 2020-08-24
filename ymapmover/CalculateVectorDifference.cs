using SharpDX;
using System;
using System.Windows.Forms;
using CodeWalker.GameFiles;
using System.IO;

namespace ymapmover
{
    public partial class CalculateVectorDifference : Form
    {
        private MainForm mainForm;
        private Vector3 offsetVec = new Vector3();

        private void CalculateVectorDifference_Load(object sender, EventArgs e)
        {
            // Nothing
        }

        public CalculateVectorDifference(MainForm ParentForm)
        {
            InitializeComponent();
            mainForm = ParentForm;
        }

        private void CalculateButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(vector1.Text) || !string.IsNullOrEmpty(vector2.Text))
            {
                try
                {
                    string[] oldLocSplit = vector1.Text.Split(',');
                    string[] NewLocSplit = vector2.Text.Split(',');

                    offsetVec = new Vector3(VecDiff(float.Parse(NewLocSplit[0]), float.Parse(oldLocSplit[0])), VecDiff(float.Parse(NewLocSplit[1]), float.Parse(oldLocSplit[1])), VecDiff(float.Parse(NewLocSplit[2]), float.Parse(oldLocSplit[2])));
                    newOffset.Text = offsetVec.X.ToString() + ", " + offsetVec.Y.ToString() + ", " + offsetVec.Z.ToString();
                }
                catch (Exception)
                {
                    MessageBox.Show("These values don't appear to be in the correct format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void InputButton_Click(object sender, EventArgs e)
        {
            mainForm.xMoveBox = offsetVec.X.ToString();
            mainForm.yMoveBox = offsetVec.Y.ToString();
            mainForm.zMoveBox = offsetVec.Z.ToString();
            Close();
        }

        private float VecDiff(float val1, float val2)
        {
            return (float)Math.Sqrt(Math.Pow(val1 - val2, 2));
        }

        private void InvertButton_Click(object sender, EventArgs e)
        {
            string oldText = vector1.Text;

            vector1.Text = vector2.Text;
            vector2.Text = oldText;
        }

        private void CentreButton_Click(object sender, EventArgs e)
        {
            string filename = mainForm.CurrentListBox;

            if (!string.IsNullOrEmpty(filename)) {
                YmapFile ymap = new YmapFile();
                ymap.Load(File.ReadAllBytes(filename));

                Vector3 extentMin = ymap.CMapData.entitiesExtentsMin;
                Vector3 extentMax = ymap.CMapData.entitiesExtentsMax;

                Vector3 centrepoint = new Vector3((extentMin.X + extentMax.X) / 2, (extentMin.Y + extentMax.Y) / 2, (extentMin.Z + extentMax.Z) / 2);
                vector1.Text = centrepoint.X + ", " + centrepoint.Y + ", " + centrepoint.Z;
            }
            else
            {
                MessageBox.Show("You don't seem to have a ymap selected on the Main Form.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
