using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using BILiteDataLayer;

namespace BILiteMain
{
    public class MainController
    {
        private MainForm form = new MainForm();
        private List<Control> form1ControlList;
        private NewControl newControl = new NewControl();        

        public MainController()
        {
            form1ControlList = new List<Control>();
        }

        //gets list of controls currently on the screen
        public void GetControlsList()
        {
            foreach (Control ctrl in form.Controls)
            {
                form1ControlList.Add(ctrl);
            }
        }

        //public Control CreateListBox()
        //{
        //    Control lb = newControl.NewListBox();
        //    return lb;
        //}

        public Control CreateTreeView()
        {
            Control tr = newControl.NewTreeView();
            return tr;
        }

        public void systemDBConnection()
        {
            Connection con = new Connection();
            con.SystemDBConnection();
        }

        public bool fireSysDbExceptionMessage()
        {
            if (Connection.fireSystemDBConnectionException())
            {
                return true;
            }
            else
                return false;
        }
    }
}
