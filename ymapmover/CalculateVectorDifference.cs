using SharpDX;
using System;
using System.Windows.Forms;

namespace ymapmover
{
    public partial class CalculateVectorDifference : Form
    {
        private MainForm mainForm;
        private Vector3 OffsetVec = new Vector3();

        public CalculateVectorDifference(MainForm ParentForm)
        {
            InitializeComponent();
            mainForm = ParentForm;
        }

        private void calculateButton_Click(object sender, EventArgs e)
        {
            if (vector1.Text != "" || vector2.Text != "")
            {
                string[] oldLocSplit = vector1.Text.Split(',');
                string[] newLocSplit = vector2.Text.Split(',');
                Vector3 oldVec = new Vector3(float.Parse(oldLocSplit[0]), float.Parse(oldLocSplit[1]), float.Parse(oldLocSplit[2]));
                Vector3 newVec = new Vector3(float.Parse(newLocSplit[0]), float.Parse(newLocSplit[1]), float.Parse(newLocSplit[2]));

                OffsetVec = new Vector3(VecDiff(newVec.X, oldVec.X), VecDiff(newVec.Y, oldVec.Y), VecDiff(newVec.Z, oldVec.Z));
                newOffset.Text = OffsetVec.X.ToString() + ", " + OffsetVec.Y.ToString() + ", " + OffsetVec.Z.ToString();
            }
        }

        private void InputButton_Click(object sender, EventArgs e)
        {
            mainForm.xMoveBox = OffsetVec.X.ToString();
            mainForm.yMoveBox = OffsetVec.Y.ToString();
            mainForm.zMoveBox = OffsetVec.Z.ToString();
            Close();
        }

        private float VecDiff(float x1, float x2)
        {
            if (x1 < 0.0f && x2 < 0.0f)
            {
                return ((x1 * -1) + x2) * -1;
            }
            else
            {
                return x1 - x2;
            }
        }
    }
}
