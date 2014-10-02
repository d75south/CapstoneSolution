using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using BILiteDataLayer;

namespace BILiteReporting
{
    public class TableObjects
    {
        public static CheckedListBox CreateTableObject(String tableName)
        {
            CheckedListBox checkedListBox = new CheckedListBox();
            DataTable table = new DataTable();
            String[] tableReference = tableName.Split('.');
            String serverName = tableReference[0];
            String actualTableName = tableReference[2];
            table = Connection.PopulateTable(@"Use " + serverName + @" Select Column_Name 
                                               From Information_Schema.Columns Where Table_Name =" + "'" + 
                                               actualTableName + "'", @"Server=localhost;Trusted_Connection=True;");
            foreach (DataRow dr in table.Rows)
            {
                checkedListBox.Items.Add(dr["Column_Name"].ToString());
            }

            return checkedListBox;
        }
    }
}
