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
    public partial class AddPDP : Form
    {
        public AddPDP()
        {
            InitializeComponent();
            PSHelperClass objInstance = PSHelperClass.GetInstance();
            List<PSHPDP> lstPDP = objInstance.GetAllPDP();
            List<PSHStage> lstStage = objInstance.GetAllStages();

            cbPDPs.DataSource = lstPDP;
            cblStages.DataSource = lstStage;
            cblStages.DisplayMember = "StageName";
            cblStages.ValueMember = "StageGuid";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            PSHPDP objPDP = (PSHPDP)cbPDPs.SelectedValue;
            List<PDPStageMapping> lstStageMaping = new List<PDPStageMapping>();
            foreach (PSHStage objStage in cblStages.SelectedItems)
            {
                PDPStageMapping objMap = new PDPStageMapping()
                {
                    PDPId = objPDP.PDPGuid,
                    StageId = objStage.StageGuid
                };
                lstStageMaping.Add(objMap);
            }

            PSHelperClass objInstance = PSHelperClass.GetInstance();
            objInstance.AddPDP2Stage(lstStageMaping);
        }
    }
}
