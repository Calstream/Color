namespace Color1
{
	partial class Sliders
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
			this.hue_trackBar = new System.Windows.Forms.TrackBar();
			this.saturation_trackBar = new System.Windows.Forms.TrackBar();
			this.value_trackBar = new System.Windows.Forms.TrackBar();
			this.hue_label = new System.Windows.Forms.Label();
			this.sat_label = new System.Windows.Forms.Label();
			this.val_label = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.hue_trackBar)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.saturation_trackBar)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.value_trackBar)).BeginInit();
			this.SuspendLayout();
			// 
			// hue_trackBar
			// 
			this.hue_trackBar.LargeChange = 60;
			this.hue_trackBar.Location = new System.Drawing.Point(91, 12);
			this.hue_trackBar.Maximum = 360;
			this.hue_trackBar.Name = "hue_trackBar";
			this.hue_trackBar.Size = new System.Drawing.Size(174, 45);
			this.hue_trackBar.SmallChange = 10;
			this.hue_trackBar.TabIndex = 0;
			this.hue_trackBar.Scroll += new System.EventHandler(this.trackbar_scroll);
			// 
			// saturation_trackBar
			// 
			this.saturation_trackBar.LargeChange = 20;
			this.saturation_trackBar.Location = new System.Drawing.Point(91, 69);
			this.saturation_trackBar.Maximum = 100;
			this.saturation_trackBar.Minimum = -100;
			this.saturation_trackBar.Name = "saturation_trackBar";
			this.saturation_trackBar.Size = new System.Drawing.Size(174, 45);
			this.saturation_trackBar.SmallChange = 10;
			this.saturation_trackBar.TabIndex = 1;
			this.saturation_trackBar.Scroll += new System.EventHandler(this.trackbar_scroll);
			// 
			// value_trackBar
			// 
			this.value_trackBar.LargeChange = 90;
			this.value_trackBar.Location = new System.Drawing.Point(91, 120);
			this.value_trackBar.Maximum = 100;
			this.value_trackBar.Minimum = -100;
			this.value_trackBar.Name = "value_trackBar";
			this.value_trackBar.Size = new System.Drawing.Size(174, 45);
			this.value_trackBar.SmallChange = 80;
			this.value_trackBar.TabIndex = 2;
			this.value_trackBar.Scroll += new System.EventHandler(this.trackbar_scroll);
			// 
			// hue_label
			// 
			this.hue_label.AutoSize = true;
			this.hue_label.Location = new System.Drawing.Point(12, 21);
			this.hue_label.Name = "hue_label";
			this.hue_label.Size = new System.Drawing.Size(27, 13);
			this.hue_label.TabIndex = 3;
			this.hue_label.Text = "Hue";
			// 
			// sat_label
			// 
			this.sat_label.AutoSize = true;
			this.sat_label.Location = new System.Drawing.Point(12, 69);
			this.sat_label.Name = "sat_label";
			this.sat_label.Size = new System.Drawing.Size(55, 13);
			this.sat_label.TabIndex = 4;
			this.sat_label.Text = "Saturation";
			// 
			// val_label
			// 
			this.val_label.AutoSize = true;
			this.val_label.Location = new System.Drawing.Point(12, 120);
			this.val_label.Name = "val_label";
			this.val_label.Size = new System.Drawing.Size(56, 13);
			this.val_label.TabIndex = 5;
			this.val_label.Text = "Brightness";
			// 
			// Sliders
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(283, 175);
			this.Controls.Add(this.val_label);
			this.Controls.Add(this.sat_label);
			this.Controls.Add(this.hue_label);
			this.Controls.Add(this.value_trackBar);
			this.Controls.Add(this.saturation_trackBar);
			this.Controls.Add(this.hue_trackBar);
			this.Name = "Sliders";
			this.Text = "Sliders";
			((System.ComponentModel.ISupportInitialize)(this.hue_trackBar)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.saturation_trackBar)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.value_trackBar)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TrackBar hue_trackBar;
		private System.Windows.Forms.TrackBar saturation_trackBar;
		private System.Windows.Forms.TrackBar value_trackBar;
		private System.Windows.Forms.Label hue_label;
		private System.Windows.Forms.Label sat_label;
		private System.Windows.Forms.Label val_label;
	}
}