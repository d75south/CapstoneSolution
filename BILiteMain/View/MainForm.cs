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
        private String SelectedNode { get; set; }
        private bool IsInList { get; set; }
        private bool IsSelectedNodeParentParentNode { get; set; }
        private String TableReference { get; set; }

        public MainForm()
        {
            InitializeComponent();

            this.Size = new Size(1000, 1000);
            this.AutoScroll = true;     
            this.Load += MainForm_Load;
            this.Resize += MainForm_Resize;
                       
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
           
        }

        private void OptionTreeContext_MouseClick(object sender, EventArgs e)
        {
            var txt = sender as MenuItem;
            CheckedListBox chListbox = new CheckedListBox();
            chListbox = controller.GetTableObject(TableReference);

            if (SelectedNode != null)
            {
                controller.TableList.Add(TableReference);

                panel4.Controls.Add(chListbox);
            }
            SelectedNode = null;

            foreach (Control ctrl in panel4.Controls)
            {
                if (ctrl is CheckedListBox)
                {
                  //  ctrl.MouseClick += ctrl_MouseClick;
                    ctrl.MouseDown += ctrl_MouseDown;
                    ctrl.MouseMove += ctrl_MouseMove;
                    ctrl.MouseUp += ctrl_MouseUp;
                }
            }
        }

        //public void ctrl_MouseClick(object sender, EventArgs e)
        //{
        //    Control control = (Control)sender;
        //}

        public void ctrl_MouseDown(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
            activeControl = sender as Control;
            if (activeControl is CheckedListBox)
            {
                previousLocation = activeControl.Location;
            }
            else if (activeControl is Panel)
            {
                return;
            }
            else
            {
                previousLocation = activeControl.Parent.Location;
            }
            
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

                        TableReference = "[" + controller.GetActualDBName(treeview.SelectedNode.Parent.ToString().Replace("TreeNode: ", "")) + "]."+treeview.SelectedNode.ToString().Replace("TreeNode: ", "");
                   }

                   else  if (IsInList)
                   {
                       OptionTreeContext.Show(this, new Point(e.X, e.Y));

                       TableReference = "[" + controller.GetActualDBName(treeview.SelectedNode.Parent.Parent.ToString().Replace("TreeNode: ", "")) + "]." + treeview.SelectedNode.Parent.ToString().Replace("TreeNode: ", "");
                   }
               }
            }
        }
    }     
}
