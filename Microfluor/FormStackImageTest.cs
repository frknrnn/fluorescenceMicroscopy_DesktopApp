using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Microfluor
{
    public partial class FormStackImageTest : Form
    {
        
        public FormStackImageTest()
        {
            InitializeComponent();
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            this.Text = hScrollBar1.Value.ToString();
            //pictureBox1.Dispose();
            pictureBox1.Image = MainForm.allImages[hScrollBar1.Value];
        }
    }
}
