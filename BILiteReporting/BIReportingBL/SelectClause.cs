using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BILiteReporting
{
    public class SelectClause
    {

        private static StringBuilder AppendableString = new StringBuilder();

        public static string SelectStatementAssembler(string stringToAppend)
        {

            //move stringbuilder to class level as private variable
            //change this method to use that variable. remove for loop and
            //just append passed in value. Change this to return a String
            //use that string to construct the Select textbox. 
            //if select textbox is updated directly, have a method that searches the string from the textbox,
            //for the Select statement only, and checks for the first occurence of the specified string. If it finds it,
            //the loop restarts and checks for the next. If the index is -1, at the end, then the value passed 
            //is used to check the appropriate box in the table object

            AppendableString.Append("Select ");

            AppendableString.Append(stringToAppend + ", ");

            return AppendableString.ToString();
        }
    }
}
