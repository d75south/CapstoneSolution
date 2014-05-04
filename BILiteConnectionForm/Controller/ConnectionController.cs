using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BILiteDataLayer;
using System.Data.SqlClient;

namespace BILiteMain.Controller
{
    class ConnectionController
    {
        BILiteConnForm ConnFormView = new BILiteConnForm();

        
        public void establishNewConnection(String connName, String connLocation, Boolean isWinAuth)
        {
            //*************MOVE THIS INTO A BL CLASS FOR BUILDING CONNECTION STRINGS*************
            //SqlConnectionStringBuilder sqlConnbuilder = new SqlConnectionStringBuilder();
            //if (ConnFormView.isWinAuth)
            //{
            //    sqlConnbuilder["Server"] = ConnFormView.Location;
            //    sqlConnbuilder["Database"] = ConnFormView.dbName;
            //    sqlConnbuilder["Integrated Security"] = "True";
            //    sqlConnbuilder["Asynchronous Processing"] = "True";
            //
            //}^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

            //Establishes the new connection
            Connection conn1 = new Connection();
            //conn1.NewConnection(sqlConnbuilder.ToString());

            // sends new conn1 specs into db
            //uses connection established at form initialization
            Connection con = new Connection();
            con.SystemDBConnection();
        }

        public bool fireSysDbExceptionMessage()
        {
            if (Connection.fireSystemDBConnectionException())            
                return true;            
            else
                return false;
        }        
    }
}
