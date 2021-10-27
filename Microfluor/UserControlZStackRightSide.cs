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
    public partial class UserControlZStackRightSide : UserControl
    {
        bool redButtonClicked = false, greenButtonClicked = false, blueButtonClicked = false, whiteButtonClicked = false, uvButtonClicked=false;
        public List<int> stack_selected_channels = new List<int>();
        public bool flag_distance = false;
        public bool flag_slice = false;
        public bool flag_setStackStart = false;
        public bool flag_setStackEnd = false;

        public UserControlZStackRightSide()
        {
            InitializeComponent();
            label_distance.Visible = false;
            label_stackEnd.Visible = false;
            label_stackStart.Visible = false;
            label_slices.Visible = false;
            stack_selected_channels.Add(0);
            MainForm.stack_selected_channels = stack_selected_channels;
            MainForm.flag_distance = flag_distance;
            MainForm.flag_slices = flag_slice;
            MainForm.flag_stackEnd = flag_setStackEnd;
            MainForm.flag_stackStart = flag_setStackStart;
        }

        private void buttonFine_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonRed_Click(object sender, EventArgs e)
        {
            if (!redButtonClicked)
            {
                buttonRed.Image = Properties.Resources.redPressed;
                redButtonClicked = true;
                stack_selected_channels.Add(1);
            }
            else
            {
                buttonRed.Image = Properties.Resources.redUnPressed;
                redButtonClicked = false;
                stack_selected_channels.Remove(1);
            }
            MainForm.stack_selected_channels = stack_selected_channels;

        }

        private void buttonFocusUp_Click(object sender, EventArgs e)
        {

        }

        private void labelStakDistance_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void UserControlZStackRightSide_Load(object sender, EventArgs e)
        {

        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            label_distance.Visible = false;
            label_stackEnd.Visible = false;
            label_stackStart.Visible = false;
            label_slices.Visible = false;
            label_distance.Text = "0";
            label_stackEnd.Text = "0.000";
            label_stackStart.Text = "0.000";
            label_slices.Text = "0";
            flag_distance = false;
            flag_slice = false;
            flag_setStackStart = false;
            flag_setStackEnd = false;
            MainForm.flag_distance = flag_distance;
            MainForm.flag_slices = flag_slice;
            MainForm.flag_stackEnd = flag_setStackEnd;
            MainForm.flag_stackStart = flag_setStackStart;

        }

        private void btn_setStart_Click(object sender, EventArgs e)
        {
            label_stackStart.Text = textBox_stackStart.Text;
            label_stackStart.Visible = true;
            flag_setStackStart = true;
            MainForm.flag_distance = flag_distance;
            MainForm.flag_slices = flag_slice;
            MainForm.flag_stackEnd = flag_setStackEnd;
            MainForm.flag_stackStart = flag_setStackStart;
        }

        private void btn_setEnd_Click(object sender, EventArgs e)
        {
            label_stackEnd.Text = textBox_stackEnd.Text;
            label_stackEnd.Visible = true;
            flag_setStackEnd = true;
            MainForm.flag_distance = flag_distance;
            MainForm.flag_slices = flag_slice;
            MainForm.flag_stackEnd = flag_setStackEnd;
            MainForm.flag_stackStart = flag_setStackStart;
        }

        private void btn_setDistance_Click(object sender, EventArgs e)
        {
            if (flag_slice == true)
            {
                flag_slice = false;
                label_slices.Visible = false;
                label_distance.Text = textBox_distance.Text;
                label_distance.Visible = true;
                flag_distance = true;
                MainForm.flag_distance = flag_distance;
                MainForm.flag_slices = flag_slice;
                MainForm.flag_stackEnd = flag_setStackEnd;
                MainForm.flag_stackStart = flag_setStackStart;
            }
            else
            {
                label_distance.Text = textBox_distance.Text;
                label_distance.Visible = true;
                flag_distance = true;
                MainForm.flag_distance = flag_distance;
                MainForm.flag_slices = flag_slice;
                MainForm.flag_stackEnd = flag_setStackEnd;
                MainForm.flag_stackStart = flag_setStackStart;
            }
            
        }

        private void btn_slices_Click(object sender, EventArgs e)
        {

            if (flag_distance == true)
            {
                flag_distance = false;
                label_distance.Visible = false;
                label_slices.Text = textBox_slices.Text;
                label_slices.Visible = true;
                flag_slice = true;
                MainForm.flag_distance = flag_distance;
                MainForm.flag_slices = flag_slice;
                MainForm.flag_stackEnd = flag_setStackEnd;
                MainForm.flag_stackStart = flag_setStackStart;
            }
            else
            {
                label_slices.Text = textBox_slices.Text;
                label_slices.Visible = true;
                flag_slice = true;
                MainForm.flag_distance = flag_distance;
                MainForm.flag_slices = flag_slice;
                MainForm.flag_stackEnd = flag_setStackEnd;
                MainForm.flag_stackStart = flag_setStackStart;
            }

            
        }

        private void buttonUv_Click(object sender, EventArgs e)
        {
            if (!uvButtonClicked)
            {
                buttonUv.Image = Properties.Resources.uv_button_pressed;
                uvButtonClicked = true;
                stack_selected_channels.Add(5);
            }
            else
            {
                buttonUv.Image = Properties.Resources.uv_button_unpressed;
                uvButtonClicked = false;
                stack_selected_channels.Remove(5);
            }
            MainForm.stack_selected_channels = stack_selected_channels;
        }

        private void buttonGreen_Click(object sender, EventArgs e)
        {
            if (!greenButtonClicked)
            {
                buttonGreen.Image = Properties.Resources.greenPressed;
                greenButtonClicked = true;
                stack_selected_channels.Add(2);
            }
            else
            {
                buttonGreen.Image = Properties.Resources.greenUnPressed;
                greenButtonClicked = false;
                stack_selected_channels.Remove(2);
            }
            MainForm.stack_selected_channels = stack_selected_channels;
        }

        private void buttonBlue_Click(object sender, EventArgs e)
        {
            if (!blueButtonClicked)
            {
                buttonBlue.Image = Properties.Resources.bluePressed;
                blueButtonClicked = true;
                stack_selected_channels.Add(3);
            }
            else
            {
                buttonBlue.Image = Properties.Resources.blueUnPressed;
                blueButtonClicked = false;
                stack_selected_channels.Remove(3);
            }
            MainForm.stack_selected_channels = stack_selected_channels;
        }

        private void buttonWhite_Click(object sender, EventArgs e)
        {

            if (!whiteButtonClicked)
            {
                buttonWhite.Image = Properties.Resources.whitePressed;
                whiteButtonClicked = true;
                stack_selected_channels.Add(4);
            }
            else
            {
                buttonWhite.Image = Properties.Resources.whiteUnPressed;
                whiteButtonClicked = false;
                stack_selected_channels.Remove(4);
            }
            MainForm.stack_selected_channels = stack_selected_channels;
        }
    }
}
