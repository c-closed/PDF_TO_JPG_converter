namespace PDF_TO_JPG_translater
{
    partial class ProgressForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgressForm));
            fileProgressBar = new ProgressBar();
            cancelButton = new Button();
            fileLabel = new Label();
            pageLabel = new Label();
            pageProgressBar = new ProgressBar();
            SuspendLayout();
            // 
            // fileProgressBar
            // 
            fileProgressBar.Location = new Point(20, 45);
            fileProgressBar.Name = "fileProgressBar";
            fileProgressBar.Size = new Size(340, 20);
            fileProgressBar.TabIndex = 1;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(280, 140);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(80, 25);
            cancelButton.TabIndex = 2;
            cancelButton.Text = "취소하기";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // fileLabel
            // 
            fileLabel.AutoSize = true;
            fileLabel.Location = new Point(20, 20);
            fileLabel.Name = "fileLabel";
            fileLabel.Size = new Size(42, 15);
            fileLabel.TabIndex = 4;
            fileLabel.Text = "파일 : ";
            // 
            // pageLabel
            // 
            pageLabel.AutoSize = true;
            pageLabel.Location = new Point(20, 75);
            pageLabel.Name = "pageLabel";
            pageLabel.Size = new Size(54, 15);
            pageLabel.TabIndex = 5;
            pageLabel.Text = "페이지 : ";
            // 
            // pageProgressBar
            // 
            pageProgressBar.Location = new Point(20, 100);
            pageProgressBar.Name = "pageProgressBar";
            pageProgressBar.Size = new Size(340, 20);
            pageProgressBar.TabIndex = 3;
            // 
            // ProgressForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 181);
            Controls.Add(pageLabel);
            Controls.Add(fileLabel);
            Controls.Add(pageProgressBar);
            Controls.Add(cancelButton);
            Controls.Add(fileProgressBar);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ProgressForm";
            Text = "진행상태";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ProgressBar fileProgressBar;
        private Button cancelButton;
        private Label fileLabel;
        private Label pageLabel;
        private ProgressBar pageProgressBar;
    }
}