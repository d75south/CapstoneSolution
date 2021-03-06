﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using BILiteDataLayer;
using System.Data;
using BILiteReporting;

namespace BILiteMain
{
    public class MainController
    {
        private MainForm form = new MainForm();
        private List<Control> form1ControlList;
        private DataTable connectionInfo = new DataTable();
        private StringBuilder strBuilder = new StringBuilder();
        public List<String> TableList = new List<string>();
        private TableObjects tableObject = new TableObjects();

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

            connectionInfo = OptionsTree.GetConnectionsData("SELECT * FROM Connections");

            foreach (DataRow dataRow in connectionInfo.Rows)
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

        public List<String> GetConnectionNameList()
        {
           List<String> resultStringArray = new List<String>();

            foreach (DataRow dataRow in connectionInfo.Rows)
            {
                resultStringArray.Add(dataRow["Connection_Name"].ToString());
            }

            return resultStringArray;
        }

        public String GetActualDBNameFromTableReference(String ConnectionName)
        {

            DataRow connName = connectionInfo.Select("Connection_Name = " +"'"+ 
                ConnectionName.Trim() + "'").Single();
            String[] connNameString = connName["Connection_String"].ToString().Split(';');
            String connNameResult = connNameString[1].Replace("Database=", "").Trim();

            return connNameResult;
        }

        public StringBuilder GetTableList()
        {
            strBuilder.Clear();
            foreach (String str in TableList)
            {
                strBuilder.Append(str);
                strBuilder.Append(";");
            }

            return strBuilder;
        }

        public MyCheckedListBox GetTableObject(String tableName)
        {
            return TableObjects.CreateTableObject(tableName);
        }

        public Point GetTableObjectLocation(String tableName)
        {
            return TableObjects.GetTableObjectList()[tableName];
        }

        public String getConnectionError()
        {
            return Connection.exceptionString.ToString();
        }

        public String GetActualTableName(String tableName)
        {
            return TableObjects.GetActualTableName(tableName);
        }

        public String UpdateSelectStatement(String stringToAppend)
        {
            return SelectClause.SelectStatementAssembler(stringToAppend: stringToAppend);
        }

        public String GetColumnDataType(String tableName)
        {
            return TableObjects.GetColumnDataType(tableName);
        }


        public void AddPKeysToDataTableForTableAdded(String checkBoxName)
        {
            TableObjects.AddPKeysToDataTableForTableAdded(checkBoxName);
        }

        public void AddFKeysToDataTableForTableAdded(String checkBoxName)
        {
            TableObjects.AddFKeysToDataTableForTableAdded(checkBoxName);
        }

        public List<String> GetPrimaryKeysForTable(String checkBoxName)
        {
            return TableObjects.GetPrimaryKeysForTable(checkBoxName);
        }

        public List<String> GetForeignKeysForTable(String checkBoxName)
        {
            return TableObjects.GetForeignKeysForTable(checkBoxName);
        }


        public DataTable GetTableItemsTable()
        {
            return TableObjects.GetTableItemsTable();
        }
    }
}
