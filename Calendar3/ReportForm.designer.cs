﻿namespace Calendar
{
    partial class ReportForm
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
            this.reportBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // reportBrowser
            // 
            this.reportBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportBrowser.Location = new System.Drawing.Point(0, 0);
            this.reportBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.reportBrowser.Name = "reportBrowser";
            this.reportBrowser.Size = new System.Drawing.Size(1018, 749);
            this.reportBrowser.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 749);
            this.Controls.Add(this.reportBrowser);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Report_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser reportBrowser;
    }
}