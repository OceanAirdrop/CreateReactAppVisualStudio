using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreateReactAppVS.Dialogs
{
    public partial class AddNpmPackageForm : Form
    {
        public AddNpmPackageForm()
        {
            InitializeComponent();
        }

        private void AddNpmPackageForm_Load(object sender, EventArgs e)
        {
            textBoxNpmCommand.Select();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
