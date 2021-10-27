using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using System.IO;
using FrmDrag;
using System.Diagnostics;
using System.Media;
using Emgu.Util;
using Emgu.CV.Structure;
using Emgu.CV;
using System.IO.Ports;
using NumSharp;
using Emgu.CV.Util;
using DirectShowLib;

namespace Microfluor
{
    public partial class MainForm : Form
    {
        public static List<Image> allImages = new List<Image>();
        public bool result_status = false;
        public static bool isDeviceDisconnect = false;
        byte tabButtonClickedControl = 1; // 1 = live, 2 = Z tack, 3 = Timelapse, 4 = Results
        Font tabLabelRegularFont = new Font("Segoe UI Light", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
        Font tabLabelBoldFont = new Font("Segoe UI Light", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
        private MakeMovable _move;

        
        UserControlResultTabBottomSide resultTabBottom = new UserControlResultTabBottomSide();
        
        UserControlLiveTabRightSide liveTabRight = new UserControlLiveTabRightSide();
        UserControlZStackRightSide z_stackTabRightSide = new UserControlZStackRightSide();
        UserControlTimeLapseRightSide timeLapseRightSide = new UserControlTimeLapseRightSide();
        UserControlResultTabRightSide resultTabRightSide = new UserControlResultTabRightSide();
        UserControlCncStageBottomSide stageTabBottomSide = new UserControlCncStageBottomSide();
        UserControlCncStageRightSide stageTabRightSide = new UserControlCncStageRightSide();

        LoadingForm loadingForm_gui = new LoadingForm();
        previewPictureBoxForm previewPictureBoxF = new previewPictureBoxForm();
        resultForm resultForm = new resultForm();
        Image temp;
        Image<Gray,byte> grayTemp;
        public static List<int> stack_selected_channels { get; set; }
        public static byte result_selected_channel { get; set; }

        public static bool flag_stackStart { get; set; }
        public static bool flag_stackEnd { get; set; }
        public static bool flag_distance { get; set; }
        public static bool flag_slices { get; set; }


        int selectedVideoIndex, stackCounter = 0;
        FilterInfoCollection captureDevice;
        VideoCaptureDevice videoSource;
        bool isMaximizeMode = false;
        int timer_count = 0;
        public int start_measurement_timelapse = 0;
        public int measurement_timelapse = 0;
        public AForge.Video.DirectShow.FilterInfo optocell_cam { get; set; }
        public bool isResultPanelShow = false;
        public static SerialPort serialPortDevice { get; set; }
        public string zStack = "0.2";
        public string zStackFeedRate = "1000";

        public string xyStepSize = "2";
        public string xyFeedRate = "1000";
        public string position_isChanged = "";
        public string x = "";
        public string y = "";
        public string z = "";

        public bool flag_TextBoxPosition=false;

        String mainPath;
        public string readStackFolderName;
        public string[] filePaths;
        public string redFolder;
        public string blueFolder;
        public string greenFolder;
        public string whiteFolder;
        public string uvFolder;

        public static List<Image> stack_images = new List<Image>();

        public static List<Image<Bgr,byte>> redStackImageList = new List<Image<Bgr,byte>>();
        public static List<Image<Bgr, byte>> greenStackImageList = new List<Image<Bgr, byte>>();
        public static List<Image<Bgr, byte>> blueStackImageList = new List<Image<Bgr, byte>>();
        public static List<Image<Bgr, byte>> whiteStackImageList = new List<Image<Bgr, byte>>();
        public static List<Image<Bgr, byte>> uvStackImageList = new List<Image<Bgr, byte>>();
        public bool red_timelapse = false, green_timelapse = false, blue_timelapse = false, white_timelapse = false, uv_timelapse = false;


        public  int waittime = 15;

        public int xyWaitTime = 50;
        public  bool flag1 = false;
        private  bool flag2 = false;

        private bool flagXp = false;
        private bool flagXn = false;
        private bool flagYp = false;
        private bool flagYn = false;
        private bool flagZ = false;

        private bool flag_TimeLapseZstackControl = false;

        public bool port_flag = false;
        public static bool fine = false;
        public static bool coarse = false;


        Graphics graphics;

        double ratioX, ratioY, ratio;
        int newWidth;
        int newHeight;
        Bitmap newImage;
        private AForge.Video.DirectShow.FilterInfo optocell_capture_device;

        public MainForm()
        {
            InitializeComponent();

           
            _move = new MakeMovable(this);
            _move.SetMovable(panelHeader, panelRightSide, pictureBoxMainLogo);

            //previewPictureBoxF.Dock = DockStyle.Fill;
            resultForm.Dock = DockStyle.Fill;

            panelPictureBoxContainer.Controls.Add(previewPictureBoxF);
            panelPictureBoxContainer.Controls["previewPictureBoxForm"].BringToFront();

            panelPictureBoxContainer.Controls.Add(resultForm);

           

           

            stageTabRightSide.Dock = DockStyle.Fill;
            stageTabBottomSide.Dock = DockStyle.Fill;

          
            liveTabRight.Dock = DockStyle.Fill;

            resultTabBottom.Dock = DockStyle.Fill;
            resultTabRightSide.Dock = DockStyle.Fill;

            z_stackTabRightSide.Dock = DockStyle.Fill;

            timeLapseRightSide.Dock = DockStyle.Fill;
            

            cncStagePanel.Controls.Add(stageTabRightSide);
            cncStagePanel.Controls["UserControlCncStageRightSide"].BringToFront();

            panelRightSide.Controls.Add(liveTabRight);
            panelRightSide.Controls.Add(z_stackTabRightSide);
            panelRightSide.Controls.Add(timeLapseRightSide);
            panelRightSide.Controls.Add(resultTabRightSide);
            

            panelRightSide.Controls["UserControlLiveTabRightSide"].BringToFront();
        }
        public int selectedVideoDeviceIndex
        {
            get { return selectedVideoIndex; }
            set { selectedVideoIndex = value; }
        }
        public FilterInfoCollection captureDeviceProperties
        {
            get { return captureDevice; }
            set { captureDevice = value; }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (videoSource.IsRunning)
            {
                videoSource.Stop();
            }
            Application.Exit();
        }
        // bottom panel controls location change
        //void bottomPanelLocationChange(Control control)
        //{
        //    foreach (Control c in control.Controls)
        //    {
        //        if (c.HasChildren)
        //        {
        //            bottomPanelLocationChange(c);
        //        }
        //        if (isMaximizeMode)
        //            c.Location = new Point(c.Location.X + 200, c.Location.Y);
        //        else
        //            c.Location = new Point(c.Location.X - 200, c.Location.Y);
        //    }
        //}

        private void setMaximizePanel()
        {
            panelPictureBoxContainer.Dock = DockStyle.None;
            panelPictureBoxContainer.Anchor = AnchorStyles.None;
            previewPictureBoxF.previewPictureBox.Width = 1270;
            previewPictureBoxF.previewPictureBox.Height = 950;
            panelPictureBoxContainer.Width = 1280;
            panelPictureBoxContainer.Height = 960;
            
        }

        private void setMinimizePanel()
        {
            panelPictureBoxContainer.Dock = DockStyle.None;
            panelPictureBoxContainer.Anchor = AnchorStyles.None;
            previewPictureBoxF.previewPictureBox.Width = 840;
            previewPictureBoxF.previewPictureBox.Height = 625;
            panelPictureBoxContainer.Width = 850;
            panelPictureBoxContainer.Height = 635;
        }





        private void buttonMaxmize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                isMaximizeMode = true;
                this.WindowState = FormWindowState.Maximized;
                buttonMaxmize.BackgroundImage = Properties.Resources.maxSizeButtonIcon;
                if (isResultPanelShow)
                {
                    setResultPanelMaximize();
                    resultWindowStateChangesFirstImage();
                }
                else
                {
                    setMaximizePanel();
                    
                }
                
               
                //for (int i = 0; i < panelBottomSide.Controls.Count; i++)
                //{
                //    bottomPanelLocationChange(panelBottomSide.Controls[i]);
                //}

            }
            else if (this.WindowState == FormWindowState.Maximized)
            {
                isMaximizeMode = false;
                this.WindowState = FormWindowState.Normal;
                
                buttonMaxmize.BackgroundImage = Properties.Resources.normalSizeButtonIcon;
                if (isResultPanelShow)
                {
                    setResultPanelMinimize();
                    resultWindowStateChangesFirstImage();
                }
                else
                {
                    setMinimizePanel();
                }
               
                //for (int i = 0; i < panelBottomSide.Controls.Count; i++)
                //{
                //    bottomPanelLocationChange(panelBottomSide.Controls[i]);
                //}

            }
        }

        public Bitmap ScaleImage(Bitmap bmp, int maxWidth, int maxHeight)
        {
            ratioX = (double)maxWidth / bmp.Width;
            ratioY = (double)maxHeight / bmp.Height;
            ratio = Math.Min(ratioX, ratioY);


            newWidth = (int)(bmp.Width * ratio);
            newHeight = (int)(bmp.Height * ratio);


            //newWidth = maxWidth;
            //newHeight = maxHeight;

            //heightDiffrences = maxHeight - newHeight;
            //ptab.modifiedPictureBox1._transformation._translation.Y = -(heightDiffrences / 2);
            newImage = new Bitmap(newWidth, newHeight);

            try
            {
                using (graphics = Graphics.FromImage(newImage))
                {
                    graphics.DrawImage(bmp, 0, 0, newWidth, newHeight);
                    graphics.Dispose();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Beklenmeyen bir hata oluştu..:" + e.ToString());
            }

            return newImage;
        }





        private void ButtonTimeLapseStart_Click(object sender, EventArgs e)
        {
            saveDataForm svdForm = new saveDataForm();
            if (stack_selected_channels.Count == 1)
            {
                svdForm.label_warning_message.Text = "Please select a led channel.";
                svdForm.ShowDialog();
            }
            else
            {
                red_timelapse = false; green_timelapse = false; blue_timelapse = false; white_timelapse = false; uv_timelapse = false;

                for (int i = 0; i < stack_selected_channels.Count(); i++)
                {
                    if (stack_selected_channels[i] == 1)
                    {
                        red_timelapse = true;
                    }
                    if (stack_selected_channels[i] == 2)
                    {
                        green_timelapse = true;
                    }
                    if (stack_selected_channels[i] == 3)
                    {
                        blue_timelapse = true;
                    }
                    if (stack_selected_channels[i] == 4)
                    {
                        white_timelapse = true;
                    }
                    if (stack_selected_channels[i] == 5)
                    {
                        uv_timelapse = true;
                    }

                }

                if (string.IsNullOrEmpty(timeLapseRightSide.textBoxMesaurementsNumber.Text))
                {
                    svdForm.label_warning_message.Text = "Measurement number is empty.";
                    svdForm.ShowDialog();
                }
                else if (string.IsNullOrEmpty(timeLapseRightSide.textBox_interval.Text))
                {
                    svdForm.label_warning_message.Text = "Duration is empty.";
                    svdForm.ShowDialog();
                }
                else
                {
                    svdForm.label_warning_message.Text = "Please wait";
                    svdForm.btnOK.Visible = false;
                    svdForm.Show();
                    int duration = int.Parse(timeLapseRightSide.textBox_interval.Text);
                    int measurement = int.Parse(timeLapseRightSide.textBoxMesaurementsNumber.Text);
                    measurement_timelapse = measurement;
                    start_measurement_timelapse = 0;
                    resultTabBottom.resultTrackBar.Maximum = measurement - 1;

                    loadingForm_gui.circularProgressBar.Maximum = measurement + 1;
                    loadingForm_gui.circularProgressBar.Value = 0;
                    loadingForm_gui.circularProgressBar.Text = (0).ToString() + "/" + measurement.ToString();


                    videoSource.Stop();
                    videoSource.VideoResolution = videoSource.VideoCapabilities[0];

                    videoSource.Start();
                    Thread.Sleep(2500);



                    DateTime univDateTime = new DateTime();
                    univDateTime = DateTime.Now;
                    string dateTime = univDateTime.ToString();

                    string specialFolderName = univDateTime.Month.ToString() + "-" + univDateTime.Day.ToString() + "-" + univDateTime.Year.ToString() + "#" + univDateTime.Hour.ToString() + "-" + univDateTime.Minute.ToString() + "-" + univDateTime.Second.ToString();


                    readStackFolderName = specialFolderName;

                    string mainFolder = Properties.Settings.Default.zStackFolder + @"\MiniFluor TimeLapse";
                    System.IO.Directory.CreateDirectory(mainFolder);
                    string projectFolder = System.IO.Path.Combine(mainFolder, specialFolderName);
                    System.IO.Directory.CreateDirectory(projectFolder);

                    string redChannelFolder = "Red";
                    string blueChannelFolder = "Blue";
                    string greenChannelFolder = "Green";
                    string whiteChannelFolder = "White";
                    string uvChannelFolder = "Uv";

                    redFolder = System.IO.Path.Combine(projectFolder, redChannelFolder);
                    System.IO.Directory.CreateDirectory(redFolder);
                    blueFolder = System.IO.Path.Combine(projectFolder, blueChannelFolder);
                    System.IO.Directory.CreateDirectory(blueFolder);
                    greenFolder = System.IO.Path.Combine(projectFolder, greenChannelFolder);
                    System.IO.Directory.CreateDirectory(greenFolder);
                    whiteFolder = System.IO.Path.Combine(projectFolder, whiteChannelFolder);
                    System.IO.Directory.CreateDirectory(whiteFolder);
                    uvFolder = System.IO.Path.Combine(projectFolder, uvChannelFolder);
                    System.IO.Directory.CreateDirectory(uvFolder);
                    int start_measurement = measurement;
                    timer1.Interval = duration * 60 * 1000;
                    svdForm.btnOK.Visible = true;
                    svdForm.Close();

                    timer1.Start();

                    loadingForm_gui.ShowDialog();

                }

            
                

            }



        }


        public void position_control(string position)
        {
            bool flag = true;
            while (flag)
            {
                serialPortMicroFluor.WriteLine("?");
                for (int j = 0; j < 2; j++)
                {
                    if (serialPortMicroFluor.BytesToRead > 0)
                    {
                        string f = serialPortMicroFluor.ReadExisting();

                        if (f.IndexOf("MPos:") != -1)
                        {
                            int pFrom = f.IndexOf("MPos:") + "MPos:".Length;
                            int pTo = f.LastIndexOf("FS:") - 1;
                            if (pTo != -2)
                            {
                                String result = f.Substring(pFrom, pTo - pFrom);
                                int x_pFrom = 0;
                                int x_to = result.IndexOf(',');
                                x = result.Substring(x_pFrom, x_to);
                                int y_pFrom = result.IndexOf(",") + ",".Length;
                                int y_to = result.LastIndexOf(",");
                                y = result.Substring(y_pFrom, y_to - y_pFrom);
                                z = result.Substring(y_to + 1);
                                stageTabBottomSide.label_x.Text = x;
                                stageTabBottomSide.label_y.Text = y;
                                stageTabBottomSide.label_z.Text = z;
                            }
                            if (z.IndexOf(position) != -1)
                            {
                                flag = false;
                            }
                        }
                        Thread.Sleep(10);
                    }
                }

            }
        }



        private void ButtonStartStack_Click(object sender, EventArgs e)
        {
            saveDataForm svdForm = new saveDataForm();
            FormStackImageTest stckFrmTest = new FormStackImageTest();
            if (stack_selected_channels.Count==1)
            {
                svdForm.label_warning_message.Text = "Please select a led channel.";
                svdForm.ShowDialog();
            }
            else
            {
                bool red = false, green = false, blue = false, white = false, uv = false;

                for (int i = 0; i < stack_selected_channels.Count(); i++)
                {
                    if (stack_selected_channels[i] == 1)
                    {
                        red = true;
                    }
                    if (stack_selected_channels[i] == 2)
                    {
                        green = true;
                    }
                    if (stack_selected_channels[i] == 3)
                    {
                        blue = true;
                    }
                    if (stack_selected_channels[i] == 4)
                    {
                        white = true;
                    }
                    if (stack_selected_channels[i] == 5)
                    {
                        uv = true;
                    }

                }

                bool flag_control = true;

                if (flag_stackStart == false)
                {
                    svdForm.label_warning_message.Text = "Stack Start is not set.";
                    svdForm.ShowDialog();
                    

                }
                else if (flag_stackEnd == false)
                {
                    svdForm.label_warning_message.Text = "Stack End is not set.";
                    svdForm.ShowDialog();
                    
                }
                else if (flag_slices == false)
                {
                    if (flag_distance==true)
                    {
                        flag_control = false;
                    }
                    else
                    {
                        svdForm.label_warning_message.Text = "Please set number of slices or distance";
                        svdForm.ShowDialog();
                    }
                    
                }
                else if (flag_distance == false)
                {
                    if (flag_slices == true)
                    {
                        flag_control = false;
                    }
                    else
                    {
                        svdForm.label_warning_message.Text = "Please set number of slices or distance";
                        svdForm.ShowDialog();
                    }
                }
             
                if (flag_control)
                {
                    
                }
                else
                {
                    
                    int stackSliceNum = 0;
                    float distance = 0;
                    float start_point = 0;
                    float end_point = 0;
                    float step_size = 0;

                    if (flag_distance == true)
                    {

                    }
                    if (flag_slices == true)
                    {
                        start_point = float.Parse(z_stackTabRightSide.label_stackStart.Text);
                        end_point = float.Parse(z_stackTabRightSide.label_stackEnd.Text);
                        stackSliceNum = int.Parse(z_stackTabRightSide.label_slices.Text);
                        step_size = (start_point - end_point) / stackSliceNum;

                    }
                   

                   
                    stckFrmTest.hScrollBar1.Maximum = stackSliceNum - 1;
                    resultTabBottom.resultTrackBar.Maximum = stackSliceNum - 1;
                    LoadingForm loadingForm_gui = new LoadingForm();
                    loadingForm_gui.circularProgressBar.Maximum = stackSliceNum + 1;
                    loadingForm_gui.circularProgressBar.Value = 0;
                    loadingForm_gui.circularProgressBar.Text = (0).ToString() + "/" + stackSliceNum.ToString();
                    loadingForm_gui.Show();

                    serialPortDevice.Write("1\x18");
                    serialPortDevice.WriteLine("~");
                    string new_z = z_stackTabRightSide.label_stackStart.Text.ToString();
                    serialPortDevice.WriteLine("G90G01Z" + new_z + "F1000");
                    position_control(z_stackTabRightSide.label_stackStart.Text.ToString());


                    videoSource.Stop();
                    videoSource.VideoResolution = videoSource.VideoCapabilities[0];

                    videoSource.Start();
                    Thread.Sleep(2500);



                    DateTime univDateTime = new DateTime();
                    univDateTime = DateTime.Now;
                    string dateTime = univDateTime.ToString();

                    string specialFolderName = univDateTime.Month.ToString() + "-" + univDateTime.Day.ToString() + "-" + univDateTime.Year.ToString() + "#" + univDateTime.Hour.ToString() + "-" + univDateTime.Minute.ToString() + "-" + univDateTime.Second.ToString();


                    readStackFolderName = specialFolderName;

                    string mainFolder = Properties.Settings.Default.zStackFolder + @"\MiniFluor Z-Stack";
                    System.IO.Directory.CreateDirectory(mainFolder);
                    string projectFolder = System.IO.Path.Combine(mainFolder, specialFolderName);
                    System.IO.Directory.CreateDirectory(projectFolder);

                    string redChannelFolder = "Red";
                    string blueChannelFolder = "Blue";
                    string greenChannelFolder = "Green";
                    string whiteChannelFolder = "White";
                    string uvChannelFolder = "Uv";

                    redFolder = System.IO.Path.Combine(projectFolder, redChannelFolder);
                    System.IO.Directory.CreateDirectory(redFolder);
                    blueFolder = System.IO.Path.Combine(projectFolder, blueChannelFolder);
                    System.IO.Directory.CreateDirectory(blueFolder);
                    greenFolder = System.IO.Path.Combine(projectFolder, greenChannelFolder);
                    System.IO.Directory.CreateDirectory(greenFolder);
                    whiteFolder = System.IO.Path.Combine(projectFolder, whiteChannelFolder);
                    System.IO.Directory.CreateDirectory(whiteFolder);
                    uvFolder = System.IO.Path.Combine(projectFolder, uvChannelFolder);
                    System.IO.Directory.CreateDirectory(uvFolder);




                    for (int i = 0; i < stackSliceNum; i++)
                    {

                        loadingForm_gui.circularProgressBar.Value = i + 1;
                        loadingForm_gui.circularProgressBar.Text = (i + 1).ToString() + "/" + stackSliceNum.ToString();




                        ////////////////////////////////// RED CHANNEL /////////////


                        /////////change led



                        //////////////////

                        if (red)
                        {
                            string redImage = System.IO.Path.Combine(redFolder, i.ToString() + ".jpeg");

                            Mat blue_red = Mat.Zeros(2448, 3264, Emgu.CV.CvEnum.DepthType.Cv8U, 1);
                            Mat green_red = Mat.Zeros(2448, 3264, Emgu.CV.CvEnum.DepthType.Cv8U, 1);
                            Mat red_red = grayTemp.Mat;
                            Mat red_channel_image = new Mat(2448, 3264, Emgu.CV.CvEnum.DepthType.Cv8U, 3);

                            CvInvoke.Merge(new VectorOfMat(blue_red, green_red, red_red), red_channel_image);


                            red_channel_image.ToBitmap().Save(redImage, System.Drawing.Imaging.ImageFormat.Jpeg);

                            redStackImageList.Add(image_newResolution(red_channel_image.ToImage<Bgr,byte>(), 1200, 900));
                        }


                        ////////////////////////////////// GREEN CHANNEL /////////////


                        /////////change led
                        ///



                        //////////////////

                        if (green)
                        {
                            string greenImage = System.IO.Path.Combine(greenFolder, i.ToString() + ".jpeg");

                            Mat blue_green = Mat.Zeros(2448, 3264, Emgu.CV.CvEnum.DepthType.Cv8U, 1);
                            Mat green_green = grayTemp.Mat;
                            Mat red_green = Mat.Zeros(2448, 3264, Emgu.CV.CvEnum.DepthType.Cv8U, 1);
                            Mat green_channel_image = new Mat(2448, 3264, Emgu.CV.CvEnum.DepthType.Cv8U, 3);
                            CvInvoke.Merge(new VectorOfMat(blue_green, green_green, red_green), green_channel_image);

                            green_channel_image.ToBitmap().Save(greenImage, System.Drawing.Imaging.ImageFormat.Jpeg);
                            greenStackImageList.Add(image_newResolution(green_channel_image.ToImage<Bgr,byte>(), 1200, 900));
                        }



                        ////////////////////////////////// BLUE CHANNEL /////////////


                        /////////change led


                        //////////////////
                        if (blue)
                        {
                            string blueImage = System.IO.Path.Combine(blueFolder, i.ToString() + ".jpeg");



                            Mat blue_blue = grayTemp.Mat;
                            Mat green_blue = Mat.Zeros(2448, 3264, Emgu.CV.CvEnum.DepthType.Cv8U, 1);
                            Mat red_blue = Mat.Zeros(2448, 3264, Emgu.CV.CvEnum.DepthType.Cv8U, 1);
                            Mat blue_channel_image = new Mat(2448, 3264, Emgu.CV.CvEnum.DepthType.Cv8U, 3);

                            CvInvoke.Merge(new VectorOfMat(blue_blue, green_blue, red_blue), blue_channel_image);

                            blue_channel_image.ToBitmap().Save(blueImage, System.Drawing.Imaging.ImageFormat.Jpeg);
                            blueStackImageList.Add(image_newResolution(blue_channel_image.ToImage<Bgr,byte>(), 1200, 900));
                        }



                        ////////////////////////////////// WHİTE CHANNEL /////////////


                        /////////change led



                        //////////////////
                        ///


                        if (white)
                        {

                            string whiteImage = System.IO.Path.Combine(whiteFolder, i.ToString() + ".jpeg");

                            grayTemp.ToBitmap().Save(whiteImage, System.Drawing.Imaging.ImageFormat.Jpeg);
                            Image<Bgr, byte> gray_rgb = new Image<Bgr, byte>(3264, 2448);
                            CvInvoke.CvtColor(grayTemp, gray_rgb, Emgu.CV.CvEnum.ColorConversion.Gray2Rgb);
                            whiteStackImageList.Add(image_newResolution(gray_rgb, 1200, 900));
                        }



                        ////////////////////////////////// UV CHANNEL /////////////


                        /////////change led



                        //////////////////

                        if (uv)
                        {
                            string uvImage = System.IO.Path.Combine(uvFolder, i.ToString() + ".jpeg");

                            Image<Bgr, byte> uv_channel_image = new Image<Bgr, byte>(3264, 2448);
                            double red_parameter = 0.5;
                            double green_parameter = 0;
                            double blue_parameter = 0.5;


                            for (int l = 0; l < 2448; l++)
                            {
                                for (int m = 0; m < 3264; m++)
                                {
                                    int gray_intensity = grayTemp.Data[l, m, 0];

                                    uv_channel_image.Data[l, m, 0] = (byte)(gray_intensity * red_parameter);
                                    uv_channel_image.Data[l, m, 1] = (byte)(gray_intensity * green_parameter);
                                    uv_channel_image.Data[l, m, 2] = (byte)(gray_intensity * blue_parameter);
                                }
                            }


                            uv_channel_image.ToBitmap().Save(uvImage, System.Drawing.Imaging.ImageFormat.Jpeg);
                            uvStackImageList.Add(image_newResolution(uv_channel_image, 1200, 900));
                        }


                        ////////////////////STACK STEP///////////////////
                        ///


                        distance = start_point - step_size;
                        start_point = distance;
                        serialPortDevice.WriteLine("G90G01Z" + distance.ToString() + "F1000");
                        Thread.Sleep((int)step_size*1000+2000);
                        


                        ////////////////////////////////////



                        //allImages.Add(previewPictureBoxF.previewPictureBox.Image);



                    }
                    

                    ///////////////////////////////////////
                    if (red)
                    {
                        resultForm.redPictureBox.Image = redStackImageList[0].ToBitmap();
                        resultTabRightSide.sliderRedChannel.Enabled = true;
                        resultForm.redButtonClicked = true;
                        resultForm.buttonRed.Enabled = true;
                        resultForm.buttonRed.Image = Properties.Resources.redPressed;
                      
                    }
                    else
                    {
                        resultTabRightSide.sliderRedChannel.Enabled = false;
                        resultForm.buttonRed.Enabled = false;
                        resultForm.redButtonClicked = false;
                        resultForm.buttonRed.Image = Properties.Resources.redUnPressed;
                        //resultForm.buttonRed.Image = Properties.Resources.buttonRedDisable;
                        resultForm.redPictureBox.Image = Properties.Resources.disableBlack;
                       
                    }


                    ///////////////////////////
                    if (green)
                    {
                        resultForm.greenPictureBox.Image = greenStackImageList[0].ToBitmap();
                        resultTabRightSide.sliderGreenChannel.Enabled = true;
                        resultForm.greenButtonClicked = true;
                        resultForm.buttonGreen.Enabled = true;
                        resultForm.buttonGreen.Image = Properties.Resources.greenPressed;
                        
                    }
                    else
                    {
                        resultTabRightSide.sliderGreenChannel.Enabled = false;
                        resultForm.buttonGreen.Enabled = false;
                        resultForm.greenButtonClicked = false;
                        resultForm.buttonGreen.Image = Properties.Resources.greenUnPressed;
                        //resultForm.buttonGreen.Image = Properties.Resources.buttonGreenDisable;
                        resultForm.greenPictureBox.Image = Properties.Resources.disableBlack;
                       
                    }


                    /////////////////////////
                    if (blue)
                    {
                       
                        resultForm.bluePictureBox.Image = blueStackImageList[0].ToBitmap();
                        resultTabRightSide.sliderBlueChannel.Enabled = true;
                        resultForm.buttonBlue.Enabled = true;
                        resultForm.blueButtonClicked = true;
                        resultForm.buttonBlue.Image = Properties.Resources.bluePressed;
                       
                    }
                    else
                    {
                        resultTabRightSide.sliderBlueChannel.Enabled = false;
                        resultForm.buttonBlue.Enabled = false;
                        resultForm.blueButtonClicked = false;
                        resultForm.buttonBlue.Image = Properties.Resources.blueUnPressed;
                        //resultForm.buttonBlue.Image = Properties.Resources.buttonBlueDisable;
                        resultForm.bluePictureBox.Image = Properties.Resources.disableBlack;
                        
                    }

                    /////////////////////////////
                    if (white)
                    {
                        
                        resultForm.whitePictureBox.Image = whiteStackImageList[0].ToBitmap();
                        resultTabRightSide.sliderWhiteChannel.BarInnerColor = Color.White;
                        resultTabRightSide.sliderWhiteChannel.BarPenColorBottom = Color.White;
                        resultTabRightSide.sliderWhiteChannel.BarPenColorTop = Color.White;
                        resultTabRightSide.sliderWhiteChannel.ElapsedInnerColor = Color.White;
                        resultTabRightSide.sliderWhiteChannel.ElapsedPenColorBottom = Color.White;
                        resultTabRightSide.sliderWhiteChannel.ElapsedPenColorTop = Color.White;
                        resultTabRightSide.sliderWhiteChannel.ForeColor = Color.White;
                        resultTabRightSide.sliderWhiteChannel.ThumbOuterColor = Color.White;
                        resultTabRightSide.sliderWhiteChannel.ThumbPenColor = Color.White;
                        resultTabRightSide.sliderWhiteChannel.ThumbInnerColor = Color.White;
                        resultTabRightSide.sliderWhiteChannel.Enabled = true;
                        resultForm.whiteButtonClicked = true;
                        resultForm.buttonWhite.Enabled = true;
                        resultForm.buttonWhite.Image = Properties.Resources.whitePressed;
                        
                    }
                    else
                    {
                        resultForm.buttonWhite.Image = Properties.Resources.buttonWhiteDisable;
                        resultTabRightSide.sliderWhiteChannel.BarInnerColor = Color.Gray;
                        resultTabRightSide.sliderWhiteChannel.BarPenColorBottom = Color.Gray;
                        resultTabRightSide.sliderWhiteChannel.BarPenColorTop = Color.Gray;
                        resultTabRightSide.sliderWhiteChannel.ElapsedInnerColor = Color.Gray;
                        resultTabRightSide.sliderWhiteChannel.ElapsedPenColorBottom = Color.Gray;
                        resultTabRightSide.sliderWhiteChannel.ElapsedPenColorTop = Color.Gray;
                        resultTabRightSide.sliderWhiteChannel.ForeColor = Color.Gray;
                        resultTabRightSide.sliderWhiteChannel.ThumbOuterColor = Color.Gray;
                        resultTabRightSide.sliderWhiteChannel.ThumbPenColor = Color.Gray;
                        resultTabRightSide.sliderWhiteChannel.ThumbInnerColor = Color.Gray;
                        resultTabRightSide.sliderWhiteChannel.Enabled = false;
                        resultForm.buttonWhite.Enabled = false;
                        resultForm.whiteButtonClicked = false;
                        //resultForm.buttonWhite.Image = Properties.Resources.buttonWhiteDisable;
                        resultForm.whitePictureBox.Image = Properties.Resources.disableBlack;
                    }

                    ////////////////////////////////
                    if (uv)
                    {
                       
                        resultForm.uvPictureBox.Image = uvStackImageList[0].ToBitmap();
                        resultTabRightSide.sliderUvChannel.Enabled = true;
                        resultForm.buttonUv.Enabled = true;
                        resultForm.uvButtonClicked = true;
                        resultForm.buttonUv.Image = Properties.Resources.uv_button_pressed;
                       
                    }
                    else
                    {
                        resultTabRightSide.sliderUvChannel.Enabled = false;
                        resultForm.buttonUv.Enabled = false;
                        resultForm.uvButtonClicked = false;
                        resultForm.buttonUv.Image = Properties.Resources.uv_button_unpressed;
                        //resultForm.buttonUv.Image = Properties.Resources.buttonUvDisable;
                        resultForm.uvPictureBox.Image = Properties.Resources.disableBlack;
                    }


                    ////////////////////
                    ///




                    //////////////////
                    //


                    setSliderValues(resultForm.redButtonClicked, resultForm.greenButtonClicked, resultForm.blueButtonClicked, resultForm.whiteButtonClicked, resultForm.uvButtonClicked);
                    Image<Bgr, byte> first_image = createNewImage(1200, 900, 3);

                    int total_slider_value = totalChannelSliderValue(red, green, blue, white, uv);

                    if (red)
                    {
                        
                        int channel_value = Convert.ToInt32(resultTabRightSide.sliderRedChannel.Value);
                        double red_channel_rate = calculateChannelMixerRate(channel_value, total_slider_value);
                        first_image = channelMixer(redStackImageList[0], first_image, red_channel_rate, 1200, 900);

                    }

                    if (green)
                    {

                        int channel_value = Convert.ToInt32(resultTabRightSide.sliderGreenChannel.Value);
                        double green_channel_rate = calculateChannelMixerRate(channel_value, total_slider_value);
                        first_image = channelMixer(greenStackImageList[0], first_image, green_channel_rate, 1200, 900);

                    }

                    if (blue)
                    {

                        int channel_value = Convert.ToInt32(resultTabRightSide.sliderBlueChannel.Value);
                        double blue_channel_rate = calculateChannelMixerRate(channel_value, total_slider_value);
                        first_image = channelMixer(blueStackImageList[0], first_image, blue_channel_rate, 1200, 900);

                    }
                    if (white)
                    {

                        int channel_value = Convert.ToInt32(resultTabRightSide.sliderWhiteChannel.Value);
                        double white_channel_rate = calculateChannelMixerRate(channel_value, total_slider_value);
                        first_image = channelMixer(whiteStackImageList[0], first_image, white_channel_rate, 1200, 900);

                    }
                    if (uv)
                    {

                        int channel_value = Convert.ToInt32(resultTabRightSide.sliderUvChannel.Value);
                        double uv_channel_rate = calculateChannelMixerRate(channel_value, total_slider_value);
                        first_image = channelMixer(uvStackImageList[0], first_image, uv_channel_rate, 1200, 900);

                    }

                    


                    videoSource.Stop();
                    videoSource.VideoResolution = videoSource.VideoCapabilities[5];

                    videoSource.Start();
                    svdForm.Close();
                    saveDataForm svdForm2 = new saveDataForm();

                    loadingForm_gui.Close();
                    svdForm2.label_warning_message.Text = "DATA SAVED";
                    svdForm2.btnOK.Visible = true;
                    svdForm2.ShowDialog();
                    isResultPanelShow = true;
                    if (this.WindowState == FormWindowState.Normal)
                    {
                        setResultPanelMinimize();
                    }
                    else
                    {
                        setResultPanelMaximize();
                    }
                    resultForm.mainPictureBox.Image = ScaleImage(first_image.ToBitmap(),resultForm.mainPictureBox.Width,resultForm.mainPictureBox.Height);
                    changeTabButtonColor(tabButtonClickedControl);
                    tabButtonClickedControl = 4;
                    buttonResults.BackgroundImage = Properties.Resources.result_button_selected;
                    buttonResults.Cursor = Cursors.Hand;
                    panelPictureBoxContainer.Controls["resultForm"].BringToFront();
                    
                    panelRightSide.Controls["UserControlResultTabRightSide"].BringToFront();
                    result_selected_channel = 1;
                    resultTabBottom.label_trackbar.Text = "0";
                    result_status = true;
                    //stckFrmTest.pictureBox1.Image = allImages[0];
                    //stckFrmTest.Show();
                }

            }



        }

        private Image<Bgr,byte> createNewImage(int width,int height,int channel)
        {
            Mat new_ımage = Mat.Zeros(height, width, Emgu.CV.CvEnum.DepthType.Cv8U, channel);
            return new_ımage.ToImage<Bgr,byte>();
        }

        private Image<Bgr,byte> channelMixer(Image<Bgr,byte> input,Image<Bgr,byte> output,double rate,int width,int height)
        {

            for (int l = 0; l < height; l++)
            {
                for (int m = 0; m < width; m++)
                {
                    int red_intensity = input.Data[l, m, 0];
                    int green_intensity = input.Data[l, m, 1];
                    int blue_intensity = input.Data[l, m, 2];
                    

                    output.Data[l, m, 0] = (byte)(output.Data[l,m,0]+(red_intensity * rate));
                    output.Data[l, m, 1] = (byte)(output.Data[l,m,1]+(green_intensity * rate));
                    output.Data[l, m, 2] = (byte)(output.Data[l,m,2]+(blue_intensity * rate));
                }
            }

            return output;

        }



        private int totalChannelSliderValue(bool red, bool green, bool blue, bool white, bool uv)
        {
            int total_value = 0;
            if (red) { total_value=total_value+Convert.ToInt32(resultTabRightSide.sliderRedChannel.Value); }
            if (green) { total_value = total_value + Convert.ToInt32(resultTabRightSide.sliderGreenChannel.Value); }
            if (blue) { total_value = total_value + Convert.ToInt32(resultTabRightSide.sliderBlueChannel.Value); }
            if (white) { total_value = total_value + Convert.ToInt32(resultTabRightSide.sliderWhiteChannel.Value); }
            if (uv) { total_value = total_value + Convert.ToInt32(resultTabRightSide.sliderUvChannel.Value); }

            return total_value;

        }

       private double calculateChannelMixerRate(int channel_value,int total_slider_value)
        {
            
            double rate = ((double)channel_value / total_slider_value);

            return rate;
        }


        private bool controlResultTabSelectedChannels(bool red, bool green, bool blue, bool white, bool uv)
        {
            saveDataForm svdForm2 = new saveDataForm();
            int active_channel_count = 0;
            if (red) { active_channel_count++; }
            if (green) { active_channel_count++; }
            if (blue) { active_channel_count++; }
            if (white) { active_channel_count++; }
            if (uv) { active_channel_count++; }
            if (active_channel_count == 0)
            {
                svdForm2.label_warning_message.Text = "A Channel must be selected.";
                svdForm2.btnOK.Visible = true;
                svdForm2.ShowDialog();
                return true;
            }
            return false;
            
        }

        private void setSliderValues(bool red, bool green,bool blue,bool white,bool uv)
        {
            saveDataForm svdForm2 = new saveDataForm();
            int active_channel_count = 0;
            if (red) { active_channel_count++; }
            if (green) { active_channel_count++; }
            if (blue) { active_channel_count++; }
            if (white) { active_channel_count++; }
            if (uv) { active_channel_count++; }
            int new_value = Convert.ToInt32(100 / active_channel_count);
            if (red) { resultTabRightSide.sliderRedChannel.Value = new_value; }
            if (green) { resultTabRightSide.sliderGreenChannel.Value = new_value; }
            if (blue) { resultTabRightSide.sliderBlueChannel.Value = new_value; }
            if (white) { resultTabRightSide.sliderWhiteChannel.Value = new_value; }
            if (uv) { resultTabRightSide.sliderUvChannel.Value = new_value; }
            
           
        }


      

        Image<Bgr,byte> image_newResolution(Image<Bgr,byte> a,int new_wd,int new_hg)
        {
            
            Image<Bgr, byte> resizedImage = a.Resize(new_wd, new_hg,Emgu.CV.CvEnum.Inter.LinearExact);

            return resizedImage ;

        }


      
        

        private void createVideoTimeLapse()
        {
            //int width = allImages[0].Width;
            //int height = allImages[0].Height;
            //var framRate = 25;

            //using (var vFWriter = new VideoFileWriter())
            //{
            //    // create new video file
            //    vFWriter.Open("code-bude_test_video.avi", width, height, framRate, VideoCodec.Raw);

            //    foreach (var imageFrame in allImages)
            //    {
            //        vFWriter.WriteVideoFrame((Bitmap)imageFrame);
            //    }
            //    vFWriter.Close();
            //}
        }

       


        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            temp = (Image)eventArgs.Frame.Clone();
            Bitmap temp_img = (Bitmap)temp;

            grayTemp = temp_img.ToImage<Gray, byte>();
            Image<Gray, Byte> gray2Temp = temp_img.ToImage<Gray, byte>();
            previewPictureBoxF.previewPictureBox.Image = ScaleImage(gray2Temp.ToBitmap(), previewPictureBoxF.previewPictureBox.Width, previewPictureBoxF.previewPictureBox.Height);


            //Image oldBitmap = pictureBoxMain.Image;
            //temp = oldBitmap;
            //try
            //{
            //    lock (pictureBoxMain)
            //    {
            //        Bitmap myLocalBitmap = (Bitmap)eventArgs.Frame.Clone();
            //        pictureBoxMain.Image = myLocalBitmap;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Webcam frame error: " + ex.Message);
            //}
            //finally
            //{
            //    if (oldBitmap != null)
            //        oldBitmap.Dispose();
            //}
        }


        private void panelHeader_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                isMaximizeMode = true;
                this.WindowState = FormWindowState.Maximized;
                buttonMaxmize.BackgroundImage = Properties.Resources.maxSizeButtonIcon;
                if (isResultPanelShow)
                {
                    setResultPanelMaximize();
                    resultWindowStateChangesFirstImage();
                }
                else
                {
                    setMaximizePanel();

                }


                //for (int i = 0; i < panelBottomSide.Controls.Count; i++)
                //{
                //    bottomPanelLocationChange(panelBottomSide.Controls[i]);
                //}

            }
            else if (this.WindowState == FormWindowState.Maximized)
            {
                isMaximizeMode = false;
                this.WindowState = FormWindowState.Normal;
                buttonMaxmize.BackgroundImage = Properties.Resources.normalSizeButtonIcon;
                if (isResultPanelShow)
                {
                    setResultPanelMinimize();
                    resultWindowStateChangesFirstImage();
                }
                else
                {
                    setMinimizePanel();
                }

                //for (int i = 0; i < panelBottomSide.Controls.Count; i++)
                //{
                //    bottomPanelLocationChange(panelBottomSide.Controls[i]);
                //}

            }
        }


        private void resultWindowStateChangesFirstImage()
        {
            Image<Bgr, byte> first_image = createNewImage(1200, 900, 3);

            int total_slider_value = totalChannelSliderValue(resultForm.redButtonClicked, resultForm.greenButtonClicked, resultForm.blueButtonClicked, resultForm.whiteButtonClicked, resultForm.uvButtonClicked);

            if (resultForm.redButtonClicked)
            {

                int channel_value = Convert.ToInt32(resultTabRightSide.sliderRedChannel.Value);
                double red_channel_rate = calculateChannelMixerRate(channel_value, total_slider_value);
                first_image = channelMixer(redStackImageList[Convert.ToInt32(resultTabBottom.resultTrackBar.Value)], first_image, red_channel_rate, 1200, 900);

            }

            if (resultForm.greenButtonClicked)
            {

                int channel_value = Convert.ToInt32(resultTabRightSide.sliderGreenChannel.Value);
                double green_channel_rate = calculateChannelMixerRate(channel_value, total_slider_value);
                first_image = channelMixer(greenStackImageList[Convert.ToInt32(resultTabBottom.resultTrackBar.Value)], first_image, green_channel_rate, 1200, 900);

            }

            if (resultForm.blueButtonClicked)
            {

                int channel_value = Convert.ToInt32(resultTabRightSide.sliderBlueChannel.Value);
                double blue_channel_rate = calculateChannelMixerRate(channel_value, total_slider_value);
                first_image = channelMixer(blueStackImageList[Convert.ToInt32(resultTabBottom.resultTrackBar.Value)], first_image, blue_channel_rate, 1200, 900);

            }
            if (resultForm.whiteButtonClicked)
            {

                int channel_value = Convert.ToInt32(resultTabRightSide.sliderWhiteChannel.Value);
                double white_channel_rate = calculateChannelMixerRate(channel_value, total_slider_value);
                first_image = channelMixer(whiteStackImageList[Convert.ToInt32(resultTabBottom.resultTrackBar.Value)], first_image, white_channel_rate, 1200, 900);

            }
            if (resultForm.uvButtonClicked)
            {

                int channel_value = Convert.ToInt32(resultTabRightSide.sliderUvChannel.Value);
                double uv_channel_rate = calculateChannelMixerRate(channel_value, total_slider_value);
                first_image = channelMixer(uvStackImageList[Convert.ToInt32(resultTabBottom.resultTrackBar.Value)], first_image, uv_channel_rate, 1200, 900);

            }

            resultForm.mainPictureBox.Image = ScaleImage(first_image.ToBitmap(), resultForm.mainPictureBox.Width, resultForm.mainPictureBox.Height);
            resultTabBottom.label_trackbar.Text = (resultTabBottom.resultTrackBar.Value).ToString();
        }

        private void resultTrackbarChangeValue(object sender, EventArgs e)
        {
            Image<Bgr, byte> first_image = createNewImage(1200, 900, 3);

            int total_slider_value = totalChannelSliderValue(resultForm.redButtonClicked,resultForm.greenButtonClicked,resultForm.blueButtonClicked,resultForm.whiteButtonClicked,resultForm.uvButtonClicked);

            if (resultForm.redButtonClicked)
            {

                int channel_value = Convert.ToInt32(resultTabRightSide.sliderRedChannel.Value);
                double red_channel_rate = calculateChannelMixerRate(channel_value, total_slider_value);
                first_image = channelMixer(redStackImageList[Convert.ToInt32(resultTabBottom.resultTrackBar.Value)], first_image, red_channel_rate, 1200, 900);

            }

            if (resultForm.greenButtonClicked)
            {

                int channel_value = Convert.ToInt32(resultTabRightSide.sliderGreenChannel.Value);
                double green_channel_rate = calculateChannelMixerRate(channel_value, total_slider_value);
                first_image = channelMixer(greenStackImageList[Convert.ToInt32(resultTabBottom.resultTrackBar.Value)], first_image, green_channel_rate, 1200, 900);

            }

            if (resultForm.blueButtonClicked)
            {

                int channel_value = Convert.ToInt32(resultTabRightSide.sliderBlueChannel.Value);
                double blue_channel_rate = calculateChannelMixerRate(channel_value, total_slider_value);
                first_image = channelMixer(blueStackImageList[Convert.ToInt32(resultTabBottom.resultTrackBar.Value)], first_image, blue_channel_rate, 1200, 900);

            }
            if (resultForm.whiteButtonClicked)
            {

                int channel_value = Convert.ToInt32(resultTabRightSide.sliderWhiteChannel.Value);
                double white_channel_rate = calculateChannelMixerRate(channel_value, total_slider_value);
                first_image = channelMixer(whiteStackImageList[Convert.ToInt32(resultTabBottom.resultTrackBar.Value)], first_image, white_channel_rate, 1200, 900);

            }
            if (resultForm.uvButtonClicked)
            {

                int channel_value = Convert.ToInt32(resultTabRightSide.sliderUvChannel.Value);
                double uv_channel_rate = calculateChannelMixerRate(channel_value, total_slider_value);
                first_image = channelMixer(uvStackImageList[Convert.ToInt32(resultTabBottom.resultTrackBar.Value)], first_image, uv_channel_rate, 1200, 900);

            }

            resultForm.mainPictureBox.Image =ScaleImage(first_image.ToBitmap(),resultForm.mainPictureBox.Width,resultForm.mainPictureBox.Height);
            resultTabBottom.label_trackbar.Text = (resultTabBottom.resultTrackBar.Value).ToString();


        }


        private void setNewColorBalance()
        {
            Image<Bgr, byte> first_image = createNewImage(1200, 900, 3);

            int total_slider_value = totalChannelSliderValue(resultForm.redButtonClicked, resultForm.greenButtonClicked, resultForm.blueButtonClicked, resultForm.whiteButtonClicked, resultForm.uvButtonClicked);

            if (resultForm.redButtonClicked)
            {

                int channel_value = Convert.ToInt32(resultTabRightSide.sliderRedChannel.Value);
                double red_channel_rate = calculateChannelMixerRate(channel_value, total_slider_value);
                first_image = channelMixer(redStackImageList[Convert.ToInt32(resultTabBottom.resultTrackBar.Value)], first_image, red_channel_rate, 1200, 900);

            }

            if (resultForm.greenButtonClicked)
            {

                int channel_value = Convert.ToInt32(resultTabRightSide.sliderGreenChannel.Value);
                double green_channel_rate = calculateChannelMixerRate(channel_value, total_slider_value);
                first_image = channelMixer(greenStackImageList[Convert.ToInt32(resultTabBottom.resultTrackBar.Value)], first_image, green_channel_rate, 1200, 900);

            }

            if (resultForm.blueButtonClicked)
            {

                int channel_value = Convert.ToInt32(resultTabRightSide.sliderBlueChannel.Value);
                double blue_channel_rate = calculateChannelMixerRate(channel_value, total_slider_value);
                first_image = channelMixer(blueStackImageList[Convert.ToInt32(resultTabBottom.resultTrackBar.Value)], first_image, blue_channel_rate, 1200, 900);

            }
            if (resultForm.whiteButtonClicked)
            {

                int channel_value = Convert.ToInt32(resultTabRightSide.sliderWhiteChannel.Value);
                double white_channel_rate = calculateChannelMixerRate(channel_value, total_slider_value);
                first_image = channelMixer(whiteStackImageList[Convert.ToInt32(resultTabBottom.resultTrackBar.Value)], first_image, white_channel_rate, 1200, 900);

            }
            if (resultForm.uvButtonClicked)
            {

                int channel_value = Convert.ToInt32(resultTabRightSide.sliderUvChannel.Value);
                double uv_channel_rate = calculateChannelMixerRate(channel_value, total_slider_value);
                first_image = channelMixer(uvStackImageList[Convert.ToInt32(resultTabBottom.resultTrackBar.Value)], first_image, uv_channel_rate, 1200, 900);

            }

            resultForm.mainPictureBox.Image =ScaleImage(first_image.ToBitmap(),resultForm.mainPictureBox.Width,resultForm.mainPictureBox.Height) ;
        }


        private void resultFormRedButton(object sender, EventArgs e)
        {

            if (!resultForm.redButtonClicked)
            {
                resultForm.buttonRed.Image = Properties.Resources.redPressed;
                resultForm.redButtonClicked = true;
                resultTabRightSide.sliderRedChannel.Enabled = true;
               
            }
            else
            {
                resultForm.redButtonClicked = false;
                if (controlResultTabSelectedChannels(resultForm.redButtonClicked, resultForm.greenButtonClicked, resultForm.blueButtonClicked, resultForm.whiteButtonClicked, resultForm.uvButtonClicked))
                {
                    resultForm.redButtonClicked = true;
                }
                else
                {
                    resultForm.buttonRed.Image = Properties.Resources.redUnPressed;
                    resultForm.redButtonClicked = false;
                    resultTabRightSide.sliderRedChannel.Enabled = false;
                }
              
                
            }

            setSliderValues(resultForm.redButtonClicked, resultForm.greenButtonClicked, resultForm.blueButtonClicked, resultForm.whiteButtonClicked, resultForm.uvButtonClicked);
            setNewColorBalance();
        }
        private void resultFormGreenButton(object sender, EventArgs e)
        {
            if (!resultForm.greenButtonClicked)
            {
                resultForm.buttonGreen.Image = Properties.Resources.greenPressed;
                resultForm.greenButtonClicked = true;
                resultTabRightSide.sliderGreenChannel.Enabled = true;
               
            }
            else
            {
                resultForm.greenButtonClicked = false;
                if (controlResultTabSelectedChannels(resultForm.redButtonClicked, resultForm.greenButtonClicked, resultForm.blueButtonClicked, resultForm.whiteButtonClicked, resultForm.uvButtonClicked))
                {
                    resultForm.greenButtonClicked = true;
                }
                else
                {
                    resultForm.buttonGreen.Image = Properties.Resources.greenUnPressed;
                    resultForm.greenButtonClicked = false;
                    resultTabRightSide.sliderGreenChannel.Enabled = false;
                }
              
                
            }
            setSliderValues(resultForm.redButtonClicked, resultForm.greenButtonClicked, resultForm.blueButtonClicked, resultForm.whiteButtonClicked, resultForm.uvButtonClicked);
            setNewColorBalance();
        }
        private void resultFormBlueButton(object sender, EventArgs e)
        {
            if (!resultForm.blueButtonClicked)
            {
                resultForm.buttonBlue.Image = Properties.Resources.bluePressed;
                resultForm.blueButtonClicked = true;
                resultTabRightSide.sliderBlueChannel.Enabled = true;
               
            }
            else
            {
                resultForm.blueButtonClicked = false;
                if (controlResultTabSelectedChannels(resultForm.redButtonClicked, resultForm.greenButtonClicked, resultForm.blueButtonClicked, resultForm.whiteButtonClicked, resultForm.uvButtonClicked))
                {
                    resultForm.blueButtonClicked = true;
                }
                else
                {
                    resultForm.buttonBlue.Image = Properties.Resources.blueUnPressed;
                    resultForm.blueButtonClicked = false;
                    resultTabRightSide.sliderBlueChannel.Enabled = false;
                }
               
                
            }
            setSliderValues(resultForm.redButtonClicked, resultForm.greenButtonClicked, resultForm.blueButtonClicked, resultForm.whiteButtonClicked, resultForm.uvButtonClicked);
            setNewColorBalance();
        }
        private void resultFormWhiteButton(object sender, EventArgs e)
        {
            if (!resultForm.whiteButtonClicked)
            {
                resultForm.buttonWhite.Image = Properties.Resources.whitePressed;
                resultForm.whiteButtonClicked = true;
                resultTabRightSide.sliderWhiteChannel.BarInnerColor = Color.White;
                resultTabRightSide.sliderWhiteChannel.BarPenColorBottom = Color.White;
                resultTabRightSide.sliderWhiteChannel.BarPenColorTop = Color.White;
                resultTabRightSide.sliderWhiteChannel.ElapsedInnerColor = Color.White;
                resultTabRightSide.sliderWhiteChannel.ElapsedPenColorBottom = Color.White;
                resultTabRightSide.sliderWhiteChannel.ElapsedPenColorTop = Color.White;
                resultTabRightSide.sliderWhiteChannel.ForeColor = Color.White;
                resultTabRightSide.sliderWhiteChannel.ThumbOuterColor = Color.White;
                resultTabRightSide.sliderWhiteChannel.ThumbPenColor = Color.White; 
                resultTabRightSide.sliderWhiteChannel.ThumbInnerColor = Color.White;
                resultTabRightSide.sliderWhiteChannel.Enabled = true;
                
            }
            else
            {
                resultForm.whiteButtonClicked = false;
                if (controlResultTabSelectedChannels(resultForm.redButtonClicked, resultForm.greenButtonClicked, resultForm.blueButtonClicked, resultForm.whiteButtonClicked, resultForm.uvButtonClicked))
                {
                    resultForm.whiteButtonClicked = true;
                }
                else
                {
                    resultForm.buttonWhite.Image = Properties.Resources.whiteUnPressed;
                    resultForm.whiteButtonClicked = false;
                    resultTabRightSide.sliderWhiteChannel.BarInnerColor = Color.Gray;
                    resultTabRightSide.sliderWhiteChannel.BarPenColorBottom = Color.Gray;
                    resultTabRightSide.sliderWhiteChannel.BarPenColorTop = Color.Gray;
                    resultTabRightSide.sliderWhiteChannel.ElapsedInnerColor = Color.Gray;
                    resultTabRightSide.sliderWhiteChannel.ElapsedPenColorBottom = Color.Gray;
                    resultTabRightSide.sliderWhiteChannel.ElapsedPenColorTop = Color.Gray;
                    resultTabRightSide.sliderWhiteChannel.ForeColor = Color.Gray;
                    resultTabRightSide.sliderWhiteChannel.ThumbOuterColor = Color.Gray;
                    resultTabRightSide.sliderWhiteChannel.ThumbPenColor = Color.Gray;
                    resultTabRightSide.sliderWhiteChannel.ThumbInnerColor = Color.Gray;
                    resultTabRightSide.sliderWhiteChannel.Enabled = false;
                }
             
               
            }
            setSliderValues(resultForm.redButtonClicked, resultForm.greenButtonClicked, resultForm.blueButtonClicked, resultForm.whiteButtonClicked, resultForm.uvButtonClicked);
            setNewColorBalance();
        }
        private void resultFormUvButton(object sender, EventArgs e)
        {
            if (!resultForm.uvButtonClicked)
            {
                resultForm.buttonUv.Image = Properties.Resources.uv_button_pressed;
                resultForm.uvButtonClicked = true;
                resultTabRightSide.sliderUvChannel.Enabled = true;
                
            }
            else
            {
                resultForm.uvButtonClicked = false;
                if (controlResultTabSelectedChannels(resultForm.redButtonClicked, resultForm.greenButtonClicked, resultForm.blueButtonClicked, resultForm.whiteButtonClicked, resultForm.uvButtonClicked))
                {
                    resultForm.uvButtonClicked = true;
                }
                else
                {
                    resultForm.buttonUv.Image = Properties.Resources.uv_button_unpressed;
                    resultForm.uvButtonClicked = false;
                    resultTabRightSide.sliderUvChannel.Enabled = false;
                }
                    
                
            }
            setSliderValues(resultForm.redButtonClicked, resultForm.greenButtonClicked, resultForm.blueButtonClicked, resultForm.whiteButtonClicked, resultForm.uvButtonClicked);
            setNewColorBalance();
        }


        private void sliderRedValueChanged(object sender, EventArgs e)
        {
            setNewColorBalance();
        }
        private void sliderGreenValueChanged(object sender, EventArgs e)
        {
            setNewColorBalance();
        }
        private void sliderBlueValueChanged(object sender, EventArgs e)
        {
            setNewColorBalance();
        }
        private void sliderWhiteValueChanged(object sender, EventArgs e)
        {
            setNewColorBalance();
        }
        private void sliderUvValueChanged(object sender, EventArgs e)
        {
            setNewColorBalance();
        }






        private void cncExamples()
        {

            serialPortMicroFluor.WriteLine("$H");

            LoadingForm loadingForm_gui = new LoadingForm();
            loadingForm_gui.circularProgressBar.Maximum = 500;
            loadingForm_gui.circularProgressBar.Value = 0;
            loadingForm_gui.circularProgressBar.Text = "Please Wait";
            loadingForm_gui.Show();
            int value = 0;
            bool home_flag = true;
            while (home_flag)
            {
                value++;
                if (value > 500)
                {
                    value = 0;
                }
                loadingForm_gui.circularProgressBar.Value = value;
                if (value>100)
                {
                    serialPortMicroFluor.WriteLine("?");
                    for (int j = 0; j < 2; j++)
                    {
                        if (serialPortMicroFluor.BytesToRead > 0)
                        {

                            string f = serialPortMicroFluor.ReadExisting();
                            Debug.WriteLine(f);
                            if (f.IndexOf("Idle") != -1)
                            {

                                home_flag = false;

                            }

                        }


                    }
                }
                
                Thread.Sleep(40);
            }

            loadingForm_gui.Close();

        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            mainPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            mainPath += @"\" + "Microfluor";
            if (!Directory.Exists(mainPath))
            {
                DirectoryInfo di = Directory.CreateDirectory(mainPath);               
            }

            connectDevice();
            timer_position.Interval = 50;
            timer_position.Start();

            //cncExamples();

            z_stackTabRightSide.buttonStartStack.Click += ButtonStartStack_Click;
            timeLapseRightSide.buttonTimelapseStart.Click += ButtonTimeLapseStart_Click;
            liveTabRight.buttonAcquire.Click += ButtonAcquire_Click;
            resultTabBottom.resultTrackBar.ValueChanged += resultTrackbarChangeValue;
            resultForm.buttonRed.Click += resultFormRedButton;
            resultForm.buttonGreen.Click += resultFormGreenButton;
            resultForm.buttonBlue.Click += resultFormBlueButton;
            resultForm.buttonWhite.Click += resultFormWhiteButton;
            resultForm.buttonUv.Click += resultFormUvButton;
            resultTabRightSide.sliderRedChannel.ValueChanged += sliderRedValueChanged;
            resultTabRightSide.sliderGreenChannel.ValueChanged += sliderGreenValueChanged;
            resultTabRightSide.sliderBlueChannel.ValueChanged += sliderBlueValueChanged;
            resultTabRightSide.sliderWhiteChannel.ValueChanged += sliderWhiteValueChanged;
            resultTabRightSide.sliderUvChannel.ValueChanged += sliderUvValueChanged;

           
            stageTabRightSide.btn_Home.Click += btnHome_Click;

            stageTabRightSide.btn_Z_up.Click += btnUp_Click;
            stageTabRightSide.btn_Z_up.MouseDown += btnUp_MouseDown;
            stageTabRightSide.btn_Z_up.KeyDown += btnUp_KeyDown;
            stageTabRightSide.btn_Z_up.MouseUp += btnUp_MouseUp;
            stageTabRightSide.btn_Z_up.KeyUp += btnUp_KeyUp;

            stageTabRightSide.btn_Z_down.KeyDown += btnDown_KeyDown;
            stageTabRightSide.btn_Z_down.MouseDown += btnDown_MouseDown;
            stageTabRightSide.btn_Z_down.MouseUp += btnDown_MouseUp;
            stageTabRightSide.btn_Z_down.KeyUp += btnDown_KeyUp;


            stageTabRightSide.btn_X_up.MouseDown += btn_X_down_MouseDown;
            stageTabRightSide.btn_X_up.MouseUp += btn_X_down_MouseUp;

            stageTabRightSide.btn_X_down.MouseDown += btn_X_up_MouseDown;
            stageTabRightSide.btn_X_down.MouseUp += btn_X_up_MouseUp;


            stageTabRightSide.btn_Y_up.MouseDown += btn_Y_down_MouseDown;
            stageTabRightSide.btn_Y_up.MouseUp += btn_Y_down_MouseUp;

            stageTabRightSide.btn_Y_down.MouseDown += btn_Y_up_MouseDown;
            stageTabRightSide.btn_Y_down.MouseUp += btn_Y_up_MouseUp;

            stageTabRightSide.btn_coarse.Click += coarse_Clicked;
            stageTabRightSide.btn_fine.Click += fine_Clicked;
            stageTabRightSide.btn_stepMode.Click += stepMode_Clicked;

            

            stageTabRightSide.btn_go.Click += btn_go_Clicked;


            timeLapseRightSide.btn_Zstack.Click += timeLapseStackTabShow_Click;
            timeLapseRightSide.panel_zStack.Visible = false;


            setMinimizePanel();
            timer_device_control.Interval = 8 * 1000;
            timer_device_control.Start();

            buttonLive.BackgroundImage = Properties.Resources.preview_button_selected;
            videoSource = new VideoCaptureDevice();
            try
            {
                
                videoSource = new VideoCaptureDevice(optocell_cam.MonikerString);
                videoSource.NewFrame += new NewFrameEventHandler(VideoSource_NewFrame);
                videoSource.VideoResolution = videoSource.VideoCapabilities[5];
                videoSource.Start();
                
            }
            catch(Exception ex)
            {
                MessageBox.Show("There is no available camera found" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
            
        }


        // livetab bottom da bulunan acquire butonunun click eventini burada yakalıyorum...
        private void ButtonAcquire_Click(object sender, EventArgs e)
        {
            string subFolderAcquirePath = DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToLongTimeString();
            subFolderAcquirePath = subFolderAcquirePath.Replace('/', '-');
            subFolderAcquirePath = subFolderAcquirePath.Replace(':', '.');
            subFolderAcquirePath = @mainPath + "\\" + subFolderAcquirePath + "\\"+ "Acquire"+ "\\";
            if (!Directory.Exists(subFolderAcquirePath))
            {
                DirectoryInfo di = Directory.CreateDirectory(subFolderAcquirePath);
            }

            if (liveTabRight.redButtonClicked)
            {
                Image im = temp;
                im.Save(subFolderAcquirePath+"R.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
            }
            if (liveTabRight.greenButtonClicked)
            {
                Image im = temp;
                im.Save(subFolderAcquirePath + "G.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
            }
            if (liveTabRight.blueButtonClicked)
            {
                Image im = temp;
                im.Save(subFolderAcquirePath + "B.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
            }
            if (liveTabRight.whiteButtonClicked)
            {
                Image im = temp;
                im.Save(subFolderAcquirePath + "W.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
            }

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoSource.IsRunning)
            {
                videoSource.Stop();
            }
        }

        private void buttonLive_Click(object sender, EventArgs e)
        {
            isResultPanelShow = false;
            if (this.WindowState == FormWindowState.Normal)
            {
                setMinimizePanel();
            }
            else
            {
                setMaximizePanel();
            }
            changeTabButtonColor(tabButtonClickedControl);
            tabButtonClickedControl = 1;
            buttonLive.BackgroundImage = Properties.Resources.preview_button_selected;
            buttonLive.Cursor = Cursors.Hand;
            panelPictureBoxContainer.Controls["previewPictureBoxForm"].BringToFront();
           
           
            panelRightSide.Controls["UserControlLiveTabRightSide"].BringToFront();
            if (!videoSource.IsRunning)
            {
                videoSource.Start();
            }

        }

        private void setResultPanelMinimize()
        {
            panelPictureBoxContainer.Dock = DockStyle.None;
            panelPictureBoxContainer.Anchor = AnchorStyles.None;
            panelPictureBoxContainer.Width = 1200;
            panelPictureBoxContainer.Height = 100;
            resultForm.mainPictureBox.Width = 800;
            resultForm.mainPictureBox.Height = 600;
            resultForm.redPictureBox.Width = 150;
            resultForm.redPictureBox.Height = 100;
            resultForm.greenPictureBox.Width = 150;
            resultForm.greenPictureBox.Height = 100;
            resultForm.bluePictureBox.Width = 150;
            resultForm.bluePictureBox.Height = 100;
            resultForm.whitePictureBox.Width = 150;
            resultForm.whitePictureBox.Height = 100;
            resultForm.uvPictureBox.Width = 150;
            resultForm.uvPictureBox.Height = 100;
        }
        private void setResultPanelMaximize()
        {
            panelPictureBoxContainer.Dock = DockStyle.Fill;
            resultForm.mainPictureBox.Width = 1200;
            resultForm.mainPictureBox.Height = 900;
            resultForm.redPictureBox.Width = 200;
            resultForm.redPictureBox.Height = 200;
            resultForm.greenPictureBox.Width = 200;
            resultForm.greenPictureBox.Height = 200;
            resultForm.bluePictureBox.Width = 200;
            resultForm.bluePictureBox.Height = 200;
            resultForm.whitePictureBox.Width = 200;
            resultForm.whitePictureBox.Height = 200;
            resultForm.uvPictureBox.Width = 200;
            resultForm.uvPictureBox.Height = 200;

        }
        
        private void buttonResults_Click(object sender, EventArgs e)
        {
            
            saveDataForm svdForm2 = new saveDataForm();
            if (result_status==true)
            {
                isResultPanelShow = true;
                if (this.WindowState == FormWindowState.Normal)
                {
                    
                    setResultPanelMinimize();
                }
                else
                {
                    setResultPanelMaximize();
                }

                changeTabButtonColor(tabButtonClickedControl);
                tabButtonClickedControl = 4;
                buttonResults.BackgroundImage = Properties.Resources.result_button_selected;
                buttonResults.Cursor = Cursors.Hand;
                panelPictureBoxContainer.Controls["resultForm"].BringToFront();
                
                panelRightSide.Controls["UserControlResultTabRightSide"].BringToFront();
            }
            else
            {
                svdForm2.label_warning_message.Text = "No available data to be displayed.";
                svdForm2.btnOK.Visible = true;
                svdForm2.ShowDialog();
            }

        }

        private void buttonTimeLapse_Click(object sender, EventArgs e)
        {
            isResultPanelShow = false;
            changeTabButtonColor(tabButtonClickedControl);
            tabButtonClickedControl = 3;
            buttonTimeLapse.BackgroundImage = Properties.Resources.timelapse_button_selected;
            buttonTimeLapse.Cursor = Cursors.Hand;
            panelPictureBoxContainer.Controls["previewPictureBoxForm"].BringToFront();
            panelRightSide.Controls["UserControlTimeLapseRightSide"].BringToFront();
            
        }

        private void buttonZ_Stack_Click(object sender, EventArgs e)
        {
            isResultPanelShow = false;
            changeTabButtonColor(tabButtonClickedControl);
            tabButtonClickedControl = 2;
            buttonZ_Stack.BackgroundImage = Properties.Resources.Zstack_button_selected;
            buttonZ_Stack.Cursor = Cursors.Hand;
            panelPictureBoxContainer.Controls["previewPictureBoxForm"].BringToFront();
            
            panelRightSide.Controls["UserControlZStackRightSide"].BringToFront();


        }

       

        private void panelHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonMinimize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal || this.WindowState == FormWindowState.Maximized)
            {
                
                this.WindowState = FormWindowState.Minimized;
                //this.panelContainer.Refresh();
                //this.headerPanel.Refresh();
            }
        }

        private void getTimeLapseCapture()
        {
            if (red_timelapse)
            {
                string redImage = System.IO.Path.Combine(redFolder, start_measurement_timelapse.ToString() + ".jpeg");

                Mat blue_red = Mat.Zeros(2448, 3264, Emgu.CV.CvEnum.DepthType.Cv8U, 1);
                Mat green_red = Mat.Zeros(2448, 3264, Emgu.CV.CvEnum.DepthType.Cv8U, 1);
                Mat red_red = grayTemp.Mat;
                Mat red_channel_image = new Mat(2448, 3264, Emgu.CV.CvEnum.DepthType.Cv8U, 3);

                CvInvoke.Merge(new VectorOfMat(blue_red, green_red, red_red), red_channel_image);


                red_channel_image.ToBitmap().Save(redImage, System.Drawing.Imaging.ImageFormat.Jpeg);

                redStackImageList.Add(image_newResolution(red_channel_image.ToImage<Bgr, byte>(), 1200, 900));
            }


            ////////////////////////////////// GREEN CHANNEL /////////////


            /////////change led
            ///



            //////////////////

            if (green_timelapse)
            {
                string greenImage = System.IO.Path.Combine(greenFolder, start_measurement_timelapse.ToString() + ".jpeg");

                Mat blue_green = Mat.Zeros(2448, 3264, Emgu.CV.CvEnum.DepthType.Cv8U, 1);
                Mat green_green = grayTemp.Mat;
                Mat red_green = Mat.Zeros(2448, 3264, Emgu.CV.CvEnum.DepthType.Cv8U, 1);
                Mat green_channel_image = new Mat(2448, 3264, Emgu.CV.CvEnum.DepthType.Cv8U, 3);
                CvInvoke.Merge(new VectorOfMat(blue_green, green_green, red_green), green_channel_image);

                green_channel_image.ToBitmap().Save(greenImage, System.Drawing.Imaging.ImageFormat.Jpeg);
                greenStackImageList.Add(image_newResolution(green_channel_image.ToImage<Bgr, byte>(), 1200, 900));
            }



            ////////////////////////////////// BLUE CHANNEL /////////////


            /////////change led


            //////////////////
            if (blue_timelapse)
            {
                string blueImage = System.IO.Path.Combine(blueFolder, start_measurement_timelapse.ToString() + ".jpeg");



                Mat blue_blue = grayTemp.Mat;
                Mat green_blue = Mat.Zeros(2448, 3264, Emgu.CV.CvEnum.DepthType.Cv8U, 1);
                Mat red_blue = Mat.Zeros(2448, 3264, Emgu.CV.CvEnum.DepthType.Cv8U, 1);
                Mat blue_channel_image = new Mat(2448, 3264, Emgu.CV.CvEnum.DepthType.Cv8U, 3);

                CvInvoke.Merge(new VectorOfMat(blue_blue, green_blue, red_blue), blue_channel_image);

                blue_channel_image.ToBitmap().Save(blueImage, System.Drawing.Imaging.ImageFormat.Jpeg);
                blueStackImageList.Add(image_newResolution(blue_channel_image.ToImage<Bgr, byte>(), 1200, 900));
            }



            ////////////////////////////////// WHİTE CHANNEL /////////////


            /////////change led



            //////////////////
            ///


            if (white_timelapse)
            {

                string whiteImage = System.IO.Path.Combine(whiteFolder, start_measurement_timelapse.ToString() + ".jpeg");

                grayTemp.ToBitmap().Save(whiteImage, System.Drawing.Imaging.ImageFormat.Jpeg);
                Image<Bgr, byte> gray_rgb = new Image<Bgr, byte>(3264, 2448);
                CvInvoke.CvtColor(grayTemp, gray_rgb, Emgu.CV.CvEnum.ColorConversion.Gray2Rgb);
                whiteStackImageList.Add(image_newResolution(gray_rgb, 1200, 900));
            }



            ////////////////////////////////// UV CHANNEL /////////////


            /////////change led



            //////////////////

            if (uv_timelapse)
            {
                string uvImage = System.IO.Path.Combine(uvFolder, start_measurement_timelapse.ToString() + ".jpeg");

                Image<Bgr, byte> uv_channel_image = new Image<Bgr, byte>(3264, 2448);
                double red_parameter = 0.5;
                double green_parameter = 0;
                double blue_parameter = 0.5;


                for (int l = 0; l < 2448; l++)
                {
                    for (int m = 0; m < 3264; m++)
                    {
                        int gray_intensity = grayTemp.Data[l, m, 0];

                        uv_channel_image.Data[l, m, 0] = (byte)(gray_intensity * red_parameter);
                        uv_channel_image.Data[l, m, 1] = (byte)(gray_intensity * green_parameter);
                        uv_channel_image.Data[l, m, 2] = (byte)(gray_intensity * blue_parameter);
                    }
                }


                uv_channel_image.ToBitmap().Save(uvImage, System.Drawing.Imaging.ImageFormat.Jpeg);
                uvStackImageList.Add(image_newResolution(uv_channel_image, 1200, 900));
            }


            ////////////////////STACK STEP///////////////////
            ///

            ////////////////////////////////////

            start_measurement_timelapse++;

            //allImages.Add(previewPictureBoxF.previewPictureBox.Image);
        }

        private void finishTimeLapseProtocol()
        {
            if (red_timelapse)
            {
                resultForm.redPictureBox.Image = redStackImageList[0].ToBitmap();
                resultTabRightSide.sliderRedChannel.Enabled = true;
                resultForm.redButtonClicked = true;
                resultForm.buttonRed.Enabled = true;
                resultForm.buttonRed.Image = Properties.Resources.redPressed;

            }
            else
            {
                resultTabRightSide.sliderRedChannel.Enabled = false;
                resultForm.buttonRed.Enabled = false;
                resultForm.redButtonClicked = false;
                resultForm.buttonRed.Image = Properties.Resources.redUnPressed;
                //resultForm.buttonRed.Image = Properties.Resources.buttonRedDisable;
                resultForm.redPictureBox.Image = Properties.Resources.disableBlack;

            }


            ///////////////////////////
            if (green_timelapse)
            {
                resultForm.greenPictureBox.Image = greenStackImageList[0].ToBitmap();
                resultTabRightSide.sliderGreenChannel.Enabled = true;
                resultForm.greenButtonClicked = true;
                resultForm.buttonGreen.Enabled = true;
                resultForm.buttonGreen.Image = Properties.Resources.greenPressed;

            }
            else
            {
                resultTabRightSide.sliderGreenChannel.Enabled = false;
                resultForm.buttonGreen.Enabled = false;
                resultForm.greenButtonClicked = false;
                resultForm.buttonGreen.Image = Properties.Resources.greenUnPressed;
                //resultForm.buttonGreen.Image = Properties.Resources.buttonGreenDisable;
                resultForm.greenPictureBox.Image = Properties.Resources.disableBlack;

            }


            /////////////////////////
            if (blue_timelapse)
            {

                resultForm.bluePictureBox.Image = blueStackImageList[0].ToBitmap();
                resultTabRightSide.sliderBlueChannel.Enabled = true;
                resultForm.buttonBlue.Enabled = true;
                resultForm.blueButtonClicked = true;
                resultForm.buttonBlue.Image = Properties.Resources.bluePressed;

            }
            else
            {
                resultTabRightSide.sliderBlueChannel.Enabled = false;
                resultForm.buttonBlue.Enabled = false;
                resultForm.blueButtonClicked = false;
                resultForm.buttonBlue.Image = Properties.Resources.blueUnPressed;
                //resultForm.buttonBlue.Image = Properties.Resources.buttonBlueDisable;
                resultForm.bluePictureBox.Image = Properties.Resources.disableBlack;

            }

            /////////////////////////////
            if (white_timelapse)
            {

                resultForm.whitePictureBox.Image = whiteStackImageList[0].ToBitmap();
                resultTabRightSide.sliderWhiteChannel.BarInnerColor = Color.White;
                resultTabRightSide.sliderWhiteChannel.BarPenColorBottom = Color.White;
                resultTabRightSide.sliderWhiteChannel.BarPenColorTop = Color.White;
                resultTabRightSide.sliderWhiteChannel.ElapsedInnerColor = Color.White;
                resultTabRightSide.sliderWhiteChannel.ElapsedPenColorBottom = Color.White;
                resultTabRightSide.sliderWhiteChannel.ElapsedPenColorTop = Color.White;
                resultTabRightSide.sliderWhiteChannel.ForeColor = Color.White;
                resultTabRightSide.sliderWhiteChannel.ThumbOuterColor = Color.White;
                resultTabRightSide.sliderWhiteChannel.ThumbPenColor = Color.White;
                resultTabRightSide.sliderWhiteChannel.ThumbInnerColor = Color.White;
                resultTabRightSide.sliderWhiteChannel.Enabled = true;
                resultForm.whiteButtonClicked = true;
                resultForm.buttonWhite.Enabled = true;
                resultForm.buttonWhite.Image = Properties.Resources.whitePressed;

            }
            else
            {
                resultForm.buttonWhite.Image = Properties.Resources.buttonWhiteDisable;
                resultTabRightSide.sliderWhiteChannel.BarInnerColor = Color.Gray;
                resultTabRightSide.sliderWhiteChannel.BarPenColorBottom = Color.Gray;
                resultTabRightSide.sliderWhiteChannel.BarPenColorTop = Color.Gray;
                resultTabRightSide.sliderWhiteChannel.ElapsedInnerColor = Color.Gray;
                resultTabRightSide.sliderWhiteChannel.ElapsedPenColorBottom = Color.Gray;
                resultTabRightSide.sliderWhiteChannel.ElapsedPenColorTop = Color.Gray;
                resultTabRightSide.sliderWhiteChannel.ForeColor = Color.Gray;
                resultTabRightSide.sliderWhiteChannel.ThumbOuterColor = Color.Gray;
                resultTabRightSide.sliderWhiteChannel.ThumbPenColor = Color.Gray;
                resultTabRightSide.sliderWhiteChannel.ThumbInnerColor = Color.Gray;
                resultTabRightSide.sliderWhiteChannel.Enabled = false;
                resultForm.buttonWhite.Enabled = false;
                resultForm.whiteButtonClicked = false;
                //resultForm.buttonWhite.Image = Properties.Resources.buttonWhiteDisable;
                resultForm.whitePictureBox.Image = Properties.Resources.disableBlack;
            }

            ////////////////////////////////
            if (uv_timelapse)
            {

                resultForm.uvPictureBox.Image = uvStackImageList[0].ToBitmap();
                resultTabRightSide.sliderUvChannel.Enabled = true;
                resultForm.buttonUv.Enabled = true;
                resultForm.uvButtonClicked = true;
                resultForm.buttonUv.Image = Properties.Resources.uv_button_pressed;

            }
            else
            {
                resultTabRightSide.sliderUvChannel.Enabled = false;
                resultForm.buttonUv.Enabled = false;
                resultForm.uvButtonClicked = false;
                resultForm.buttonUv.Image = Properties.Resources.uv_button_unpressed;
                //resultForm.buttonUv.Image = Properties.Resources.buttonUvDisable;
                resultForm.uvPictureBox.Image = Properties.Resources.disableBlack;
            }


            ////////////////////
            ///




            //////////////////
            //


            setSliderValues(resultForm.redButtonClicked, resultForm.greenButtonClicked, resultForm.blueButtonClicked, resultForm.whiteButtonClicked, resultForm.uvButtonClicked);
            Image<Bgr, byte> first_image = createNewImage(1200, 900, 3);

            int total_slider_value = totalChannelSliderValue(red_timelapse, green_timelapse, blue_timelapse, white_timelapse, uv_timelapse);

            if (red_timelapse)
            {

                int channel_value = Convert.ToInt32(resultTabRightSide.sliderRedChannel.Value);
                double red_channel_rate = calculateChannelMixerRate(channel_value, total_slider_value);
                first_image = channelMixer(redStackImageList[0], first_image, red_channel_rate, 1200, 900);

            }

            if (green_timelapse)
            {

                int channel_value = Convert.ToInt32(resultTabRightSide.sliderGreenChannel.Value);
                double green_channel_rate = calculateChannelMixerRate(channel_value, total_slider_value);
                first_image = channelMixer(greenStackImageList[0], first_image, green_channel_rate, 1200, 900);

            }

            if (blue_timelapse)
            {

                int channel_value = Convert.ToInt32(resultTabRightSide.sliderBlueChannel.Value);
                double blue_channel_rate = calculateChannelMixerRate(channel_value, total_slider_value);
                first_image = channelMixer(blueStackImageList[0], first_image, blue_channel_rate, 1200, 900);

            }
            if (white_timelapse)
            {

                int channel_value = Convert.ToInt32(resultTabRightSide.sliderWhiteChannel.Value);
                double white_channel_rate = calculateChannelMixerRate(channel_value, total_slider_value);
                first_image = channelMixer(whiteStackImageList[0], first_image, white_channel_rate, 1200, 900);

            }
            if (uv_timelapse)
            {

                int channel_value = Convert.ToInt32(resultTabRightSide.sliderUvChannel.Value);
                double uv_channel_rate = calculateChannelMixerRate(channel_value, total_slider_value);
                first_image = channelMixer(uvStackImageList[0], first_image, uv_channel_rate, 1200, 900);

            }
            loadingForm_gui.Close();
            


            videoSource.Stop();
            videoSource.VideoResolution = videoSource.VideoCapabilities[5];

            videoSource.Start();
            saveDataForm svdForm2 = new saveDataForm();

            timer1.Stop();
            svdForm2.label_warning_message.Text = "DATA SAVED";
            svdForm2.btnOK.Visible = true;
            svdForm2.ShowDialog();
            isResultPanelShow = true;
            if (this.WindowState == FormWindowState.Normal)
            {
                setResultPanelMinimize();
            }
            else
            {
                setResultPanelMaximize();
            }
            resultForm.mainPictureBox.Image =ScaleImage( first_image.ToBitmap(),resultForm.mainPictureBox.Width,resultForm.mainPictureBox.Height);
            changeTabButtonColor(tabButtonClickedControl);
            tabButtonClickedControl = 4;
            buttonResults.BackgroundImage = Properties.Resources.result_button_selected;
            buttonResults.Cursor = Cursors.Hand;
            panelPictureBoxContainer.Controls["resultForm"].BringToFront();
            
            panelRightSide.Controls["UserControlResultTabRightSide"].BringToFront();
            result_selected_channel = 1;
            resultTabBottom.label_trackbar.Text = "0";
            result_status = true;
            
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            if (start_measurement_timelapse==measurement_timelapse-1)
            {
                getTimeLapseCapture();
                loadingForm_gui.circularProgressBar.Value = (start_measurement_timelapse + 1);
                loadingForm_gui.circularProgressBar.Text = (start_measurement_timelapse).ToString() + "/" + measurement_timelapse.ToString();
                finishTimeLapseProtocol();
                
            }
            else
            {

                getTimeLapseCapture();
                loadingForm_gui.circularProgressBar.Value = (start_measurement_timelapse + 1);
                loadingForm_gui.circularProgressBar.Text = (start_measurement_timelapse).ToString() + "/" + measurement_timelapse.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            videoSource.Stop();
            saveDataForm svdForm = new saveDataForm();
            svdForm.Opacity = 100;
            svdForm.label_warning_message.Text = "Please wait...";
            svdForm.btnOK.Visible = false;
            svdForm.Show();

            bool usb_camera_control = false;
            string cam_name = "USB Camera";

            captureDevice = new FilterInfoCollection(AForge.Video.DirectShow.FilterCategory.VideoInputDevice);

            foreach (AForge.Video.DirectShow.FilterInfo device in captureDevice)
            {
                if (device.Name == cam_name)
                {
                    optocell_capture_device = device;
                    usb_camera_control = true;
                    Debug.WriteLine(device.Name);
                }
            }


            
            if (usb_camera_control)
            {
                optocell_cam = optocell_capture_device;
                DsDevice[] capDevices = DsDevice.GetDevicesOfCat(DirectShowLib.FilterCategory.VideoInputDevice);
                DsDevice[] opto_cam;
                foreach (var item in capDevices)
                {
                    Debug.WriteLine(item.Mon.GetHashCode().ToString());

                    IFilterGraph2 graphBuilder = (IFilterGraph2)(new FilterGraph());
                    IBaseFilter capFilter = null;

                    int hr = graphBuilder.AddSourceFilterForMoniker(item.Mon, null, item.Name, out capFilter);

                    IAMVideoProcAmp cameraControl = capFilter as IAMVideoProcAmp;

                    cameraControl.Set(VideoProcAmpProperty.Saturation, 0, VideoProcAmpFlags.Manual);
                    Debug.WriteLine("Saturasyon degıstı");



                }
                connectDevice();
                videoSource = new VideoCaptureDevice();
                videoSource = new VideoCaptureDevice(optocell_cam.MonikerString);
                videoSource.VideoResolution = videoSource.VideoCapabilities[5];
                videoSource.NewFrame += new NewFrameEventHandler(VideoSource_NewFrame);

                activeDeviceButton();
                videoSource.Start();
                isDeviceDisconnect = false;
                timer_device_control.Start();
                svdForm.Close();

            }
            else
            {
                deactiveDeviceButton();
                svdForm.label_warning_message.Text = "Please check the connection";
                svdForm.btnOK.Visible = true;
            }

        }

        private void activeDeviceButton()
        {
          
        }

        private void deactiveDeviceButton()
        {
            

        }


        void changeTabButtonColor(byte buttonNumber)
        {
            switch (buttonNumber)
            {
                case 1:
                    buttonLive.BackgroundImage = Properties.Resources.preview_button;
                    buttonLive.Cursor = Cursors.Default;
                    break;
                case 2:
                    buttonZ_Stack.BackgroundImage = Properties.Resources.Zstack_button;
                    buttonZ_Stack.Cursor = Cursors.Default;
                    break;
                    
                case 3:
                    buttonTimeLapse.BackgroundImage = Properties.Resources.timelapse_button;
                    buttonTimeLapse.Cursor = Cursors.Default;
                    break;                  
                case 4:
                    buttonResults.BackgroundImage = Properties.Resources.result_button;
                    buttonResults.Cursor = Cursors.Default;
                    break;
               
            }
        }


        public void connectDevice()
        {
            coarse = true;
            //string[] ports = SerialPort.GetPortNames();
            //string a = "";
            serialPortMicroFluor.BaudRate = 115200;
            serialPortMicroFluor.Handshake = Handshake.None;
            serialPortMicroFluor.DtrEnable = true;
            serialPortMicroFluor.RtsEnable = true;
            //for (int i = 0; i < ports.Length; i++)
            //{
            //    Console.WriteLine(ports[i] + "\n");
            //}
            serialPortMicroFluor.PortName = "COM5";//Set your board COM
            if (serialPortMicroFluor.IsOpen == false)
            {
                serialPortMicroFluor.Open();
                Thread.Sleep(2000);
                Debug.WriteLine("deneme");
                

                //for (int i = 0; i < 5; i++)
                //{
                //    if (serialPortMicroFluor.BytesToRead > 0)
                //    {
                //        string f = serialPortMicroFluor.ReadExisting();
                //        Debug.WriteLine(f);
                //    }
                //    Thread.Sleep(200);

                //}

                //serialPortMicroFluor.WriteLine("G21G91Z-2F1000");
                //serialPortMicroFluor.WriteLine("G21G91Z-2F1000");
                //serialPortMicroFluor.WriteLine("!");

                //for (int i = 0; i < 10; i++)
                //{
                //    serialPortMicroFluor.WriteLine("?");
                //    if (serialPortMicroFluor.BytesToRead > 0)
                //    {
                //        string f = serialPortMicroFluor.ReadExisting();
                //        Debug.WriteLine(f);
                //    }
                //    Thread.Sleep(100);

                //}
                //Debug.WriteLine("Devam");
                //serialPortMicroFluor.WriteLine("G21G91Z-2F1000");
                //serialPortMicroFluor.WriteLine("G21G91Z-2F1000");
                
                //Thread.Sleep(1000);
                //serialPortMicroFluor.WriteLine("?");
                //for (int i = 0; i < 5; i++)
                //{
                //    if (serialPortMicroFluor.BytesToRead > 0)
                //    {
                //        string f = serialPortMicroFluor.ReadExisting();
                //        Debug.WriteLine(f);
                //    }
                //    Thread.Sleep(200);

                //}
                //serialPortMicroFluor.WriteLine("G21G91Z-2F1000");


            }
            else
            {
                serialPortMicroFluor.Close();
                serialPortMicroFluor.Open();
               

                port_flag = true;
            }
           
            MainForm.serialPortDevice = serialPortMicroFluor;

            if (coarse)
            {
                waittime = 1;
            }
            if (fine)
            {
                waittime = 50;
            }

            
        }

        private void timeLapseStackTabShow_Click(object sender, EventArgs e)
        {
            if (flag_TimeLapseZstackControl==false)
            {
                timeLapseRightSide.btn_Zstack.Image = Properties.Resources.whitePressed;
                flag_TimeLapseZstackControl = true;
                timeLapseRightSide.panel_zStack.Visible = true;
            }
            else
            {
                timeLapseRightSide.btn_Zstack.Image = Properties.Resources.whiteUnPressed;
                flag_TimeLapseZstackControl = false;
                timeLapseRightSide.panel_zStack.Visible = false;
            }
            
        }





        public void gonder1()
        {
            while (flag1)
            {
                serialPortMicroFluor.WriteLine("G91G01Z"+zStack+"F"+zStackFeedRate);
                Thread.Sleep(waittime);
            }
        }
        public void gonder2()
        {
            while (flag2)
            {
                serialPortMicroFluor.WriteLine("G91G01Z-" + zStack + "F" + zStackFeedRate);
                Thread.Sleep(waittime);
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            serialPortMicroFluor.WriteLine("G91G01Z-" + zStack + "F" + zStackFeedRate);
        }

        private void btnUp_MouseDown(object sender, MouseEventArgs e)
        {
            flag1 = true;
            serialPortDevice.Write("1\x18");
            serialPortDevice.WriteLine("~");
            var thread = new Thread(() => gonder1());
            thread.Start();
        }

        private void btnUp_KeyDown(object sender, KeyEventArgs e)
        {
            flag1 = true;
            serialPortDevice.Write("1\x18");
            serialPortDevice.WriteLine("~");
            var thread = new Thread(() => gonder1());
            thread.Start();
        }

        private void btnUp_MouseUp(object sender, MouseEventArgs e)
        {
            serialPortDevice.WriteLine("!");
            flag1 = false;
            
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void settingBtn_Click(object sender, EventArgs e)
        {
            FrmSetting sform = new FrmSetting();

            sform.ShowDialog();
            if (sform.save_status_flag)
            {

            }
        }

        private void timer_device_control_Tick(object sender, EventArgs e)
        {
            saveDataForm svdForm = new saveDataForm();
            isDeviceDisconnect = false;
            string cam_name = "USB Camera";

            captureDevice = new FilterInfoCollection(AForge.Video.DirectShow.FilterCategory.VideoInputDevice);

            foreach (AForge.Video.DirectShow.FilterInfo device in captureDevice)
            {
                if (device.Name == cam_name)
                {
                    isDeviceDisconnect = true;
                }
            }

            if (isDeviceDisconnect == false)
            {
                svdForm.label_warning_message.Text = "Please check the connection.";
                svdForm.btnOK.Visible = true;
                deactiveDeviceButton();
                svdForm.ShowDialog();
                timer_device_control.Stop();
            }
        }

       

        private void timer_position_Tick_1(object sender, EventArgs e)
        {
            serialPortMicroFluor.WriteLine("?");
            for (int j = 0; j < 2; j++)
            {
                if (serialPortMicroFluor.BytesToRead > 0)
                {

                    string f = serialPortMicroFluor.ReadExisting();
              
                    if (f.IndexOf("MPos:") != -1)
                    {
                        int pFrom = f.IndexOf("MPos:") + "MPos:".Length;
                        int pTo = f.LastIndexOf("FS:")-1;
                        if (pTo != -2)
                        {
                            String result = f.Substring(pFrom, pTo - pFrom);
                            
                            int x_pFrom = 0;
                            int x_to = result.IndexOf(',');

                            x = result.Substring(x_pFrom, x_to);

                            int y_pFrom = result.IndexOf(",") + ",".Length;
                            int y_to = result.LastIndexOf(",");
                            y = result.Substring(y_pFrom, y_to - y_pFrom);

                            z = result.Substring(y_to + 1);


                            stageTabBottomSide.label_x.Text = x;
                            stageTabBottomSide.label_y.Text = y;
                            stageTabBottomSide.label_z.Text = z;
                            
                            
                            if (result != position_isChanged)
                            {
                                position_isChanged = result;
                                stageTabRightSide.textBox_X.Text = x;
                                stageTabRightSide.textBox_Y.Text = y;
                                stageTabRightSide.textBox_Z.Text = z;
                                z_stackTabRightSide.textBox_stackStart.Text = z;
                                z_stackTabRightSide.textBox_stackEnd.Text = z;
                                timeLapseRightSide.textBox_stackStart.Text = z;
                                timeLapseRightSide.textBox_stackEnd.Text = z;
                            }


                        }
                      
                    }


                  
                    if (f.Length>10)
                    {
                        if (f.Substring(1, 5) == "Alarm")
                        {
                            serialPortMicroFluor.WriteLine("$X");
                        }
                    }
                   
                }

                Thread.Sleep(10);

            }
        }


        private void btnHome_Click(object sender,EventArgs e)
        {
            serialPortDevice.Write("1\x18");
            serialPortDevice.WriteLine("~");
            cncExamples();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            isResultPanelShow = false;
            changeTabButtonColor(tabButtonClickedControl);
            tabButtonClickedControl = 5;
            panelPictureBoxContainer.Controls["previewPictureBoxForm"].BringToFront();
            panelRightSide.Controls["UserControlCncStageRightSide"].BringToFront();

        }

        private void btnUp_KeyUp(object sender, KeyEventArgs e)
        {
            serialPortDevice.WriteLine("!");
            flag1 = false;
        }

        private void btnDown_KeyDown(object sender, KeyEventArgs e)
        {
            flag2 = true;
            serialPortDevice.Write("1\x18");
            serialPortDevice.WriteLine("~");
            var thread = new Thread(() => gonder2());
            thread.Start();
        }

        private void btnDown_MouseDown(object sender, MouseEventArgs e)
        {
            flag2 = true;
            serialPortDevice.Write("1\x18");
            serialPortDevice.WriteLine("~");
            var thread = new Thread(() => gonder2());
            thread.Start();
        }

        private void btnDown_MouseUp(object sender, MouseEventArgs e)
        {
            serialPortDevice.WriteLine("!");
            flag2 = false;
          
        }

        private void btnDown_KeyUp(object sender, KeyEventArgs e)
        {
            serialPortDevice.WriteLine("!");
            flag2 = false;

        }

        private void radioButtonCoarse_CheckedChanged(object sender, EventArgs e)
        {
            zStack = "1";
            zStackFeedRate = "1000";
            waittime = 1;
        }

        private void radioButtonFine_CheckedChanged(object sender, EventArgs e)
        {
            zStack = "0.001";
            zStackFeedRate = "50";
            waittime = 10;
        }



        public void goXp()
        {
            while (flagXp)
            {
                serialPortMicroFluor.WriteLine("G91G01X-" + xyStepSize + "F" + xyFeedRate);
                Debug.WriteLine("G91G01X-" + xyStepSize + "F" + xyFeedRate);
                Thread.Sleep(xyWaitTime);
            }
        }


        private void btn_X_up_MouseDown(object sender, MouseEventArgs e)
        {
            flagXp = true;
            serialPortDevice.Write("1\x18");
            serialPortDevice.WriteLine("~");
            var thread = new Thread(() => goXp());
            thread.Start();
        }

        private void btn_X_up_MouseUp(object sender,MouseEventArgs e)
        {
            serialPortDevice.WriteLine("!");
            flagXp = false;
        }


        public void goXn()
        {
            while (flagXn)
            {
                serialPortMicroFluor.WriteLine("G91G01X" + xyStepSize + "F" + xyFeedRate);
                Debug.WriteLine("G91G01X" + xyStepSize + "F" + xyFeedRate);
                Thread.Sleep(xyWaitTime);
            }
        }


        private void btn_X_down_MouseDown(object sender, MouseEventArgs e)
        {
            flagXn = true;
            serialPortDevice.Write("1\x18");
            serialPortDevice.WriteLine("~");
            var thread = new Thread(() => goXn());
            thread.Start();
        }

        private void btn_X_down_MouseUp(object sender, MouseEventArgs e)
        {
            serialPortDevice.WriteLine("!");
            flagXn = false;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        public void goYp()
        {
            while (flagYp)
            {
                serialPortMicroFluor.WriteLine("G91G01Y-" + xyStepSize + "F" + xyFeedRate);
                Thread.Sleep(xyWaitTime);
            }
        }


        private void btn_Y_up_MouseDown(object sender, MouseEventArgs e)
        {
            flagYp = true;
            serialPortDevice.Write("1\x18");
            serialPortDevice.WriteLine("~");
            var thread = new Thread(() => goYp());
            thread.Start();
        }

        private void btn_Y_up_MouseUp(object sender, MouseEventArgs e)
        {
            serialPortDevice.WriteLine("!");
            Thread.Sleep(10);
            serialPortDevice.WriteLine("~");
            flagYp = false;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        public void goYn()
        {
            while (flagYn)
            {
                serialPortMicroFluor.WriteLine("G91G01Y" + xyStepSize + "F" + xyFeedRate);
                Thread.Sleep(xyWaitTime);
            }

        }


        private void btn_Y_down_MouseDown(object sender, MouseEventArgs e)
        {
            flagYn = true;
            serialPortDevice.Write("1\x18");
            serialPortDevice.WriteLine("~");
            var thread = new Thread(() => goYn());
            thread.Start();
        }

        private void btn_Y_down_MouseUp(object sender, MouseEventArgs e)
        {
            serialPortDevice.WriteLine("!");
            Thread.Sleep(10);
            serialPortDevice.WriteLine("~");
            flagYn = false;
            //serialPortDevice.WriteLine("~");
        }



        private void coarse_Clicked(object sender, EventArgs e)
        {
            stageTabRightSide.btn_coarse.Image = Properties.Resources.whitePressed;
            stageTabRightSide.btn_fine.Image = Properties.Resources.whiteUnPressed;
            stageTabRightSide.btn_stepMode.Image = Properties.Resources.whiteUnPressed;
          
         
            xyStepSize = "2";
            xyFeedRate = "1000";
            xyWaitTime = 50;
            zStack = "0.2";
            zStackFeedRate = "1000";
            waittime = 15;

        }

        private void fine_Clicked(object sender, EventArgs e)
        {
            stageTabRightSide.btn_coarse.Image = Properties.Resources.whiteUnPressed;
            stageTabRightSide.btn_fine.Image = Properties.Resources.whitePressed;
            stageTabRightSide.btn_stepMode.Image = Properties.Resources.whiteUnPressed;
           
            xyStepSize = "0.5";
            xyFeedRate = "100";
            xyWaitTime = 15;
            zStack = "0.05";
            zStackFeedRate = "1";
            waittime = 15;
        }

        private void stepMode_Clicked(object sender, EventArgs e)
        {
            stageTabRightSide.btn_coarse.Image = Properties.Resources.whiteUnPressed;
            stageTabRightSide.btn_fine.Image = Properties.Resources.whiteUnPressed;
            stageTabRightSide.btn_stepMode.Image = Properties.Resources.whitePressed;
           
            xyStepSize = "0.001";
            xyFeedRate = "100";
            xyWaitTime = 200;
            zStack = "0.001";
            zStackFeedRate = "100";
            waittime = 200;
        }


        private void btn_go_Clicked(object sender, EventArgs e)
        {
            serialPortDevice.Write("1\x18");
            serialPortDevice.WriteLine("~");
            string new_x = stageTabRightSide.textBox_X.Text;
            string new_y = stageTabRightSide.textBox_Y.Text;
            string new_z = stageTabRightSide.textBox_Z.Text;
            new_x=new_x.Replace(",", ".");
            new_y=new_y.Replace(",", ".");
            new_z= new_z.Replace(",", ".");

            serialPortDevice.WriteLine("G90G01X" +new_x+ "Y" +new_y+ "Z" +new_z+ "F1000");
            
        }





    }
}
