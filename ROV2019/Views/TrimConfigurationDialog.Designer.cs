namespace ROV2019.Views
{
    partial class TrimConfigurationDialog
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
            this.RunTimeField = new System.Windows.Forms.NumericUpDown();
            this.RunTimeLabel = new System.Windows.Forms.Label();
            this.RunLeftButton = new System.Windows.Forms.Button();
            this.RunRightButton = new System.Windows.Forms.Button();
            this.RunForwardButton = new System.Windows.Forms.Button();
            this.RunBackwardsButton = new System.Windows.Forms.Button();
            this.RunUpButton = new System.Windows.Forms.Button();
            this.RunDownButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.ForwardTrackBar = new System.Windows.Forms.TrackBar();
            this.YawTrackBar = new System.Windows.Forms.TrackBar();
            this.LeftDriftLabel = new System.Windows.Forms.Label();
            this.RollTrackBar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.RunTimeField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ForwardTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YawTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RollTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // RunTimeField
            // 
            this.RunTimeField.Location = new System.Drawing.Point(200, 55);
            this.RunTimeField.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.RunTimeField.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.RunTimeField.Name = "RunTimeField";
            this.RunTimeField.Size = new System.Drawing.Size(28, 20);
            this.RunTimeField.TabIndex = 0;
            this.RunTimeField.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // RunTimeLabel
            // 
            this.RunTimeLabel.AutoSize = true;
            this.RunTimeLabel.Location = new System.Drawing.Point(160, 39);
            this.RunTimeLabel.Name = "RunTimeLabel";
            this.RunTimeLabel.Size = new System.Drawing.Size(102, 13);
            this.RunTimeLabel.TabIndex = 1;
            this.RunTimeLabel.Text = "Run For : : Seconds";
            // 
            // RunLeftButton
            // 
            this.RunLeftButton.Location = new System.Drawing.Point(119, 52);
            this.RunLeftButton.Name = "RunLeftButton";
            this.RunLeftButton.Size = new System.Drawing.Size(75, 23);
            this.RunLeftButton.TabIndex = 2;
            this.RunLeftButton.Text = "Left";
            this.RunLeftButton.UseVisualStyleBackColor = true;
            // 
            // RunRightButton
            // 
            this.RunRightButton.Location = new System.Drawing.Point(234, 55);
            this.RunRightButton.Name = "RunRightButton";
            this.RunRightButton.Size = new System.Drawing.Size(75, 23);
            this.RunRightButton.TabIndex = 3;
            this.RunRightButton.Text = "Right";
            this.RunRightButton.UseVisualStyleBackColor = true;
            // 
            // RunForwardButton
            // 
            this.RunForwardButton.Location = new System.Drawing.Point(175, 12);
            this.RunForwardButton.Name = "RunForwardButton";
            this.RunForwardButton.Size = new System.Drawing.Size(75, 23);
            this.RunForwardButton.TabIndex = 4;
            this.RunForwardButton.Text = "FWD";
            this.RunForwardButton.UseVisualStyleBackColor = true;
            // 
            // RunBackwardsButton
            // 
            this.RunBackwardsButton.Location = new System.Drawing.Point(175, 81);
            this.RunBackwardsButton.Name = "RunBackwardsButton";
            this.RunBackwardsButton.Size = new System.Drawing.Size(75, 23);
            this.RunBackwardsButton.TabIndex = 5;
            this.RunBackwardsButton.Text = "BKD";
            this.RunBackwardsButton.UseVisualStyleBackColor = true;
            // 
            // RunUpButton
            // 
            this.RunUpButton.Location = new System.Drawing.Point(330, 12);
            this.RunUpButton.Name = "RunUpButton";
            this.RunUpButton.Size = new System.Drawing.Size(75, 23);
            this.RunUpButton.TabIndex = 6;
            this.RunUpButton.Text = "Up";
            this.RunUpButton.UseVisualStyleBackColor = true;
            // 
            // RunDownButton
            // 
            this.RunDownButton.Location = new System.Drawing.Point(330, 41);
            this.RunDownButton.Name = "RunDownButton";
            this.RunDownButton.Size = new System.Drawing.Size(75, 23);
            this.RunDownButton.TabIndex = 7;
            this.RunDownButton.Text = "Down";
            this.RunDownButton.UseVisualStyleBackColor = true;
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(153, 386);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 8;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // ForwardTrackBar
            // 
            this.ForwardTrackBar.AllowDrop = true;
            this.ForwardTrackBar.LargeChange = 10;
            this.ForwardTrackBar.Location = new System.Drawing.Point(12, 175);
            this.ForwardTrackBar.Maximum = 100;
            this.ForwardTrackBar.Minimum = -100;
            this.ForwardTrackBar.Name = "ForwardTrackBar";
            this.ForwardTrackBar.Size = new System.Drawing.Size(182, 45);
            this.ForwardTrackBar.TabIndex = 9;
            this.ForwardTrackBar.TickFrequency = 10;
            this.ForwardTrackBar.Scroll += new System.EventHandler(this.ForwardTrackBar_Scroll);
            // 
            // YawTrackBar
            // 
            this.YawTrackBar.AllowDrop = true;
            this.YawTrackBar.LargeChange = 10;
            this.YawTrackBar.Location = new System.Drawing.Point(250, 175);
            this.YawTrackBar.Maximum = 100;
            this.YawTrackBar.Minimum = -100;
            this.YawTrackBar.Name = "YawTrackBar";
            this.YawTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.YawTrackBar.Size = new System.Drawing.Size(45, 182);
            this.YawTrackBar.TabIndex = 10;
            this.YawTrackBar.TickFrequency = 10;
            this.YawTrackBar.Scroll += new System.EventHandler(this.YawTrackBar_Scroll);
            // 
            // LeftDriftLabel
            // 
            this.LeftDriftLabel.AutoSize = true;
            this.LeftDriftLabel.Location = new System.Drawing.Point(65, 159);
            this.LeftDriftLabel.Name = "LeftDriftLabel";
            this.LeftDriftLabel.Size = new System.Drawing.Size(80, 13);
            this.LeftDriftLabel.TabIndex = 12;
            this.LeftDriftLabel.Text = "Left/Right Drift:";
            // 
            // RollTrackBar
            // 
            this.RollTrackBar.AllowDrop = true;
            this.RollTrackBar.LargeChange = 10;
            this.RollTrackBar.Location = new System.Drawing.Point(330, 175);
            this.RollTrackBar.Maximum = 100;
            this.RollTrackBar.Minimum = -100;
            this.RollTrackBar.Name = "RollTrackBar";
            this.RollTrackBar.Size = new System.Drawing.Size(182, 45);
            this.RollTrackBar.TabIndex = 13;
            this.RollTrackBar.TickFrequency = 10;
            this.RollTrackBar.Scroll += new System.EventHandler(this.RollTrackBar_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(223, 159);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Front/Back Drift:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(363, 159);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Up/Down Drift (Roll):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(105, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(234, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Don\'t use these buttons to move: Use controller.";
            // 
            // TrimConfigurationDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 421);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RollTrackBar);
            this.Controls.Add(this.LeftDriftLabel);
            this.Controls.Add(this.YawTrackBar);
            this.Controls.Add(this.ForwardTrackBar);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.RunDownButton);
            this.Controls.Add(this.RunUpButton);
            this.Controls.Add(this.RunBackwardsButton);
            this.Controls.Add(this.RunForwardButton);
            this.Controls.Add(this.RunRightButton);
            this.Controls.Add(this.RunLeftButton);
            this.Controls.Add(this.RunTimeLabel);
            this.Controls.Add(this.RunTimeField);
            this.Name = "TrimConfigurationDialog";
            this.Text = "TrimConfigurationDialog";
            ((System.ComponentModel.ISupportInitialize)(this.RunTimeField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ForwardTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YawTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RollTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown RunTimeField;
        private System.Windows.Forms.Label RunTimeLabel;
        private System.Windows.Forms.Button RunLeftButton;
        private System.Windows.Forms.Button RunRightButton;
        private System.Windows.Forms.Button RunForwardButton;
        private System.Windows.Forms.Button RunBackwardsButton;
        private System.Windows.Forms.Button RunUpButton;
        private System.Windows.Forms.Button RunDownButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.TrackBar ForwardTrackBar;
        private System.Windows.Forms.TrackBar YawTrackBar;
        private System.Windows.Forms.Label LeftDriftLabel;
        private System.Windows.Forms.TrackBar RollTrackBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}