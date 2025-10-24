namespace Updater
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelTitle = new MetroFramework.Controls.MetroLabel();
            this.progressBar = new MetroFramework.Controls.MetroProgressBar();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelTitle.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelTitle.Location = new System.Drawing.Point(0, 0);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(10);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(100, 23);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "업데이트 진행 중...";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(35, 80);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(720, 30);
            this.progressBar.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 535);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroLabel labelTitle;
        private MetroFramework.Controls.MetroProgressBar progressBar;
    }
}

