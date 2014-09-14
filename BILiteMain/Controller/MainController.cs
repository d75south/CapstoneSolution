using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using BILiteDataLayer;
using System.Data;

namespace BILiteMain
{
    public class MainController
    {
        private MainForm form = new MainForm();
        private List<Control> form1ControlList;       

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

        public void systemDBConnection()
        {           
            Connection.SystemDBConnection();
        }

        public bool fireDbExceptionMessage()
        {
            if (Connection.fireDBConnectionException())
            {
                return true;
            }
            else
                return false;
        }

        public TreeView GetOptionsTree()
        {
            TreeView treeView1 = new TreeView();
            
            
            foreach (DataRow dataRow in OptionsTree.GetConnectionsData("SELECT * FROM Connections").Rows)
            {
                HashSet<String> hs = new HashSet<String>();
                TreeNode connection = new TreeNode();
                String Connection_String = dataRow["Connection_String"].ToString().Trim();
                connection = treeView1.Nodes.Add(dataRow["Connection_Name"].ToString().Trim());
                foreach (DataRow dr in OptionsTree.GetTablesColumns(Connection_String).Rows)
                {
                    hs.Add(dr[0].ToString().Trim());
                }

                foreach (String str in hs)
                {
                    TreeNode table = new TreeNode();
                    table = connection.Nodes.Add(str.Trim());
                    foreach (DataRow dr in OptionsTree.GetTablesColumns(Connection_String).Rows)
                    {
                        if (table.Text.ToString().Trim() == dr[0].ToString().Trim())
                        {
                            table.Nodes.Add(dr[1].ToString().Trim());
                        }
                    }
                }
            }
            return treeView1;
        }

        public String getConnectionError()
        {
            return Connection.exceptionString.ToString();
        }
    }
}
