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
        private ToolStripMenuItem adminMenuStripItem;

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
            TreeView t1 = controller.GetOptionsTree();
            panel3.Controls.Add(t1);
            t1.Dock = DockStyle.Fill;
        }

    }
}
