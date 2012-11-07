using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;

namespace Calendar
{
    

    public class RandomEventGenerator
    {
        int numRecords, dayRange = 100, hourRange = 0;
        DateTime beginDate, endDate;
        SqlCommand sqlCmd;
        SqlDataReader sqlRdr;
        Random r = new Random();
        string[] catArray = { "Holiday", "School", "Birthday", "Work", "Appointment" };

        public RandomEventGenerator(DateTime beginDate, SqlCommand sqlCmd, SqlDataReader sqlRdr)
        {
            this.beginDate = beginDate;
            this.sqlCmd = sqlCmd;
            this.sqlRdr = sqlRdr;
        }

        private void insertRecord(DateTime date)
        {
            if (!sqlRdr.IsClosed)
                sqlRdr.Close();
            DateTime event_begin = randomDate();
            DateTime event_end = event_begin.AddHours(r.Next(8));
            sqlCmd.CommandText = "INSERT INTO event(event_begin, event_end, event_created, event_subject, event_description, event_cat)"
                                                    + "VALUES ('" + date + "','" + date.AddDays(r.Next(8)) + "','" + DateTime.Now + "','" + "Randomly Generated Event"
                                                    + "','" + "This is a randomly generated event for testing purposes." + "','" + catArray[r.Next(4)] + "')";
            sqlRdr = sqlCmd.ExecuteReader();
        }

        public void insertRecords(int numRecordsToInsert)
        {
            for (int i = 0; i < numRecordsToInsert; i++)
            {
                insertRecord(randomDate());
            }
            sqlRdr.Close();
        }

        private DateTime randomDate()
        {
            DateTime random = DateTime.Now;
            random = random.AddDays(r.Next(dayRange));
            return random;
        }
    }

}
