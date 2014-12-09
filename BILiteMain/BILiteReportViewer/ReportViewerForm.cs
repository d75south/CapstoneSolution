using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BILiteDataLayer;

namespace BILiteConnectionForm.BILiteReportViewer
{
    public partial class ReportViewerForm : Form
    {
        private DataTable reportTable = new DataTable("ReportViewerTable");
        public ReportViewerForm()
        {
            InitializeComponent();
            this.Load += MainForm_Load;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
            String commandString = BILiteMain.MainForm.selectForBuild.ToString();
            
            reportTable = Connection.PopulateTable(commandString, "Server=localhost;Trusted_Connection=True");
            dataGridView1.DataSource = reportTable;
            
        }
    }
}
