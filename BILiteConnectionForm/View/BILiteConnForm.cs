using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BILiteMain.Controller;

namespace BILiteMain
{
    public partial class BILiteConnForm : Form
    {

        public String connName { get; set; }
        public String connLocation { get; set; }
        public String dbName { get; set; }
        public Boolean isWinAuth { get; set; }
        private ConnectionController connectionController;
        private const int CP_NOCLOSE_BUTTON = 0x200;

        public delegate void ThrowsMessageBox(Object sender, EventArgs e);

        public BILiteConnForm()
        {
            InitializeComponent();
            SubmitToDBButton.Click += SubmitFormToSysDatabase;
            this.Load += BILiteConnForm_Load;
            WinAuthCheckBox.CheckedChanged += WinAuthCheckBox_CheckedChanged;
            CancelButton.Click += Cancel_Button_Click;                                
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle = cp.ClassStyle | CP_NOCLOSE_BUTTON;
                return cp;
            }
        }

       private void BILiteConnForm_Load(object sender, EventArgs e)
        {
            WinAuthCheckBox.Checked = true;

            if (WinAuthCheckBox.Checked)
            {
                PasswordTextBox.Enabled = false;
                UserNameTextBox.Enabled = false;
            }

            connectionController = new ConnectionController();

            DatabaseDropDown.Enabled = false;           
        }



        private void SubmitFormToSysDatabase(Object sender, EventArgs e)
        {
            if (connectionController.fireSysDbExceptionMessage())
            {
                MessageBox.Show("There was an error connecting to the system database");
            }
            else
            {

            }
        }

        private void WinAuthCheckBox_CheckedChanged(Object sender, EventArgs e)
        {
            if (WinAuthCheckBox.Checked)
            {
                PasswordTextBox.Enabled = false;
                UserNameTextBox.Enabled = false;
            }
            else
            {
                PasswordTextBox.Enabled = true;
                UserNameTextBox.Enabled = true;
            }
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
