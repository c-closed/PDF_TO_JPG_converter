namespace PDF_TO_JPG_converter
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            listBoxFiles = new ListBox();
            btnSelectFiles = new Button();
            btnConvert = new Button();
            btnClearList = new Button();
            SuspendLayout();
            // 
            // listBoxFiles
            // 
            listBoxFiles.FormattingEnabled = true;
            resources.ApplyResources(listBoxFiles, "listBoxFiles");
            listBoxFiles.Name = "listBoxFiles";
            // 
            // btnSelectFiles
            // 
            resources.ApplyResources(btnSelectFiles, "btnSelectFiles");
            btnSelectFiles.Name = "btnSelectFiles";
            btnSelectFiles.UseVisualStyleBackColor = true;
            // 
            // btnConvert
            // 
            resources.ApplyResources(btnConvert, "btnConvert");
            btnConvert.Name = "btnConvert";
            btnConvert.UseVisualStyleBackColor = true;
            // 
            // btnClearList
            // 
            resources.ApplyResources(btnClearList, "btnClearList");
            btnClearList.Name = "btnClearList";
            btnClearList.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnClearList);
            Controls.Add(btnConvert);
            Controls.Add(btnSelectFiles);
            Controls.Add(listBoxFiles);
            Name = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private ListBox listBoxFiles;
        private Button btnSelectFiles;
        private Button btnConvert;
        private Button btnClearList;
    }
}
