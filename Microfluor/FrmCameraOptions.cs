using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
namespace Microfluor
{

    public partial class FrmCameraOptions : Form
    {
        private MakeMovable _move;    
        IList<String> listCameraDeviceName = new List<String>();
        FilterInfoCollection captureDevice;
        public AForge.Video.DirectShow.FilterInfo optocell_capture_device;
        Boolean cam_flag = false;
        int time_count = 0;
        public FrmCameraOptions()
        {
            InitializeComponent();         
            
        }
       

        private void FrmCameraOptions_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.DefaultSettings)
            {
              
                string userNAme = Environment.UserName;
                string zStackFolder = @"c:\Users\" + userNAme + @"\Documents";
                
                Properties.Settings.Default.zStackFolder = zStackFolder;
           
                Properties.Settings.Default.Save();
            }







            string cam_name = "USB Camera";

            captureDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            foreach (FilterInfo device in captureDevice)
            {
                if (device.Name == cam_name)
                {
                    optocell_capture_device = device;

                    listCameraDeviceName.Add(device.Name);
                    cam_flag = true;
                    Debug.WriteLine(device.Name);
                }
            }

            timer1.Start();


        }
        private void start_mainForm()
        {
            if (cam_flag)
            {
                MainForm frm = new MainForm();
                frm.captureDeviceProperties = captureDevice;
                frm.selectedVideoDeviceIndex = listCameraDeviceName.IndexOf(optocell_capture_device.Name);
                frm.optocell_cam = optocell_capture_device;
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Camera not found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
        }
        private void FrmCameraOptions_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

      

        private void timer1_Tick(object sender, EventArgs e)
        {
            time_count++;
            if (time_count == 30)
            {
                timer1.Stop();
                start_mainForm();
            }
        }
    }
}

