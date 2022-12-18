using System;
using System.Windows.Forms;

namespace DataDynamics.WinForm.PictureViewer
{
    public partial class PictureViewerForm : Form
    {
        public PictureViewerForm()
        {
            InitializeComponent();
        }

        private void btnShowPicture_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK) pictureBox1.Load(openFileDialog.FileName);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        private void btnSetBackground_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
                pictureBox1.BackColor = colorDialog.Color;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void checkStretch_CheckedChanged(object sender, EventArgs e)
        {
            if (checkStretch.Checked)
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            else
                pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
        }
    }
}