using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreateReactAppVS.Dialogs
{
    public partial class AddBoilerPlateFileForm : Form
    {
        public string m_sourceFileName;
        public string m_sourceFullDir;
        public string m_sourceRelativeDir;
        public string m_destinationDir;

        public FileInfo m_sourceInfo;

        public AddBoilerPlateFileForm()
        {
            InitializeComponent();
            m_sourceFileName = m_sourceFullDir = m_sourceRelativeDir = m_destinationDir = "";
        }

        private string GetAppLocation()
        {
            var appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            return appPath;
        }

        private string GetBoilerPlateLocation()
        {
            var appPath = GetAppLocation();

            return Path.Combine(GetAppLocation(), "BoilerPlateFiles");
        }

        private void AddBoilerPlateFileForm_Load(object sender, EventArgs e)
        {
            textBoxRelativeSourcePath.Text = m_sourceFullDir;
            textBoxDestination.Text = m_destinationDir;

            buttonSelectSourceFile.Select();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            m_sourceFileName = m_sourceInfo.Name;
            m_sourceFullDir = m_sourceInfo.FullName.Replace(GetBoilerPlateLocation(), "");
            //m_sourceRelativeDir = textBoxDestination.Text;
            m_destinationDir = textBoxDestination.Text;
            this.DialogResult = DialogResult.OK;
        }

        private void ButtonSelectSourceFile_Click(object sender, EventArgs e)
        {

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = GetBoilerPlateLocation();
                openFileDialog.Filter = "All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if(openFileDialog.FileName.ToLower().StartsWith( GetBoilerPlateLocation().ToLower() ) == false )
                    {
                        MessageBox.Show("Sorry. File needs to be selected from BoilerPlateFiles directory");
                        return;
                    }

                    textBoxRelativeSourcePath.Text = ConvertSourceFileToRelativeDir(openFileDialog.FileName);
                   
                    m_sourceInfo = new FileInfo(openFileDialog.FileName);

                    textBoxDestination.Text = $"\\{m_sourceInfo.Name}";
                }
            }
        }

        private string ConvertSourceFileToRelativeDir(string file)
        {
            return file.Replace(GetBoilerPlateLocation(), "");
        }

        private void ButtonSelectDestinationFolder_Click(object sender, EventArgs e)
        {

        }
    }
}
