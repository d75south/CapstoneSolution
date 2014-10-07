using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using BILiteDataLayer;
using System.Drawing;

namespace BILiteReporting
{
    public  class TableObjects
    {
        private static Dictionary<CheckedListBox, Point> tableObjectsDict = new Dictionary<CheckedListBox,Point>();

        private static CheckedListBox CreateTableObject(String tableName)
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

        /// <summary>
        /// Creates a table object based on the name passed in and combines the objectName with the location of
        /// the object. This allows for calling mouse move based on the active control being the name of the table
        /// object and using the stored location as the previous location for the offset on mouse move.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="tableObjectLocation"></param>
        public static void CreateTableDictionaryObject(String tableName, Point tableObjectLocation)
        {
            tableObjectsDict.Add(CreateTableObject(tableName), tableObjectLocation);
        }

        public static Dictionary<CheckedListBox, Point> GetTableObjectsList()
        {
            return tableObjectsDict;
        }
    }
}
