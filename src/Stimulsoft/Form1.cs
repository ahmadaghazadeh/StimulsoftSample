using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clinic.Core;
using Dapper;
using Stimulsoft.Report;

namespace Stimulsoft
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var rpt = new StiReport();
            rpt.Load("E:\\Aghazadeh\\My Projects\\Sample\\C#\\Stimulsoft\\Stimulsoft\\Reports\\Report.mrt");
            rpt["@InDate"] = "1395/01/01";
            rpt.Show();
        }
    }

    internal class Post
    {
        public Guid PostID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Code { get; set; }
    }
}
