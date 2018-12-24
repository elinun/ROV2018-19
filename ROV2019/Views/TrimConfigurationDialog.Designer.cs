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
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.RunTimeField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
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
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(12, 175);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(182, 45);
            this.trackBar1.TabIndex = 9;
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(77, 100);
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar2.Size = new System.Drawing.Size(45, 182);
            this.trackBar2.TabIndex = 10;
            // 
            // TrimConfigurationDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 421);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.trackBar1);
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
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
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
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.TrackBar trackBar2;
    }
}