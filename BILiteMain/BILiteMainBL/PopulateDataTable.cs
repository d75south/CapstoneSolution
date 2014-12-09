using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using BILiteDataLayer;


namespace BILiteMain
{
    class PopulateDataTable
    {                     
        public static DataTable PopulateTable(String commandString, String connectionString)
        {          
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand command = new SqlCommand(commandString, con);
        SqlDataAdapter sqda = new SqlDataAdapter(command);
        sqda.Fill(dt);
        return dt;
        }
    }
}
