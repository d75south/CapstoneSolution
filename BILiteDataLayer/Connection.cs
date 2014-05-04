using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BILiteDataLayer
{
    public class Connection
    {
        private static StringBuilder exceptionString = new StringBuilder();
        public Connection(){}
        
        public SqlConnection NewConnection(String connectString)
        {            
            using (SqlConnection connection = new SqlConnection(connectString))
            {
                connection.Open();
                return connection;
            }            
        }

        public void SystemDBConnection()
        {
            exceptionString.Clear();
            try
            {
                NewConnection("Server=localhost;Database=TestData;Trusted_Connection=True;");
            }
            catch (Exception ex)
            {
                exceptionString.Append(ex.ToString());
            }
        }

        public static Boolean fireSystemDBConnectionException()
        {            
            if (exceptionString.Length > 0)
                return true;
            else
                return false;
        }

    }
}

