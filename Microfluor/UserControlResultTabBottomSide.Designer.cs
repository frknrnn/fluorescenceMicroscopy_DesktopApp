namespace Microfluor
{
    partial class UserControlResultTabBottomSide
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
            this.label_trackbar = new System.Windows.Forms.Label();
            this.resultTrackBar = new Microfluor.ColorSlider();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_trackbar
            // 
            this.label_trackbar.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label_trackbar.AutoSize = true;
            this.label_trackbar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label_trackbar.ForeColor = System.Drawing.Color.White;
            this.label_trackbar.Location = new System.Drawing.Point(1091, 13);
            this.label_trackbar.Name = "label_trackbar";
            this.label_trackbar.Size = new System.Drawing.Size(40, 24);
            this.label_trackbar.TabIndex = 7;
            this.label_trackbar.Text = "300";
            // 
            // resultTrackBar
            // 
            this.resultTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.resultTrackBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(51)))), ((int)(((byte)(50)))));
            this.resultTrackBar.BarInnerColor = System.Drawing.Color.White;
            this.resultTrackBar.BarPenColorBottom = System.Drawing.Color.White;
            this.resultTrackBar.BarPenColorTop = System.Drawing.Color.White;
            this.resultTrackBar.BorderRoundRectSize = new System.Drawing.Size(8, 8);
            this.resultTrackBar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.resultTrackBar.ElapsedInnerColor = System.Drawing.Color.White;
            this.resultTrackBar.ElapsedPenColorBottom = System.Drawing.Color.White;
            this.resultTrackBar.ElapsedPenColorTop = System.Drawing.Color.White;
            this.resultTrackBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            this.resultTrackBar.ForeColor = System.Drawing.Color.White;
            this.resultTrackBar.LargeChange = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.resultTrackBar.Location = new System.Drawing.Point(130, 11);
            this.resultTrackBar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.resultTrackBar.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.resultTrackBar.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.resultTrackBar.MouseWheelBarPartitions = 1;
            this.resultTrackBar.Name = "resultTrackBar";
            this.resultTrackBar.ScaleDivisions = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.resultTrackBar.ScaleSubDivisions = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.resultTrackBar.ShowDivisionsText = true;
            this.resultTrackBar.ShowSmallScale = false;
            this.resultTrackBar.Size = new System.Drawing.Size(956, 28);
            this.resultTrackBar.SmallChange = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.resultTrackBar.TabIndex = 1;
            this.resultTrackBar.ThumbInnerColor = System.Drawing.Color.White;
            this.resultTrackBar.ThumbPenColor = System.Drawing.Color.White;
            this.resultTrackBar.ThumbRoundRectSize = new System.Drawing.Size(1, 1);
            this.resultTrackBar.ThumbSize = new System.Drawing.Size(6, 20);
            this.resultTrackBar.TickAdd = 0F;
            this.resultTrackBar.TickColor = System.Drawing.Color.Transparent;
            this.resultTrackBar.TickDivide = 0F;
            this.resultTrackBar.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Controls.Add(this.resultTrackBar, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label_trackbar, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1280, 50);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // UserControlResultTabBottomSide
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(51)))), ((int)(((byte)(50)))));
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UserControlResultTabBottomSide";
            this.Size = new System.Drawing.Size(1280, 50);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private ColorSlider colorSlider1;
        public System.Windows.Forms.Label label_trackbar;
        public ColorSlider resultTrackBar;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
