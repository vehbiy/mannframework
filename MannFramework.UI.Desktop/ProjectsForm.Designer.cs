namespace MannFramework.Macondo.Desktop
{
    partial class ProjectsForm
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
            this.cmbProjects = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgItems = new System.Windows.Forms.DataGridView();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsEnum = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tvProperties = new System.Windows.Forms.TreeView();
            this.btnGenerateAndSave = new System.Windows.Forms.Button();
            this.btnRefreshProjects= new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.cmbMappings = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkGeneratorTypes = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnGenerateAndSaveSelected = new System.Windows.Forms.Button();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbProjects
            // 
            this.cmbProjects.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProjects.FormattingEnabled = true;
            this.cmbProjects.Location = new System.Drawing.Point(112, 31);
            this.cmbProjects.Name = "cmbProjects";
            this.cmbProjects.Size = new System.Drawing.Size(315, 21);
            this.cmbProjects.TabIndex = 0;
            this.cmbProjects.SelectedIndexChanged += new System.EventHandler(this.cmbProjects_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Project";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl1.Location = new System.Drawing.Point(0, 208);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(880, 430);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgItems);
            this.tabPage1.Controls.Add(this.treeView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(872, 404);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Items";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgItems
            // 
            this.dgItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemName,
            this.IsEnum});
            this.dgItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgItems.Location = new System.Drawing.Point(3, 3);
            this.dgItems.Name = "dgItems";
            this.dgItems.ReadOnly = true;
            this.dgItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgItems.Size = new System.Drawing.Size(866, 398);
            this.dgItems.TabIndex = 1;
            // 
            // ItemName
            // 
            this.ItemName.DataPropertyName = "Name";
            this.ItemName.HeaderText = "Name";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            this.ItemName.Width = 60;
            // 
            // IsEnum
            // 
            this.IsEnum.DataPropertyName = "IsEnum";
            this.IsEnum.HeaderText = "IsEnum";
            this.IsEnum.Name = "IsEnum";
            this.IsEnum.ReadOnly = true;
            this.IsEnum.Width = 48;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(3, 3);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(866, 398);
            this.treeView1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tvProperties);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(872, 404);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Properties";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tvProperties
            // 
            this.tvProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvProperties.Location = new System.Drawing.Point(3, 3);
            this.tvProperties.Name = "tvProperties";
            this.tvProperties.Size = new System.Drawing.Size(866, 398);
            this.tvProperties.TabIndex = 0;
            // 
            // btnGenerateAndSave
            // 
            this.btnGenerateAndSave.Location = new System.Drawing.Point(342, 130);
            this.btnGenerateAndSave.Name = "btnGenerateAndSave";
            this.btnGenerateAndSave.Size = new System.Drawing.Size(150, 23);
            this.btnGenerateAndSave.TabIndex = 3;
            this.btnGenerateAndSave.Text = "Generate and Save All";
            this.btnGenerateAndSave.UseVisualStyleBackColor = true;
            this.btnGenerateAndSave.Visible = false;
            this.btnGenerateAndSave.Click += new System.EventHandler(this.btnGenerateAndSave_Click);

            // 
            // btnRefreshProjects
            // 
            this.btnRefreshProjects.Location = new System.Drawing.Point(300, 130);
            this.btnRefreshProjects.Name = "btnRefreshProjects";
            this.btnRefreshProjects.Size = new System.Drawing.Size(120, 23);
            this.btnRefreshProjects.TabIndex = 3;
            this.btnRefreshProjects.Text = "Refresh Projects";
            this.btnRefreshProjects.UseVisualStyleBackColor = true;
            this.btnRefreshProjects.Visible = true;
            this.btnRefreshProjects.Click += new System.EventHandler(this.btnRefreshProjects_Click);

            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(26, 172);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 13);
            this.lblMessage.TabIndex = 4;
            // 
            // cmbMappings
            // 
            this.cmbMappings.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMappings.FormattingEnabled = true;
            this.cmbMappings.Location = new System.Drawing.Point(112, 75);
            this.cmbMappings.Name = "cmbMappings";
            this.cmbMappings.Size = new System.Drawing.Size(380, 21);
            this.cmbMappings.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Mapping";
            // 
            // chkGeneratorTypes
            // 
            this.chkGeneratorTypes.FormattingEnabled = true;
            this.chkGeneratorTypes.Location = new System.Drawing.Point(512, 31);
            this.chkGeneratorTypes.Name = "chkGeneratorTypes";
            this.chkGeneratorTypes.Size = new System.Drawing.Size(258, 154);
            this.chkGeneratorTypes.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(433, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Generators";
            // 
            // btnGenerateAndSaveSelected
            // 
            this.btnGenerateAndSaveSelected.Location = new System.Drawing.Point(29, 130);
            this.btnGenerateAndSaveSelected.Name = "btnGenerateAndSaveSelected";
            this.btnGenerateAndSaveSelected.Size = new System.Drawing.Size(161, 23);
            this.btnGenerateAndSaveSelected.TabIndex = 9;
            this.btnGenerateAndSaveSelected.Text = "Generate and Save Selected";
            this.btnGenerateAndSaveSelected.UseVisualStyleBackColor = true;
            this.btnGenerateAndSaveSelected.Click += new System.EventHandler(this.btnGenerateAndSaveSelected_Click);
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Location = new System.Drawing.Point(209, 130);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(75, 23);
            this.btnOpenFolder.TabIndex = 10;
            this.btnOpenFolder.Text = "Open Folder";
            this.btnOpenFolder.UseVisualStyleBackColor = true;
            this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
            // 
            // ProjectsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 638);
            this.Controls.Add(this.btnOpenFolder);
            this.Controls.Add(this.btnGenerateAndSaveSelected);
            this.Controls.Add(this.btnRefreshProjects);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkGeneratorTypes);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbMappings);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnGenerateAndSave);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbProjects);
            this.Name = "ProjectsForm";
            this.Text = "ProjectsLocalized";
            this.Load += new System.EventHandler(this.ProjectsForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbProjects;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.DataGridView dgItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsEnum;
        private System.Windows.Forms.TreeView tvProperties;
        private System.Windows.Forms.Button btnGenerateAndSave;
        private System.Windows.Forms.Button btnRefreshProjects;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.ComboBox cmbMappings;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox chkGeneratorTypes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnGenerateAndSaveSelected;
        private System.Windows.Forms.Button btnOpenFolder;
    }
}