using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MannFramework.Macondo.Desktop
{
    //public partial class HashingForm : MannFramework.Macondo.Desktop.BaseForm
    public partial class HashingForm : Form
    {
        public HashingForm()
        {
            InitializeComponent();
        }

        private void HashingForm_Load(object sender, EventArgs e)
        {
            cmbHashType.DataSource = Enum.GetValues(typeof(HashAlgorithm));
        }

        private void btnCreateHash_Click(object sender, EventArgs e)
        {
            txtHashedData.Text = Helpers.CreateOneWayHash(txtData.Text, (HashAlgorithm)cmbHashType.SelectedValue);
        }
    }
}
