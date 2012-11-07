using System;
using System.IO;
using System.Collections;
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
    public partial class txtCalendarBox : Form
    {
        SqlConnection sc;
        SqlCommand sqlCmd;
        SqlDataAdapter sqlAdapter;
        SqlDataReader sqlRdr;
        BindingSource bs;
        Int32 lastSelectedEvent;
        DateTime lastSelectedDate;
        HappyBirthday hb;

    public txtCalendarBox()
    {
        InitializeComponent();

        // Initialize HappyBirthday screen 
        hb = new HappyBirthday();

        string dbPath = Application.StartupPath + "\\event_db.mdf";
        sc = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename=" + dbPath + ";Integrated Security=True;User Instance=True");

        sc.Close();
        sc.Open();

        // Add event handlers for the DateSelected and DateChanged events 
        monthCalendar.DateSelected += new DateRangeEventHandler(monthCalendar_DateSelected);
        monthCalendar.SetDate(System.DateTime.Now);
        boldEventDays();
        dateSelected(System.DateTime.Now);
        timer1.Start();
    }

    private void monthCalendar_DateSelected(object sender, System.Windows.Forms.DateRangeEventArgs e)
    {
        notesTextBox.Clear();
        dateSelected(e.Start);
    }

    //Date selected method
    private void dateSelected(DateTime selectedDate)
    {
        BackgroundImage = null;

        bool containsBDay = false;
        lastSelectedDate = selectedDate;

        sqlCmd = new SqlCommand("select event_id, event_subject as 'Event Subject', event_begin as 'Event Begin',event_end as 'Event End', event_cat as 'Event Category' from event where event_begin>='" +
                                            selectedDate.ToString("M/dd/yyyy 00:00:00") +
                                            "' AND event_begin<='" +
                                            selectedDate.ToString("M/dd/yyyy 23:59:59") +
                                            "'", sc);

        sqlAdapter = new SqlDataAdapter();
        sqlAdapter.SelectCommand = sqlCmd;

        DataTable dt = new DataTable();
        sqlAdapter.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            for (int lpCtr = 0; lpCtr < dt.Rows.Count; lpCtr++)
            {
                if (dt.Rows[0].ItemArray.GetValue(dt.Columns.IndexOf("Event Category")).Equals("Birthday"))
                    containsBDay = true;
            }
            if (containsBDay)
            {
                if (hb.IsDisposed) hb = new HappyBirthday();
                hb.Show();
                containsBDay = false;
            }
        }

        bs = new BindingSource();
        bs.DataSource = dt;
        eventGrid.DataSource = bs;
        eventGrid.Columns[0].Visible = false;
        sqlAdapter.Dispose();
    }

    private void eventGridView_SelectedValueChanged(object sender, EventArgs e)
    {
        entrySelect();
    }

    public void entryDeselect()
    {
        monthCalendar.SelectionStart = System.DateTime.Now;
        dateSelected(lastSelectedDate);
        notesTextBox.Clear();
    }

    public void entrySelect()
    {
        try
        {
            sqlCmd = new SqlCommand("select event_description as 'Event Description', event_cat as 'Event Category' from event where event_id='" + eventGrid.Rows[eventGrid.CurrentRow.Index].Cells[0].Value.ToString() + "'", sc);
            sqlRdr = sqlCmd.ExecuteReader();

            sqlRdr.Read();
            
            try
            {
            notesTextBox.Text = sqlRdr.GetString(0);
            
                //Change the background image depending on the event's category
                    if (sqlRdr.GetString(1).Equals("School"))
                {
                    BackgroundImage = Calendar.Properties.Resources.school;
                }
                else if (sqlRdr.GetString(1).Equals("Work"))
                {
                    BackgroundImage = Calendar.Properties.Resources.work;
                }
                else if (sqlRdr.GetString(1).Equals("Appointment"))
                {
                    BackgroundImage = Calendar.Properties.Resources.appointment;
                }
                else if (sqlRdr.GetString(1).Equals("Birthday"))
                {
                    BackgroundImage = Calendar.Properties.Resources.birthday;
                }
                else if (sqlRdr.GetString(1).Equals("Holiday"))
                {
                    BackgroundImage = Calendar.Properties.Resources.holiday;
                }
                else
                {
                    BackgroundImage = null;
                }

            }
            catch (InvalidOperationException) { }

            

            sqlRdr.Close();

            lastSelectedEvent = (Int32)eventGrid.Rows[eventGrid.CurrentRow.Index].Cells[0].Value;
        }
        catch (NullReferenceException){}
    }

    private void eventBindingNavigatorSaveItem_Click(object sender, EventArgs e)
    {
        Validate();
    }

    private void eventBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
    {
        Validate();
    }

    private void btnEvent_Click(object sender, EventArgs e)
    {
        eForm(false);
    }

    private void dataGridView_DoubleClick(object sender, EventArgs e)
    {
        eForm(true);
    }

    private void eForm(bool editForm)
    {
        eventForm eForm;

        if (editForm == false)
        {
            eForm = new eventForm(sc, lastSelectedDate, this);
        }
        else
        {
            eForm = new eventForm(sc, lastSelectedEvent, this);
        }

    }

    //Bold the dates that have events
    public void boldEventDays()
    {
        sqlCmd = new SqlCommand("select event_begin as 'Event Begin' from event", sc);
        sqlRdr = sqlCmd.ExecuteReader();

        monthCalendar.RemoveAllBoldedDates();

        while (sqlRdr.Read())
        {
            monthCalendar.AddBoldedDate(sqlRdr.GetDateTime(0));
        }
        monthCalendar.UpdateBoldedDates();

        sqlRdr.Close();
    }

    //Delete an Event
    private void deleteEventButton_MouseUp(object sender, MouseEventArgs e)
    {
        if (eventGrid.SelectedCells.Count < 1)
        {
            MessageBox.Show("Please select a date below to delete first.");
        }
        else
        {
            DialogResult response = MessageBox.Show(this, "Are you sure you want to delete the selected event?", "Deletion Confirmation", MessageBoxButtons.YesNo);
            if (response == DialogResult.Yes)
            {
                sqlCmd = new SqlCommand("delete from event WHERE event_id='" + eventGrid.Rows[eventGrid.CurrentRow.Index].Cells[0].Value.ToString() + "'", sc);
                MessageBox.Show("" + sqlCmd.ExecuteNonQuery() + " event deleted");
                notesTextBox.Clear();
                eventGrid.DataSource = null;
                boldEventDays();
                dateSelected(System.DateTime.Now);
            }
        }
    }

    //Create a Report Button
    private void btnCreateReport_Click(object sender, EventArgs e)
    {
        ReportOptions ro = new ReportOptions(sqlCmd, sqlRdr);
        ro.Visible = true;
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
        currentTime.Text = DateTime.Now.ToString("F");
    }
  }
}
