using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV.Structure;
using Emgu.CV;
using Emgu.CV.Util;

namespace Microfluor
{
    public partial class resultForm : UserControl
    {
        
        public List<Image> redChannel { get; set; }
        public List<Image> blueChannel { get; set; }
        public List<Image> greenChannel { get; set; }
        public List<Image> whiteChannel { get; set; }
        public List<Image> uvChannel { get; set; }
        public static bool redButtonClicked { get; set; }
        public static bool greenButtonClicked { get; set; }
        public static bool blueButtonClicked { get; set; }
        public static bool whiteButtonClicked { get; set; }
        public static bool uvButtonClicked { get; set; }

       
        


        public resultForm()
        {
            InitializeComponent();
            
        }


       


        private void buttonRed_Click(object sender, EventArgs e)
        {
           
           
        }

        private void buttonUv_Click(object sender, EventArgs e)
        {
         
           
        }

        private void buttonGreen_Click(object sender, EventArgs e)
        {
          
            
        }

        private void buttonBlue_Click(object sender, EventArgs e)
        {
           
           
        }

        private void buttonWhite_Click(object sender, EventArgs e)
        {
            

            
        }

        private void greenPictureBox_Click(object sender, EventArgs e)
        {

        }
    }
}
