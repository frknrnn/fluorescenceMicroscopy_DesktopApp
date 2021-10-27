using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DirectShowLib;
using System.Diagnostics;
using Accord;

namespace Microfluor
{
    public partial class UserControlLiveTabRightSide : UserControl
    {
        byte ledSequenceControl = 1; //1 = red, 2 = green, 3 = blue, 4 = white
        string cam_name = "USB Camera";
        DsDevice opto_cam;
        public bool redButtonClicked = false, greenButtonClicked = false, blueButtonClicked = false, whiteButtonClicked = false, uvButtonClicked = false;
        public UserControlLiveTabRightSide()
        {
            InitializeComponent();


            DsDevice[] capDevices = DsDevice.GetDevicesOfCat(DirectShowLib.FilterCategory.VideoInputDevice);
            
            foreach (var item in capDevices)
            {

                if (item.Name==cam_name)
                {
                    opto_cam =item;
                }
                

                
             
            }

        }
        void ledClickedControl(byte ledNumber)
        {
            switch (ledNumber)
            {
                case 1:
                    buttonRed.Image = Properties.Resources.redUnPressed;break;
                case 2:
                    buttonGreen.Image = Properties.Resources.greenUnPressed; break;
                case 3:
                    buttonBlue.Image = Properties.Resources.blueUnPressed; break;
                case 4:
                    buttonWhite.Image = Properties.Resources.whiteUnPressed; break;
                case 5:
                    buttonUv.Image = Properties.Resources.uv_button_unpressed; break;
            }
        }

        private void UserControlLiveTabRightSide_Load(object sender, EventArgs e)
        {
            labelRedValue.Text = circleTrackBarRed.Value.ToString();
            labelGreenValue.Text = circleTrackBarGreen.Value.ToString();
            labelBlueValue.Text = circleTrackBarBlue.Value.ToString();
            labelWhiteValue.Text = circleTrackBarWhite.Value.ToString();
        }

        private void circleTrackBarRed_ValueChanged(object sender, EventArgs e)
        {
            labelRedValue.Text = circleTrackBarRed.Value.ToString();
        }

        private void circleTrackBarGreen_ValueChanged(object sender, EventArgs e)
        {
            labelGreenValue.Text = circleTrackBarGreen.Value.ToString();

        }

        private void circleTrackBarBlue_ValueChanged(object sender, EventArgs e)
        {
            labelBlueValue.Text = circleTrackBarBlue.Value.ToString();

        }

        private void circleTrackBarWhite_ValueChanged(object sender, EventArgs e)
        {
          

            labelWhiteValue.Text = circleTrackBarWhite.Value.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void buttonRed_Click(object sender, EventArgs e)
        {
            ledClickedControl(ledSequenceControl);
            buttonRed.Image = Properties.Resources.redPressed;
            ledSequenceControl = 1;

        }

        private void buttonGreen_Click(object sender, EventArgs e)
        {
            ledClickedControl(ledSequenceControl);
            buttonGreen.Image = Properties.Resources.greenPressed;
            ledSequenceControl = 2;
        }

        private void buttonBlue_Click(object sender, EventArgs e)
        {
            ledClickedControl(ledSequenceControl);
            buttonBlue.Image = Properties.Resources.bluePressed;
            ledSequenceControl = 3;
        }

        private void buttonWhite_Click(object sender, EventArgs e)
        {
            ledClickedControl(ledSequenceControl);
            buttonWhite.Image = Properties.Resources.whitePressed;
            ledSequenceControl = 4;
        }

        private void buttonFine_Click(object sender, EventArgs e)
        {
            
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void buttonFocusUp_Click(object sender, EventArgs e)
        {

        }

        private void brightness_trackbar_ValueChanged(object sender, EventArgs e)
        {
            //////////brightness min -64,  max 64, pstepping 1, Default 0, controlflags manuel /////////////
            if (brightness_trackbar.Value >= 254)
            {
                brightness_trackbar.Value = 254;
            }
            IFilterGraph2 graphBuilder = (IFilterGraph2)(new FilterGraph());
            IBaseFilter capFilter = null;

            int hr = graphBuilder.AddSourceFilterForMoniker(opto_cam.Mon, null, opto_cam.Name, out capFilter);

            IAMVideoProcAmp cameraControl = capFilter as IAMVideoProcAmp;

            cameraControl.Set(VideoProcAmpProperty.Brightness, brightness_trackbar.Value-64, VideoProcAmpFlags.Manual);
          

        }

        private void gain_trackbar_ValueChanged(object sender, EventArgs e)
        {
            //////////Gain min 0,  max 100, pstepping 1, Default 0, controlflags manuel /////////////
            if (gain_trackbar.Value >= 254)
            {
                gain_trackbar.Value = 254;
            }
            IFilterGraph2 graphBuilder = (IFilterGraph2)(new FilterGraph());
            IBaseFilter capFilter = null;

            int hr = graphBuilder.AddSourceFilterForMoniker(opto_cam.Mon, null, opto_cam.Name, out capFilter);

            IAMVideoProcAmp cameraControl = capFilter as IAMVideoProcAmp;

            cameraControl.Set(VideoProcAmpProperty.Gain, gain_trackbar.Value, VideoProcAmpFlags.Manual);
          
        }

        private void contrast_trackbar_ValueChanged(object sender, EventArgs e)
        {

            //////////Contrast min 0,  max 64, pstepping 1, Default 32, controlflags manuel /////////////
            if (contrast_trackbar.Value >= 254)
            {
                contrast_trackbar.Value = 254;
            }
            IFilterGraph2 graphBuilder = (IFilterGraph2)(new FilterGraph());
            IBaseFilter capFilter = null;

            int hr = graphBuilder.AddSourceFilterForMoniker(opto_cam.Mon, null, opto_cam.Name, out capFilter);

            IAMVideoProcAmp cameraControl = capFilter as IAMVideoProcAmp;

            cameraControl.Set(VideoProcAmpProperty.Contrast, contrast_trackbar.Value, VideoProcAmpFlags.Manual);
            

        }

        private void buttonGreen_capture_Click(object sender, EventArgs e)
        {
            if (!greenButtonClicked)
            {
                buttonGreen_capture.Image = Properties.Resources.greenPressed;
                greenButtonClicked = true;
            }
            else
            {
                buttonGreen_capture.Image = Properties.Resources.greenUnPressed;
                greenButtonClicked = false;
            }
        }

        private void buttonblue_capture_Click(object sender, EventArgs e)
        {
            if (!blueButtonClicked)
            {
                buttonblue_capture.Image = Properties.Resources.bluePressed;
                blueButtonClicked = true;
            }
            else
            {
                buttonblue_capture.Image = Properties.Resources.blueUnPressed;
                blueButtonClicked = false;
            }
        }

        private void buttonWhite_capture_Click(object sender, EventArgs e)
        {
            if (!whiteButtonClicked)
            {
                buttonWhite_capture.Image = Properties.Resources.whitePressed;
                whiteButtonClicked = true;
            }
            else
            {
                buttonWhite_capture.Image = Properties.Resources.whiteUnPressed;
                whiteButtonClicked = false;
            }
        }

        private void buttonUv_capture_Click(object sender, EventArgs e)
        {

            if (!uvButtonClicked)
            {
                buttonUv_capture.Image = Properties.Resources.uv_button_pressed;
                uvButtonClicked = true;
            }
            else
            {
                buttonUv_capture.Image = Properties.Resources.uv_button_unpressed;
                uvButtonClicked = false;
            }
        }

        private void trackbar_exposure_ValueChanged(object sender, EventArgs e)
        {

            //////////Exposure min -13,  max -1, pstepping 1, Default 6, controlflags manuel and auto /////////////

            if (brightness_trackbar.Value >= 254)
            {
                brightness_trackbar.Value = 254;
            }
            IFilterGraph2 graphBuilder = (IFilterGraph2)(new FilterGraph());
            IBaseFilter capFilter = null;

            int hr = graphBuilder.AddSourceFilterForMoniker(opto_cam.Mon, null, opto_cam.Name, out capFilter);

            IAMCameraControl cameraControl = capFilter as IAMCameraControl;
          

            
            cameraControl.Set(CameraControlProperty.Exposure,trackbar_exposure.Value*(-1),CameraControlFlags.Manual);
        
        }

        private void buttonUv_Click(object sender, EventArgs e)
        {
            ledClickedControl(ledSequenceControl);
            buttonUv.Image = Properties.Resources.uv_button_pressed;
            ledSequenceControl = 5;
        }

        private void shiftCircleTrackbar1_ValueChanged(object sender, EventArgs e)
        {
            labelUv.Text = circleTrackBarUv.Value.ToString();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!redButtonClicked)
            {
                buttonRed_capture.Image = Properties.Resources.redPressed;
                redButtonClicked = true;
            }
            else
            {
                buttonRed_capture.Image = Properties.Resources.redUnPressed;
                redButtonClicked = false;
            }
        }
    }
}
