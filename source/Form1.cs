#nullable enable

using Syncfusion.Pdf.Parsing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDF_TO_JPG_converter
{
    public partial class Form1 : Form
    {
        private readonly List<string> pdfFilePaths = new();

        public Form1()
        {
            InitializeComponent();

            AllowDrop = true;
            DragEnter += Form1_DragEnter;
            DragDrop += Form1_DragDrop;

            btnSelectFiles.Click += BtnSelectFiles_Click;
            btnConvert.Click += BtnConvert_Click;
            btnClearList.Click += BtnClearList_Click;
        }

        private void Form1_DragEnter(object? sender, DragEventArgs e)
        {
            if (e.Data?.GetDataPresent(DataFormats.FileDrop) == true)
                e.Effect = DragDropEffects.Copy;
        }

        private void Form1_DragDrop(object? sender, DragEventArgs e)
        {
            if (e.Data?.GetDataPresent(DataFormats.FileDrop) == true)
            {
                if (e.Data.GetData(DataFormats.FileDrop) is string[] files)
                    AddPdfFiles(files);
            }
        }

        private void BtnSelectFiles_Click(object? sender, EventArgs e)
        {
            using OpenFileDialog dialog = new()
            {
                Multiselect = true,
                Filter = "PDF Files (*.pdf)|*.pdf|All Files (*.*)|*.*"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
                AddPdfFiles(dialog.FileNames);
        }

        private void AddPdfFiles(string[] files)
        {
            foreach (string file in files)
            {
                if (Path.GetExtension(file).Equals(".pdf", StringComparison.OrdinalIgnoreCase) &&
                    !pdfFilePaths.Contains(file))
                {
                    pdfFilePaths.Add(file);
                    listBoxFiles.Items.Add(Path.GetFileName(file));
                }
            }
        }

        private void BtnClearList_Click(object? sender, EventArgs e)
        {
            pdfFilePaths.Clear();
            listBoxFiles.Items.Clear();
        }

        private async void BtnConvert_Click(object? sender, EventArgs e)
        {
            if (pdfFilePaths.Count == 0)
            {
                MessageBox.Show("��ȯ�� PDF ������ ���� ������ּ���.", "�˸�",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var progressForm = new ProgressForm
            {
                WorkFunction = worker => WorkerFunctionParallel(worker)
            };

            progressForm.Show();

            string? result = await Task.Run(() => progressForm.WorkFunction?.Invoke(progressForm.Worker));

            progressForm.ResultMessage = result;
            progressForm.Close();

            MessageBox.Show(progressForm.ResultMessage ?? "��ȯ �۾��� �Ϸ�Ǿ����ϴ�.", "���",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string WorkerFunctionParallel(BackgroundWorker worker)
        {
            int totalFiles = pdfFilePaths.Count;
            int completedFiles = 0;
            object progressLock = new();

            // ������ ���� ó��: ���� ���� ���� ó��, �ִ� ������ ���� ����
            var options = new ParallelOptions { MaxDegreeOfParallelism = Math.Max(1, Environment.ProcessorCount - 1), CancellationToken = worker.CancellationPending ? new CancellationToken(true) : new CancellationToken() };

            try
            {
                Parallel.ForEach(pdfFilePaths, options, (pdfPath, state) =>
                {
                    if (worker.CancellationPending)
                    {
                        state.Stop();
                        return;
                    }

                    using var pdf = new PdfLoadedDocument(pdfPath);
                    int totalPages = pdf.Pages.Count;

                    string outputFolder = Path.Combine(Path.GetDirectoryName(pdfPath) ?? string.Empty,
                        Path.GetFileNameWithoutExtension(pdfPath) + "_JPG");
                    Directory.CreateDirectory(outputFolder);

                    for (int i = 0; i < totalPages; i++)
                    {
                        if (worker.CancellationPending)
                        {
                            state.Stop();
                            return;
                        }

                        // Syncfusion�� PdfLoadedDocument �� ExportAsImage�� ������ ���������� ���� �� �����Ƿ�,
                        // ���� Ȯ�� �ʿ�. ���� ���� ������ ���Ϻ��� ���� ó�� ���

                        using Bitmap bmp = pdf.ExportAsImage(i);
                        using Bitmap bmpCopy = new(bmp);
                        string jpgFile = Path.Combine(outputFolder, $"Page_{i + 1}.jpg");
                        bmpCopy.Save(jpgFile, ImageFormat.Jpeg);

                        int pageProgress = (int)((float)(i + 1) / totalPages * 100);

                        lock (progressLock)
                        {
                            int fileProgress = (int)((float)completedFiles / totalFiles * 100);

                            try
                            {
                                if (!worker.CancellationPending)
                                {
                                    worker.ReportProgress(0, new object[] {
                                        $"{Path.GetFileName(pdfPath)} ({i + 1}/{totalPages}) ������ ��ȯ ��...",
                                        fileProgress, pageProgress,
                                        completedFiles + 1, totalFiles,
                                        i + 1, totalPages
                                    });
                                }
                            }
                            catch (InvalidOperationException)
                            {
                                // �۾� �Ϸ� �� ȣ���� �� �߻��ϴ� ���� ����
                            }
                        }
                    }

                    Interlocked.Increment(ref completedFiles);
                });
            }
            catch (OperationCanceledException)
            {
                // �۾� ��� �� ���� ����
            }

            try
            {
                if (!worker.CancellationPending)
                {
                    worker.ReportProgress(100, new object[] {
                        $"{completedFiles}�� ���� ��ȯ �Ϸ�!", 100, 100,
                        completedFiles, totalFiles, 0, 0
                    });
                }
            }
            catch (InvalidOperationException)
            {
                // �۾� �Ϸ� �� ȣ���� �� �߻��ϴ� ���� ����
            }

            return $"{completedFiles}�� ���� ��ȯ �Ϸ�!";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // �ʿ� �ʱ�ȭ ����
        }
    }
}
