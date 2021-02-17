using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectServerHelper
{
    public partial class PSParentForm : Form
    {
        private int childFormNumber = 0;

        public PSParentForm()
        {
            InitializeComponent();
        }

        private void OpenAddStage(object sender, EventArgs e)
        {
            Form childForm = new AddNewStage();
            childForm.MdiParent = this;
            childForm.Text = "Add Stage -" + childFormNumber++;
            childForm.WindowState = FormWindowState.Maximized;
            childForm.Show();
        }

        private void OpenAddPhase(object sender, EventArgs e)
        {
            Form childForm = new AddPhase();
            childForm.MdiParent = this;
            childForm.Text = "Add Phase -" + childFormNumber++;
            childForm.WindowState = FormWindowState.Maximized;
            childForm.Show();
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OpenAddPDP(object sender, EventArgs e)
        {
            Form childForm = new AddPDP();
            childForm.MdiParent = this;
            childForm.Text = "Add PDP -" + childFormNumber++;
            childForm.WindowState = FormWindowState.Maximized;
            childForm.Show();
        }

    }
}
