namespace ProjectServerHelper
{
    partial class AddNewStage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtStageName = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbPhases = new System.Windows.Forms.ComboBox();
            this.cbWFStatusPDP = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cblPDPs = new System.Windows.Forms.CheckedListBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Stage Name";
            // 
            // txtStageName
            // 
            this.txtStageName.Location = new System.Drawing.Point(136, 23);
            this.txtStageName.Name = "txtStageName";
            this.txtStageName.Size = new System.Drawing.Size(421, 20);
            this.txtStageName.TabIndex = 1;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(136, 56);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(421, 20);
            this.txtDescription.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Description";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Phase";
            // 
            // cbPhases
            // 
            this.cbPhases.DisplayMember = "PhaseName";
            this.cbPhases.FormattingEnabled = true;
            this.cbPhases.Location = new System.Drawing.Point(136, 89);
            this.cbPhases.Name = "cbPhases";
            this.cbPhases.Size = new System.Drawing.Size(421, 21);
            this.cbPhases.TabIndex = 5;
            // 
            // cbWFStatusPDP
            // 
            this.cbWFStatusPDP.DisplayMember = "PDPName";
            this.cbWFStatusPDP.FormattingEnabled = true;
            this.cbWFStatusPDP.Location = new System.Drawing.Point(136, 123);
            this.cbWFStatusPDP.Name = "cbWFStatusPDP";
            this.cbWFStatusPDP.Size = new System.Drawing.Size(421, 21);
            this.cbWFStatusPDP.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Workflow Status PDP";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Available PDPs";
            // 
            // cblPDPs
            // 
            this.cblPDPs.FormattingEnabled = true;
            this.cblPDPs.Location = new System.Drawing.Point(136, 160);
            this.cblPDPs.Name = "cblPDPs";
            this.cblPDPs.Size = new System.Drawing.Size(421, 64);
            this.cblPDPs.TabIndex = 9;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(136, 240);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // AddNewStage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(592, 353);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cblPDPs);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbWFStatusPDP);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbPhases);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtStageName);
            this.Controls.Add(this.label1);
            this.Name = "AddNewStage";
            this.Text = "AddNewStage";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtStageName;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbPhases;
        private System.Windows.Forms.ComboBox cbWFStatusPDP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckedListBox cblPDPs;
        private System.Windows.Forms.Button btnSave;
    }
}