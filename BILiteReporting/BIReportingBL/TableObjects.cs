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

        private static Dictionary<String, Point> tableObjects = new Dictionary<String, Point>();
        private static Dictionary<Point, Point> tableLocations = new Dictionary<Point, Point>();
        public string TableName { get; set; }
        //private static Point TableLocation { get; set; }
        //private static Point AnchorLocation { get; set; }
        private static int tableXLocation = 0;
        private static int tableYLocation = 0;
        private static int tableNameAddition = 0;
        private static bool firstTableFlag = true;
        private static int KeysInitiatedFlag = 0;
        private static DataTable PrimaryKeys = new DataTable("Primary Keys");
        private static DataTable ForeignKeys = new DataTable("Foreign Keys");
        private static DataTable TableItemsTable = new DataTable();


        public static MyCheckedListBox CreateTableObject(String tableName)
        {
            MyCheckedListBox checkedListBox = new MyCheckedListBox();
            TableItemsTable.Clear();
            String[] tableReference = tableName.Split('.');
            String serverName = tableReference[0];
            String actualTableName = tableReference[2];
            TableItemsTable = Connection.PopulateTable(@"Use " + serverName + @" Select Table_Name, Column_Name, Data_Type 
                                               From Information_Schema.Columns Where Table_Name =" + "'" + 
                                               actualTableName + "'", @"Server=localhost;Trusted_Connection=True;");
            foreach (DataRow dr in TableItemsTable.Rows)
            {
                checkedListBox.Items.Add(dr["Column_Name"].ToString());

            }

            if (tableXLocation > 400)
            {
                tableXLocation = 10;
                Point lastTableLocation = tableObjects.Values.Last();
                tableYLocation = lastTableLocation.Y + 135;
            }
            else
            {
                if (firstTableFlag)
                {
                    tableXLocation = tableXLocation + 10;
                }
                else
                {
                    tableXLocation = tableXLocation + 200;
                }
            }

            if (tableObjects.ContainsKey(actualTableName))
            {
                tableNameAddition = tableNameAddition + 1;
                Point tempPoint = new Point(tableXLocation, tableYLocation);
                StoreTableOject(actualTableName + tableNameAddition.ToString(), tempPoint);
            }
            else
            {
                Point tempPoint = new Point(tableXLocation, tableYLocation);
                StoreTableOject(actualTableName, tempPoint);
            }

            if (KeysInitiatedFlag == 0)
            {
                DataColumn TableName = new DataColumn("TableName");
                TableName.DataType = System.Type.GetType("System.String");
                PrimaryKeys.Columns.Add(TableName);

                DataColumn ColumnName = new DataColumn("ColumnName");
                ColumnName.DataType = System.Type.GetType("System.String");
                PrimaryKeys.Columns.Add(ColumnName);

                DataColumn FKTableName = new DataColumn("TableName");
                FKTableName.DataType = System.Type.GetType("System.String");
                ForeignKeys.Columns.Add(FKTableName);

                DataColumn FKColumnName = new DataColumn("ColumnName");
                FKColumnName.DataType = System.Type.GetType("System.String");
                ForeignKeys.Columns.Add(FKColumnName);
            }
            KeysInitiatedFlag = 1;
            firstTableFlag = false;
            return checkedListBox;
        }
       
        public static String GetActualTableName(String tableName)
        {
            String[] tableReference = tableName.Split('.');
            String actualTableName = tableReference[2];
            return actualTableName;
        }

        private static void StoreTableOject(String tableName, Point tableObjectLocation)
        {
            tableObjects.Add(tableName, tableObjectLocation);
        }

        public static Dictionary<String, Point> GetTableObjectList()
        {
            return tableObjects;
        }

        /*Using the column data type determine whether you can aggregate the column or not*/
        public static String GetColumnDataType(String checkBoxName)
        {
            DataTable table = new DataTable();
            String[] tableReference = checkBoxName.Split('.');
            int countTableColumnName = tableReference.Count();
            String serverName = tableReference[0];
            String actualTableName = tableReference[2];
            String actualColumnName;
            if(countTableColumnName == 4)
            {
                 actualColumnName = tableReference[3];
            }
            else if(countTableColumnName == 5){
                actualColumnName = tableReference[3] + '.' + tableReference[4];
            }
            else
            {
                return "";
            }
            table = Connection.PopulateTable(@"Use " + serverName + @" Select Data_Type 
                                               From Information_Schema.Columns Where Table_Name =" + "'" +
                                               actualTableName + "'" + "and Column_Name = "+ "'" + actualColumnName + "'"
                                               , @"Server=localhost;Trusted_Connection=True;");

            String result = "";
            foreach (DataRow dr in table.Rows)
            {
                result = dr["Data_Type"].ToString();
            }

            return result;
        }



        public static void AddPKeysToDataTableForTableAdded(String checkBoxName)
        {
            //break up checkBoxName into its constituent parts
            String[] tempArray = checkBoxName.Split('.');

            String databaseName = tempArray[0];
            String schemaName = tempArray[1];
            String tableName = tempArray[2];

           String sqlString = "USE " + databaseName + " EXEC sp_pkeys @table_Name=" + "'" + tableName + "'" + ", @table_owner=" + "'" + schemaName + "'";
           DataTable dt = new DataTable();
           dt =  Connection.PopulateTable(sqlString, Connection.MasterDBConnectionString);
            foreach(DataRow dr in dt.Rows)
            {
                DataRow newDr = PrimaryKeys.NewRow();
                newDr["TableName"] = dr["TABLE_NAME"];
                newDr["ColumnName"] = dr["COLUMN_NAME"];

                PrimaryKeys.Rows.Add(newDr);
            }
        }

        public static void AddFKeysToDataTableForTableAdded(String checkBoxName)
        {
            //break up checkBoxName into its constituent parts
            String[] tempArray = checkBoxName.Split('.');

            String databaseName = tempArray[0];
            String schemaName = tempArray[1];
            String tableName = tempArray[2];

            String sqlString = "USE " + databaseName + " EXEC sp_fkeys @fktable_Name=" + "'" + tableName + "'" + ", @fktable_owner=" + "'" + schemaName + "'";
            DataTable dt = new DataTable();
            dt = Connection.PopulateTable(sqlString, Connection.MasterDBConnectionString);
            foreach (DataRow dr in dt.Rows)
            {
                DataRow newFKDr = ForeignKeys.NewRow();
                newFKDr["TableName"] = dr["FKTABLE_NAME"];
                newFKDr["ColumnName"] = dr["FKCOLUMN_NAME"];

                ForeignKeys.Rows.Add(newFKDr);
            }
        }

        public static List<String> GetPrimaryKeysForTable(String checkBoxName)
        {
            List<String> primaryKeysList = new List<string>();

            String[] tempArray = checkBoxName.Split('.');

            String databaseName = tempArray[0];
            String schemaName = tempArray[1];
            String tableName = tempArray[2];

            //All you need here is the table name - don't get confused
            foreach(DataRow dr in PrimaryKeys.Rows)
            {
                if(dr["TableName"].ToString() == tableName)
                {
                    primaryKeysList.Add(dr["ColumnName"].ToString());
                }
            }

            return primaryKeysList;
        }

        public static List<String> GetForeignKeysForTable(String checkBoxName)
        {
            List<String> foreignKeysList = new List<string>();

            String[] tempArray = checkBoxName.Split('.');

            String databaseName = tempArray[0];
            String schemaName = tempArray[1];
            String tableName = tempArray[2];

            foreach (DataRow dr in ForeignKeys.Rows)
            {
                if (dr["TableName"].ToString() == tableName)
                {
                    foreignKeysList.Add(dr["ColumnName"].ToString());
                }
            }

            return foreignKeysList;
        }

        public static DataTable GetTableItemsTable()
        {
            return TableItemsTable;
        }
    }
}
