using CreateReactAppVS.Controller;
using CreateReactAppVS.Model;
using CreateReactAppVS.SeriLogging;
using CreateReactAppVS.Utilities;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using CreateReactAppVS.Dialogs;

namespace SetupReactApp
{
    public partial class MainForm : Form
    {
        public static MainForm m_instance = null;

        public ReactTemplate m_reactTemplate = null;

        public static MainForm GetInstance()
        {
            return m_instance;
        }

        public MainForm()
        {
            InitializeComponent();
            m_instance = this;
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
        private void Form1_Load(object sender, EventArgs e)
        {
            var appPath = GetAppLocation();

            SetupSeriLog();
            SetupDirectoryStructure(appPath);
            SetupDefaultNPMPackageList();
            SetupDefaultProjectFolderList();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            CheckNodeIsInstalled();
        }

        public void CheckNodeIsInstalled()
        {
            var result = CommandLine.ExecuteCommandSync("node -v");

            if (result.StartsWith("v") == false)
                MessageBox.Show("Node is not installed on this computer!! You wont be able to create a React proejct.");
        }

        public static void SetupDirectoryStructure(string baseDir)
        {
            try
            {
                FileIO.CreateDirectory(baseDir);

                string logFileDir = baseDir;
               
                logFileDir += @"\Log";
                FileIO.CreateDirectory(logFileDir);

                logFileDir = baseDir;
                logFileDir += @"\ReactProjectTemplates";
                FileIO.CreateDirectory(logFileDir);

                logFileDir = baseDir;
                logFileDir += @"\BoilerPlateFiles";
                FileIO.CreateDirectory(logFileDir);

                Log.Information($"Base Directory Setup: {baseDir}");
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod().Name);
            }
        }


        private string GetLocalLogDir()
        {
            // Create Local Log Directory
            var appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            return $@"{appPath}\Log";
        }

        private void SetupDefaultNPMPackageList()
        {
            listViewNPMPackages.CheckBoxes = true;

            var lstViewItem = new ListViewItem("npm install --save typescript @types/node @types/react-dom");
            lstViewItem.SubItems.Add("You need TypeScript");
            lstViewItem.Checked = true;
            listViewNPMPackages.Items.Add(lstViewItem);

            lstViewItem = new ListViewItem("npm install --save react react-dom react-scripts");
            lstViewItem.SubItems.Add("react dom!!");
            lstViewItem.Checked = true;
            listViewNPMPackages.Items.Add(lstViewItem);

            lstViewItem = new ListViewItem("npm install --save react-router react-router-dom @types/react-router @types/react-router-dom");
            lstViewItem.SubItems.Add("react dom!!");
            lstViewItem.Checked = true;
            listViewNPMPackages.Items.Add(lstViewItem);

            lstViewItem = new ListViewItem("npm install --save rimraf -g");
            lstViewItem.SubItems.Add("Needed to delete node_modules folder");
            lstViewItem.Checked = true;
            listViewNPMPackages.Items.Add(lstViewItem);

            lstViewItem = new ListViewItem("npm install --save node-sass");
            lstViewItem.SubItems.Add("Needed for css modules");
            lstViewItem.Checked = true;
            listViewNPMPackages.Items.Add(lstViewItem);

            lstViewItem = new ListViewItem("npm install --save uuid @types/uuid");
            lstViewItem.SubItems.Add("Needed for guid identifiers");
            lstViewItem.Checked = true;
            listViewNPMPackages.Items.Add(lstViewItem);


        }

        private void SetupDefaultProjectFolderList()
        {
            listViewProjectFolders.CheckBoxes = true;

            var lstViewItem = new ListViewItem("src\\Model");
            lstViewItem.SubItems.Add("The model data classes");
            lstViewItem.Checked = true;
            listViewProjectFolders.Items.Add(lstViewItem);

            lstViewItem = new ListViewItem("src\\Services");
            lstViewItem.SubItems.Add("The api/service code lives here");
            lstViewItem.Checked = true;
            listViewProjectFolders.Items.Add(lstViewItem);

            lstViewItem = new ListViewItem("src\\Components");
            lstViewItem.SubItems.Add("Where the components live!");
            lstViewItem.Checked = true;
            listViewProjectFolders.Items.Add(lstViewItem);

            lstViewItem = new ListViewItem("src\\Pages");
            lstViewItem.SubItems.Add("The top level pages");
            lstViewItem.Checked = true;
            listViewProjectFolders.Items.Add(lstViewItem);

            lstViewItem = new ListViewItem("src\\DataStore");
            lstViewItem.SubItems.Add("The app data store");
            lstViewItem.Checked = true;
            listViewProjectFolders.Items.Add(lstViewItem);
        }

        private void SetupSeriLog()
        {
            // Create Local Log Directory
            FileIO.CreateDirectory(GetLocalLogDir());
          

            var testSink = new InMemorySink();
            var logWindow = new LogWindowSink(richTextBoxLogWindow);

            string fileName = $@"{GetLocalLogDir()}\app-releaser" + "-{Date}.txt";


            Log.Logger = new LoggerConfiguration()
                .WriteTo.Sink(testSink)
                .WriteTo.Sink(logWindow)
                .WriteTo.RollingFile(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Message}{NewLine}{Exception}", pathFormat: fileName)
                .CreateLogger();

            Log.Information("Application Starting Up.");
            Log.Information("SeriLog Setup.");
        }

        protected void ThreadSafe(MethodInvoker method)
        {
            if (InvokeRequired)
                Invoke(method);
            else
                method();
        }

        public void OutputToRichTextBox(string output)
        {
            ThreadSafe(delegate
            {
                if (output.Contains("[Error]"))
                    RichTextBoxAppendText(output, Color.Red);
                else
                    RichTextBoxAppendText(output, Color.Black);

                richTextBoxLogWindow.ScrollToCaret();
            });
        }

        public void RichTextBoxAppendText(string text, Color color, bool addNewLine = true)
        {
            richTextBoxLogWindow.SuspendLayout();
            richTextBoxLogWindow.SelectionColor = color;
            richTextBoxLogWindow.AppendText(addNewLine
                ? $"{text}{Environment.NewLine}"
                : text);
            richTextBoxLogWindow.ScrollToCaret();
            richTextBoxLogWindow.ResumeLayout();
        }

       

        private void ButtonCreateApp_Click(object sender, EventArgs e)
        {
           
        }

        private void ButtonSelectFolder_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = GetAppLocation();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok && !string.IsNullOrWhiteSpace(dialog.FileName))
            {
                //MessageBox.Show("You selected: " + dialog.FileName);
                textBoxLocation.Text = dialog.FileName;
            }
        }



        private void ToolStripButtonCreateReactApp_Click(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show("Are you sure you want to create this project?", "Sure", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No)
                return;

            var project = GenerateJsonExportFile();

            if (project.ProjectFolder == "")
            {
                MessageBox.Show("Please slect a destination folder");
                return;
            }

            var projectFolder = project.GetProjectFolder();

            if (Directory.Exists(projectFolder) && FileIO.IsDirectoryEmpty(projectFolder) == false)
            {
                MessageBox.Show($"Sorry. Folder: {projectFolder} already exists AND IS NOT EMPTY. Please select another one.");
                return;
            }
            else
            {
                System.IO.Directory.CreateDirectory(projectFolder);
            }

            // Now create the
            CreateReactAppController.CreateReactApp(project.Name.ToLower(), project.ProjectFolder);

            CreateReactAppController.NpmAuditFix(projectFolder);

            foreach (var item in project.NpmPackageList)
            {
                if (item.Included == false)
                    continue;

                CreateReactAppController.NpmInstallPackage("npm.cmd", item.Name.Replace("npm", ""), projectFolder);
            }

            foreach (var item in project.ProjectFolderList)
            {
                if (item.Included == false)
                    continue;

                var folder = Path.Combine(projectFolder, item.Name);
                System.IO.Directory.CreateDirectory(folder);
            }

            // Copy file from boiler plate directory to project directory
            foreach (var item in project.BoilerPlateFileList)
            {
                if (item.Included == false)
                    continue;

                var source = Path.Combine(GetBoilerPlateLocation(), StrUtils.RemoveLeadingSlash(item.SourceDir));
                var dest = Path.Combine(projectFolder, StrUtils.RemoveLeadingSlash(item.DestDir));

                if (System.IO.Directory.Exists(dest) == false)
                    System.IO.Directory.CreateDirectory(new FileInfo(dest).DirectoryName);

                File.Copy(source, dest, true);
            }

            var solutionfile = GenVisualStudioSolution.Generate(project.Name.ToLower(), projectFolder);

            // Open File Explorer to the location of project
            Process.Start(projectFolder);

            // Start Visual Studio 
            Process.Start(solutionfile);

            MessageBox.Show("Project Setup!");
        }

       

        private void SetTitleBar(string templateName)
        {
            this.Text = $"Create New React Project - {templateName}";
        }

        private void ToolStripButtonLoadTemplate_Click(object sender, EventArgs e)
        {
            var appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var initialDir = Path.Combine(appPath, "ReactProjectTemplates");

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = initialDir;
                openFileDialog.Filter = "json files (*.json)|*.json";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (openFileDialog.FileName.ToLower().EndsWith(".json") == false)
                    {
                        MessageBox.Show($"Error. Please select a json file (.json)");
                        return;
                    }

                    m_reactTemplate = ImportJsonFile(openFileDialog.FileName);
                    PopulateForm(m_reactTemplate);

                    var fName = new FileInfo(openFileDialog.FileName).Name;

                    SetTitleBar(fName);
                }
            }
        }

        private void ToolStripButtonSaveTemplate_Click(object sender, EventArgs e)
        {
            m_reactTemplate = GenerateJsonExportFile();

            if (IsReactTemplateValid(m_reactTemplate) == false)
                return;

            var appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var initialDir = Path.Combine(appPath, "ReactProjectTemplates");

            var jsonData = JsonConvert.SerializeObject(m_reactTemplate, Formatting.Indented);

            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = initialDir;
            saveFileDialog.Title = "Save React Template";
            saveFileDialog.DefaultExt = "json";
            saveFileDialog.Filter = "json files (*.json)|*.json|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo f = new FileInfo(saveFileDialog.FileName);
                //write string to file
                System.IO.File.WriteAllText(f.FullName, jsonData);

                Log.Information($"File Successfuly Saved: {f.FullName}");


                this.DialogResult = DialogResult.OK;
                return;
            }
        }

        private void ToolStripButtonAddNPMPackage_Click(object sender, EventArgs e)
        {
            var form = new AddNpmPackageForm();
            if ( form.ShowDialog() == DialogResult.OK)
            {
                AddToNpmPackageList(form.textBoxNpmCommand.Text, form.textBoxComment.Text);
            }

        }

        private void AddToNpmPackageList(string npmCmd, string comment)
        {
            var lstViewItem = new ListViewItem(npmCmd);
            lstViewItem.SubItems.Add(comment);
            lstViewItem.Checked = true;
            listViewNPMPackages.Items.Add(lstViewItem);
        }

        private void AddToProjectFoldersList(string npmCmd, string comment)
        {
            var lstViewItem = new ListViewItem(npmCmd);
            lstViewItem.SubItems.Add(comment);
            lstViewItem.Checked = true;
            listViewProjectFolders.Items.Add(lstViewItem);
        }

        private void ToolStripAddProjectFolder_Click(object sender, EventArgs e)
        {
            var form = new AddProjFolderForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                AddToProjectFoldersList(form.textBoxFolderName.Text, form.textBoxComment.Text);
            }
        }

        private void ToolStripButtonAddCopyBoilerplateFile_Click(object sender, EventArgs e)
        {
            var form = new AddBoilerPlateFileForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                AddToBoilerplateList(form.m_sourceFileName, form.m_sourceFullDir, form.m_destinationDir);
            }
        }

        private void AddToBoilerplateList(string fileName, string sourceFolder, string relativeDestination)
        {
            var lstViewItem = new ListViewItem(fileName);
            lstViewItem.SubItems.Add(sourceFolder);
            lstViewItem.SubItems.Add(relativeDestination);
            lstViewItem.Checked = true;
            listViewBoilerPlateFiles.Items.Add(lstViewItem);
        }

        private void ToolStripButtonRemoveNpmPackage_Click(object sender, EventArgs e)
        {
            var listView = listViewNPMPackages;
            if (listView.SelectedItems.Count > 0)
            {
                var item = listView.SelectedItems[0];

                var dialogResult = MessageBox.Show("Are you sure you want to remove this item?", "Sure", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    listView.Items.Remove(item);
                }
            }
        }

        private void ToolStripButtonRemoveFolder_Click(object sender, EventArgs e)
        {
            var listView = listViewProjectFolders;
            if (listView.SelectedItems.Count > 0)
            {
                var item = listView.SelectedItems[0];

                var dialogResult = MessageBox.Show("Are you sure you want to remove this item?", "Sure", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    listView.Items.Remove(item);
                }
            }
        }

        private void ToolStripButtonRemoveFile_Click(object sender, EventArgs e)
        {
            var listView = listViewBoilerPlateFiles;
            if (listView.SelectedItems.Count > 0)
            {
                var item = listView.SelectedItems[0];

                var dialogResult = MessageBox.Show("Are you sure you want to remove this item?", "Sure", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    listView.Items.Remove(item);
                }
            }
        }

        private ReactTemplate GenerateJsonExportFile()
        {
            var result = new ReactTemplate();

            result.Name = textBoxProjectName.Text;
            result.ProjectFolder = textBoxLocation.Text;
            result.ReactAppCmd = textBoxNpmCmd.Text;
            result.ReactAppFlags = textBoxReactAppFlags.Text;
            result.RunNPMAuditFix = checkBoxNPMAuditFix.Checked;
            result.CreateVisualStudioSolution = checkBoxGenerateVisStudioSolution.Checked;

            result.NpmPackageList.Clear();
            foreach ( ListViewItem item in listViewNPMPackages.Items )
            {
                result.NpmPackageList.Add(new NpmPackage() { Included = item.Checked, Name = item.Text, Comment = item.SubItems[1].Text });
            }

            result.ProjectFolderList.Clear();
            foreach (ListViewItem item in listViewProjectFolders.Items)
            {
                result.ProjectFolderList.Add(new ProjFolder() { Included = item.Checked, Name = item.Text, Comment = item.SubItems[1].Text });
            }

            result.BoilerPlateFileList.Clear();
            foreach (ListViewItem item in listViewBoilerPlateFiles.Items)
            {
                result.BoilerPlateFileList.Add(new BoilerPlateFile() { Included = item.Checked, FileName = item.Text, SourceDir = item.SubItems[1].Text, DestDir = item.SubItems[2].Text });
            }

            return result;
        }

        private void PopulateForm(ReactTemplate result)
        {
            textBoxProjectName.Text = result.Name;
            textBoxNpmCmd.Text = result.ReactAppCmd;
            textBoxReactAppFlags.Text = result.ReactAppFlags;
            checkBoxNPMAuditFix.Checked = result.RunNPMAuditFix;
            checkBoxGenerateVisStudioSolution.Checked = result.CreateVisualStudioSolution;

            listViewNPMPackages.Items.Clear();
            foreach (var item in result.NpmPackageList)
            {
                AddToNpmPackageList(item.Name, item.Comment);
            }

            listViewProjectFolders.Items.Clear();
            foreach (var item in result.ProjectFolderList)
            {
                AddToProjectFoldersList(item.Name, item.Comment);
            }

            listViewBoilerPlateFiles.Items.Clear();
            foreach (var item in result.BoilerPlateFileList)
            {
                AddToBoilerplateList(item.FileName, item.SourceDir, item.DestDir);
            }
        }

        public static ReactTemplate ImportJsonFile(string fileLocation)
        {
            string json = File.ReadAllText(fileLocation);

            ReactTemplate result = JsonConvert.DeserializeObject<ReactTemplate>(json);

            return result;
        }

        public static bool IsReactTemplateValid(ReactTemplate rt, bool displayError = true)
        {
            bool result = true;

            if ( rt.Name.Contains(" ") == true )
            {
                result = false;
                if (displayError)
                    MessageBox.Show("Project name cannot contain spaces");
            }

            return result;
        }

      
    }
}
