namespace CreateReactAppVS.Dialogs
{
    partial class AddBoilerplateFolderForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSelectSourceFolder = new System.Windows.Forms.Button();
            this.listViewBoilerPlateFiles = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textBoxRelativeSourcePath = new System.Windows.Forms.TextBox();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.textBoxRelativeSourcePath);
            this.panel1.Controls.Add(this.listViewBoilerPlateFiles);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.buttonSelectSourceFolder);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(-1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(639, 358);
            this.panel1.TabIndex = 8;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label12.Location = new System.Drawing.Point(0, 328);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(639, 30);
            this.label12.TabIndex = 27;
            this.label12.Text = "To make sure the file is copied to the correct directory manually alter the desti" +
    "nation file: eg: \\src\\pages\\some-page.ts ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Select Source Folder";
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(469, 368);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 7;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.ButtonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(550, 368);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // buttonSelectSourceFolder
            // 
            this.buttonSelectSourceFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelectSourceFolder.Image = global::CreateReactAppVS.Properties.Resources.icons8_folder_16;
            this.buttonSelectSourceFolder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSelectSourceFolder.Location = new System.Drawing.Point(522, 19);
            this.buttonSelectSourceFolder.Name = "buttonSelectSourceFolder";
            this.buttonSelectSourceFolder.Size = new System.Drawing.Size(107, 25);
            this.buttonSelectSourceFolder.TabIndex = 14;
            this.buttonSelectSourceFolder.Text = "Select Folder";
            this.buttonSelectSourceFolder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSelectSourceFolder.UseVisualStyleBackColor = true;
            this.buttonSelectSourceFolder.Click += new System.EventHandler(this.ButtonSelectSourceFolder_Click);
            // 
            // listViewBoilerPlateFiles
            // 
            this.listViewBoilerPlateFiles.CheckBoxes = true;
            this.listViewBoilerPlateFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader4,
            this.columnHeader5});
            this.listViewBoilerPlateFiles.FullRowSelect = true;
            this.listViewBoilerPlateFiles.GridLines = true;
            this.listViewBoilerPlateFiles.Location = new System.Drawing.Point(10, 58);
            this.listViewBoilerPlateFiles.Name = "listViewBoilerPlateFiles";
            this.listViewBoilerPlateFiles.Size = new System.Drawing.Size(619, 255);
            this.listViewBoilerPlateFiles.TabIndex = 29;
            this.listViewBoilerPlateFiles.UseCompatibleStateImageBehavior = false;
            this.listViewBoilerPlateFiles.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Source File";
            this.columnHeader4.Width = 242;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Destination File";
            this.columnHeader5.Width = 241;
            // 
            // textBoxRelativeSourcePath
            // 
            this.textBoxRelativeSourcePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRelativeSourcePath.Location = new System.Drawing.Point(121, 22);
            this.textBoxRelativeSourcePath.Name = "textBoxRelativeSourcePath";
            this.textBoxRelativeSourcePath.ReadOnly = true;
            this.textBoxRelativeSourcePath.Size = new System.Drawing.Size(391, 20);
            this.textBoxRelativeSourcePath.TabIndex = 30;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "FileName";
            this.columnHeader1.Width = 87;
            // 
            // AddBoilerplateFolderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 401);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Name = "AddBoilerplateFolderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AddBoilerplateFolderForm";
            this.Load += new System.EventHandler(this.AddBoilerplateFolderForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button buttonSelectSourceFolder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ListView listViewBoilerPlateFiles;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.TextBox textBoxRelativeSourcePath;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}