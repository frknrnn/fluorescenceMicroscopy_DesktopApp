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
    public partial class FrmSetting : Form
    {

        defaultSettingForm dform = new defaultSettingForm();
        camOptForm cform = new camOptForm();
        public bool save_status_flag {get; set;}
        public Panel PnlContainer
        {
            get { return panel2; }
            set { panel2 = value; }
        }
        public FrmSetting()
        {
            InitializeComponent();
            camOpBtn.BackColor = Color.FromArgb(49, 51, 50);
            defaultBtn.BackColor = Color.FromArgb(0, 122, 204);
            dform.Dock = DockStyle.Fill;
            cform.Dock = DockStyle.Fill;
            panel2.Controls.Add(dform);
            panel2.Controls.Add(cform);
            save_status_flag = false;
            this.panel2.Controls["defaultSettingForm"].BringToFront();
            
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            settingsWarningForm warningForm = new settingsWarningForm();

            if (dform.check_changes())
            {
                warningForm.ShowDialog();
                if (warningForm.user_answer)
                {
                    dform.saveDefaultSettings();
                    save_status_flag = true;
                }
            }

            this.Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void defaultBtn_Click(object sender, EventArgs e)
        {
            camOpBtn.BackColor = Color.FromArgb(49, 51, 50);
            defaultBtn.BackColor = Color.FromArgb(0, 122, 204);
            this.panel2.Controls["defaultSettingForm"].BringToFront();
        }

        private void camOpBtn_Click(object sender, EventArgs e)
        {
            defaultBtn.BackColor = Color.FromArgb(49, 51, 50);
            camOpBtn.BackColor = Color.FromArgb(0, 122, 204);
            this.panel2.Controls["camOptForm"].BringToFront();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        

        private void clearBtn_Click(object sender, EventArgs e)
        {
            dform.resetDefaultSettings();
            save_status_flag = true;
            this.Close();
        }

        private void btnSettingsOk_Click(object sender, EventArgs e)
        {
            dform.saveDefaultSettings();
            save_status_flag = true;
            this.Close();
        }
    }
}
