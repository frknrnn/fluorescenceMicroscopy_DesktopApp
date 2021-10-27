namespace Microfluor
{
    partial class previewPictureBoxForm
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.previewPictureBox = new zoomer.ModifiedPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.previewPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // previewPictureBox
            // 
            this.previewPictureBox.Image = global::Microfluor.Properties.Resources.floresans_startup_image;
            this.previewPictureBox.Location = new System.Drawing.Point(3, 3);
            this.previewPictureBox.Name = "previewPictureBox";
            this.previewPictureBox.Size = new System.Drawing.Size(1003, 639);
            this.previewPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.previewPictureBox.TabIndex = 2;
            this.previewPictureBox.TabStop = false;
            this.previewPictureBox.WaitOnLoad = true;
            // 
            // previewPictureBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(51)))), ((int)(((byte)(50)))));
            this.Controls.Add(this.previewPictureBox);
            this.Name = "previewPictureBoxForm";
            this.Size = new System.Drawing.Size(1009, 645);
            this.Load += new System.EventHandler(this.previewPictureBoxForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.previewPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public zoomer.ModifiedPictureBox previewPictureBox;
    }
}
