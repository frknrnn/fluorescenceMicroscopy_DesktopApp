using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Microfluor
{
    public partial class customizedKnob : UserControl
    {
        Rectangle rectangle;//; = new Rectangle(0,0,50,50);
        Rectangle circleMain,circleTop;
        Pen circleFillcolour = new Pen(Color.FromArgb(49, 51, 50));
        int circleTopSize = 10,circleBackStartEndOffset = 2;
        
        Brush topCircleFillColour = new SolidBrush(Color.FromArgb(49, 51, 50));
        public customizedKnob()
        {
            InitializeComponent();
        }

        private void customizedKnob_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
             g.DrawRectangle(Pens.Blue, rectangle);
            g.DrawEllipse(circleFillcolour, circleMain);
            g.FillEllipse(Brushes.Red, circleMain);
            

            g.DrawEllipse(circleFillcolour, circleTop);
            g.FillEllipse(topCircleFillColour, circleTop);


        }

        private void customizedKnob_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(49, 51, 50);
            circleMain = new Rectangle(circleBackStartEndOffset, circleBackStartEndOffset,
                ClientSize.Width- circleBackStartEndOffset, ClientSize.Height- circleBackStartEndOffset);
            circleTop = new Rectangle(circleTopSize, circleTopSize, ClientSize.Width-(2*circleTopSize), ClientSize.Height-(2*circleTopSize));

            rectangle = new Rectangle(0, 0, ClientSize.Width-2, ClientSize.Height -2);
        }
    }
}
