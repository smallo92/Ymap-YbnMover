using SharpDX;
using System;
using System.Windows.Forms;

namespace ymapmover
{
    public partial class CalculateVectorDifference : Form
    {
        private MainForm mainForm;
        private Vector3 offsetVec = new Vector3();

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
                string[] NewLocSplit = vector2.Text.Split(',');

                offsetVec = new Vector3(VecDiff(float.Parse(NewLocSplit[0]), float.Parse(oldLocSplit[0])), VecDiff(float.Parse(NewLocSplit[1]), float.Parse(oldLocSplit[1])), VecDiff(float.Parse(NewLocSplit[2]), float.Parse(oldLocSplit[2])));
                newOffset.Text = offsetVec.X.ToString() + ", " + offsetVec.Y.ToString() + ", " + offsetVec.Z.ToString();
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
            if (val1 < 0.0f && val2 < 0.0f)
            {
                return (val1 * -1) - (val2 * -1);
            } else
            {
                return val1 - val2;
            }
        }

        private void CalculateVectorDifference_Load(object sender, EventArgs e)
        {

        }
    }
}
