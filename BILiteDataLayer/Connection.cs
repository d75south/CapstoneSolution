using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

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

        public static DataTable PopulateTable(String commandString, String connectionString)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(commandString, con);
            SqlDataAdapter sqda = new SqlDataAdapter(command);
            sqda.Fill(dt);
            return dt;
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

