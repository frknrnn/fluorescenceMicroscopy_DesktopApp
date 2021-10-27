using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DirectShowLib.MultimediaStreaming;
using DirectShowLib.BDA;

namespace Microfluor
{
    public partial class defaultSettingForm : UserControl
    {
       
        public string dataFolderPath;
        public string capturesFolderPath;
        public string protocolsFolderPath;
        public Boolean radioBtnViabilityFlag;
        public int cellTypeIndex;
        public defaultSettingForm()
        {
            
            InitializeComponent();


            //labelDataFolder.Text = Properties.Settings.Default.DataFolder;
            //radioButtonViability.Checked = (Boolean)Properties.Settings.Default.Viability;
            //radioBtnViabilityFlag = radioButtonViability.Checked;
            //cell_comboBox.SelectedIndex = (int)Properties.Settings.Default.CellType;
            //labelCapturesFolder.Text = Properties.Settings.Default.CaptureFolder.ToString();
            //labelDataFolder.Text = Properties.Settings.Default.DataFolder.ToString();
            //labelProtocolFolder.Text = Properties.Settings.Default.ProtocolsFolder.ToString();
        }

      

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Custom Description";

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                
                dataFolderPath = fbd.SelectedPath;
                labelDataFolder.Text = dataFolderPath;
               
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Custom Description";

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                protocolsFolderPath = fbd.SelectedPath;
                labelProtocolFolder.Text = protocolsFolderPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Custom Description";

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                capturesFolderPath = fbd.SelectedPath;
                labelCapturesFolder.Text = capturesFolderPath;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void radioViabilityStatus()
        {
            if (radioBtnViabilityFlag == true)
            {
                radioButtonViability.Checked = true;
            }
            else
            {
                radioButtonViability.Checked = false;
            }
        }
        private void radioButton1_Click(object sender, EventArgs e)
        {
            if (radioBtnViabilityFlag == false)
            {
                radioBtnViabilityFlag = true;
            }
            else
            {
                radioBtnViabilityFlag = false;
            }
            radioViabilityStatus();
        }

        public bool check_changes()
        {
            bool changes = false;
            //if (radioButtonViability.Checked!=Properties.Settings.Default.Viability)
            //{
            //    changes = true;
            //}
            //if (cell_comboBox.SelectedIndex!=Properties.Settings.Default.CellType)
            //{
            //    changes = true;
            //}
            //if (labelDataFolder.Text!=Properties.Settings.Default.DataFolder)
            //{
            //    changes = true;
            //}
            //if (labelProtocolFolder.Text!=Properties.Settings.Default.ProtocolsFolder)
            //{
            //    changes = true;
            //}
            //if (labelCapturesFolder.Text!=Properties.Settings.Default.CaptureFolder)
            //{
            //    changes = true;
            //}
           

            return changes;
        }

        public void saveDefaultSettings()
        {
            
            //Properties.Settings.Default.Viability = radioButtonViability.Checked;
            //Properties.Settings.Default.CellType = cell_comboBox.SelectedIndex;
            //Properties.Settings.Default.DataFolder = labelDataFolder.Text;
            //Properties.Settings.Default.ProtocolsFolder = labelProtocolFolder.Text;
            //Properties.Settings.Default.CaptureFolder = labelCapturesFolder.Text ;
            //Properties.Settings.Default.DefaultSettings = false;
            //Properties.Settings.Default.Save();
        }

        public void resetDefaultSettings()
        {
            //Properties.Settings.Default.Viability = false;
            //Properties.Settings.Default.CellType = 1;
            //string userNAme = Environment.UserName;
            //string dataFolder = @"c:\Users\" + userNAme + @"\Documents";
            //string captureFolder = @"c:\Users\" + userNAme + @"\Documents";
            //string protocolFolder = @"c:\Users\" + userNAme + @"\Documents";
            //Properties.Settings.Default.DataFolder = dataFolder;
            //Properties.Settings.Default.CaptureFolder = captureFolder;
            //Properties.Settings.Default.ProtocolsFolder = protocolFolder;
            //Properties.Settings.Default.DefaultSettings = true;
            //Properties.Settings.Default.Save();
        }
    }
}
