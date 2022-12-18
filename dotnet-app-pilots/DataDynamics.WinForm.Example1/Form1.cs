using System;
using System.Windows.Forms;

namespace DataDynamics.WinForm.Example1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void StockMonitoring_Load(object sender, EventArgs e)
        {
            tChart1.Panel.Gradient.Visible = false;
            tChart1.Walls.Back.Transparent = true;

            tChart1[0].FillSampleValues(12);
            tChart1[1].FillSampleValues(10);
            tChart1[2].FillSampleValues(11);
        }

        private void tChart1_DoubleClick(object sender, EventArgs e)
        {
            tChart1.ShowEditor();
        }
    }
}