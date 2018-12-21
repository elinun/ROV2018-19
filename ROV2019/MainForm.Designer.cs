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
            this.ConnectionsList = new System.Windows.Forms.TableLayoutPanel();
            this.manualAddButton = new System.Windows.Forms.Button();
            this.ScanButton = new System.Windows.Forms.Button();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.ConfigureTrimButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ConnectionsList
            // 
            this.ConnectionsList.AutoSize = true;
            this.ConnectionsList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.ConnectionsList.BackColor = System.Drawing.Color.Gray;
            this.ConnectionsList.ColumnCount = 1;
            this.ConnectionsList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ConnectionsList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ConnectionsList.Location = new System.Drawing.Point(12, 12);
            this.ConnectionsList.Name = "ConnectionsList";
            this.ConnectionsList.RowCount = 1;
            this.ConnectionsList.AutoScroll = true;
            this.ConnectionsList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ConnectionsList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ConnectionsList.Size = new System.Drawing.Size(240, 0);
            this.ConnectionsList.TabIndex = 0;
            // 
            // manualAddButton
            // 
            this.manualAddButton.AutoSize = true;
            this.manualAddButton.Location = new System.Drawing.Point(126, 376);
            this.manualAddButton.Name = "manualAddButton";
            this.manualAddButton.Size = new System.Drawing.Size(75, 23);
            this.manualAddButton.TabIndex = 1;
            this.manualAddButton.Text = "Manual Add";
            this.manualAddButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.manualAddButton.UseVisualStyleBackColor = true;
            this.manualAddButton.Click += new System.EventHandler(this.manualAddButton_Click);
            // 
            // ScanButton
            // 
            this.ScanButton.AutoSize = true;
            this.ScanButton.Location = new System.Drawing.Point(78, 376);
            this.ScanButton.Name = "ScanButton";
            this.ScanButton.Size = new System.Drawing.Size(42, 23);
            this.ScanButton.TabIndex = 2;
            this.ScanButton.Text = "Scan";
            this.ScanButton.UseVisualStyleBackColor = true;
            this.ScanButton.Click += new System.EventHandler(this.ScanButton_Click);
            // 
            // ConnectButton
            // 
            this.ConnectButton.AutoSize = true;
            this.ConnectButton.Enabled = false;
            this.ConnectButton.Location = new System.Drawing.Point(4, 376);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(57, 23);
            this.ConnectButton.TabIndex = 3;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // ConfigureTrimButton
            // 
            this.ConfigureTrimButton.Enabled = false;
            this.ConfigureTrimButton.Location = new System.Drawing.Point(21, 347);
            this.ConfigureTrimButton.Name = "ConfigureTrimButton";
            this.ConfigureTrimButton.Size = new System.Drawing.Size(75, 23);
            this.ConfigureTrimButton.TabIndex = 4;
            this.ConfigureTrimButton.Text = "Configure Trim";
            this.ConfigureTrimButton.UseVisualStyleBackColor = true;
            this.ConfigureTrimButton.Click += new System.EventHandler(this.ConfigureTrimButton_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(722, 411);
            this.Controls.Add(this.ConfigureTrimButton);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.ScanButton);
            this.Controls.Add(this.manualAddButton);
            this.Controls.Add(this.ConnectionsList);
            this.Name = "Main";
            this.Text = "CHS ROV 2K19";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel ConnectionsList;
        private System.Windows.Forms.Button manualAddButton;
        private System.Windows.Forms.Button ScanButton;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.Button ConfigureTrimButton;
    }
}

