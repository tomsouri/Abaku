using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_Environment
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CreateTextboxes();
        }
        private void CreateTextboxes()
        {
            int size = 30;
            int start = 40;
            for (int i = 0; i < 15; i++)
            {
                var t = new TextBox();
                t.Location = new Point(start + i * size, start + i * size);
                t.Size = new System.Drawing.Size(size,size);
                t.Text = "9";
                t.TextChanged += m;
                this.Controls.Add(t);
            }
            
        }
        private void m(object sender, EventArgs a)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
