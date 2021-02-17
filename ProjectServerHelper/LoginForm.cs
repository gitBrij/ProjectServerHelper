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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            PSHelperClass objInstance = PSHelperClass.CreateInstance(txtUserName.Text, txtPassword.Text,txtPWAURL.Text);
            PSParentForm objParent = new PSParentForm();
            
            objParent.Show();
            this.Visible = false;
            objParent.FormClosed += objParent_FormClosed;
        }

        void objParent_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Visible = true;
            this.Close();
        }
    }
}
