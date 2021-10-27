using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge;
using AForge.Video.DirectShow;
using System.Diagnostics;

namespace Microfluor
{
    public partial class camOptForm : UserControl
    {
        public FilterInfoCollection settingsCameraDevice;
        public VideoCaptureDevice selectedCameraDevice;
        public camOptForm()
        {
            InitializeComponent();
            string cam_name = "USB Camera";

            settingsCameraDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            foreach (FilterInfo device in settingsCameraDevice)
            {
               
                comboBox_CameraSettings.Items.Add(device.Name);
               
            }
            comboBox_CameraSettings.SelectedIndex = 0;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox_CameraSettings_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
        }
    }
}
