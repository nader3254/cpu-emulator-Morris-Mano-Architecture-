using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;


namespace Morris_emui
{
    public partial class ASM : Form
    {
        int c = 0;int chr;int c2; string[] zz = new string[3]; ASM_function f;

        public ASM()
        {
            InitializeComponent(); 


        }
        /**************************************************************************************************************
         *               F U N C T I O N S      F O R     G U I      A N D       T E X T    E A D I T O R
         **************************************************************************************************************/               
        private void button4_MouseEnter(object sender, EventArgs e)
        {
            this.button4.BackColor = ColorTranslator.FromHtml("#00C8E6");

        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            this.button4.BackColor = ColorTranslator.FromHtml("#3D454B");
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            this.button1.BackColor = ColorTranslator.FromHtml("#00C8E6");
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            this.button1.BackColor = ColorTranslator.FromHtml("#3D454B");
        }
        private void textBox1_Click(object sender, EventArgs e)
        {
          
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Asmtext_KeyPress(object sender, KeyPressEventArgs e)
        {
            //chr= Convert.ToInt32(e.KeyChar);
            // if (chr == 13)
            // {
            //    zz[0] = Asmtext.Text;c++;zz[1] = Convert.ToString(c);zz[2] = zz[0] + zz[1]   
            //     //for (int i = 0; i < c; i++)
            //     //{
            //     //    Asmtext.Text += zz[i];
            //     //}   zz[2]
            // }
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //chr = Convert.ToInt32(e.KeyChar);
            // if (chr == 13)
            {
                // textBox1.Text += c+1 + "\n"; c++;
                // zz[0] =richTextBox1.Text ; c++; zz[1] = Convert.ToString(c); zz[2] = zz[0] +zz[1];
                //richTextBox1.Text = "";
                //richTextBox1.Text = zz[2];
                //MessageBox.Show(zz[2]);

                //for (int i = 0; i < c; i++)
                //{
                //    Asmtext.Text += zz[i];
                //}   zz[2]
            }
            // Asmtext.Text = ("amr" + "\n" + "vvvv");
        }

        public int getWidth()
        {
            int w = 25;
            // get total lines of richTextBox1
            int line = richTextBox1.Lines.Length;

            if (line <= 99)
            {
                w = 20 + (int)richTextBox1.Font.Size;
            }
            else if (line <= 999)
            {
                w = 30 + (int)richTextBox1.Font.Size;
            }
            else
            {
                w = 50 + (int)richTextBox1.Font.Size;
            }

            return w;
        }

        public void AddLineNumbers()
        {
            richTextBox1.Select();
            // create & set Point pt to (0,0)
            Point pt = new Point(0, 0);
            // get First Index & First Line from richTextBox1
            int First_Index = richTextBox1.GetCharIndexFromPosition(pt);
            int First_Line = richTextBox1.GetLineFromCharIndex(First_Index);
            // set X & Y coordinates of Point pt to ClientRectangle Width & Height respectively
            pt.X = ClientRectangle.Width;
            pt.Y = ClientRectangle.Height;
            // get Last Index & Last Line from richTextBox1
            int Last_Index = richTextBox1.GetCharIndexFromPosition(pt);
            int Last_Line = richTextBox1.GetLineFromCharIndex(Last_Index);
            // set Center alignment to LineNumberTextBox
            richTextBox2.SelectionAlignment = HorizontalAlignment.Center;
            // set LineNumberTextBox text to null & width to getWidth() function value
            richTextBox2.Text = "";
            richTextBox2.Width = getWidth();
            // now add each line number to LineNumberTextBox upto last line
            for (int i = First_Line; i <= Last_Line + 1; i++)
            {
                richTextBox2.Text += i + 1 + "\n";
            }
        }

        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            this.Invalidate();
            Point pt = richTextBox1.GetPositionFromCharIndex(richTextBox1.SelectionStart);
            if (pt.X == 1)
            {
                AddLineNumbers();
            }
        }

        private void richTextBox1_VScroll(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            AddLineNumbers();
            richTextBox2.Invalidate();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "")
            {
                AddLineNumbers();
            }
        }

        private void richTextBox2_MouseDown(object sender, MouseEventArgs e)
        {
            richTextBox1.Select();
            richTextBox2.DeselectAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            f = new ASM_function(richTextBox1.Text);
            f.GenerateBuildFiles();

        }
        private void ASM_Load(object sender, EventArgs e)
        {
            AddLineNumbers();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        
      
        }
        /*************************************************************************************************************************/
        /**********************  M E T H O D S     F O R    C R E A T I N G       B I N / H E X      C O D E  ***********************
         * **********************************************************************************************************************/








    }
}












/*********************debugging********************/
//string[] w = f.Get_code_instructions();
//for(int i=0;i<w.Length;i++)
//{

//    MessageBox.Show(w[i]);
//}