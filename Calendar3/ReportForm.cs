using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Calendar
{
    public partial class ReportForm : Form
    {
        public ReportForm(String url)
        {
            InitializeComponent();
            reportBrowser.Url = new Uri("file:///" + url);
            this.Visible = true;
        }

        private void Report_Load(object sender, EventArgs e)
        {
            
        }
    }
}
