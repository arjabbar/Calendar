namespace Calendar
{
    partial class ReportOptions
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
            this.outputGroup = new System.Windows.Forms.GroupBox();
            this.htmlButton = new System.Windows.Forms.RadioButton();
            this.csvButton = new System.Windows.Forms.RadioButton();
            this.outputButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.status = new System.Windows.Forms.ToolStripStatusLabel();
            this.outputGroup.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // outputGroup
            // 
            this.outputGroup.Controls.Add(this.csvButton);
            this.outputGroup.Controls.Add(this.htmlButton);
            this.outputGroup.Location = new System.Drawing.Point(23, 30);
            this.outputGroup.Name = "outputGroup";
            this.outputGroup.Size = new System.Drawing.Size(249, 100);
            this.outputGroup.TabIndex = 0;
            this.outputGroup.TabStop = false;
            this.outputGroup.Text = "Output Options";
            // 
            // htmlButton
            // 
            this.htmlButton.AutoSize = true;
            this.htmlButton.Location = new System.Drawing.Point(21, 32);
            this.htmlButton.Name = "htmlButton";
            this.htmlButton.Size = new System.Drawing.Size(55, 17);
            this.htmlButton.TabIndex = 0;
            this.htmlButton.Text = "HTML";
            this.htmlButton.UseVisualStyleBackColor = true;
            // 
            // csvButton
            // 
            this.csvButton.AutoSize = true;
            this.csvButton.Location = new System.Drawing.Point(21, 65);
            this.csvButton.Name = "csvButton";
            this.csvButton.Size = new System.Drawing.Size(46, 17);
            this.csvButton.TabIndex = 1;
            this.csvButton.Text = "CSV";
            this.csvButton.UseVisualStyleBackColor = true;
            // 
            // outputButton
            // 
            this.outputButton.Location = new System.Drawing.Point(197, 182);
            this.outputButton.Name = "outputButton";
            this.outputButton.Size = new System.Drawing.Size(75, 41);
            this.outputButton.TabIndex = 1;
            this.outputButton.Text = "Generate Report";
            this.outputButton.UseVisualStyleBackColor = true;
            this.outputButton.Click += new System.EventHandler(this.outputButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status});
            this.statusStrip1.Location = new System.Drawing.Point(0, 240);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(284, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // status
            // 
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(0, 17);
            // 
            // ReportOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.outputButton);
            this.Controls.Add(this.outputGroup);
            this.Name = "ReportOptions";
            this.Text = "ReportOptions";
            this.outputGroup.ResumeLayout(false);
            this.outputGroup.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox outputGroup;
        private System.Windows.Forms.RadioButton csvButton;
        private System.Windows.Forms.RadioButton htmlButton;
        private System.Windows.Forms.Button outputButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel status;
    }
}