using SharpDX;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace ymapmover
{
    public partial class CalculateVectorDifference : Form
    {
        private MainForm mainForm;
        private Vector3 OffsetVec = new Vector3();
        public CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();

        public CalculateVectorDifference(MainForm ParentForm)
        {
            ci.NumberFormat.CurrencyDecimalSeparator = ".";
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

                OffsetVec = newVec - oldVec;
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
