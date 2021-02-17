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
    public partial class CreatePDP : Form
    {
        public CreatePDP()
        {
            InitializeComponent();

            PSHelperClass objInstance = PSHelperClass.GetInstance();
            List<PSHPDP> lstPDP = objInstance.GetAllPDP();

            dgvPDPs.DataSource = lstPDP;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string strPDPName = txtPDPName.Text;
            string strPDPDesc = txtDescription.Text;
            string strPDPDisplayName = txtDisplayName.Text;
            string strPageType = cbPageType.SelectedValue.ToString();

            PSHelperClass objInstance = PSHelperClass.GetInstance();
            string message = objInstance.CreatePDP(strPDPName, strPDPDisplayName, strPDPDesc, strPageType);
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
