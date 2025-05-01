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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            #region movement
            formMoveTimer = new Timer();
            formMoveTimer.Interval = 20;
            formMoveTimer.Tick += FormMoveTimer_Tick;
            #endregion

            RestTime.Started += Started;
            RestTime.TimerTicked += TimerTicked;
            RestTime.Stoped += Stoped;

            Form2 form2 = new Form2();
            form2.ShowDialog();
        }

        private void Started()
        {
            TimerTicked();
        }

        private void TimerTicked()
        {
            button3.Text = $"{RestTime.workTime - RestTime.passedTime} min";
            Bitmap bitmap = new Bitmap(100, 40);
            for (int i = 0; i < 100; i++)
                for (int j = 0; j < 40; j++)
                {
                    int red = 255 * RestTime.passedTime / RestTime.workTime;
                    bitmap.SetPixel(i, j, Color.FromArgb(red, 255 - red, 0));
                }
            button3.BackgroundImage = bitmap;
        }
        private void Stoped()
        {
            Form3 form3 = new Form3();
            form3.ShowDialog();
        }

        #region movement
        Timer formMoveTimer;
        private void FormMoveTimer_Tick(object sender, EventArgs e)
        {
            this.Location = new Point(Cursor.Position.X - 100, Cursor.Position.Y - 7);
        }

        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            formMoveTimer.Enabled = true;
        }

        private void button2_MouseUp(object sender, MouseEventArgs e)
        {
            formMoveTimer.Enabled = false;
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.ForeColor == Color.Black)
                button3.ForeColor = Color.White;
            else
                button3.ForeColor = Color.Black;
        }
    }

    public static class RestTime
    {
        public static int workTime;
        public static int passedTime;
        private static Timer timer = new Timer();

        public delegate void Handler();
        public static event Handler Started;
        public static event Handler Stoped;
        public static event Handler TimerTicked;

        public static void Start()
        {
            timer.Enabled = false;
            timer.Stop();
            timer.Tick -= Timer_Tick;
            passedTime = 0;
            timer.Interval = 60000;
            timer.Enabled = true;
            timer.Start();
            timer.Tick += Timer_Tick;
            Started();
        }

        public static void Stop()
        {
            timer.Enabled = false;
            timer.Stop();
            timer.Tick -= Timer_Tick;
            passedTime = 0;
            Stoped();
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            passedTime++;
            TimerTicked();
            if (passedTime >= workTime)
            {
                Stop();
            }
        }
    }

}
