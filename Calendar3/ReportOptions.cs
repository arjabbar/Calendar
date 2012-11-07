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
    public partial class ReportOptions : Form
    {
        SqlCommand sqlCmd;
        SqlDataReader sqlRdr;
        public ReportOptions(SqlCommand sqlCmd, SqlDataReader sqlRdr)
        {
            InitializeComponent();
            this.sqlCmd = sqlCmd;
            this.sqlRdr = sqlRdr;
        }

        private void outputButton_Click(object sender, EventArgs e)
        {
            //Uncomment these lines to randomly generate events every button click.
            RandomEventGenerator reg = new RandomEventGenerator(DateTime.Now, this.sqlCmd, this.sqlRdr);
            reg.insertRecords(5);

            ReportGenerator rg = new ReportGenerator(this.sqlCmd, this.sqlRdr);
            if (htmlButton.Checked)
            {
                rg.generateHTMLReport();
                status.Text = "Generated HTML Report.";
            }
            else if (csvButton.Checked)
            {
                rg.generateCSV();
                status.Text = "Saved in Project Folder. Opening...";
            }
        }
    }
}
