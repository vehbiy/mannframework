namespace MannFramework.Macondo.Desktop
{
    partial class HashingForm
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
            this.txtData = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbHashType = new System.Windows.Forms.ComboBox();
            this.btnCreateHash = new System.Windows.Forms.Button();
            this.txtHashedData = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Data";
            // 
            // txtData
            // 
            this.txtData.Location = new System.Drawing.Point(90, 30);
            this.txtData.Multiline = true;
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(252, 69);
            this.txtData.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Hash Type";
            // 
            // cmbHashType
            // 
            this.cmbHashType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHashType.FormattingEnabled = true;
            this.cmbHashType.Location = new System.Drawing.Point(90, 123);
            this.cmbHashType.Name = "cmbHashType";
            this.cmbHashType.Size = new System.Drawing.Size(161, 21);
            this.cmbHashType.TabIndex = 3;
            // 
            // btnCreateHash
            // 
            this.btnCreateHash.Location = new System.Drawing.Point(267, 121);
            this.btnCreateHash.Name = "btnCreateHash";
            this.btnCreateHash.Size = new System.Drawing.Size(75, 23);
            this.btnCreateHash.TabIndex = 4;
            this.btnCreateHash.Text = "Create Hash";
            this.btnCreateHash.UseVisualStyleBackColor = true;
            this.btnCreateHash.Click += new System.EventHandler(this.btnCreateHash_Click);
            // 
            // txtHashedData
            // 
            this.txtHashedData.Location = new System.Drawing.Point(90, 173);
            this.txtHashedData.Multiline = true;
            this.txtHashedData.Name = "txtHashedData";
            this.txtHashedData.Size = new System.Drawing.Size(252, 69);
            this.txtHashedData.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 176);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Hashed Data";
            // 
            // HashingForm
            // 
            this.ClientSize = new System.Drawing.Size(477, 304);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtHashedData);
            this.Controls.Add(this.btnCreateHash);
            this.Controls.Add(this.cmbHashType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtData);
            this.Name = "HashingForm";
            this.Load += new System.EventHandler(this.HashingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtData;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbHashType;
        private System.Windows.Forms.Button btnCreateHash;
        private System.Windows.Forms.TextBox txtHashedData;
        private System.Windows.Forms.Label label3;
    }
}
