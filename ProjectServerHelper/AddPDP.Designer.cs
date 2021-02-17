namespace ProjectServerHelper
{
    partial class AddPDP
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
            this.cblStages = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbPDPs = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "PDP Name";
            // 
            // cblStages
            // 
            this.cblStages.FormattingEnabled = true;
            this.cblStages.Location = new System.Drawing.Point(117, 52);
            this.cblStages.Name = "cblStages";
            this.cblStages.ScrollAlwaysVisible = true;
            this.cblStages.Size = new System.Drawing.Size(444, 79);
            this.cblStages.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Stages";
            // 
            // cbPDPs
            // 
            this.cbPDPs.DisplayMember = "PDPName";
            this.cbPDPs.FormattingEnabled = true;
            this.cbPDPs.Location = new System.Drawing.Point(117, 13);
            this.cbPDPs.Name = "cbPDPs";
            this.cbPDPs.Size = new System.Drawing.Size(444, 21);
            this.cbPDPs.TabIndex = 4;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(117, 146);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // AddPDP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(592, 353);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cbPDPs);
            this.Controls.Add(this.cblStages);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AddPDP";
            this.Text = "AddPDP";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox cblStages;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbPDPs;
        private System.Windows.Forms.Button btnSave;
    }
}