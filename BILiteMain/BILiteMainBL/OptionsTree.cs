using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using BILiteDataLayer;
using System.Windows.Forms;

namespace BILiteMain
{
    class OptionsTree
    {
        private string testVariable = null;
        private MainForm mainForm = new MainForm();
        #region "Connections Available"        
        public static DataTable GetTablesColumns(String connectionString)
        {
            String commandString = @"SELECT TABLE_SCHEMA + '.' + TABLE_NAME tname,CASE WHEN DATA_TYPE like '%decimal%' OR  
                                     DATA_TYPE like '%numeric%'
                                     THEN COLUMN_NAME + ' ('+ DATA_TYPE + '(' + 
                                     CAST(NUMERIC_PRECISION AS VARCHAR) + ',' + 
                                     CAST(NUMERIC_SCALE AS VARCHAR) +')' + ',' + ' ' + 
                                     CASE WHEN IS_NULLABLE = 'YES' THEN 'null' ELSE 'not null' END + ')' 
                                     WHEN DATA_TYPE like '%char%' THEN COLUMN_NAME +' (' + DATA_TYPE + +'('+CAST(CHARACTER_MAXIMUM_LENGTH AS VARCHAR) +')'+ ', ' +
                                     CASE WHEN IS_NULLABLE = 'YES' THEN 'null' ELSE 'not null' END + ')' 
                                     ELSE COLUMN_NAME +'('+DATA_TYPE+', ' +
                                     CASE WHEN IS_NULLABLE = 'YES' THEN 'null' ELSE 'not null' END + ')' 
                                     END AS cname
                                     FROM INFORMATION_SCHEMA.COLUMNS 
                                     ORDER BY TABLE_SCHEMA";

            return Connection.PopulateTable(commandString, connectionString);
        }

        public static DataTable GetConnectionsData(String sqlCommandString)
        {            
            return Connection.PopulateTable(sqlCommandString, Connection.SystemDBConnectionString);
        }
        #endregion

        #region"Reports Available"
        //public static DataTable GetReports(String connectionString)
//        {
//            String commandString = @"SELECT TABLE_SCHEMA + '.' + TABLE_NAME tname,CASE WHEN DATA_TYPE like '%decimal%' OR  
//                                     DATA_TYPE like '%numeric%'
//                                     THEN COLUMN_NAME + ' ('+ DATA_TYPE + '(' + 
//                                     CAST(NUMERIC_PRECISION AS VARCHAR) + ',' + 
//                                     CAST(NUMERIC_SCALE AS VARCHAR) +')' + ',' + ' ' + 
//                                     CASE WHEN IS_NULLABLE = 'YES' THEN 'null' ELSE 'not null' END + ')' 
//                                     WHEN DATA_TYPE like '%char%' THEN COLUMN_NAME +' (' + DATA_TYPE + +'('+CAST(CHARACTER_MAXIMUM_LENGTH AS VARCHAR) +')'+ ', ' +
//                                     CASE WHEN IS_NULLABLE = 'YES' THEN 'null' ELSE 'not null' END + ')' 
//                                     ELSE COLUMN_NAME +'('+DATA_TYPE+', ' +
//                                     CASE WHEN IS_NULLABLE = 'YES' THEN 'null' ELSE 'not null' END + ')' 
//                                     END AS cname
//                                     FROM INFORMATION_SCHEMA.COLUMNS 
//                                     ORDER BY TABLE_SCHEMA";

//            return PopulateDataTable.PopulateTable(commandString, connectionString);
//        }

//        public static DataTable GetConnectionsData(String sqlCommandString)
//        {
//            return PopulateDataTable.PopulateTable(sqlCommandString, Connection.SystemDBConnectionString);
//        }
        #endregion

        #region

        #endregion
    }
}
