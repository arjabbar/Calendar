namespace Calendar
{
    partial class txtCalendarBox
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
            this.btnCreateReport = new System.Windows.Forms.Button();
            this.monthCalendar = new System.Windows.Forms.MonthCalendar();
            this.notesTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnEvent = new System.Windows.Forms.Button();
            this.eventGrid = new System.Windows.Forms.DataGridView();
            this.event_dbDataSet = new Calendar.event_dbDataSet();
            this.eventBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.eventTableAdapter = new Calendar.event_dbDataSetTableAdapters.eventTableAdapter();
            this.deleteEventButton = new System.Windows.Forms.Button();
            this.lblEvents = new System.Windows.Forms.Label();
            this.currentTime = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.eventGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.event_dbDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCreateReport
            // 
            this.btnCreateReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateReport.Location = new System.Drawing.Point(320, 210);
            this.btnCreateReport.Name = "btnCreateReport";
            this.btnCreateReport.Size = new System.Drawing.Size(157, 39);
            this.btnCreateReport.TabIndex = 1;
            this.btnCreateReport.Text = "Create Report";
            this.btnCreateReport.UseVisualStyleBackColor = true;
            this.btnCreateReport.Click += new System.EventHandler(this.btnCreateReport_Click);
            // 
            // monthCalendar
            // 
            this.monthCalendar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.monthCalendar.CalendarDimensions = new System.Drawing.Size(3, 1);
            this.monthCalendar.Location = new System.Drawing.Point(88, 27);
            this.monthCalendar.MaxSelectionCount = 1;
            this.monthCalendar.Name = "monthCalendar";
            this.monthCalendar.ScrollChange = 1;
            this.monthCalendar.TabIndex = 2;
            this.monthCalendar.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar_DateSelected);
            // 
            // notesTextBox
            // 
            this.notesTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.notesTextBox.Location = new System.Drawing.Point(494, 279);
            this.notesTextBox.Multiline = true;
            this.notesTextBox.Name = "notesTextBox";
            this.notesTextBox.ReadOnly = true;
            this.notesTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.notesTextBox.Size = new System.Drawing.Size(278, 259);
            this.notesTextBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(491, 263);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Description";
            // 
            // btnEvent
            // 
            this.btnEvent.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnEvent.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEvent.Location = new System.Drawing.Point(157, 210);
            this.btnEvent.Name = "btnEvent";
            this.btnEvent.Size = new System.Drawing.Size(157, 39);
            this.btnEvent.TabIndex = 6;
            this.btnEvent.Text = "Create Event";
            this.btnEvent.UseVisualStyleBackColor = true;
            this.btnEvent.Click += new System.EventHandler(this.btnEvent_Click);
            // 
            // eventGrid
            // 
            this.eventGrid.AllowUserToAddRows = false;
            this.eventGrid.AllowUserToDeleteRows = false;
            this.eventGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.eventGrid.BackgroundColor = System.Drawing.SystemColors.Window;
            this.eventGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.eventGrid.Cursor = System.Windows.Forms.Cursors.Default;
            this.eventGrid.Location = new System.Drawing.Point(12, 279);
            this.eventGrid.MultiSelect = false;
            this.eventGrid.Name = "eventGrid";
            this.eventGrid.ReadOnly = true;
            this.eventGrid.Size = new System.Drawing.Size(476, 259);
            this.eventGrid.TabIndex = 7;
            this.eventGrid.SelectionChanged += new System.EventHandler(this.eventGridView_SelectedValueChanged);
            this.eventGrid.DoubleClick += new System.EventHandler(this.dataGridView_DoubleClick);
            // 
            // event_dbDataSet
            // 
            this.event_dbDataSet.DataSetName = "event_dbDataSet";
            this.event_dbDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // eventBindingSource
            // 
            this.eventBindingSource.DataMember = "event";
            this.eventBindingSource.DataSource = this.event_dbDataSet;
            // 
            // eventTableAdapter
            // 
            this.eventTableAdapter.ClearBeforeFill = true;
            // 
            // deleteEventButton
            // 
            this.deleteEventButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteEventButton.Location = new System.Drawing.Point(483, 210);
            this.deleteEventButton.Name = "deleteEventButton";
            this.deleteEventButton.Size = new System.Drawing.Size(157, 39);
            this.deleteEventButton.TabIndex = 8;
            this.deleteEventButton.Text = "Delete Event";
            this.deleteEventButton.UseVisualStyleBackColor = true;
            this.deleteEventButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.deleteEventButton_MouseUp);
            // 
            // lblEvents
            // 
            this.lblEvents.AutoSize = true;
            this.lblEvents.Location = new System.Drawing.Point(13, 263);
            this.lblEvents.Name = "lblEvents";
            this.lblEvents.Size = new System.Drawing.Size(40, 13);
            this.lblEvents.TabIndex = 9;
            this.lblEvents.Text = "Events";
            // 
            // currentTime
            // 
            this.currentTime.AutoSize = true;
            this.currentTime.Location = new System.Drawing.Point(295, 6);
            this.currentTime.Name = "currentTime";
            this.currentTime.Size = new System.Drawing.Size(0, 13);
            this.currentTime.TabIndex = 10;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // txtCalendarBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(784, 555);
            this.Controls.Add(this.currentTime);
            this.Controls.Add(this.lblEvents);
            this.Controls.Add(this.deleteEventButton);
            this.Controls.Add(this.eventGrid);
            this.Controls.Add(this.btnEvent);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.notesTextBox);
            this.Controls.Add(this.monthCalendar);
            this.Controls.Add(this.btnCreateReport);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "txtCalendarBox";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Calendar";
            ((System.ComponentModel.ISupportInitialize)(this.eventGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.event_dbDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreateReport;
        private System.Windows.Forms.MonthCalendar monthCalendar;
        private System.Windows.Forms.TextBox notesTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnEvent;
        private System.Windows.Forms.DataGridView eventGrid;
        private event_dbDataSet event_dbDataSet;
        private System.Windows.Forms.BindingSource eventBindingSource;
        private event_dbDataSetTableAdapters.eventTableAdapter eventTableAdapter;
        private System.Windows.Forms.Button deleteEventButton;
        private System.Windows.Forms.Label lblEvents;
        private System.Windows.Forms.Label currentTime;
        private System.Windows.Forms.Timer timer1;

    }
}

