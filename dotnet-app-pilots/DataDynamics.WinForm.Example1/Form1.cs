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

        private void btnTest_Click(object sender, EventArgs e)
        {
            Console.Out.WriteLine(sender);
            Console.Out.WriteLine(e);
            btnTest.Text = "헬로 월드";
        }
    }
}