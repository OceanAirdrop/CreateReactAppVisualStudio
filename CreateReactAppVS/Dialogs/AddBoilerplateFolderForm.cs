using CreateReactAppVS.Model;
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
    public partial class AddBoilerplateFolderForm : Form
    {
        public string m_sourceFileName;
        public string m_sourceFullDir;
        public string m_sourceRelativeDir;
        public string m_destinationDir;

        public FileInfo m_sourceInfo;

        public List<BoilerPlateFile> m_list;

        public AddBoilerplateFolderForm()
        {
            InitializeComponent();
            m_list = new List<BoilerPlateFile>();
        }

        private void AddBoilerplateFolderForm_Load(object sender, EventArgs e)
        {

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

        private string ConvertSourceFileToRelativeDir(string file)
        {
            return file.Replace(GetBoilerPlateLocation(), "");
        }

        private void ButtonSelectSourceFolder_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = GetBoilerPlateLocation();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok && !string.IsNullOrWhiteSpace(dialog.FileName))
            {
                //MessageBox.Show("You selected: " + dialog.FileName);
                textBoxRelativeSourcePath.Text = dialog.FileName;

                ProcessFiles(dialog.FileName);
            }
        }

        private void ProcessFiles(string sourcePath)
        {
            string[] fileList = Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories);

            // Display the names of the directories.
            foreach (var item in fileList)
            {
                var file = new FileInfo(item);

                var bpFile = new BoilerPlateFile();

                bpFile.Included = true;
                bpFile.SourceDir = ConvertSourceFileToRelativeDir(item);
                bpFile.DestDir = StripTopDirectory(bpFile.SourceDir);
                bpFile.FileName = file.Name;

                m_list.Add(bpFile);

                AddToListView(bpFile);
            }
        }

        private void AddToListView(BoilerPlateFile item)
        {
            var lstViewItem = new ListViewItem(item.FileName);
            lstViewItem.SubItems.Add(item.SourceDir);
            lstViewItem.SubItems.Add(item.DestDir);
            lstViewItem.Checked = true;
            listViewBoilerPlateFiles.Items.Add(lstViewItem);
        }

        private string StripTopDirectory(string path)
        {
            var result = "";
            var splitList = path.Split('\\');

            bool topDirSkipped = false;
            foreach ( var item in splitList )
            {
                if (string.IsNullOrEmpty(item) == true)
                    continue;

                if (string.IsNullOrEmpty(item) == false && topDirSkipped == false)
                {
                    topDirSkipped = true;
                    continue;
                }

                result += "\\";
                result += item;
            }

            return result;
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
