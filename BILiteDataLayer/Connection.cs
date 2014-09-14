using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlServerCe;

namespace BILiteDataLayer
{
    public class Connection
    {
        public static StringBuilder exceptionString = new StringBuilder();
        
        public Connection(){}

        public static String SystemDBConnectionString { get { return "Server=localhost;Database=TestData;Trusted_Connection=True;"; } }
        
        public static SqlConnection NewConnection(String connectString)
        {            
            using (SqlConnection connection = new SqlConnection(connectString))
            {
                connection.Open();
                return connection;
            }            
        }

        public static void SystemDBConnection()
        {
            exceptionString.Clear();
            try
            {
                NewConnection(SystemDBConnectionString);
            }
            catch (Exception ex)
            {
                exceptionString.Append(ex.ToString());
            }
            
        }

        public static Boolean fireDBConnectionException()
        {
            if (exceptionString.Length > 0)
                return true;
            else
                return false;
        }

    }
}

