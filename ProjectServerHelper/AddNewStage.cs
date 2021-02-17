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
    public partial class AddNewStage : Form
    {
        public AddNewStage()
        {
            InitializeComponent();
            PSHelperClass objInstance = PSHelperClass.GetInstance();
            List<PSHPDP> lstPDP = objInstance.GetAllPDP();
            List<PSHPhase> lstPhase = objInstance.GetAllPhases();

            cbPhases.DataSource = lstPhase;
            cbWFStatusPDP.DataSource = lstPDP;

            PSHPDP[] lstWFPDP = new PSHPDP[lstPDP.Count];//
            lstPDP.CopyTo(lstWFPDP);

            cblPDPs.DataSource = lstWFPDP;
            cblPDPs.DisplayMember = "PDPName";
            cblPDPs.ValueMember = "PDPGuid";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string StageName = txtStageName.Text;
            string StageDesc = txtDescription.Text;
            PSHPhase Phase = (PSHPhase)cbPhases.SelectedValue;
            PSHPDP WFStsPDP = (PSHPDP)cbWFStatusPDP.SelectedValue;
            List<PSHPDP> lstPDP = cblPDPs.CheckedItems.Cast<PSHPDP>().ToList();


            PSHelperClass objInstance = PSHelperClass.GetInstance();
            objInstance.AddStage(StageName, StageDesc, Phase.PhaseGuid, WFStsPDP.PDPGuid, lstPDP);
        }
    }
}
