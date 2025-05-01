using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestTime
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(label4.Text);
            x++;
            if (x == 60)
            {
                int y = Convert.ToInt32(label2.Text);
                y++;
                label2.Text = y.ToString();
                x = 0;
            }
            label4.Text = x.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RestTime.Start();
            Close();
        }
    }
}
