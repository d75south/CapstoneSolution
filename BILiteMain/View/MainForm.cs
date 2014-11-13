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
        private CheckedListBox chlb;
        private StringBuilder selectBuilder = new StringBuilder();

        public MainForm()
        {
            InitializeComponent();

            this.Size = new Size(1000, 1000);
            this.AutoScroll = true;
            this.Load += MainForm_Load;
            this.Resize += MainForm_Resize;

            t1 = new TreeView();
            label1.MouseDown += label_MouseDown;
            label2.MouseDown += label_MouseDown;
            label4.MouseDown += label_MouseDown;

            //panel5.Resize += panel5_Resize;

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

        private void OptionTreeContext_MouseClick(object sender, EventArgs e)
        {
            var txt = sender as MenuItem;
            CheckedListBox chListbox;
            chListbox = controller.GetTableObject(TableReference);
            chListbox.Location = controller.GetTableObjectLocation(controller.GetActualTableName(TableReference));
            chListbox.Name = controller.GetActualTableName(TableReference);
            chListbox.ItemCheck += CheckedList_ItemCheck;
            chListbox.Click += chbx_Click;
            chListbox.CheckOnClick = true;

            if (SelectedNode != null)
            {
                panel4.Controls.Add(chListbox);
            }

            SelectedNode = null;

            foreach (Control ctrl in panel4.Controls)
            {
                if (ctrl is CheckedListBox)
                {
                    CheckedListBox chbx = (CheckedListBox)ctrl;
                    CheckLstContext = new ContextMenuStrip();
                    chbx.ContextMenuStrip = CheckLstContext;
                    CheckLstContext.Items.Add("Remove");
                    CheckLstContext.MouseClick += CheckListContext_MouseClick;
                    
                }
            }
            panel5.Visible = true;
        }


        protected void chbx_Click(object sender, EventArgs e)
        {
            chlb = (CheckedListBox)sender;   
        }


        protected void CheckedList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            

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
                int lengthValue = (chlb.Name + "." + chlb.SelectedItem.ToString()).Length;
                if(selectBuilder.Length == lengthValue)
                {
                    valueToRemove = chlb.Name + "." + chlb.SelectedItem.ToString();
                }
                else if (selectBuilder.ToString().Contains(", " + chlb.Name + "." + chlb.SelectedItem.ToString()))
                {
                    valueToRemove = ", " + chlb.Name + "." + chlb.SelectedItem.ToString();
                }
                else
                {
                    valueToRemove =  chlb.Name + "." + chlb.SelectedItem.ToString() + ", ";
                }
                    
                String selectString = selectBuilder.ToString();
                if(selectString.Contains(valueToRemove.Trim()))
                {
                    selectString = selectString.Replace(valueToRemove, "");
                    selectBuilder = new StringBuilder();
                    selectBuilder.Append(selectString);
                }
            }
            textBox1.Text = "Select " + selectBuilder.ToString() + Environment.NewLine + 
                "From" + Environment.NewLine + "Where";
            
        }


        public void ctrl_MouseDown(object sender, MouseEventArgs e)
        {
            this.activeControl = sender as Control;

            if (!this.dragInProgress)
            {
                this.dragInProgress = true;
                this.MouseDownX = e.X;
                this.MouseDownY = e.Y;
            }

            return;

        }

        public void CheckListContext_MouseClick(object sender, MouseEventArgs e)
        {
            ContextMenuStrip ctrlStrip = sender as ContextMenuStrip;
            Control ctrl = ctrlStrip.SourceControl;
            CheckedListBox chbx = (CheckedListBox)ctrl;

            foreach (int i in chbx.CheckedIndices)
            {
                chbx.SetItemCheckState(i, CheckState.Unchecked);

                String valueToRemove = null;
                int lengthValue = (chbx.Name + "." + chbx.GetItemText(chbx.Items[i])).Length;
                if(selectBuilder.Length == lengthValue)
                {
                    valueToRemove = chbx.Name + "." + chbx.GetItemText(chbx.Items[i]);
                }
                else if (selectBuilder.ToString().Contains(", " + chbx.Name + "." + chbx.GetItemText(chbx.Items[i])))
                {
                    valueToRemove = ", " + chbx.Name + "." + chbx.GetItemText(chbx.Items[i]);
                }
                else
                {
                    valueToRemove =  chbx.Name + "." + chbx.GetItemText(chbx.Items[i]) + ", ";
                }
                    
                String selectString = selectBuilder.ToString();
                if(selectString.Contains(valueToRemove.Trim()))
                {
                    selectString = selectString.Replace(valueToRemove, "");
                    selectBuilder = new StringBuilder();
                    selectBuilder.Append(selectString);
                }
            }
            textBox1.Text = "Select " + selectBuilder.ToString() + Environment.NewLine +
                            "From" + Environment.NewLine + "Where";
            panel4.Controls.Remove(ctrl);
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

                        TableReference = "[" + controller.GetActualDBName(treeview.SelectedNode.Parent.ToString().Replace("TreeNode: ", "")) + "]." + treeview.SelectedNode.ToString().Replace("TreeNode: ", "");
                    }

                    else if (IsInList)
                    {
                        OptionTreeContext.Show(this, new Point(e.X, e.Y));

                        TableReference = "[" + controller.GetActualDBName(treeview.SelectedNode.Parent.Parent.ToString().Replace("TreeNode: ", "")) + "]." + treeview.SelectedNode.Parent.ToString().Replace("TreeNode: ", "");
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

        }

    }
}
