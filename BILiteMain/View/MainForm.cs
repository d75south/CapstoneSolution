using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BILiteMain
{
    public partial class MainForm : Form
    {
        private Point previousLocation;
        private Control activeControl;
        private MainController controller;
        private int tree_x, tree_y;
        
        public MainForm()
        {
            InitializeComponent();
            
            this.Size = new Size(1000, 1000);
            this.AutoScroll = true;
            button1.Click += button1_Click;
            this.Load += MainForm_Load;
                       
            foreach (Control ctrl in this.Controls)
            {
                ctrl.MouseClick += ctrl_MouseClick;
                ctrl.MouseDown += ctrl_MouseDown;
                ctrl.MouseMove += ctrl_MouseMove;
                ctrl.MouseUp += ctrl_MouseUp;
            }
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            controller = new MainController();            
            tree_x = 64;
            tree_y = 72;
            controller.systemDBConnection();

            if (controller.fireSysDbExceptionMessage())
            {
             MessageBox.Show("There was an error connecting to the system database");
             this.Close();
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

        private void button1_Click(object sender, EventArgs e)
        {        
            Control treeview = controller.CreateTreeView();           
            treeview.Location = new Point(tree_x, tree_y);
            tree_y = tree_y + 125;

            panel2.Controls.Add(treeview);

            treeview.MouseClick += ctrl_MouseClick;
            treeview.MouseDown += ctrl_MouseDown;
            treeview.MouseMove += ctrl_MouseMove;
            treeview.MouseUp += ctrl_MouseUp;
  
        }

        private void createNewToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var connForm = new BILiteConnForm();
            connForm.Show();
        }
    }
}
