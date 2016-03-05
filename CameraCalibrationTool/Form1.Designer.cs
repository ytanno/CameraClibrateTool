namespace CameraCalibrationTool
{
	partial class Form1
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.CaptureTab = new System.Windows.Forms.TabPage();
			this.captureBox = new System.Windows.Forms.PictureBox();
			this.CaptureButton = new System.Windows.Forms.Button();
			this.DetectTab = new System.Windows.Forms.TabPage();
			this.detectPictureBox = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.detectComboBox = new System.Windows.Forms.ComboBox();
			this.ReadBoardImage = new System.Windows.Forms.Button();
			this.CalibrationTab = new System.Windows.Forms.TabPage();
			this.CalibrateButton = new System.Windows.Forms.Button();
			this.resultTab = new System.Windows.Forms.TabPage();
			this.label2 = new System.Windows.Forms.Label();
			this.compareRemapPic = new System.Windows.Forms.PictureBox();
			this.compareRawPic = new System.Windows.Forms.PictureBox();
			this.ReadClibButton = new System.Windows.Forms.Button();
			this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
			this.captureTimer = new System.Windows.Forms.Timer(this.components);
			this.compareTimer = new System.Windows.Forms.Timer(this.components);
			this.tabControl1.SuspendLayout();
			this.CaptureTab.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.captureBox)).BeginInit();
			this.DetectTab.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.detectPictureBox)).BeginInit();
			this.CalibrationTab.SuspendLayout();
			this.resultTab.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.compareRemapPic)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.compareRawPic)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.CaptureTab);
			this.tabControl1.Controls.Add(this.DetectTab);
			this.tabControl1.Controls.Add(this.CalibrationTab);
			this.tabControl1.Controls.Add(this.resultTab);
			this.tabControl1.Location = new System.Drawing.Point(12, 12);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(370, 324);
			this.tabControl1.TabIndex = 0;
			// 
			// CaptureTab
			// 
			this.CaptureTab.Controls.Add(this.captureBox);
			this.CaptureTab.Controls.Add(this.CaptureButton);
			this.CaptureTab.Location = new System.Drawing.Point(4, 22);
			this.CaptureTab.Name = "CaptureTab";
			this.CaptureTab.Padding = new System.Windows.Forms.Padding(3);
			this.CaptureTab.Size = new System.Drawing.Size(362, 298);
			this.CaptureTab.TabIndex = 0;
			this.CaptureTab.Text = "Capture";
			this.CaptureTab.UseVisualStyleBackColor = true;
			// 
			// captureBox
			// 
			this.captureBox.Location = new System.Drawing.Point(15, 35);
			this.captureBox.Name = "captureBox";
			this.captureBox.Size = new System.Drawing.Size(320, 240);
			this.captureBox.TabIndex = 1;
			this.captureBox.TabStop = false;
			// 
			// CaptureButton
			// 
			this.CaptureButton.Location = new System.Drawing.Point(6, 6);
			this.CaptureButton.Name = "CaptureButton";
			this.CaptureButton.Size = new System.Drawing.Size(75, 23);
			this.CaptureButton.TabIndex = 0;
			this.CaptureButton.Text = "Capture";
			this.CaptureButton.UseVisualStyleBackColor = true;
			this.CaptureButton.Click += new System.EventHandler(this.CaptureButton_Click);
			// 
			// DetectTab
			// 
			this.DetectTab.Controls.Add(this.detectPictureBox);
			this.DetectTab.Controls.Add(this.label1);
			this.DetectTab.Controls.Add(this.detectComboBox);
			this.DetectTab.Controls.Add(this.ReadBoardImage);
			this.DetectTab.Location = new System.Drawing.Point(4, 22);
			this.DetectTab.Name = "DetectTab";
			this.DetectTab.Padding = new System.Windows.Forms.Padding(3);
			this.DetectTab.Size = new System.Drawing.Size(362, 298);
			this.DetectTab.TabIndex = 1;
			this.DetectTab.Text = "Detect";
			this.DetectTab.UseVisualStyleBackColor = true;
			// 
			// detectPictureBox
			// 
			this.detectPictureBox.Location = new System.Drawing.Point(81, 63);
			this.detectPictureBox.Name = "detectPictureBox";
			this.detectPictureBox.Size = new System.Drawing.Size(275, 229);
			this.detectPictureBox.TabIndex = 3;
			this.detectPictureBox.TabStop = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(113, 11);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(115, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Only JPG Image Folder";
			// 
			// detectComboBox
			// 
			this.detectComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.detectComboBox.FormattingEnabled = true;
			this.detectComboBox.Location = new System.Drawing.Point(6, 35);
			this.detectComboBox.Name = "detectComboBox";
			this.detectComboBox.Size = new System.Drawing.Size(222, 21);
			this.detectComboBox.TabIndex = 1;
			// 
			// ReadBoardImage
			// 
			this.ReadBoardImage.Location = new System.Drawing.Point(6, 6);
			this.ReadBoardImage.Name = "ReadBoardImage";
			this.ReadBoardImage.Size = new System.Drawing.Size(75, 23);
			this.ReadBoardImage.TabIndex = 0;
			this.ReadBoardImage.Text = "Read";
			this.ReadBoardImage.UseVisualStyleBackColor = true;
			this.ReadBoardImage.Click += new System.EventHandler(this.ReadBoardImage_Click);
			// 
			// CalibrationTab
			// 
			this.CalibrationTab.Controls.Add(this.CalibrateButton);
			this.CalibrationTab.Location = new System.Drawing.Point(4, 22);
			this.CalibrationTab.Name = "CalibrationTab";
			this.CalibrationTab.Padding = new System.Windows.Forms.Padding(3);
			this.CalibrationTab.Size = new System.Drawing.Size(362, 298);
			this.CalibrationTab.TabIndex = 2;
			this.CalibrationTab.Text = "Calibrate";
			this.CalibrationTab.UseVisualStyleBackColor = true;
			// 
			// CalibrateButton
			// 
			this.CalibrateButton.Location = new System.Drawing.Point(17, 18);
			this.CalibrateButton.Name = "CalibrateButton";
			this.CalibrateButton.Size = new System.Drawing.Size(328, 23);
			this.CalibrateButton.TabIndex = 0;
			this.CalibrateButton.Text = "Calibrate Set Folder";
			this.CalibrateButton.UseVisualStyleBackColor = true;
			this.CalibrateButton.Click += new System.EventHandler(this.CalibrateButton_Click);
			// 
			// resultTab
			// 
			this.resultTab.Controls.Add(this.label2);
			this.resultTab.Controls.Add(this.compareRemapPic);
			this.resultTab.Controls.Add(this.compareRawPic);
			this.resultTab.Controls.Add(this.ReadClibButton);
			this.resultTab.Location = new System.Drawing.Point(4, 22);
			this.resultTab.Name = "resultTab";
			this.resultTab.Padding = new System.Windows.Forms.Padding(3);
			this.resultTab.Size = new System.Drawing.Size(362, 298);
			this.resultTab.TabIndex = 3;
			this.resultTab.Text = "Result";
			this.resultTab.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(176, 11);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(101, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "←Raw  　Remap→";
			// 
			// compareRemapPic
			// 
			this.compareRemapPic.Location = new System.Drawing.Point(205, 35);
			this.compareRemapPic.Name = "compareRemapPic";
			this.compareRemapPic.Size = new System.Drawing.Size(151, 257);
			this.compareRemapPic.TabIndex = 2;
			this.compareRemapPic.TabStop = false;
			// 
			// compareRawPic
			// 
			this.compareRawPic.Location = new System.Drawing.Point(6, 35);
			this.compareRawPic.Name = "compareRawPic";
			this.compareRawPic.Size = new System.Drawing.Size(183, 257);
			this.compareRawPic.TabIndex = 1;
			this.compareRawPic.TabStop = false;
			// 
			// ReadClibButton
			// 
			this.ReadClibButton.Location = new System.Drawing.Point(6, 6);
			this.ReadClibButton.Name = "ReadClibButton";
			this.ReadClibButton.Size = new System.Drawing.Size(147, 23);
			this.ReadClibButton.TabIndex = 0;
			this.ReadClibButton.Text = "Read Calibration XML";
			this.ReadClibButton.UseVisualStyleBackColor = true;
			this.ReadClibButton.Click += new System.EventHandler(this.ReadClibButton_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(404, 349);
			this.Controls.Add(this.tabControl1);
			this.Name = "Form1";
			this.Text = "yt_CameraCalibrationTool";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.tabControl1.ResumeLayout(false);
			this.CaptureTab.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.captureBox)).EndInit();
			this.DetectTab.ResumeLayout(false);
			this.DetectTab.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.detectPictureBox)).EndInit();
			this.CalibrationTab.ResumeLayout(false);
			this.resultTab.ResumeLayout(false);
			this.resultTab.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.compareRemapPic)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.compareRawPic)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage CaptureTab;
		private System.Windows.Forms.Button CaptureButton;
		private System.Windows.Forms.TabPage DetectTab;
		private System.Windows.Forms.TabPage CalibrationTab;
		private System.Windows.Forms.BindingSource bindingSource1;
		private System.Windows.Forms.TabPage resultTab;
		private System.Windows.Forms.PictureBox captureBox;
		private System.Windows.Forms.Timer captureTimer;
		private System.Windows.Forms.Button ReadBoardImage;
		private System.Windows.Forms.ComboBox detectComboBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox detectPictureBox;
		private System.Windows.Forms.Button CalibrateButton;
		private System.Windows.Forms.PictureBox compareRemapPic;
		private System.Windows.Forms.PictureBox compareRawPic;
		private System.Windows.Forms.Button ReadClibButton;
		private System.Windows.Forms.Timer compareTimer;
		private System.Windows.Forms.Label label2;
	}
}

