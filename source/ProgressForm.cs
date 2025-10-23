#nullable enable

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace PDF_TO_JPG_translater
{
    public partial class ProgressForm : Form
    {
        public Func<BackgroundWorker, string>? WorkFunction { get; set; }
        public string? ResultMessage { get; set; }

        private BackgroundWorker worker = new BackgroundWorker();

        public BackgroundWorker Worker => worker;  // 외부 접근용 프로퍼티

        public ProgressForm()
        {
            InitializeComponent();  // 디자이너가 만든 UI 초기화

            InitializeBackgroundWorker();
        }

        private void InitializeBackgroundWorker()
        {
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_Completed;

            Shown += (s, e) =>
            {
                if (WorkFunction != null)
                    worker.RunWorkerAsync();
            };
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            if (worker.IsBusy)
                worker.CancelAsync();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (WorkFunction != null)
                ResultMessage = WorkFunction.Invoke(worker);
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState is object[] arr && arr.Length == 7 &&
                arr[0] is string message &&
                arr[1] is int fileProg &&
                arr[2] is int pageProg &&
                arr[3] is int curFile &&
                arr[4] is int totalFiles &&
                arr[5] is int curPage &&
                arr[6] is int totalPages)
            {
                this.Text = message;

                // 디자이너에서 생성한 컨트롤 이름 사용
                fileProgressBar.Value = Math.Min(100, fileProg);
                pageProgressBar.Value = Math.Min(100, pageProg);
                fileLabel.Text = $"파일 : {curFile} / {totalFiles}";
                pageLabel.Text = $"페이지 : {curPage} / {totalPages}";
            }
            else
            {
                this.Text = e.UserState?.ToString() ?? "변환 중...";
            }
        }

        private void Worker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
                this.Text = "사용자에 의해 취소되었습니다.";
            else
                this.Text = "변환 완료!";

            cancelButton.Enabled = false;

            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer { Interval = 800 };
            t.Tick += (s, args) =>
            {
                t.Stop();
                Close();
            };
            t.Start();
        }
    }
}

#nullable restore
