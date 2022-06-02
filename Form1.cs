using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Morris_emui
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
           timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (panel1.Width != panel2.Width&& panel1.Width > panel2.Width)
            {
                panel2.Width = panel2.Width + 5;// MessageBox.Show("hllo");
            }
            // if (  panel1.Width == panel2.Width)
            else
            {
               // MessageBox.Show("hllo");
                this.label1.Text = "load completed";
                timer1.Stop();
                main_ui m = new main_ui();m.Show();
                this.Hide();
            }
        }

     
    }
}
