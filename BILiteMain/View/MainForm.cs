using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BILiteConnectionForm;
using BILiteReporting;

namespace BILiteMain
{
    public partial class MainForm : Form
    {
        private MainController controller;
        private TreeView t1;
        private ContextMenuStrip OptionTreeContext;
        private ContextMenuStrip CheckLstContext;
        private String SelectedNode { get; set; }
        private bool IsInList { get; set; }
        private bool IsSelectedNodeParentParentNode { get; set; }
        private String TableReference { get; set; }
        private Boolean dragInProgress = false;
        int MouseDownX = 0;
        int MouseDownY = 0;
        Control activeControl;
        private MyCheckedListBox chlb;
        private StringBuilder selectBuilder = new StringBuilder();
        private StringBuilder fromBuilder = new StringBuilder();
        List<Line> lineList = new List<Line>();
        Line currentLine;
        Line ActiveLine;
        private ContextMenuStrip checkedListItem;
        private String SelectedItemName;
        private String CheckedListBoxForDetermination;
        private String FullyQualTableFieldName;
        private String ColumnDataType;
        private int JoinID = 0;
        private DataTable JoinsTable = new DataTable("Joins");
        private DataTable tableItems = new DataTable("ItemsAddedToTable");
        DataTable dataGridView1Table = new DataTable();
        private List<String> tableList = new List<String>();
        public static StringBuilder selectForBuild = new StringBuilder();
        
        public MainForm()
        {
            InitializeComponent();

            this.Size = new Size(1000, 1000);
            this.AutoScroll = true;
            this.Load += MainForm_Load;
            this.Resize += MainForm_Resize;
            panel4.MouseMove += Form1_MouseMove;
            panel4.MouseUp += Form1_MouseUp;
            panel4.MouseDown += Form1_MouseDown;

            t1 = new TreeView();
            label1.MouseDown += label_MouseDown;
            label2.MouseDown += label_MouseDown;
            label4.MouseDown += label_MouseDown;

            //panel5.Resize += panel5_Resize;

            DataColumn ID = new DataColumn("ID");
            ID.DataType = System.Type.GetType("System.Int32");
            JoinsTable.Columns.Add(ID);

            DataColumn Table1Name = new DataColumn("Table1Name");
            Table1Name.DataType = System.Type.GetType("System.String");
            JoinsTable.Columns.Add(Table1Name);

            DataColumn Table1ColName = new DataColumn("Table1ColName");
            Table1ColName.DataType = System.Type.GetType("System.String");
            JoinsTable.Columns.Add(Table1ColName);

            DataColumn Table1DataType = new DataColumn("Table1DataType");
            Table1DataType.DataType = System.Type.GetType("System.String");
            JoinsTable.Columns.Add(Table1DataType);

            DataColumn Table2Name = new DataColumn("Table2Name");
            Table2Name.DataType = System.Type.GetType("System.String");
            JoinsTable.Columns.Add(Table2Name);

            DataColumn Table2ColName = new DataColumn("Table2ColName");
            Table2ColName.DataType = System.Type.GetType("System.String");
            JoinsTable.Columns.Add(Table2ColName);

            DataColumn Table2DataType = new DataColumn("Table2DataType");
            Table2DataType.DataType = System.Type.GetType("System.String");
            JoinsTable.Columns.Add(Table2DataType);

            DataColumn JoinType = new DataColumn("JoinType");
            JoinType.DataType = System.Type.GetType("System.String");
            JoinsTable.Columns.Add(JoinType);

            DataColumn LineName = new DataColumn("LineName");
            LineName.DataType = System.Type.GetType("System.String");
            JoinsTable.Columns.Add(LineName);

            //the following represent points on the screen at the location that line begins and ends
              /* TODO: these will have to be cast to System.Drawing.Point coming out of the table and 
                       System.String going in. */

            DataColumn LineStart = new DataColumn("LineStart");
            LineStart.DataType = System.Type.GetType("System.String");
            JoinsTable.Columns.Add(LineStart);

            DataColumn LineEnd = new DataColumn("LineEnd");
            LineEnd.DataType = System.Type.GetType("System.String");
            JoinsTable.Columns.Add(LineEnd);

            DataColumn TableName = new DataColumn("TableName");
            TableName.DataType = System.Type.GetType("System.String");
            tableItems.Columns.Add(TableName);

            DataColumn Items = new DataColumn("Items");
            Items.DataType = System.Type.GetType("System.String");
            tableItems.Columns.Add(Items);

           
            DataColumn tableName = new DataColumn("TableName");
            tableName.DataType = System.Type.GetType("System.String");
            dataGridView1Table.Columns.Add(tableName);
        }

        Point deltaStart;
        Point deltaEnd;
        bool dragging = false;

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            foreach (Line cl in lineList)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left && cl.IsStartPoint(e.Location, 10))
                {
                    ActiveLine = cl;
                    dragging = true;
                    deltaStart = new Point(ActiveLine.Start.X - e.Location.X, ActiveLine.Start.Y - e.Location.Y);
                }
                if (e.Button == System.Windows.Forms.MouseButtons.Left && cl.IsEndPoint(e.Location, 10))
                {
                    ActiveLine = cl;
                    dragging = true;
                    deltaEnd = new Point(ActiveLine.End.X - e.Location.X, ActiveLine.End.Y - e.Location.Y);
                }
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
            deltaStart.X = 0;
            deltaStart.Y = 0;
            deltaEnd.X = 0;
            deltaEnd.Y = 0;
            ActiveLine = null;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                if (deltaStart.X != 0 && deltaStart.Y != 0)
                {
                    ActiveLine.Start = new Point(deltaStart.X + e.Location.X, deltaStart.Y + e.Location.Y);
                }
                if (deltaEnd.X != 0 && deltaEnd.Y != 0)
                {
                    ActiveLine.End = new Point(deltaEnd.X + e.Location.X, deltaEnd.Y + e.Location.Y);
                }

                this.Refresh();
            }
        }

        void panel5_Resize(object sender, EventArgs e)
        {
            if (panel9.Visible == true)
            {

            }
        }


        protected void MainForm_Resize(object sender, EventArgs e)
        {
            //foreach (Control ctrl in panel4.Controls)
            //{
            //    ctrl.Refresh();
            //}
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            controller = new MainController();
            controller.systemDBConnection();


            if (controller.fireDbExceptionMessage())
            {
                MessageBox.Show("There was an error connecting to the database");
                this.Close();
            }

            OptionsTreeViewLoad();
            panel5.Visible = false;
            panel5.Height = 32 * 8;
            panel5.Location = new Point(panel5.Location.X, panel5.Location.Y + 100);
            panel8.Height = 32;
            panel6.Height = 32;
            panel11.Height = 150;
            panel8.Location = new Point(panel8.Location.X, panel8.Location.Y - 59);
            panel11.Location = new Point(panel11.Location.X, panel11.Location.Y - 118);
            t1.MouseUp += On_TreeViewMouseUp;
            label1.MouseEnter += label_MouseEnter;
            label1.MouseLeave += label_MouseLeave;
            label2.MouseEnter += label_MouseEnter;
            label2.MouseLeave += label_MouseLeave;
            label4.MouseEnter += label_MouseEnter;
            label4.MouseLeave += label_MouseLeave;
        }

        private void label_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void label_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void OptionTreeContext_MouseClick(object sender, MouseEventArgs e)
        {
            var txt = sender as MenuItem;
            MyCheckedListBox chListbox;
            chListbox = controller.GetTableObject(TableReference);
            chListbox.Location = controller.GetTableObjectLocation(controller.GetActualTableName(TableReference));
            String[] tempArray = TableReference.Split('.');
            chListbox.Name = tempArray[0] + '.' + tempArray[1] + '.' + tempArray[2];
            chListbox.ItemCheck += CheckedList_ItemCheck;
            chListbox.MouseDown += ctrl_MouseDown;
            chListbox.Scroll += chListbox_Scroll;
            //TODO: need to create a delegate to handle this event
            /*Delegate should do what the ctrl_MouseDown event is currently doing and 
              take dr from menu_Click Event*/
            //chListbox.MouseDown += ctrl_MouseDown;
            chListbox.CheckOnClick = true;
            CheckLstContext = new ContextMenuStrip();
            chListbox.ContextMenuStrip = CheckLstContext;

            ToolStripItem remove = CheckLstContext.Items.Add("Remove");
            ToolStripItem innerjoin = CheckLstContext.Items.Add("Inner Join");
            ToolStripItem leftjoin = CheckLstContext.Items.Add("Left Join");
            ToolStripItem rightjoin = CheckLstContext.Items.Add("Right Join");

            remove.MouseDown += menu_Click;
            innerjoin.MouseDown += menu_Click;
            leftjoin.MouseDown += menu_Click;
            rightjoin.MouseDown += menu_Click;

            if (SelectedNode != null)
            {
                
                DataRow JoinsNewRow1 = JoinsTable.NewRow();
                JoinsNewRow1["ID"] = JoinID;
                JoinsNewRow1["Table1Name"] = TableReference;

                List<String> itemsAddedList = new List<string>();
                itemsAddedList = controller.GetListOfItemsAdded();

                foreach (String item in itemsAddedList)
                {
                    DataRow ItemsNewRow1 = tableItems.NewRow();
                    ItemsNewRow1["TableName"] = TableReference;
                    ItemsNewRow1["Items"] = TableReference;
                }

                if (JoinsTable.Rows.Count > 0)
                {
                    foreach (Control ctrl in panel4.Controls)
                    {
                        //if (ctrl is MyCheckedListBox)
                        //{
                        //    MyCheckedListBox chlstbx = (MyCheckedListBox) ctrl;
                        //    //List<String> itemsAddedList = new List<string>();
                        //    itemsAddedList = controller.GetListOfItemsAdded();
                        //    foreach(String item in itemsAddedList)
                        //    {
                        //        MessageBox.Show(item);
                        //    }
                        //}
                        
                    }
                }
                JoinsTable.Rows.Add(JoinsNewRow1);
                JoinID += 1;
                panel4.Controls.Add(chListbox);

                int? length = TableReference.Length;
                tableList.Add(TableReference);
                if (tableList.Count > 1)
                {
                    fromBuilder.Append(" Cross Join" + Environment.NewLine);
                }
                fromBuilder.Append(TableReference);
            }

            //var results = from myRow in JoinsTable.AsEnumerable()
            //              where myRow.Field<int?>("ID") == 0
            //              select myRow["Table1Name"];
            //String table1Name = results.Single().ToString();

            controller.AddPKeysToDataTableForTableAdded(chListbox.Name);

            SelectedNode = null;

            panel5.Visible = true;
        }

        //ScrollEventType mLastScroll = ScrollEventType.EndScroll;
        void chListbox_Scroll(object sender, ScrollEventArgs e)
        {
            //this seems to be where it hits on every action of the scroll bar
            //could put the code here for handling all scrolls regardless of the type
        }

        private void menu_Click(object sender, MouseEventArgs e)
        {
            //joins datatable is created at project load

            JoinID = JoinID + 1;
            DataRow dr = JoinsTable.NewRow();
            dr["ID"] = JoinID;

            ToolStripItem toolStripItem = sender as ToolStripItem;
            toolStripItem.MouseUp += toolStripItem_Click;
            //TODO: This needs to get the checkbox of the 
            Control ctrl = toolStripItem.GetCurrentParent().TopLevelControl;
            ContextMenuStrip ctxtStrip  = (ContextMenuStrip)ctrl;
            CheckedListBox chlb = (CheckedListBox)ctxtStrip.SourceControl;

            String selected = e.ToString();

  
        }

       private void toolStripItem_Click(object sender, MouseEventArgs e)
        {
            ToolStripMenuItem toolStripItem = new ToolStripMenuItem();
            
            switch (sender.ToString())
            {
                case "Remove":
                    foreach (int i in chlb.CheckedIndices)
                    {
                        chlb.SetItemCheckState(i, CheckState.Unchecked);

                        String valueToRemove = null;
                        int lengthValue = (chlb.Name + "." + chlb.GetItemText(chlb.Items[i])).Length;
                        if (selectBuilder.Length == lengthValue)
                        {
                            valueToRemove = chlb.Name + "." + chlb.GetItemText(chlb.Items[i]);
                        }
                        else if (selectBuilder.ToString().Contains(", " + chlb.Name + "." + chlb.GetItemText(chlb.Items[i])))
                        {
                            valueToRemove = ", " + chlb.Name + "." + chlb.GetItemText(chlb.Items[i]);
                        }
                        else
                        {
                            valueToRemove = chlb.Name + "." + chlb.GetItemText(chlb.Items[i]) + ", ";
                        }

                        String selectString = selectBuilder.ToString();
                        if (selectString.Contains(valueToRemove.Trim()))
                        {
                            selectString = selectString.Replace(valueToRemove, "");
                            selectBuilder = new StringBuilder();
                            selectBuilder.Append(selectString);
                        }
                    }
                    textBox1.Text = "Select " + selectBuilder.ToString() + Environment.NewLine +
                                    "From" + Environment.NewLine + "Where";
                    panel4.Controls.Remove(chlb);
                    break;
                case "Inner Join":
                    //currentLine = new Line(Pens.Black, new Point(150,150), new Point(140, 140));
                    //lineList.Add(currentLine);
                    //this.Invalidate();
                    //panel4.Refresh();
                    // TODO: update joins table to reflect inner join - use joinID as unique ID for row
                    //chlb.Name gets the name of the checkbox that generated the contextmenu
                    MessageBox.Show(chlb.Name);


                    break;
                case "Right Join":
                    //ctrl_MouseDown(ctrl, e, dr);
                    // TODO: update joins table to reflect right join - use joinID as unique ID for row
                    break;
                case "Left Join":
                    //ctrl_MouseDown(ctrl, e, dr);
                    //TODO: update joins table to reflect left join - use joinID as unique ID for row
                    break;
            } 
        }


        //protected void chbx_Click(object sender, MouseEventArgs e)
        //{
        //    chlb = (MyCheckedListBox)sender;
        //}


        protected void CheckedList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            chlb = sender as MyCheckedListBox;

            if (e.NewValue == CheckState.Checked)
            {
                String newItem = null;
                if (selectBuilder.Length == 0)
                {
                    newItem = chlb.Name + "." + chlb.SelectedItem.ToString();
                }
                else
                {
                    newItem = ", " + chlb.Name + "." + chlb.SelectedItem.ToString();
                }
                selectBuilder.Append(newItem);
            }
            if (e.NewValue == CheckState.Unchecked)
            {
                String valueToRemove = null;
                if (chlb.SelectedItem == null)
                {
                    panel4.Controls.Remove(chlb);
                    return;
                }
                
                int? lengthValue = (chlb.Name + "." + chlb.SelectedItem.ToString()).Length;
                if (lengthValue != null)
                {
                    if (selectBuilder.Length == lengthValue)
                    {
                        valueToRemove = chlb.Name + "." + chlb.SelectedItem.ToString();
                    }
                    else if (selectBuilder.ToString().Contains(", " + chlb.Name + "." + chlb.SelectedItem.ToString()))
                    {
                        valueToRemove = ", " + chlb.Name + "." + chlb.SelectedItem.ToString();
                    }
                    else
                    {
                        valueToRemove = chlb.Name + "." + chlb.SelectedItem.ToString() + ", ";
                    }

                    String selectString = selectBuilder.ToString();
                    if (selectString.Contains(valueToRemove.Trim()))
                    {
                        selectString = selectString.Replace(valueToRemove, "");
                        selectBuilder = new StringBuilder();
                        selectBuilder.Append(selectString);
                    }
                }
            }
           
            textBox1.Text = "Select " + selectBuilder.ToString() + Environment.NewLine + 
                "From" + fromBuilder.ToString() + Environment.NewLine;
            PopulateDataGridView1();
            selectForBuild.Clear();
            selectForBuild.Append(textBox1.Text.Replace("\r", " ").Replace("\n", " "));

        }

        //gets the item clicked in the table object before displaying the toolStrip menu
        public void ctrl_MouseDown(object sender, MouseEventArgs e)
        {
            SelectedItemName = null;
            ColumnDataType = null;
            this.activeControl = sender as Control;

            if (e.Button == MouseButtons.Right)
            {
                MyCheckedListBox chbx = (MyCheckedListBox)activeControl;
                chlb = chbx;
                CheckedListBoxForDetermination = chbx.Name;
                object item = chbx.SelectedItem;
                var itemS = chbx.IndexFromPoint(e.Location);
                chbx.SelectedIndex = itemS;
                if (chbx.SelectedIndex != -1)
                {
                    SelectedItemName = chbx.SelectedItem.ToString();
                }
                else
                {
                    SelectedItemName = null;
                }
                if (SelectedItemName != null || SelectedItemName != "")
                {
                    FullyQualTableFieldName = chbx.Name + '.' + SelectedItemName;
                    //returns the datatype for the column selected from the table object and stores in a class level
                    //variable
                    ColumnDataType = controller.GetColumnDataType(FullyQualTableFieldName);
                // TODO update joins table to reflect data type for clumn - use joinID to identify row to update
                }
            }
        }


        public void ctrl_MouseUp(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                this.dragInProgress = false;
                MouseDownX = 0;
                MouseDownY = 0;
                this.activeControl = null;
            }

            return;
        }

        private void createNewToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            var connForm = new BILiteConnForm();
            connForm.Focus();
            connForm.Show();
        }

        private void OptionsTreeViewLoad()
        {
            t1 = controller.GetOptionsTree();
            panel3.Controls.Add(t1);
            t1.Dock = DockStyle.Fill;

        }

        private void On_TreeViewMouseUp(object sender, MouseEventArgs e)
        {
            TreeView treeview = (TreeView)sender;

            if (e.Button == MouseButtons.Right)
            {
                // Select the clicked node
                treeview.SelectedNode = treeview.GetNodeAt(e.X, e.Y);
                SelectedNode = treeview.SelectedNode.ToString();
                

                if (treeview.SelectedNode != null)
                {
                    OptionTreeContext = new ContextMenuStrip();
                    t1.ContextMenuStrip = OptionTreeContext;
                    OptionTreeContext.Items.Add("Add");
                    OptionTreeContext.MouseClick += OptionTreeContext_MouseClick;

                    IsSelectedNodeParentParentNode = treeview.SelectedNode.Parent.Parent == null;
                    IsInList = controller.GetConnectionNameList().IndexOf(treeview.SelectedNode.Parent.ToString().Replace("TreeNode:", "").Trim()) == -1;
                    if (IsSelectedNodeParentParentNode)
                    {

                        OptionTreeContext.Show(this, new Point(e.X, e.Y));

                        TableReference = "[" + controller.GetActualDBNameFromTableReference(treeview.SelectedNode.Parent.ToString().Replace("TreeNode: ", "")) + "]." + treeview.SelectedNode.ToString().Replace("TreeNode: ", "");
                    }

                    else if (IsInList)
                    {
                        OptionTreeContext.Show(this, new Point(e.X, e.Y));

                        TableReference = "[" + controller.GetActualDBNameFromTableReference(treeview.SelectedNode.Parent.Parent.ToString().Replace("TreeNode: ", "")) + "]." + treeview.SelectedNode.Parent.ToString().Replace("TreeNode: ", "");
                    }
                }
            }
        }

        private void label_MouseDown(object sender, EventArgs e)
        {
            var label = sender as Label;
            switch (label.Text)
            {
                case "Select":
                    if (panel6.Height == 32)
                    {
                        panel6.Height = 150;
                        panel8.Location = new Point(panel8.Location.X, panel8.Location.Y + 118);
                        panel11.Location = new Point(panel11.Location.X, panel11.Location.Y + 118);
                    }
                    else
                    {
                        panel6.Height = 32;
                        panel8.Location = new Point(panel8.Location.X, panel8.Location.Y - 118);
                        panel11.Location = new Point(panel11.Location.X, panel11.Location.Y - 118);
                    }
                    break;
                case "Where":
                    if (panel8.Height == 32)
                    {
                        panel8.Height = 150;
                        panel11.Location = new Point(panel11.Location.X, panel11.Location.Y + 118);
                    }
                    else
                    {
                        panel8.Height = 32;
                        panel11.Location = new Point(panel11.Location.X, panel11.Location.Y - 118);
                    }
                    break;
                case "Query Editor":
                    if (panel11.Height == 32)
                    {
                        panel11.Height = 150;
                    }
                    else
                    {
                        panel11.Height = 32;
                    }
                    break;
                default:
                    return;
            }


        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            foreach (Line cl in lineList)
            {
                this.Invalidate();
                e.Graphics.DrawLine(cl.pen, cl.Start, cl.End);
            }
        }


        
        private void PopulateDataGridView1()
        {
            dataGridView1Table.Clear();
            foreach (String item in selectBuilder.ToString().Split(','))
            {
                DataRow newdr = dataGridView1Table.NewRow();
                newdr["TableName"] = item;
                dataGridView1Table.Rows.Add(newdr);
            }
            dataGridView1.DataSource = dataGridView1Table;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BILiteConnectionForm.BILiteReportViewer.ReportViewerForm reportForm = new BILiteConnectionForm.BILiteReportViewer.ReportViewerForm();
            reportForm.Focus();
            reportForm.Show();

        }




    }
}
