using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var g = CreateGraphics();
            g.FillRectangle(new SolidBrush(Color.Blue), 20, 20, 200, 200);
        }
    }
}