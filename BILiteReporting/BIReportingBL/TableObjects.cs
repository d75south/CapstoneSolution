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
        private static List<String> itemsAddedList = new List<string>();
        public string TableName { get; set; }
        //private static Point TableLocation { get; set; }
        //private static Point AnchorLocation { get; set; }
        private static int tableXLocation = 0;
        private static int tableYLocation = 0;
        private static int tableNameAddition = 0;
        private static bool firstTableFlag = true;


        public static MyCheckedListBox CreateTableObject(String tableName)
        {
            MyCheckedListBox checkedListBox = new MyCheckedListBox();
            itemsAddedList.Clear();
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
                itemsAddedList.Add(dr["Column_Name"].ToString());
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
        public static String GetColumnDataType(String tableColumnName)
        {
            DataTable table = new DataTable();
            String[] tableReference = tableColumnName.Split('.');
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

        public static List<String> GetListOfItemsAdded()
        {
            return itemsAddedList;
        }
    }
}
