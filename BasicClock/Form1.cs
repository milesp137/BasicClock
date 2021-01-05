using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicClock
{
    public partial class Form1 : Form
    {

        Timer t = new Timer();

        int Width = 300;
        int Height = 300;
        int SecHand = 140;
        int MinHand = 110;
        int HrHand = 80;

        //center
        int cx;
        int cy;

        Bitmap bmp;
        Graphics g;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //create bitmap
            bmp = new Bitmap(Width + 1, Height + 1);

            //center
            cx = Width / 2;
            cy = Height / 2;

            //backcolor
            this.BackColor = Color.White;

            //timer
            t.Interval = 1000;  //in millisecond
            t.Tick += new EventHandler(this.t_Tick);
            t.Start();
        }

        private void t_Tick (object sender, EventArgs e)
        {
            //create graphics
            g = Graphics.FromImage(bmp);

            //get time
            int ss = DateTime.Now.Second;
            int mm = DateTime.Now.Minute;
            int hh = DateTime.Now.Hour;

            int[] handcoord = new int[2];

            //clear
            g.Clear(Color.White);

            //draw circle
            g.DrawEllipse(new Pen(Color.Black, 1f), 0, 0, Width, Height);

            //draw figure
            g.DrawString("12", new Font("Arial", 12), Brushes.Black, new PointF(140, 2));
            g.DrawString("3", new Font("Arial", 12), Brushes.Black, new PointF(286, 140));
            g.DrawString("6", new Font("Arial", 12), Brushes.Black, new PointF(142, 282));
            g.DrawString("9", new Font("Arial", 12), Brushes.Black, new PointF(0, 140));

            //second hand
            handcoord = msCoord(ss, SecHand);
            g.DrawLine(new Pen(Color.Red, 1f), new Point(cx, cy), new Point(handcoord[0], handcoord[1]));

            //minute hand
            handcoord = msCoord(mm, MinHand);
            g.DrawLine(new Pen(Color.Black, 2f), new Point(cx, cy), new Point(handcoord[0], handcoord[1]));

            //hour hand
            handcoord = hrCoord(hh%12, mm, HrHand);
            g.DrawLine(new Pen(Color.Gray, 3f), new Point(cx, cy), new Point(handcoord[0], handcoord[1]));

            //load bmp in pictureBox1
            pictureBox1.Image = bmp;

            //dispose
            g.Dispose();
        }

        //coord for minute and second hand
        private int[] msCoord(int val, int hlen)
        {
            int[] coord = new int[2];
            val *= 6;   //each minute and second make 6 degree

            if(val >= 0 && val <= 180)
            {
                coord[0] = cx + (int)(hlen * Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                coord[0] = cx - (int)(hlen * -Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            return coord;
        }

        //coord for hour hand
        private int[] hrCoord(int hval,int mval, int hlen)
        {
            int[] coord = new int[2];

            //each hour makes 30 degree
            //each min makes 0.5 degree
            int val = (int)((hval * 30) + (mval * 0.5));

            if (val >= 0 && val <= 180)
            {
                coord[0] = cx + (int)(hlen * Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                coord[0] = cx - (int)(hlen * -Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            return coord;
        }

    }
}
