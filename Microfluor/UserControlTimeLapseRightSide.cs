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
    public partial class UserControlTimeLapseRightSide : UserControl
    {
        bool redButtonClicked = false, greenButtonClicked = false, blueButtonClicked = false, whiteButtonClicked = false, uvButtonClicked = false;
        public List<int> stack_selected_channels = new List<int>();
        public UserControlTimeLapseRightSide()
        {
            InitializeComponent();
            stack_selected_channels.Add(0);
            MainForm.stack_selected_channels = stack_selected_channels;
            label_measurements.Visible = false;
            label_distance.Visible = false;
            label_interval.Visible = false;
            label_slices.Visible = false;
            label_stackEnd.Visible = false;
            label_stackStart.Visible = false;
            
        }

        private void buttonRed_Click(object sender, EventArgs e)
        {

        }

        private void buttonGreen_Click(object sender, EventArgs e)
        {

        }

        private void btn_setMeasurement_Click(object sender, EventArgs e)
        {
            label_measurements.Text = textBoxMesaurementsNumber.Text;
            label_measurements.Visible = true;
        }

        private void btn_setInterval_Click(object sender, EventArgs e)
        {
            label_interval.Text = textBox_interval.Text;
            label_interval.Visible = true;
        }


        

        private void btn_setStart_Click_1(object sender, EventArgs e)
        {
            label_stackStart.Text = textBox_stackStart.Text;
            label_stackStart.Visible = true;
        }

        private void btn_setEnd_Click_1(object sender, EventArgs e)
        {
            label_stackEnd.Text = textBox_stackEnd.Text;
            label_stackEnd.Visible = true;
        }

        private void btn_setDistance_Click_1(object sender, EventArgs e)
        {
            label_distance.Text = textBox_distance.Text;
            label_distance.Visible = true;
        }

        private void btn_slices_Click_1(object sender, EventArgs e)
        {
            label_slices.Text = textBox_slices.Text;
            label_slices.Visible = true;
        }

        private void btn_clear_Click_1(object sender, EventArgs e)
        {
            label_measurements.Visible = false;
            label_distance.Visible = false;
            label_interval.Visible = false;
            label_slices.Visible = false;
            label_stackEnd.Visible = false;
            label_stackStart.Visible = false;
            label_measurements.Text = "0";
            label_distance.Text = "0";
            label_interval.Text = "0";
            label_slices.Text = "0";
            label_stackEnd.Text = "0.000";
            label_stackStart.Text = "0.000";
        }

        private void buttonUv_Click(object sender, EventArgs e)
        {

        }

        private void buttonBlue_Click(object sender, EventArgs e)
        {

        }

        private void buttonWhite_Click(object sender, EventArgs e)
        {

        }

        private void buttonZStack_Click(object sender, EventArgs e)
        {
        

        }
    }
}
