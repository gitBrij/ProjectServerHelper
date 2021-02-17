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
    public partial class AddPhase : Form
    {
        public AddPhase()
        {
            InitializeComponent();

            PSHelperClass objInstance = PSHelperClass.GetInstance();
            List<PSHPhase> lstPhase = objInstance.GetAllPhases();

            dgvPhases.DataSource = lstPhase;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string PhaseName = txtPhaseName.Text;
            string PhaseDesc = txtDescription.Text;

            PSHelperClass objInstance = PSHelperClass.GetInstance();
            string message = objInstance.AddPhase(PhaseName, PhaseDesc);
            MessageBox.Show(message);
        }

        private void dgvPhases_SelectionChanged(object sender, EventArgs e)
        {
            //foreach (DataGridViewRow dgvrPhase in dgvPhases.SelectedRows)
            //{
               
            //}
        }
    }
}
