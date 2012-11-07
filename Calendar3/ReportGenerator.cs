using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;

namespace Calendar
{
    class ReportGenerator
    {
        public String title = "";
        private Category category = new Category();
        SqlCommand sqlCmd;
        SqlDataReader sqlRdr;
        private DateTime now = DateTime.Now;
        private int numTotalEvents;
        private static int NUMCOLUMNS = 8;
        public int[] event_id;
        public DateTime[] event_created;
        public DateTime[] event_begin;
        public DateTime[] event_end;
        public String[] event_subject;
        public String[] event_description;
        public String[] event_cat;
        public Boolean[] event_recur;
        public Tuple<int, DateTime, DateTime, DateTime, string, string, string>[] eventRecords;
        char[] splitter = {' '};
        public ReportGenerator(SqlCommand sqlCmd, SqlDataReader sqlRdr)
        {
            this.sqlCmd = sqlCmd;
            this.sqlRdr = sqlRdr;
            this.sqlCmd.CommandText = "SELECT count(*) FROM event";
            sqlRdr = sqlCmd.ExecuteReader();
            
            while(sqlRdr.Read())
            {
                this.numTotalEvents = (int)sqlRdr[0];
            }
            sqlRdr.Close();
            
            this.event_id = new int[numTotalEvents];
            this.event_created = new DateTime[numTotalEvents];
            this.event_begin = new DateTime[numTotalEvents];
            this.event_end = new DateTime[numTotalEvents];
            this.event_subject = new String[numTotalEvents];
            this.event_description = new String[numTotalEvents];
            this.event_cat = new String[numTotalEvents];
            
            this.sqlCmd.CommandText = "SELECT * FROM event ORDER BY 2 ASC";

            sqlRdr = sqlCmd.ExecuteReader();

            int eventNum = 0;
            while (sqlRdr.Read())
            {
                this.event_id[eventNum] = (int)sqlRdr["event_id"];
                this.event_begin[eventNum] = (DateTime)sqlRdr["event_begin"];
                this.event_end[eventNum] = (DateTime)sqlRdr["event_end"];
                this.event_created[eventNum] = (DateTime)sqlRdr["event_created"];
                this.event_subject[eventNum] = (String)sqlRdr["event_subject"];
                this.event_description[eventNum] = (String)sqlRdr["event_description"];
                this.event_cat[eventNum] = (sqlRdr["event_cat"] is System.DBNull) ? "Uncategorized" : (String)sqlRdr["event_cat"];
                eventNum++;
            }

            eventRecords = new Tuple<int, DateTime, DateTime, DateTime, string, string, string>[numTotalEvents];

            for (int e = 0; e < numTotalEvents; e++)
            {
                eventRecords[e] = new Tuple<int, DateTime, DateTime, DateTime, string, string, string>
                                    (event_id[e], event_begin[e], event_end[e], event_created[e], event_subject[e], 
                                    event_description[e], event_cat[e]);
            }
            sqlRdr.Close();
        }

        public void convertStringArrayToInt(String[] array)
        {
            int[] intArray = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                intArray[i] = Convert.ToInt32(array[i]);
            }
        }

        public void printArray<T>(T[] array)
        {
            foreach (T t in array)
            {
                Console.WriteLine(t);
            }
        }

        public int numEventsBetween(DateTime dt1, DateTime dt2)
        {
            int sum = 0;
            for (int e = 0; e < numTotalEvents; e++)
            {
                if ((event_begin[e] > dt1) && (event_begin[e] < dt2))
                {
                    sum++;
                }
            }
            return sum;
        }

        public int numEventsWithinOneMonth()
        {
            int sum = 0;
            for (int e = 0; e < numTotalEvents; e++)
            {
                if ((event_begin[e] < now.AddMonths(1)) && (event_begin[e] > now))
                {
                    sum++;
                }
            }
            return sum;
        }

        public int numEventsWithinOneWeek()
        {
            int sum = 0;
            for (int e = 0; e < numTotalEvents; e++)
            {
                if ((event_begin[e] < now.AddDays(7)) && (event_begin[e] > now))
                {
                    sum++;
                }
            }
            return sum;
        }

        public int numEventsOnDay(DateTime day)
        {
            int sum = 0;
            for (int e = 0; e < numTotalEvents; e++)
            {
                if (event_begin[e].Date == day.Date)
                {
                    sum++;
                }
            }
            return sum;
        }

        public int numEventsWithinOneDay()
        {
            int sum = 0;
            for (int e = 0; e < numTotalEvents; e++)
            {
                if ((event_begin[e] > now) && (event_begin[e] < now.AddDays(1)))
                {
                    sum++;
                }
            }
            return sum;
        }

        public int numPastEvents()
        {
            return numEventsBetween(DateTime.MinValue, now);
        }

        public int numFutureEvents()
        {
            return numEventsBetween(now, DateTime.MaxValue);
        }

        public long ticksBetween(DateTime dt1, DateTime dt2)
        {
            return Math.Abs(dt1.Ticks - dt2.Ticks);
        }

        public DateTime dateTimeBetween(DateTime dt1, DateTime dt2)
        {
            return new DateTime(ticksBetween(dt1, dt2));
        }

        public int longestLastingEventID()
        {
            int longest = 0;
            for (int e = 0; e < numTotalEvents; e++)
            {
                if (ticksBetween(eventRecords[e].Item2, eventRecords[e].Item3) > longest)
                {
                    longest = eventRecords[e].Item1;
                }
            }
            return longest;
        }

        public int shortestLastingEventID()
        {
            int shortest = Int16.MaxValue;
            for (int e = 0; e < numTotalEvents; e++)
            {
                if (ticksBetween(eventRecords[e].Item2, eventRecords[e].Item3) < shortest)
                {
                    shortest = eventRecords[e].Item1;
                }
            }
            return shortest;
        }

        public DateTime averageEventLengthInTicks()
        {
            long sum = 0;
            
            for (int e = 0; e < numTotalEvents; e++)
            {
                sum += ticksBetween(eventRecords[e].Item2, eventRecords[e].Item3);
            }

            return new DateTime(sum / numTotalEvents);
        }

        public DateTime busiestDay()
        {
            int busiest = 0;
            DateTime busyDay = new DateTime();
            for (int e = 0; e < numTotalEvents; e++)
            {
                if (totalEventTicksOnDay(eventRecords[e].Item2) > busiest)
                {
                    busyDay = eventRecords[e].Item2;
                }
            }
            return busyDay;
        }

        public DateTime busiestUpcomingDay()
        {
            int busiest = 0;
            DateTime busyDay = new DateTime();
            for (int e = 0; e < numTotalEvents; e++)
            {
                if ((totalEventTicksOnDay(eventRecords[e].Item2) > busiest) && (eventRecords[e].Item2 > now))
                {
                    busyDay = eventRecords[e].Item2;
                }
            }
            return busyDay;
        }

        public DateTime busiestPastDay()
        {
            int busiest = 0;
            DateTime busyDay = new DateTime();
            for (int e = 0; e < numTotalEvents; e++)
            {
                if ((totalEventTicksOnDay(eventRecords[e].Item2) > busiest) && (eventRecords[e].Item2 < now))
                {
                    busyDay = eventRecords[e].Item2;
                }
            }
            return busyDay;
        }

        public int getEventIDFromDateTime(DateTime dt)
        {
            for (int e = 0; e < numTotalEvents; e++)
            {
                if (eventRecords[e].Item2 == dt)
                {
                    return eventRecords[e].Item1;
                }
            }
            return -1;
        }

        public double ticksToMinutes(long ticks)
        {
            Console.WriteLine(TimeSpan.FromTicks(ticks).TotalMinutes);
            return Double.Parse(TimeSpan.FromTicks(ticks).TotalMinutes.ToString("F1"));
        }

        private List<Tuple<int, DateTime, DateTime, DateTime, string, string, string>> eventsOnDay(DateTime day)
        {
            List<Tuple<int, DateTime, DateTime, DateTime, string, string, string>> dayList = 
                new List<Tuple<int, DateTime, DateTime, DateTime, string, string, string>>();

            for (int e = 0; e < numTotalEvents; e++)
            {
                if (day.Date == eventRecords[e].Item2.Date)
                {
                    dayList.Add(eventRecords[e]);
                }
            }
            return dayList;
        }

        public long totalEventTicksOnDay(DateTime day)
        {
            long sum = 0;
            for (int e = 0; e < numTotalEvents; e++)
            {
                if (eventRecords[e].Item2.Date == day.Date)
                {
                    sum += ticksBetween(eventRecords[e].Item2, eventRecords[e].Item3);
                }
            }
            return sum;
        }

        private string htmlEvent(Tuple<int, DateTime, DateTime, DateTime, string, string, string> eventTuple)
        {
            String html = "<ul class=\"event\">";

                html += tagWrap("li", "Created On : " + eventTuple.Item4.ToString("D"));
                html += tagWrap("li", "Event Start : " + eventTuple.Item2.ToString("f"));
                html += tagWrap("li", "Event End : " + eventTuple.Item3.ToString("f"));
                html += tagWrap("li", "Subject : " + eventTuple.Item5);
                html += tagWrap("li", "Category : " + eventTuple.Item7);
                html += tagWrap("li", "Description : " + eventTuple.Item6);
 
            html += "</ul>";
            return html;
        }

        private string htmlEventList(List<Tuple<int, DateTime, DateTime, DateTime, string, string, string>> events)
        {
            string html = "";
            foreach (Tuple<int, DateTime, DateTime, DateTime, string, string, string> e in events)
            {
                html += htmlEvent(e);
            }
            return html;
        }

        private List<Tuple<int, DateTime, DateTime, DateTime, string, string, string>> getEventsByCategory(String category)
        {
            List<Tuple<int, DateTime, DateTime, DateTime, string, string, string>> dayList =
                new List<Tuple<int, DateTime, DateTime, DateTime, string, string, string>>();

            for (int e = 0; e < numTotalEvents; e++)
            {
                if (category.ToUpper()==eventRecords[e].Item7.ToUpper())
                {
                    dayList.Add(eventRecords[e]);
                }
            }
            return dayList;
        }

        private List<Tuple<int, DateTime, DateTime, DateTime, string, string, string>> getUpcomingEventsByCategory(String category)
        {
            List<Tuple<int, DateTime, DateTime, DateTime, string, string, string>> dayList =
                new List<Tuple<int, DateTime, DateTime, DateTime, string, string, string>>();

            for (int e = 0; e < numTotalEvents; e++)
            {
                if ((category.ToUpper() == eventRecords[e].Item7.ToUpper()) && (eventRecords[e].Item2 > now))
                {
                    dayList.Add(eventRecords[e]);
                }
            }
            return dayList;
        }

        private List<Tuple<int, DateTime, DateTime, DateTime, string, string, string>> getPastEventsByCategory(String category)
        {
            List<Tuple<int, DateTime, DateTime, DateTime, string, string, string>> dayList =
                new List<Tuple<int, DateTime, DateTime, DateTime, string, string, string>>();

            for (int e = 0; e < numTotalEvents; e++)
            {
                if ((category.ToUpper() == eventRecords[e].Item7.ToUpper()) && (eventRecords[e].Item2 < now))
                {
                    dayList.Add(eventRecords[e]);
                }
            }
            return dayList;
        }

        public void printAllRecordsToConsole()
        {
            for (int record = 0; record < numTotalEvents; record++)
            {
                Console.WriteLine(eventRecords[record]);
            }
        }

        public void generateHTMLReport()
        {
            
            string directory = Environment.CurrentDirectory;
            string filename = "Report-" + now.ToString("dd MMMM yyyy") + ".html";
            string fullPath = directory + "\\" + filename;
            System.IO.StreamWriter writer = new System.IO.StreamWriter(fullPath);
            string styleSheet = "style.css";
            string font = "<link href='http://fonts.googleapis.com/css?family=Source+Code+Pro' rel='stylesheet' type='text/css'>";
            string header = "<html><head><title>" + filename + "</title>" + insertCSS(styleSheet) + font + "</head><body>";
            string banner = tagWrap("h1", "Event Report Generated " + now.ToString("D"));
            string bodyOverview = tagWrap("h3", "Overview");
            bodyOverview += tagWrap("p", "Total # of events : " + numTotalEvents);
            bodyOverview += tagWrap("p", "Total # of past events : " + numPastEvents());
            bodyOverview += tagWrap("p", "Total # of upcoming events : " + numFutureEvents());
            bodyOverview += tagWrap("p", "Events occuring within 1 month : " + numEventsWithinOneMonth());
            bodyOverview += tagWrap("p", "Events occuring within 1 week : " + numEventsWithinOneWeek());
            bodyOverview += tagWrap("p", "Events occuring within 24 hours : " + numEventsWithinOneDay());

            string bodyStats = tagWrap("h3", "Statistics");
            bodyStats += (numFutureEvents() > 0) ? tagWrap("p", "Busiest Upcoming Day : " + busiestUpcomingDay().ToString("D") 
                        + "<br> With a total of " + ticksToMinutes(totalEventTicksOnDay(busiestUpcomingDay())) + " minutes of busy-time."
                         + htmlEventList(eventsOnDay(busiestUpcomingDay())))
                        : "";
            bodyStats += (numPastEvents() > 0) ? tagWrap("p", "Busiest Past Day : " + busiestPastDay().ToString("D") + "<br> With a total of "
                        + ticksToMinutes(totalEventTicksOnDay(busiestPastDay())) + " minutes of busy-time." 
                        + htmlEventList(eventsOnDay(busiestPastDay())))
                        : "";
            bodyStats += tagWrap("h3", "Upcoming Events By Category");

            string[] categories = {category.appointment, category.birthday, category.holiday, category.school, category.work};

            for (int cat = 0; cat < categories.Length; cat++)
            {
                List<Tuple<int, DateTime, DateTime, DateTime, string, string, string>> eventsInCategory = getUpcomingEventsByCategory(categories[cat]);
                bodyStats += (eventsInCategory.Count > 0) ? tagWrap("h3", categories[cat]) : "";
                bodyStats += (eventsInCategory.Count > 0) ? htmlEventList(eventsInCategory) : "";
            }

            string body = bodyOverview + bodyStats;
            string footer = "</body></html>";
            writer.WriteLine(header + banner + body + footer);
            writer.Close();
            ReportForm rf = new ReportForm(fullPath);
            
        }

        public void generateCSV()
        {
            string directory = Environment.CurrentDirectory;
            string filename = "Report-" + now.ToString("dd MMMM yyyy") + ".csv";
            string fullPath = directory + "\\" + filename;
            

            string colHeaders = "\"Event ID\"," + "\"Event Begin\"," + "\"Event End\"," + "\"Event Created\"," + "\"Event Subject\"," + "\"Event Description\"," + "\"Event Category\"\n";
            string tableData = "";

            for (int e = 0; e < numTotalEvents; e++)
            {
                if (eventRecords[e].Item2 > now)
                {
                    tableData += "\"" + eventRecords[e].Item1 + "\",\"" + eventRecords[e].Item2 + "\",\"" + eventRecords[e].Item3 + "\",\"" + eventRecords[e].Item4 + "\",\"" + eventRecords[e].Item5 + "\",\"" + eventRecords[e].Item6 + "\",\"" + eventRecords[e].Item7 + "\"\n";
                }
            }
            if (File.Exists(fullPath))
            {
                try
                {
                    File.WriteAllText(fullPath, String.Empty);
                }
                catch (IOException ioe)
                {
                    Console.WriteLine(ioe.Message);
                }
            }
            System.IO.StreamWriter writer = new System.IO.StreamWriter(fullPath);
            writer.WriteLine(colHeaders + tableData);
            writer.Close();
            Process.Start(fullPath);
        }

        private String tagWrap<T>(String tag, T text)
        {
            return "<" + tag + ">" + text + "</" + tag + ">";
        }

        private String tagWrap<T>(String tag, String style, T text)
        {
            return "<" + tag + " style=\"" + style + "\"" + ">" + text + "</" + tag + ">";
        }

        private String tagWrap<T>(String tag, String attr, String attrEquals, T text)
        {
            return "<" + tag + " " + attr + "=\"" + attrEquals + "\"" + ">" + text + "</" + tag + ">";
        }

        private String insertCSS(string file)
        {
            String css = "<style type=\"text/css\">";
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    css += sr.ReadToEnd();
                    sr.Close();
                }
                return css + "</style>";
            }
            catch (FileNotFoundException e)
            {
                return "";
            }
        }

        private class Category
        {
            public string school, holiday, birthday, work, appointment;

            public Category()
            {
                holiday = "HOLIDAY";
                school = "SCHOOL";
                birthday = "BIRTHDAY";
                work = "WORK";
                appointment = "APPOINTMENT";
            }
        }
    }
}
