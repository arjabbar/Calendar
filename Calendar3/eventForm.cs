using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Calendar
{
    public partial class eventForm : Form
    {
        SqlConnection sc;
        SqlCommand sqlCmd;
        SqlDataReader sqlRdr;
        Calendar.txtCalendarBox myParent;
        Int32 eventID;

        public eventForm(SqlConnection sc, DateTime selectedDate, Calendar.txtCalendarBox myParent)//New Event Constructor
        {
            InitializeComponent();
            
            this.myParent = myParent;
            btnEdit.Visible = false;
            btnCreate.Visible = true;

            beginDatePicker.CustomFormat = "dddd,MMMM d, yyyy 'at' h:mm:ss tt";
            beginDatePicker.Format = DateTimePickerFormat.Custom;
            endDatePicker.CustomFormat = "dddd,MMMM d, yyyy 'at' h:mm:ss tt";
            endDatePicker.Format = DateTimePickerFormat.Custom;

            selectedDate = selectedDate.AddSeconds(1);

            beginDatePicker.Text = selectedDate.ToString();
            endDatePicker.Text = selectedDate.ToString();
            
            this.sc = sc;
            this.Visible = true;
        }

        public eventForm(SqlConnection sc, Int32 eventID, Calendar.txtCalendarBox myParent)//Edit Constructor
        {
            InitializeComponent();

            this.myParent = myParent;
            btnCreate.Visible = false;
            btnEdit.Visible = true;

            this.eventID = eventID;

            beginDatePicker.CustomFormat = "dddd,MMMM d, yyyy 'at' h:mm:ss tt";
            beginDatePicker.Format = DateTimePickerFormat.Custom;
            endDatePicker.CustomFormat = "dddd,MMMM d, yyyy 'at' h:mm:ss tt";
            endDatePicker.Format = DateTimePickerFormat.Custom;

            this.Text = "Edit Event";

            sqlCmd = new SqlCommand("SELECT event_subject as 'Event Subject', event_begin as 'Event Begin', event_end as 'Event End', event_description as 'Event Description', event_cat as 'Event Category' FROM event WHERE event_id='" + eventID.ToString() + "'", sc);

            sqlRdr = sqlCmd.ExecuteReader();

            sqlRdr.Read();

            try
            {
                txtSubject.Text = sqlRdr.GetValue(0).ToString();
                beginDatePicker.Text = sqlRdr.GetValue(1).ToString();
                endDatePicker.Text = sqlRdr.GetValue(2).ToString();
                txtDescription.Text = sqlRdr.GetValue(3).ToString();
                catCombo.Text = sqlRdr.GetValue(4).ToString();
            }
            catch (InvalidOperationException) { }

            sqlRdr.Close();

            this.sc = sc;
            this.Visible = true;
        }

        //This method takes the data from the form and puts it in the database.
        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (txtSubject.Text != "" && txtDescription.Text != "" && endDatePicker.Value.Subtract(beginDatePicker.Value).ToString().Contains('-') == false)//Ensures values for the title and description
            {
                String subject = txtSubject.Text;
                String beginDate = beginDatePicker.Value.ToString();
                String endDate = endDatePicker.Value.ToString();
                String description = txtDescription.Text.Replace("'", "");
                String eventCat = catCombo.Text;
                
                sqlCmd = new SqlCommand("INSERT into event(event_begin, event_end, event_created, event_subject, event_description, event_cat)" +
                                        "VALUES('" + DateTime.Parse(beginDate).ToString() + "', '" + DateTime.Parse(endDate).ToString() + "', '" + DateTime.Now.ToString() + "', '" + subject + "', '" + description +"', '" + eventCat + "')", sc);
                
                sqlRdr = sqlCmd.ExecuteReader();

                sqlRdr.Close();

                this.Close();
                myParent.boldEventDays();
                myParent.entryDeselect();
            }
            else
            {
                if (txtSubject.Text == "")
                {
                    MessageBox.Show("Please enter a title for this event.");
                }
                else if(txtDescription.Text == "")
                {
                    MessageBox.Show("Please enter a description for this event.");
                }
                else if(endDatePicker.Value.Subtract(beginDatePicker.Value).ToString().Contains('-') == true)
                {
                    MessageBox.Show("The selected Begin and End Dates are Invalid.");
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtSubject.Text != "" && txtDescription.Text != "")//Ensures values for the title and description
            {
                String subject = txtSubject.Text;
                String beginDate = beginDatePicker.Value.ToString();
                String endDate = endDatePicker.Value.ToString();
                String description = txtDescription.Text;
                String eventCat = catCombo.Text;

                //SQL command needs to be changed
                sqlCmd = new SqlCommand("UPDATE event SET event_begin = '" + beginDate.ToString() + "', " 
                                                       + "event_end = '" + endDate.ToString() + "', "
                                                       + "event_created = '" + DateTime.Now.ToString() + "', " 
                                                       + "event_subject = '" + subject + "', " 
                                                       + "event_description = '" + description + "', " 
                                                       + "event_cat = '" + eventCat + "' "
                                       + "WHERE event_id = '" + eventID + "'", sc);

                sqlRdr = sqlCmd.ExecuteReader();

                sqlRdr.Close();

                this.Close();
                myParent.boldEventDays();
                myParent.entryDeselect();
            }
            else
            {
                if (txtSubject.Text == "")
                {
                    MessageBox.Show("Please enter a title for this event.");
                }
                else if (txtDescription.Text == "")
                {
                    MessageBox.Show("Please enter a description for this event.");
                }
            }
        }
    }
}
