using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace BILiteMain
{
    class NewControl
    {
        private int treeCounter;           

        public NewControl()
        {            
            treeCounter = 0;
        }

        //public Control NewListBox()
        //{
        //    ListBox list = new ListBox();
        //    String ctrlName = "txt" + listCounter.ToString();
        //    list.Name = ctrlName;
        //    list.Location = new Point(list_x, list_y);
        //    listCounter += 1;
        //    list_y = list_y + 125;
        //    return list;
        //}

        public Control NewTreeView()
        {
            MixedCheckBoxesTreeView tree = new MixedCheckBoxesTreeView();
            String ctrlName = "txt" + treeCounter.ToString();
            tree.Name = ctrlName;
            treeCounter += 1;            
            return tree;
        }
    }
}
