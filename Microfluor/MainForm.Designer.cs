namespace Microfluor
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panelHeader = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.questionBtn = new System.Windows.Forms.Button();
            this.settingBtn = new System.Windows.Forms.Button();
            this.buttonMinimize = new System.Windows.Forms.Button();
            this.buttonMaxmize = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.pictureBoxMainLogo = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.serialPortMicroFluor = new System.IO.Ports.SerialPort(this.components);
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonLive = new System.Windows.Forms.Button();
            this.buttonTimeLapse = new System.Windows.Forms.Button();
            this.buttonZ_Stack = new System.Windows.Forms.Button();
            this.buttonResults = new System.Windows.Forms.Button();
            this.panelRightSide = new System.Windows.Forms.Panel();
            this.cncStagePanel = new System.Windows.Forms.TableLayoutPanel();
            this.panelPictureBoxContainer = new System.Windows.Forms.Panel();
            this.timer_device_control = new System.Windows.Forms.Timer(this.components);
            this.timer_position = new System.Windows.Forms.Timer(this.components);
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMainLogo)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.button1);
            this.panelHeader.Controls.Add(this.questionBtn);
            this.panelHeader.Controls.Add(this.settingBtn);
            this.panelHeader.Controls.Add(this.buttonMinimize);
            this.panelHeader.Controls.Add(this.buttonMaxmize);
            this.panelHeader.Controls.Add(this.buttonClose);
            this.panelHeader.Controls.Add(this.pictureBoxMainLogo);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1400, 46);
            this.panelHeader.TabIndex = 0;
            this.panelHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.panelHeader_Paint);
            this.panelHeader.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panelHeader_MouseDoubleClick);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI Light", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(251)))));
            this.button1.Location = new System.Drawing.Point(1034, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(166, 32);
            this.button1.TabIndex = 21;
            this.button1.Text = "Refresh Camera";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // questionBtn
            // 
            this.questionBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.questionBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.questionBtn.FlatAppearance.BorderSize = 0;
            this.questionBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.questionBtn.Font = new System.Drawing.Font("Segoe UI Light", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.questionBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(251)))));
            this.questionBtn.Location = new System.Drawing.Point(1221, 5);
            this.questionBtn.Name = "questionBtn";
            this.questionBtn.Size = new System.Drawing.Size(47, 38);
            this.questionBtn.TabIndex = 20;
            this.questionBtn.Text = "?";
            this.questionBtn.UseVisualStyleBackColor = true;
            // 
            // settingBtn
            // 
            this.settingBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.settingBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.settingBtn.FlatAppearance.BorderSize = 0;
            this.settingBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.settingBtn.Font = new System.Drawing.Font("Segoe UI Light", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.settingBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(251)))));
            this.settingBtn.Location = new System.Drawing.Point(936, 8);
            this.settingBtn.Name = "settingBtn";
            this.settingBtn.Size = new System.Drawing.Size(92, 33);
            this.settingBtn.TabIndex = 19;
            this.settingBtn.Text = "Settings";
            this.settingBtn.UseVisualStyleBackColor = true;
            this.settingBtn.Click += new System.EventHandler(this.settingBtn_Click);
            // 
            // buttonMinimize
            // 
            this.buttonMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMinimize.BackgroundImage = global::Microfluor.Properties.Resources.minimizeButtonIcon;
            this.buttonMinimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonMinimize.FlatAppearance.BorderSize = 0;
            this.buttonMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMinimize.Font = new System.Drawing.Font("Segoe UI Light", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonMinimize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(251)))));
            this.buttonMinimize.Location = new System.Drawing.Point(1274, 6);
            this.buttonMinimize.Name = "buttonMinimize";
            this.buttonMinimize.Size = new System.Drawing.Size(35, 37);
            this.buttonMinimize.TabIndex = 18;
            this.buttonMinimize.UseVisualStyleBackColor = true;
            this.buttonMinimize.Click += new System.EventHandler(this.buttonMinimize_Click);
            // 
            // buttonMaxmize
            // 
            this.buttonMaxmize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMaxmize.BackgroundImage = global::Microfluor.Properties.Resources.normalSizeButtonIcon;
            this.buttonMaxmize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonMaxmize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonMaxmize.FlatAppearance.BorderSize = 0;
            this.buttonMaxmize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMaxmize.Font = new System.Drawing.Font("Segoe UI Light", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonMaxmize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(251)))));
            this.buttonMaxmize.Location = new System.Drawing.Point(1315, 8);
            this.buttonMaxmize.Name = "buttonMaxmize";
            this.buttonMaxmize.Size = new System.Drawing.Size(35, 32);
            this.buttonMaxmize.TabIndex = 17;
            this.buttonMaxmize.UseVisualStyleBackColor = true;
            this.buttonMaxmize.Click += new System.EventHandler(this.buttonMaxmize_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.BackgroundImage = global::Microfluor.Properties.Resources.closeButtonIcon;
            this.buttonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonClose.FlatAppearance.BorderSize = 0;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Font = new System.Drawing.Font("Segoe UI Light", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(251)))));
            this.buttonClose.Location = new System.Drawing.Point(1356, 9);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(35, 31);
            this.buttonClose.TabIndex = 16;
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // pictureBoxMainLogo
            // 
            this.pictureBoxMainLogo.Image = global::Microfluor.Properties.Resources.miniFluor_pageLogo;
            this.pictureBoxMainLogo.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxMainLogo.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxMainLogo.Name = "pictureBoxMainLogo";
            this.pictureBoxMainLogo.Size = new System.Drawing.Size(447, 44);
            this.pictureBoxMainLogo.TabIndex = 15;
            this.pictureBoxMainLogo.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.panelHeader, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1400, 700);
            this.tableLayoutPanel3.TabIndex = 13;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 280F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.cncStagePanel, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panelPictureBoxContainer, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 49);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1394, 648);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.panelRightSide, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(1114, 5);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(280, 643);
            this.tableLayoutPanel4.TabIndex = 11;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.buttonLive, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.buttonTimeLapse, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.buttonZ_Stack, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.buttonResults, 0, 3);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 4;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(28, 643);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // buttonLive
            // 
            this.buttonLive.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonLive.BackgroundImage")));
            this.buttonLive.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonLive.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonLive.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonLive.FlatAppearance.BorderSize = 0;
            this.buttonLive.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonLive.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonLive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLive.Font = new System.Drawing.Font("Segoe UI Light", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonLive.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(251)))));
            this.buttonLive.Location = new System.Drawing.Point(3, 0);
            this.buttonLive.Margin = new System.Windows.Forms.Padding(0);
            this.buttonLive.Name = "buttonLive";
            this.buttonLive.Size = new System.Drawing.Size(25, 160);
            this.buttonLive.TabIndex = 7;
            this.buttonLive.UseVisualStyleBackColor = true;
            this.buttonLive.Click += new System.EventHandler(this.buttonLive_Click);
            // 
            // buttonTimeLapse
            // 
            this.buttonTimeLapse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonTimeLapse.BackgroundImage")));
            this.buttonTimeLapse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonTimeLapse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonTimeLapse.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonTimeLapse.FlatAppearance.BorderSize = 0;
            this.buttonTimeLapse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonTimeLapse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonTimeLapse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTimeLapse.Font = new System.Drawing.Font("Segoe UI Light", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonTimeLapse.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(251)))));
            this.buttonTimeLapse.Location = new System.Drawing.Point(3, 320);
            this.buttonTimeLapse.Margin = new System.Windows.Forms.Padding(0);
            this.buttonTimeLapse.Name = "buttonTimeLapse";
            this.buttonTimeLapse.Size = new System.Drawing.Size(25, 160);
            this.buttonTimeLapse.TabIndex = 9;
            this.buttonTimeLapse.UseVisualStyleBackColor = true;
            this.buttonTimeLapse.Click += new System.EventHandler(this.buttonTimeLapse_Click);
            // 
            // buttonZ_Stack
            // 
            this.buttonZ_Stack.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonZ_Stack.BackgroundImage")));
            this.buttonZ_Stack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonZ_Stack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonZ_Stack.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonZ_Stack.FlatAppearance.BorderSize = 0;
            this.buttonZ_Stack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonZ_Stack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonZ_Stack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonZ_Stack.Font = new System.Drawing.Font("Segoe UI Light", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonZ_Stack.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(251)))));
            this.buttonZ_Stack.Location = new System.Drawing.Point(3, 160);
            this.buttonZ_Stack.Margin = new System.Windows.Forms.Padding(0);
            this.buttonZ_Stack.Name = "buttonZ_Stack";
            this.buttonZ_Stack.Size = new System.Drawing.Size(25, 160);
            this.buttonZ_Stack.TabIndex = 6;
            this.buttonZ_Stack.UseVisualStyleBackColor = true;
            this.buttonZ_Stack.Click += new System.EventHandler(this.buttonZ_Stack_Click);
            // 
            // buttonResults
            // 
            this.buttonResults.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonResults.BackgroundImage")));
            this.buttonResults.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonResults.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonResults.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonResults.FlatAppearance.BorderSize = 0;
            this.buttonResults.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonResults.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonResults.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonResults.Font = new System.Drawing.Font("Segoe UI Light", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonResults.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(251)))));
            this.buttonResults.Location = new System.Drawing.Point(3, 480);
            this.buttonResults.Margin = new System.Windows.Forms.Padding(0);
            this.buttonResults.Name = "buttonResults";
            this.buttonResults.Size = new System.Drawing.Size(25, 163);
            this.buttonResults.TabIndex = 8;
            this.buttonResults.UseVisualStyleBackColor = true;
            this.buttonResults.Click += new System.EventHandler(this.buttonResults_Click);
            // 
            // panelRightSide
            // 
            this.panelRightSide.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRightSide.Location = new System.Drawing.Point(38, 0);
            this.panelRightSide.Margin = new System.Windows.Forms.Padding(10, 0, 5, 0);
            this.panelRightSide.Name = "panelRightSide";
            this.panelRightSide.Size = new System.Drawing.Size(237, 643);
            this.panelRightSide.TabIndex = 3;
            // 
            // cncStagePanel
            // 
            this.cncStagePanel.ColumnCount = 1;
            this.cncStagePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.cncStagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cncStagePanel.Location = new System.Drawing.Point(5, 0);
            this.cncStagePanel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.cncStagePanel.Name = "cncStagePanel";
            this.cncStagePanel.RowCount = 1;
            this.cncStagePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.cncStagePanel.Size = new System.Drawing.Size(240, 648);
            this.cncStagePanel.TabIndex = 13;
            // 
            // panelPictureBoxContainer
            // 
            this.panelPictureBoxContainer.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelPictureBoxContainer.Location = new System.Drawing.Point(257, 6);
            this.panelPictureBoxContainer.Name = "panelPictureBoxContainer";
            this.panelPictureBoxContainer.Size = new System.Drawing.Size(850, 635);
            this.panelPictureBoxContainer.TabIndex = 14;
            // 
            // timer_device_control
            // 
            this.timer_device_control.Tick += new System.EventHandler(this.timer_device_control_Tick);
            // 
            // timer_position
            // 
            this.timer_position.Tick += new System.EventHandler(this.timer_position_Tick_1);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(51)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(1400, 700);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMainLogo)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.PictureBox pictureBoxMainLogo;
        private System.Windows.Forms.Button buttonMinimize;
        private System.Windows.Forms.Button buttonMaxmize;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button questionBtn;
        private System.Windows.Forms.Button settingBtn;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button1;
        private System.IO.Ports.SerialPort serialPortMicroFluor;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Timer timer_device_control;
        private System.Windows.Forms.Timer timer_position;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Button buttonLive;
        private System.Windows.Forms.Button buttonTimeLapse;
        private System.Windows.Forms.Button buttonResults;
        private System.Windows.Forms.Button buttonZ_Stack;
        private System.Windows.Forms.Panel panelRightSide;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel cncStagePanel;
        private System.Windows.Forms.Panel panelPictureBoxContainer;
    }
}