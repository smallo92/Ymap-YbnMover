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
                string[] OldLocSplit = vector1.Text.Split(',');
                Vector3 OldVec = new Vector3(float.Parse(OldLocSplit[0]), float.Parse(OldLocSplit[1]), float.Parse(OldLocSplit[2]));
                string[] NewLocSplit = vector2.Text.Split(',');
                Vector3 NewVec = new Vector3(float.Parse(NewLocSplit[0]), float.Parse(NewLocSplit[1]), float.Parse(NewLocSplit[2]));
                OffsetVec = NewVec - OldVec;
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
    }
}
