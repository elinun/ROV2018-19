namespace ROV2019
{
    partial class Main
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
            this.ConnectionsListFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // ConnectionsListFlow
            // 
            this.ConnectionsListFlow.Location = new System.Drawing.Point(91, 94);
            this.ConnectionsListFlow.Name = "ConnectionsListFlow";
            this.ConnectionsListFlow.Size = new System.Drawing.Size(200, 100);
            this.ConnectionsListFlow.TabIndex = 0;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(434, 411);
            this.Controls.Add(this.ConnectionsListFlow);
            this.Name = "Main";
            this.Text = "CHS ROV 2K19";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel ConnectionsListFlow;
    }
}

