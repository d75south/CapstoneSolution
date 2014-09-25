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

namespace BILiteMain
{
    public partial class MainForm : Form
    {
        private Point previousLocation;
        private Control activeControl;
        private MainController controller;
        private TreeView t1;
        private ContextMenuStrip OptionTreeContext;

        public MainForm()
        {
            InitializeComponent();

            this.Size = new Size(1000, 1000);
            this.AutoScroll = true;     
            this.Load += MainForm_Load;
            this.Resize += MainForm_Resize;
            //addMenu1.MenuItems.Add("Add", new EventHandler());
                       
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is ListBox)
                {
                    ctrl.MouseClick += ctrl_MouseClick;
                    ctrl.MouseDown += ctrl_MouseDown;
                    ctrl.MouseMove += ctrl_MouseMove;
                    ctrl.MouseUp += ctrl_MouseUp;
                }
            }

            t1 = new TreeView();

        }


        void MainForm_Resize(object sender, EventArgs e)
        {
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

            t1.MouseUp += On_TreeViewMouseUp;
            OptionTreeContext.MouseClick+= OptionTreeContext_MouseClick;

        }

        private void OptionTreeContext_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (t1.SelectedNode != null)
                {

                    if (t1.SelectedNode.Parent.Parent == null)
                    {
                        MessageBox.Show("[" + controller.GetActualDBName(t1.SelectedNode.Parent.ToString().Replace("TreeNode: ", "")) + "]");
                    }

                    else if (controller.GetConnectionNameList().IndexOf(t1.SelectedNode.Parent.ToString().Replace("TreeNode:", "").Trim()) == -1)
                    {
                        MessageBox.Show("[" + controller.GetActualDBName(t1.SelectedNode.Parent.Parent.ToString().Replace("TreeNode: ", "")) + "]");
                    }
                }
            }
        }

        public void ctrl_MouseClick(object sender, EventArgs e)
        {
            Control control = (Control)sender;
        }

        public void ctrl_MouseDown(object sender, MouseEventArgs e)
        {
            activeControl = (Control)sender;
            previousLocation = e.Location;
            Cursor = Cursors.Hand;
        }

        public void ctrl_MouseMove(object sender, MouseEventArgs e)
        {
            if (activeControl == null || activeControl != sender)
                return;
            var location = activeControl.Location;
            location.Offset(e.Location.X - previousLocation.X, e.Location.Y - previousLocation.Y);
            activeControl.Location = location;
        }

        public void ctrl_MouseUp(object sender, MouseEventArgs e)
        {
            activeControl = null;
            Cursor = Cursors.Default;
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

            //move all of this into a ContextMenuOpening Event
            /*move part dealing with treeview.SelectedNode value not null then into a seperate method
              amounts to basically, everything below the if(e.button == MouseButtons.Right) block
             */
            //check the contextMenu.for the value in the list, if it's not there then items.add("add")
            //else items.add("add");
            /*context menu event creates the table and adds fully qualified table name to a tag of the 
              table object it creates. This wil be used by the From clause to establish the Select 
              statement. 
             *Still need a class for creating a list of table objects. Takes the table name as an
              argument and uses this to return a datatable with a list of the colums so it can populate
              the list box that will be the table. 
             *The table will have a a label above it positioned by the anchor of the listbox. 
             *This may have to invalidate as the listbox(table) moves.
             *MessageBox.Show("[" + controller.GetActualDBName(treeview.SelectedNode.Parent.Parent.ToString().Replace("TreeNode: ", "")) + "]" +
                              treeview.SelectedNode.Parent.ToString().Replace("TreeNode: ", "."));
             */

            TreeView treeview = (TreeView)sender;

            if (e.Button == MouseButtons.Right)
            {
                // Select the clicked node
               treeview.SelectedNode = treeview.GetNodeAt(e.X, e.Y);

               if (treeview.SelectedNode != null)
               {

                   if (treeview.SelectedNode.Parent.Parent == null)
                   {
                       OptionTreeContext = new ContextMenuStrip();
                       t1.ContextMenuStrip = OptionTreeContext;
                       OptionTreeContext.Items.Add("Add");
                       OptionTreeContext.Show(this, new Point(e.X, e.Y));

                      // MessageBox.Show("[" + controller.GetActualDBName(treeview.SelectedNode.Parent.ToString().Replace("TreeNode: ", "")) + "]");
                   }

                   else  if (controller.GetConnectionNameList().IndexOf(treeview.SelectedNode.Parent.ToString().Replace("TreeNode:", "").Trim()) == -1)
                   {
                       OptionTreeContext = new ContextMenuStrip();
                       t1.ContextMenuStrip = OptionTreeContext;
                       OptionTreeContext.Items.Add("Add");
                       OptionTreeContext.Show(this, new Point(e.X, e.Y));

                      // MessageBox.Show("[" + controller.GetActualDBName(treeview.SelectedNode.Parent.Parent.ToString().Replace("TreeNode: ", "")) + "]");
                   }
               }
            }
        }
    }     
}
