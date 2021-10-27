using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Microfluor
{
    public partial class settingsWarningForm : Form
    {
        public Boolean user_answer { get; set; }
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn(
         int nLeftRect,
         int nTopRect,
         int RightRect,
         int nBottomRect,
         int nWidthEllipse,
         int nHeightEllipse


         );
        public settingsWarningForm()
        {
            InitializeComponent();
        }

        private void settingsWarningForm_Load(object sender, EventArgs e)
        {

        }


        private void btnYes_Click(object sender, EventArgs e)
        {
            user_answer = true;
            this.Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            user_answer = false;
            this.Close();
        }

        
    }
}
